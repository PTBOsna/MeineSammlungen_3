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
    /// Interaktionslogik für AddEditExponate.xaml
    /// </summary>
    public partial class AddEditExponate : Window
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
        public AddEditExponate(string _openArgs)
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
                var myDat = from m in con.Exponate
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
                    OrtTExt.Text = item.m.Fundstelle_Ort;
                    //IDLabel.Content = item.m.ID;
                    LandText.Text = item.m.Fundstelle_Land;
                    KoordinatenText.Text = item.m.Koordinaten;
                    HinweiseExpoText.Text = item.m.Hinweise;
                    FunddatumText.Text = item.m.Fund_Datum;
                    //EinschlussText.Text = item.m.Einschluss;
                    //HinweiseText.Text = item.m.Hineise;
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
        private void cbAblage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void imgListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Return_click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Img_new(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_DelImg(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_ChangeIPTC_click(object sender, RoutedEventArgs e)
        {

        }


    }
}
