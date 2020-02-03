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
        private string ImgPath;
        public    DataClassesSammlungenDataContext con = new DataClassesSammlungenDataContext();
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
            lblObjektNr.Content = "Objekt Nr.: " + sel.Nr;
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
            switch (ModulID)
            {
                case 1:
                    string openArgs = ModulID + "#1"; //1 -> neuer Datensatz
                    AddEditMikro mmNeu = new AddEditMikro(openArgs);
                    mmNeu.ShowDialog();
                    break;
                case 2:
                    PageModul.Content = new PageExponate(gID);
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
                    PageModul.Content = new PageExponate(gID);
                    break;
            }
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
            }
        }

        private void Del_Butten_Click(object sender, RoutedEventArgs e)
        {

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
            txtBlende.Text =exif.Blende;
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

        }

        private void MenuItem_Ablage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_ImgToGD_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}
