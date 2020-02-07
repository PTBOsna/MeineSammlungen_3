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
    /// Interaktionslogik für PageExponate.xaml
    /// </summary>
    public partial class PageExponate : Page
    {
        private Int32 gdID;
        public PageExponate(Int32 _gID)
        {
            InitializeComponent();
            this.gdID = _gID;
            this.gdID = _gID;
            DataClassesSammlungenDataContext conn = new DataClassesSammlungenDataContext();
            Exponate currM = (from m in conn.Exponate where m.Grunddaten_ID == gdID select m).FirstOrDefault();

            this.DataContext = currM;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //DataClassesSammlungenDataContext con = new DataClassesSammlungenDataContext();
            //var currEx = from ex in con.Exponate where ex.Grunddaten_ID == gdID select ex;
            //foreach (var ex in currEx)
            //{
            //    LandText.Text = ex.Fundstelle_Land;
            //    OrtTExt.Text = ex.Fundstelle_Ort;
            //    KoordinatenText.Text = ex.Koordinaten;
            //    HinweiseExpoText.Text = ex.Hinweise;
            //    FunddatumText.Text = ex.Fund_Datum;
            //}
        }
    }
}
