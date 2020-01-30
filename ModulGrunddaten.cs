using System;
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
            #endregion
        }
    }
}
