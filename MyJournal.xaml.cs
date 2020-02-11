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
    /// Interaktionslogik für MyJournal.xaml
    /// </summary>
    public partial class MyJournal : Window
    {
        public DataClassesSammlungenDataContext con = new DataClassesSammlungenDataContext();
        public MyJournal()
        {
            InitializeComponent();
            this.DataContext = con.Journal;
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            SaveJournal();

        }

        private void SaveJournal()
        {
            try
            {
                //Journal jur = DGJournal.SelectedItem as Journal;
                con.SubmitChanges();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Fehler beim Speichern");
            }

        }

        private void Btn_SaveClose_Click(object sender, RoutedEventArgs e)
        {
            SaveJournal();
            DialogResult = true;
        }

        private void Btn_Close_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
