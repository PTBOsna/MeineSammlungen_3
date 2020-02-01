using System;
using System.Collections.Generic;
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
            { myModID = myVarID; }
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

        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)

        {
            //letzte laufende Nr holen für Grunddaten neu
            lfNr = (from x in con.Grunddaten select x.LfdNr).Max();
            Grunddaten gd = new Grunddaten();
            ModulMikro mm = new ModulMikro();

            gd.ID = myVarID;
            gd.Objekt = ObjektText.Text.Trim();
            gd.Detail = DetailText.Text.Trim();
            gd.Modul = myModID;
            gd.Bemerkung = BemerkungText.Text.Trim();
            if (istNeu == 1)
            {
                gd.LfdNr = lfNr + 1;
                gd.Nr = myModID.ToString() + "-" + (lfNr + 1).ToString().Trim();
                gd.Erstellt = DateTime.Now;

            }
            else
            {
                gd.LfdNr = lfNr;
                gd.Nr = Nr.Trim();
                gd.Geaendert = DateTime.Now;
            }

            gd.Ablageort_neu = ablageID;
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
                int NewID = (from x in con.Grunddaten select x.ID).Max();
                mm.Grunddaten_ID = NewID;
                Admin.AddMikro(mm);

            }
            else
            {
                
                Admin.EditGrunddaten(gd);
                Admin.EditMikro(mm);
            }
            if (istNeu==1)
            {
            MessageBox.Show("Datensatz als " + gd.Nr + " übernommen.");
            }
            else
                MessageBox.Show("Datensatz " + gd.Nr + " geändert.");
            DialogResult = true;
        }

        private void Save()
        {

        }

        private void Btn_Img_new(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_DelImg(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Return_click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_ChangeIPTC_click(object sender, RoutedEventArgs e)
        {

        }
    }
}
