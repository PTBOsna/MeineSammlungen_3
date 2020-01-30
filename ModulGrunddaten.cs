﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeineSammlungen_3
{
    class Admin
    {
        public static DataClassesSammlungenDataContext con = new DataClassesSammlungenDataContext();
        #region Grunddaten
        public static void AddGrunddaten(Grunddaten _ModulMikro)
        {
            using (DataClassesSammlungenDataContext conn = new DataClassesSammlungenDataContext())
            {
                conn.Grunddaten.InsertOnSubmit(_ModulMikro);
                conn.SubmitChanges();
            }

        }

        public static void EditGrunddaten(Grunddaten _ModulMikro)
        {
            using (DataClassesSammlungenDataContext conn = new DataClassesSammlungenDataContext())
            {
                Grunddaten gd = (from g in conn.Grunddaten where g.ID == _ModulMikro.ID select g).FirstOrDefault();
                gd.LfdNr = _ModulMikro.LfdNr;
                gd.Modul = _ModulMikro.Modul;
                gd.Nr = _ModulMikro.Nr;
                gd.Objekt = _ModulMikro.Objekt;
                gd.Detail = _ModulMikro.Detail;
                gd.Bemerkung = _ModulMikro.Bemerkung;
                gd.Ablageort = _ModulMikro.Ablageort;
                gd.Ablageort_neu = _ModulMikro.Ablageort_neu;
                gd.Erstellt = _ModulMikro.Erstellt;
                gd.Geaendert = _ModulMikro.Geaendert;
                gd.Checked = _ModulMikro.Checked;
                gd.ImgCount = _ModulMikro.ImgCount;
                conn.SubmitChanges();
            }

        }
        public static void DeleteGrunddat(Grunddaten _ModulMikro)
        {
            using (DataClassesSammlungenDataContext conn = new DataClassesSammlungenDataContext())
            {
                var gd = from g in conn.Grunddaten where g.ID == _ModulMikro.ID select g;
                conn.Grunddaten.DeleteAllOnSubmit(gd);
                conn.SubmitChanges();
            }

        }
        #endregion
        #region Modul
        public static void AddModule(Module _ModulMikro)
        {
            using (DataClassesSammlungenDataContext conn = new DataClassesSammlungenDataContext())
            {
                conn.Module.InsertOnSubmit(_ModulMikro);
                conn.SubmitChanges();
            }

        }

        public static void EditModule(Module _ModulMikro)
        {
            using (DataClassesSammlungenDataContext conn = new DataClassesSammlungenDataContext())
            {
                Module md = (from m in conn.Module where m.ID == _ModulMikro.ID select m).FirstOrDefault();
                md.Modul = _ModulMikro.Modul;
                conn.SubmitChanges();
            }

        }
        public static void DeleteModule(Module _ModulMikro)
        {
            using (DataClassesSammlungenDataContext conn = new DataClassesSammlungenDataContext())
            {
                var gd = from g in conn.Module where g.ID == _ModulMikro.ID select g;
                conn.Module.DeleteAllOnSubmit(gd);
                conn.SubmitChanges();
            }

        }
        #endregion
        #region MikroMakro
        public static void AddModulMikro(ModulMikro _ModulMikro)
        {
            using (DataClassesSammlungenDataContext conn = new DataClassesSammlungenDataContext())
            {
                conn.ModulMikro.InsertOnSubmit(_ModulMikro);
                conn.SubmitChanges();
            }

        }

        public static void EditModulMikro(ModulMikro _ModulMikro)
        {
            using (DataClassesSammlungenDataContext conn = new DataClassesSammlungenDataContext())
            {
                ModulMikro md = (from m in conn.ModulMikro where m.ID == _ModulMikro.ID select m).FirstOrDefault();
                md.Aufhellung = _ModulMikro.Aufhellung;
                md.Einschluss = _ModulMikro.Einschluss;
                md.Farbung = _ModulMikro.Farbung;
                md.Fixierung = _ModulMikro.Fixierung;
                md.Grunddaten_ID = _ModulMikro.Grunddaten_ID;
                md.Hineise = _ModulMikro.Hineise;
                md.Schnittart = _ModulMikro.Schnittart;
                md.Schnittebene = _ModulMikro.Schnittebene;
                conn.SubmitChanges();
            }

        }
        public static void DeleteModulMikro(ModulMikro _ModulMikro)
        {
            using (DataClassesSammlungenDataContext conn = new DataClassesSammlungenDataContext())
            {
                var gd = from g in conn.ModulMikro where g.ID == _ModulMikro.ID select g;
                conn.ModulMikro.DeleteAllOnSubmit(gd);
                conn.SubmitChanges();
            }

        }
        #endregion
        #region Exponate
        public static void AddExponate(Exponate exponate)
        {
            using (DataClassesSammlungenDataContext conn = new DataClassesSammlungenDataContext())
            {
                conn.Exponate.InsertOnSubmit(exponate);
                conn.SubmitChanges();
            }

        }

        public static void EditExponate(Exponate exponate)
        {
            using (DataClassesSammlungenDataContext conn = new DataClassesSammlungenDataContext())
            {
                Exponate ex = (from x in conn.Exponate where x.ID == exponate.ID select x).FirstOrDefault();
                ex.Fundstelle_Land = exponate.Fundstelle_Land;
                ex.Fundstelle_Ort = exponate.Fundstelle_Ort;
                ex.Fund_Datum = exponate.Fund_Datum;
                ex.Grunddaten_ID = exponate.Grunddaten_ID;
                ex.Hinweise = exponate.Hinweise;
                ex.Koordinaten = exponate.Koordinaten;
                conn.SubmitChanges();
            }

        }
        public static void DelExponate(Exponate exponate)
        {
            using (DataClassesSammlungenDataContext conn = new DataClassesSammlungenDataContext())
            {
                var ex = from x in conn.Exponate where x.ID == exponate.ID select x;
                conn.Exponate.DeleteAllOnSubmit(ex);
                conn.SubmitChanges();
            }

        }
        #endregion
        #region Mineral
        public static void AddMineralien(Mineralien mineralien)
        {
            using (DataClassesSammlungenDataContext conn = new DataClassesSammlungenDataContext())
            {
                conn.Mineralien.InsertOnSubmit(mineralien);
                conn.SubmitChanges();
            }

        }

        public static void EditMineralien(Mineralien mineralien)
        {
            using (DataClassesSammlungenDataContext conn = new DataClassesSammlungenDataContext())
            {
                Mineralien min = (from m in conn.Mineralien where m.ID == mineralien.ID select m).FirstOrDefault();
                min.Gewicht = mineralien.Gewicht;
                min.Grunddaten_ID = mineralien.Grunddaten_ID;
                min.Hinweise = mineralien.Hinweise;
                min.Koordinaten = mineralien.Koordinaten;
                min.Volumen = mineralien.Volumen;
                min.Zusammensetzung = mineralien.Zusammensetzung;
                min.Dichte = mineralien.Dichte;
                conn.SubmitChanges();
            }

        }
        public static void DelMineralien(Mineralien mineralien)
        {
            using (DataClassesSammlungenDataContext conn = new DataClassesSammlungenDataContext())
            {
                var ex = from m in conn.Mineralien where m.ID == mineralien.ID select m;
                conn.Mineralien.DeleteAllOnSubmit(ex);
                conn.SubmitChanges();
            }

        }
        #endregion

        #region Ablage
        public static void AddAblage(Ablage ablage)
        {
            using (DataClassesSammlungenDataContext conn = new DataClassesSammlungenDataContext())
            {
                conn.Ablage.InsertOnSubmit(ablage);
                conn.SubmitChanges();
            }

        }

        public static void EditAblage(Ablage ablage)
        {
            using (DataClassesSammlungenDataContext conn = new DataClassesSammlungenDataContext())
            {
                Ablage ab = (from a in conn.Ablage where a.ID == ablage.ID select a).FirstOrDefault();
                ab.Ablageort = ablage.Ablageort;
                ab.Beschreibung = ablage.Beschreibung;
                conn.SubmitChanges();
            }

        }
        public static void DeleteAblage(Ablage ablage)
        {
            using (DataClassesSammlungenDataContext conn = new DataClassesSammlungenDataContext())
            {
                var ab = from a in conn.Ablage where a.ID == ablage.ID select a;
                conn.Ablage.DeleteAllOnSubmit(ab);
                conn.SubmitChanges();
            }
        }
        #endregion
      
    }
}
