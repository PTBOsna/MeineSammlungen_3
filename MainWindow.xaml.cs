﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MeineSammlungen_3
{

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Variablen für diese Klasse
        Int32 ImgCount = 0; //Bildzähler
        Int32 gID = 0; //Grunddaten-ID
        Int32 ModulID = 0; //Modul-ID
        string objektNr; //Angezeigte Nr des Objekts
        private string ImgPath;
        public DataClassesSammlungenDataContext con = new DataClassesSammlungenDataContext();
        public MainWindow()
        {
            InitializeComponent();
            Admin.GetSettings();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Module> modules = (from m in con.Module select m).ToList();
            ModulGrid.ItemsSource = modules;
        }

        private void ModulGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Module selected = ModulGrid.SelectedItem as Module;
            if (selected == null)
            {
                return;
            }
            ClearDisplay();
            ModulID = selected.ID;
            var myDetail = from d in con.Grunddaten where d.Modul == ModulID select d;
            GdGrid.ItemsSource = myDetail.ToList();
        }

        private void BtnAllesClick(object sender, RoutedEventArgs e)
        {
            var myDetail = from d in con.Grunddaten select d;
            GdGrid.ItemsSource = myDetail.ToList();
        }

        private void GdGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ImgDisplay.Source = null;
            ClearDisplay();
            Grunddaten sel = GdGrid.SelectedItem as Grunddaten;
            if (sel == null)
            {
                return;
            }
            //Ablageort ermitteln
            var abl = (from a in con.Ablage where a.ID == sel.Ablageort_neu select a).FirstOrDefault();
            //foreach (var item in abl)
            //{
            AblageortText.Text = abl.Ablageort;
            //}
            objektNr = sel.Nr;
            lblObjektNr.Content = "Objekt Nr.: " + objektNr;
            ObjektText.Text = sel.Objekt;
            DetailText.Text = sel.Detail;
            //AblageortText.Text = abl;
            BemerkungText.Text = sel.Bemerkung;
            ErstelltText.Text = sel.Erstellt.ToString();
            GeaendertText.Text = sel.Geaendert.ToString();
            ImgCount = sel.ImgCount;
            AnzahlBilderText.Text = ImgCount.ToString();
            gID = sel.ID;
            if (sel.Checked == true)
            { lblBearbeitung.Content = "Weitere Bearbeitung erforderlich"; }
            else lblBearbeitung.Content = null;

            switch (sel.Modul)
            {
                case 1:
                    //MessageBox.Show("Case1");
                    PageModul.Content = new PageMikroMakro(gID);
                    break;
                case 2:
                    PageModul.Content = new PageExponate(gID);
                    break;
                case 3:
                    PageModul.Content = new PageMineral(gID);
                    break;
            }
            //Img-Liste füllen
            PictureList selPicture = new PictureList(gID.ToString());

            imgListBox.ItemsSource = selPicture;

        }

        private void PageModul_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void BtnAddDetail_Click(object sender, RoutedEventArgs e)
        {
            string openArgs = null;
            switch (ModulID)
            {
                case 1:
                    openArgs = ModulID + "#1"; //1 -> neuer Datensatz
                    AddEditMikro mmNeu = new AddEditMikro(openArgs);
                    mmNeu.ShowDialog();
                    break;
                case 2:
                    openArgs = ModulID + "#1"; //1 -> neuer Datensatz
                    AddEditExponate exNeu = new AddEditExponate(openArgs);
                    exNeu.ShowDialog();
                    break;
                case 3:
                    openArgs = ModulID + "#1"; //1 -> neuer Datensatz
                    AddEditMineral minNeu = new AddEditMineral(openArgs);
                    minNeu.ShowDialog();
                    break;
            }
            ReloadGD();
        }

        private void Button_edit_Click(object sender, RoutedEventArgs e)
        {
            switch (ModulID)
            {
                case 1:
                    string openArgs = gID + "#2"; //2 -> Datensatz editieren
                    AddEditMikro mmNeu = new AddEditMikro(openArgs);
                    mmNeu.ShowDialog();
                    ReloadGD();
                    break;
                case 2:
                    string openArgs2 = gID + "#2"; //2 -> Datensatz editieren
                    AddEditExponate exNeu = new AddEditExponate(openArgs2);
                    exNeu.ShowDialog();
                    break;
                case 3:
                    string openArgs3 = gID + "#2"; //2 -> Datensatz editieren
                    AddEditMineral minNeu = new AddEditMineral(openArgs3);
                    minNeu.ShowDialog();
                    break;
            }
            ReloadGD();
        }

        private void ReloadGD()
        {

            using (DataClassesSammlungenDataContext conn = new DataClassesSammlungenDataContext())
            {
                List<Grunddaten> fillList = (from g in conn.Grunddaten
                                             where g.Modul == ModulID
                                             select g).ToList();
                //foreach (var el in myDetail)
                //{
                //    MessageBox.Show(el.Objekt + "-" + el.Checked.ToString());
                //}
                GdGrid.ItemsSource = fillList;
                PictureList selPicture = new PictureList(gID.ToString());

                imgListBox.ItemsSource = selPicture;

            }
        }

        private void Del_Butten_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("Gelöscht wird Objekt Nr.: " + objektNr);
            Grunddaten selected = GdGrid.SelectedItem as Grunddaten;
            if (selected == null)
            {
                MessageBox.Show("Bitte Objekt auswählen!");
            }
            else
            {
                if (MessageBoxResult.Yes == MessageBox.Show("Sind Sie sicher?", "Objekt " + objektNr + " Löschen", MessageBoxButton.YesNo, MessageBoxImage.Warning))
                {
                    Admin.DeleteGrunddat(selected);
                    //DialogResult = false;
                }
            }
            if (ImgCount > 0)
            {
                var result = MessageBox.Show("Es sind " + ImgCount.ToString() + " Bilder vorhanden." + Environment.NewLine + "Löschen?", "Fehler", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    PictureList.delImg.ImgDel(gID.ToString());
                }
                else
                    MessageBox.Show("Bilder sind weiterhin im Ordner " + ImgPath + "vorhanden." + Environment.NewLine + "Ggf. manuell löschen!", "Zugehörige Bilder");
            }

            //dann details in Modulen löschen
            if (ModulID == 1)
            {
                if (Admin.DeleteMikro(gID) == true)
                {
                    MessageBox.Show("Daten wurden gelöscht.");
                }
                else
                    MessageBox.Show("Es ist ein Fehler beim Löschen der Detaildaaten aufgetreten!");
            }
            if (ModulID == 2)
            {
                if (Admin.DelExponate(gID) == true)
                {
                    MessageBox.Show("Daten wurden gelöscht.");
                }
                else
                    MessageBox.Show("Es ist ein Fehler beim Löschen der Detaildaaten aufgetreten!");
            }
            if (ModulID == 3)
            {
                if (Admin.DelMineralien(gID) == true)
                {
                    MessageBox.Show("Daten wurden gelöscht.");
                }
                else
                    MessageBox.Show("Es ist ein Fehler beim Löschen der Detaildaaten aufgetreten!");
            }
            DataClassesSammlungenDataContext con = new DataClassesSammlungenDataContext();
            var gdListe = from g in con.Grunddaten where g.Modul == ModulID select g;
            GdGrid.ItemsSource = gdListe.ToList();
            object item = GdGrid.Items[0];
            GdGrid.SelectedItem = item;
            GdGrid.ScrollIntoView(item);

            //ShowDaten();
        }

        private void ImgListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dynamic row = imgListBox.SelectedItem;
            if (row == null)
            {
                //ImgDisplay.Source = null;
                //readExif.ClearExit();
                return;
            }
            //MessageBox.Show(row.Path);
            ImgPath = row.Path;

            BitmapImage myBitMap = new BitmapImage();
            myBitMap.BeginInit();
            myBitMap.CacheOption = BitmapCacheOption.OnLoad;
            myBitMap.UriSource = new Uri(ImgPath);
            myBitMap.EndInit();
            ImgDisplay.Source = myBitMap;
            //Modul_Image.readIPTC.ReadIPTC(ImgPath);
            //Modul_Image.readExif.ShowEXIF(ImgPath);
            //ShowMetaDaten();
            // -> ToDo: ImgHandling
            IPTCDaten iptc = new IPTCDaten(ImgPath);
            ExifDaten exif = new ExifDaten(ImgPath);
            //ImgHandling.IPTCDaten.myIPTC_Daten(currImg);

            txtObject.Text = iptc.iObjekt;
            txtDetail.Text = iptc.iDeteil;
            //txtBemerkung.Text = iptc.iBemerkung;
            txtQuelle.Text = iptc.iQuelle;
            txtOrt.Text = iptc.iFundstelleOrt;
            //txtCountry.Text = iptc.iFundstelleCountry;
            //txtLand.Text = iptc.iFundstelleLand;
            //txtPosition.Text = iptc.iPostition;
            //txtErstellt.Text = iptc.iErstellt;
            //txtDErstellt.Text = iptc.iDigitalErstellt;
            //txtAutor.Text = iptc.iAutor;
            //txtCRight.Text = iptc.iCopyright;
            //txtHinweise.Text = iptc.iHinweise;
            txtStichworte.Text = iptc.iStichwortText;

            //Exif-Daten
            //ExifDaten.clearExif();
            //ImgHandling.EXIF.ReadEXIF(ImgPath);
            txtKamera.Text = exif.Kamera;
            txtBlende.Text = exif.Blende;
            txtBelichtung.Text = exif.Belichtung;
            txtIso.Text = exif.ISO;
            txtBrennweiste.Text = exif.Brennweite;
            txtAufnahmeDat.Text = exif.AufnahmeDat;
        }

        private void Click_ExitMnu(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Click_SettingMnu(object sender, RoutedEventArgs e)
        {
            MySettings ms = new MySettings();
            ms.ShowDialog();
        }

        private void MenuItem_Ablage_Click(object sender, RoutedEventArgs e)
        {
            ListeAblage la = new ListeAblage();
            la.ShowDialog();
        }

        private void MenuItem_ImgToGD_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Click_EditImgData(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ImgPath) == true)
            {
                MessageBox.Show("Bitte zunächst ein Bild auswählen!");
                return;
            }
            //imgPath + "*" + myVarID.ToString().Trim() + "*" + Nr.Trim()
            //string objNr = ModulID.ToString() + "-" + gID.ToString();
            ShowMeta iptcchange = new ShowMeta(ImgPath + "*" + gID.ToString() + "*" + objektNr.Trim());
            //ShowMeta iptcchange = new ShowMeta(ImgPath + "*" + objNr);
            iptcchange.ShowDialog();
        }

        private void Click_ShowSelectImg(object sender, RoutedEventArgs e)
        {
            ShowImage si = new ShowImage(ImgPath);
            si.ShowDialog();
        }

        private void ClearDisplay()
        {
            lblObjektNr.Content = null;
            ObjektText.Text = null;
            DetailText.Text = null;
            AblageortText.Text = null;
            BemerkungText.Text = null;
            ErstelltText.Text = null;
            GeaendertText.Text = null;
            AnzahlBilderText.Text = null;
            lblBearbeitung.Content = null;
            PageModul.Content = new PageNull();
            imgListBox.ItemsSource = null;
            ImgDisplay.Source = null;
            ImgDisplay.Source = null;
            //IPTC und EXIF leeren
            //ImgHandling.EXIF.clearExif();
            txtObject.Text = null;
            txtDetail.Text = null;
            txtQuelle.Text = null;
            txtOrt.Text = null;
            txtStichworte.Text = null;
            txtKamera.Text = null;
            txtBlende.Text = null;
            txtBelichtung.Text = null;
            txtIso.Text = null;
            txtBrennweiste.Text = null;
            txtAufnahmeDat.Text = null;
        }



        private void ButtonJournal_Click(object sender, RoutedEventArgs e)
        {
            MyJournal journal = new MyJournal();
            try
            {

                journal.ShowDialog();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Fehler beim Speichern");
            }
        }



        private void txtSuche_TextChanged(object sender, TextChangedEventArgs e)
        {
            var myDetail = from d in con.Grunddaten where d.Objekt.Contains(txtSuche.Text) select d;
            GdGrid.ItemsSource = myDetail.ToList();
        }

        private void btnClearSearch(object sender, RoutedEventArgs e)
        {
            ClearDisplay();
            txtSuche.Text = null;
        }

        private void MenuItem_Bildarten_Click(object sender, RoutedEventArgs e)
        {
            ListeBildarten lb = new ListeBildarten();
            lb.ShowDialog();
        }

        private void MenuItem_Module_Click(object sender, RoutedEventArgs e)
        {
            ListeModule md = new ListeModule();
            md.ShowDialog();
        }

        private void Click_mnu_test(object sender, RoutedEventArgs e)
        {
            testform tst = new testform();
            tst.ShowDialog();
        }
    }

}
