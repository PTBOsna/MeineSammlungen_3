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
    /// Interaktionslogik für PageMineral.xaml
    /// </summary>
    public partial class PageMineral : Page
    {
        private Int32 gdID;
        public PageMineral(Int32 _gdID)
        {
            InitializeComponent();
            this.gdID = _gdID;
            DataClassesSammlungenDataContext con = new DataClassesSammlungenDataContext();
            Mineralien currEx = (from ex in con.Mineralien where ex.Grunddaten_ID == gdID select ex).FirstOrDefault();
            this.DataContext = currEx;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //DataClassesSammlungenDataContext con = new DataClassesSammlungenDataContext();
            //var currEx = from ex in con.Mineralien where ex.Grunddaten_ID == gdID select ex;
            //foreach (var ex in currEx)
            //{
            //    LandText.Text = ex.Fundstelle_Land;
            //    OrtTExt.Text = ex.Fundstelle_Ort;
            //    KoordinatenText.Text = ex.Koordinaten;
            //    HinweiseExpoText.Text = ex.Hinweise;
            //    FunddatumText.Text = ex.Fund_Datum;
            //    Dichte.Text = ex.Dichte.ToString();
            //    Volumen.Text = ex.Volumen.ToString();
            //    Gewicht.Text = ex.Gewicht.ToString();
            //    Zusammensetzung.Text = ex.Zusammensetzung;
            //}
        }

       
    }
}
