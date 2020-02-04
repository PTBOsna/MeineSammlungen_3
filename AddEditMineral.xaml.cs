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
    /// Interaktionslogik für AddEditMineral.xaml
    /// </summary>
    public partial class AddEditMineral : Window
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
        public AddEditMineral(string _openArgs)
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

        }

        private void cbAblage_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
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

        private void imgListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
