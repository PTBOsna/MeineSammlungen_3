using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MeineSammlungen_3
{
    /// <summary>
    /// Interaktionslogik für AddEditMikro.xaml
    /// </summary>
    public partial class AddEditMikro : Window
    {
        string openArgs; //VarID # IstNeu<1 oder n>
        Int32 istNeu;
        Int32 myVarID; // enthält entweder die Modul-ID (bei Add Daten) oder die Grunddaten-ID (bei Edit-Daten)
        Int32 myModID; // Modul-ID
        Int32 myMID;
        Int32 myImgCount;
        Int32 ablageID;
        string imgPath;
        Int32 lfNr;
        string Nr;
        public DataClassesSammlungenDataContext con = new DataClassesSammlungenDataContext();
        public AddEditMikro(string _openArgs)
        {
            InitializeComponent();
            this.openArgs = _openArgs;
            String[] strlist = openArgs.Split('#');

            myVarID = Int32.Parse(strlist[0]);
            istNeu = Int32.Parse(strlist[1]);
            if (istNeu == 1)
            {
                myModID = myVarID;
                //lfNr = (from x in con.Grunddaten select x.LfdNr).Max();
                lfNr = (from x in con.Grunddaten select x.LfdNr).Max() + 1;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Ablage einbinden
            var abl = from a in con.Ablage select a;
            //cbAblage.DataContext = abl;
            cbAblage.ItemsSource = abl;

            //Zweig edit Datensatz 

            if (istNeu != 1)
            {
                var myDat = from m in con.ModulMikro
                            from g in con.Grunddaten
                            from a in con.Ablage
                            where m.Grunddaten_ID == g.ID && g.ID == myVarID && g.Ablageort_neu == a.ID
                            select new { m, g, a };
                //und anzeigen
                foreach (var item in myDat)
                {

                    ObjektText.Text = item.g.Objekt;
                    DetailText.Text = item.g.Detail;
                    AblageortText.Text = item.a.Ablageort; //item.g.Ablageort;
                    ablageID = item.a.ID;
                    BemerkungText.Text = item.g.Bemerkung;
                    ErstelltText.Text = item.g.Erstellt.ToString();
                    GeaendertText.Text = item.g.Geaendert.ToString();
                    myImgCount = item.g.ImgCount;
                    LblImgCount.Content = "Zugehörige Bilder: " + myImgCount.ToString();
                    Nr = item.g.Nr;
                    lfNr = item.g.LfdNr; //da nicht neu, wird die vorhandene lfNr genutzt
                    lblObjektNr.Content = "Objekt Nr.: " + item.g.Nr;
                    if (item.g.Checked == true)
                    {
                        ckbWeitereBearbeitung.IsChecked = true;
                    }
                    else ckbWeitereBearbeitung.IsChecked = false;
                    myModID = item.g.Modul;
                    SchnittText.Text = item.m.Schnittebene;
                    //IDLabel.Content = item.m.ID;
                    SchnittartText.Text = item.m.Schnittart;
                    FarbeText.Text = item.m.Farbung;
                    HellText.Text = item.m.Aufhellung;
                    FixierungText.Text = item.m.Fixierung;
                    EinschlussText.Text = item.m.Einschluss;
                    HinweiseText.Text = item.m.Hineise;
                    myMID = item.m.ID;
                    // Titel anzeigen
                    this.Title = "Details zu Objekt '" + item.g.Nr.Trim() + "' ansehen/ändern";
                }
                if (myImgCount > 0)
                {
                    PictureList selPicture = new PictureList(myVarID.ToString());
                    imgListBox.ItemsSource = selPicture;
                }
            }
        }

        private void cbAblage_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var ab = from a in con.Ablage where a.ID == (Int32)cbAblage.SelectedValue select a;
            foreach (var item in ab)
            {
                ablageID = item.ID;
                AblageortText.Text = item.Ablageort;
            }
        }

        private void imgListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic row = imgListBox.SelectedItem;
            if (row == null)
            {
                //ImgDisplay.Source = null;
                //readExif.ClearExit();
                return;
            }
            //MessageBox.Show(row.Path);
            imgPath = row.Path;
            ShowMetaDaten(imgPath);
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)

        {
            if (ablageID == 0)
            {
                MessageBox.Show("Bitte Ablage auswählen");
            }
            else if (string.IsNullOrEmpty(ObjektText.Text) == true)
            {
                MessageBox.Show("Bitte einen Objektnamen einfügen!");
                return;
            }
            else
            {
                if (SaveAll() == true)
                {
                    if (istNeu == 1)
                    {
                        MessageBox.Show("Datensatz als " + Nr + " übernommen.");
                    }
                    else
                        MessageBox.Show("Datensatz " + Nr + " geändert.");
                    DialogResult = true;
                }
                return;
            }

        }

        private bool SaveAll()
        {


            try
            {
                Grunddaten gd = new Grunddaten();
                ModulMikro mm = new ModulMikro();

                gd.ID = myVarID;
                gd.Objekt = ObjektText.Text.Trim();
                gd.Detail = DetailText.Text.Trim();
                gd.Modul = myModID;
                gd.Bemerkung = BemerkungText.Text.Trim();
                if (istNeu == 1)
                {
                    //neue LfdNr
                    gd.LfdNr = lfNr;
                    gd.Nr = myModID.ToString() + "-" + lfNr.ToString().Trim();
                    gd.Erstellt = DateTime.Now;

                }
                else
                {
                    gd.LfdNr = lfNr;
                    gd.Nr = Nr.Trim();
                    gd.Geaendert = DateTime.Now;
                }
                Nr = gd.Nr;
                gd.Ablageort_neu = ablageID;
                gd.ImgCount = myImgCount;
                if (ckbWeitereBearbeitung.IsChecked == true)
                { gd.Checked = true; }
                else
                    gd.Checked = false;
                mm.Farbung = FarbeText.Text;
                mm.ID = myMID;
                mm.Aufhellung = HellText.Text.Trim();
                mm.Einschluss = EinschlussText.Text.Trim();
                mm.Fixierung = FixierungText.Text.Trim();
                mm.Hineise = HinweiseText.Text.Trim();
                mm.Schnittart = SchnittartText.Text.Trim();
                mm.Schnittebene = SchnittText.Text.Trim();
                mm.Grunddaten_ID = myVarID;

                if (istNeu == 1)
                {
                    //gd.LfdNr = lfNr + 1;
                    //gd.Erstellt = DateTime.Now;
                    //gd.Modul = myModID;
                    gd.ImgCount = 0; //Muss noch angepasst werden
                    gd.Ablageort_neu = ablageID; //muss noch angepasst werden
                                                 //gd.Checked = false; //muss noch angepasst werden
                    Admin.AddGrunddaten(gd);
                    // die Neue GD-ID und MM-ID holen und  myVarID/myMID damit belegen 
                    myVarID = (from x in con.Grunddaten select x.ID).Max();
                    myMID = (from x in con.ModulMikro select x.ID).Max();
                    mm.Grunddaten_ID = myVarID;
                    Admin.AddMikro(mm);

                }
                else
                {

                    Admin.EditGrunddaten(gd);
                    Admin.EditMikro(mm);
                }

                return true;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Fehler beim Speichern!");
                return false;
            }


        }



        private void Btn_Img_new(object sender, RoutedEventArgs e)
        {
            if (istNeu == 1)
            { SaveAll(); }
            istNeu = 2; //Datensatz ist jetzt nicht mehr neu
            string curName = null;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Bilder einfügen";
            ofd.Filter = "Image Files(*.JPG;|*.JPG| All files(*.*) | *.*";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == true)
            {
                //FilePath = ofd.FileName;
                Admin.cName = System.IO.Path.GetFileName(ofd.FileName);
                String _NewName = myVarID.ToString() + "#" + myImgCount + System.IO.Path.GetExtension(ofd.FileName);
                string NewName = System.IO.Path.Combine(Admin.ImgPath, _NewName);
                // System.Windows.Forms.MessageBox.Show(NewName);
                try
                {
                    File.Copy(ofd.FileName, NewName);
                    curName = System.IO.Path.GetFileName(NewName);
                    PictureList selPicture = new PictureList(curName);
                    imgListBox.ItemsSource = selPicture;
                    myImgCount += 1;
                    LblImgCount.Content = "Zugehörige Bilder: " + myImgCount.ToString();

                }
                catch (Exception ex)
                {

                    MessageBox.Show("Bild bereits vorhaden!" + Environment.NewLine + ex.Message);
                    return;
                }
                string objNr = myModID.ToString() + "-" + myVarID.ToString();
                string cImg = System.IO.Path.Combine(Admin.ImgPath, curName);

                ShowMeta iptcchange = new ShowMeta(cImg + "*" + myVarID.ToString().Trim() + "*" + Nr.Trim());
                iptcchange.ShowDialog();
                //ShowMetaDaten(curName);
                DataClassesSammlungenDataContext con = new DataClassesSammlungenDataContext();
                var currGd = (from gd in con.Grunddaten where gd.ID == myVarID select gd.ImgCount).First();
                currGd = myImgCount;
                con.SubmitChanges();

                //Speichern, um ImgCount zu aktualisieren
            }
        }

        private void ShowMetaDaten(string curName)
        {
            IPTCDaten iptc = new IPTCDaten(curName);

            ExifDaten exif = new ExifDaten(curName);

            txtObject.Text = iptc.iObjekt;
            txtDetail.Text = iptc.iDeteil;
            txtQuelle.Text = iptc.iQuelle;
            txtOrt.Text = iptc.iFundstelleOrt;
            txtBemerkung.Text = iptc.iBemerkung;
            txtStichworte.Text = iptc.iStichwortText;
            txtKamera.Text = exif.Kamera;
            txtBelichtung.Text = exif.Belichtung;
            txtBlende.Text = exif.Blende;
            txtBrennweite.Text = exif.Brennweite;
            txtIso.Text = exif.ISO;
            txtAufnahmeDat.Text = exif.AufnahmeDat;
        }

        private void Btn_DelImg(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Return_click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Btn_ChangeIPTC_click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(imgPath) == true)
            {
                MessageBox.Show("Bitte zunächst ein Bild auswählen!");
                return;
            }
            //string objNr = myModID.ToString() + "-" + myVarID.ToString();
            ShowMeta iptcchange = new ShowMeta(imgPath + "*" + myVarID.ToString().Trim() + "*" + Nr.Trim());
            iptcchange.ShowDialog();
        }
    }
}
