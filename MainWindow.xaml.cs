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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MeineSammlungen_3
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //DataClassesSammlungenDataContext con = new DataClassesSammlungenDataContext();
            List<Module> modules = (from m in Admin. con.Module select m).ToList();
            ModulGrid. ItemsSource = modules;
            

        }

        private void ModulGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Module selected = ModulGrid.SelectedItem as Module; if (selected == null)
            {
                return;
            }
            var myDetail = from d in Admin.con.Grunddaten where d.Modul == selected.ID select d;
            GdGrid.ItemsSource = myDetail.ToList();


        }

        private void BtnAllesClick(object sender, RoutedEventArgs e)
        {
            var myDetail = from d in Admin.con.Grunddaten  select d;
            GdGrid.ItemsSource = myDetail.ToList();
        }
    }
}
