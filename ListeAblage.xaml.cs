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
    /// Interaktionslogik für ListeAblage.xaml
    /// </summary>
    public partial class ListeAblage : Window
    {
        public DataClassesSammlungenDataContext con = new DataClassesSammlungenDataContext();
        public ListeAblage()
        {
            InitializeComponent();
            this.DataContext = con.Ablage;
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            con.SubmitChanges();
        }

        private void Btn_SaveClose_Click(object sender, RoutedEventArgs e)
        {
            con.SubmitChanges();
            DialogResult = true;
        }

        private void Btn_Close_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
