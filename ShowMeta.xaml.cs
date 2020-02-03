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
    /// Interaktionslogik für ShowMeta.xaml
    /// </summary>
    public partial class ShowMeta : Window
    {
        string openArgs; //enthält currImg und Objekt-Nr mit * getrennt. Aus Objekt-Nr. wird gdID extrahiert 
        Int32 gdID;
        string currImg;
        string cTitel;
        DataClassesSammlungenDataContext con = new DataClassesSammlungenDataContext();
        public ShowMeta(string _openargs)
        {
            InitializeComponent();
            this.openArgs = _openargs;
            String[] strlist = openArgs.Split('*');

            currImg = strlist[0];
            //string[] strlist2 = strlist[1].Split('-');
            cTitel = strlist[2];
            gdID =  Int32.Parse( strlist[1]);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = "Metadaten für Objekt-Nr " + cTitel;
            IPTCDaten iptc = new IPTCDaten(currImg);
            txtObject.Text = iptc.iObjekt;
            txtDetail.Text = iptc.iDeteil;
            txtBemerkung.Text = iptc.iBemerkung;
            txtQuelle.Text = iptc.iQuelle;
            txtSpezial.Text = iptc.iSpezial;
            txtOrt.Text = iptc.iFundstelleOrt;
            txtCountry.Text = iptc.iFundstelleCountry;
            txtLand.Text = iptc.iFundstelleLand;
            txtPosition.Text = iptc.iPostition;
            txtErstellt.Text = iptc.iErstellt;
            txtDErstellt.Text = iptc.iDigitalErstellt;
            txtAutor.Text = iptc.iAutor;
            txtCRight.Text = iptc.iCopyright;
            txtHinweise.Text = iptc.iHinweise;
            txtStichworte.Text = iptc.iStichwortText;
            ShowListBox(iptc);
            BitmapImage myBitMap = new BitmapImage();
            myBitMap.BeginInit();
            myBitMap.CacheOption = BitmapCacheOption.OnLoad;
            myBitMap.UriSource = new Uri(currImg);
            myBitMap.EndInit();
            ImgDisplay.Source = myBitMap;
            ExifDaten exif = new ExifDaten(currImg);
            txtKamera.Text = exif.Kamera;
            txtBlende.Text = exif.Blende;
            txtBelichtung.Text = exif.Belichtung;
            txtISO.Text = exif.ISO;
            txtBrennweite.Text = exif.Brennweite;
            txtAufnahmeDat.Text = exif.AufnahmeDat;
            txtLat.Content = "Latitude (Länge): " + exif.Latitude;
            txtLatDez.Content = "Latitude " + exif.LatitudeDez;
            txtLong.Content = "Longitude (Breite): " + exif.Longitude;
            txtLongDez.Content = "Longitude " + exif.LongitudeDez;
            txtAlt.Content = exif.Altitude;

            //Quellenliste laden und zuweisen

            var quellen = from q in con.Bildtyp orderby q.Bildtyp1 select q.Bildtyp1;

            cbQuelle.ItemsSource = quellen.ToList();
        }

        private void ShowListBox(IPTCDaten iptc)
        {
            if (iptc.iStichworte != null)
            {
                listStichwort.ItemsSource = iptc.iStichworte.ToList();
                //cbStichworte.ItemsSource = IPTCDaten.iStichworte.ToList();
            }
            else { listStichwort.Items.Clear(); }
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            txtErstellt.Text = txtErstelltPicker.SelectedDate.ToString();
        }

        private void cbQuelle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtQuelle.Text = cbQuelle.SelectedItem.ToString();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Del_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_ToIptc_clilck(object sender, RoutedEventArgs e)
        {

        }

        private void BtAddGDClick(object sender, RoutedEventArgs e)
        {
            var mb = MessageBox.Show("Achtung, die vorhandenen IPTC-Daten werden überschrieben!", "Grunddaten übernehmen!", MessageBoxButton.OKCancel);
            if (mb == MessageBoxResult.No)
            { return; }
            if (gdID != 0)
            {
                var gdat = from g in con.Grunddaten where g.ID == gdID select g;

                foreach (var gd in gdat)
                {
                    txtObject.Text = gd.Objekt;
                    txtDetail.Text = gd.Detail;
                    txtBemerkung.Text = gd.Bemerkung;
                    txtHinweise.Text = "In DB als Objekt Nr. '" + gd.Nr.Trim() + "' aufgenommen.";
                }

                txtCRight.Text = "© PBBerlin";
                txtAutor.Text = "PTBBerlin";
                txtSpezial.Text = Admin.cName; //Enthält den Originalnamen des ImgFiles
            }
        }

        private void BtSaveClick(object sender, RoutedEventArgs e)
        {
            IPTCDaten iptc = new IPTCDaten(currImg);
            if (string.IsNullOrEmpty(txtHinweise.Text) == true)
            {
                txtHinweise.Text = txtHinweise.Text = "In DB als Objekt Nr. '" + cTitel + "' aufgenommen.";
            }
            iptc.iObjekt = txtObject.Text;
            iptc.iDeteil = txtDetail.Text;
            iptc.iBemerkung = txtBemerkung.Text;
            iptc.iQuelle = txtQuelle.Text;
            iptc.iSpezial = txtSpezial.Text;
            iptc.iFundstelleOrt = txtOrt.Text;
            iptc.iFundstelleCountry = txtCountry.Text;
            iptc.iFundstelleLand = txtLand.Text;
            iptc.iPostition = txtPosition.Text;
            if (string.IsNullOrEmpty(txtErstellt.Text) == false)
            {
                try
                {
                    DateTime da = DateTime.Parse(txtErstellt.Text);
                    iptc.iErstellt = da.ToString("yyyyMMdd", System.Globalization.CultureInfo.GetCultureInfo("de-DE"));

                }
                catch (Exception)
                {
                    MessageBox.Show("Bitte Datum manuell eintragen!", "Feld 'Erstellt' enthält falsches Datumsformat!");
                }
            }
            if (string.IsNullOrEmpty(txtDErstellt.Text) == false)
            {
                try
                {
                    DateTime oDate = DateTime.Parse(txtDErstellt.Text);
                    iptc.iDigitalErstellt = oDate.ToString("yyyyMMdd", System.Globalization.CultureInfo.GetCultureInfo("de-DE"));

                }
                catch (Exception)
                {

                    MessageBox.Show("Bitte Datum manuell eintragen!", "Feld 'Digital erstellt' enthält falsches Datumsformat!");
                }
            }
            iptc.iDigitalErstellt = txtDErstellt.Text;
            iptc.iAutor = txtAutor.Text;
            iptc.iCopyright = txtCountry.Text;
            iptc.iHinweise = txtHinweise.Text;
            iptc.iStichworte = iptc.iStichworte;
            // ImgHandling.myIPTCDaten.iCopyright = txtCountry.Text;

            iptc.WriteIPTC(currImg);
            MessageBox.Show("Daten übernommen");
            DialogResult = true;

        }

        private void BtExitClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
