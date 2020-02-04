using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace MeineSammlungen_3
{
    public static class Admin
    {
        //public static DataClassesSammlungenDataContext con = new DataClassesSammlungenDataContext();
        #region Grunddaten
        class cGrunddaten
        {
            public Int32 cID { get; set; }
            public string cNr { get; set; }
            public Int32 cModul { get; set; }
            public string cObjekt { get; set; }
            public string cDetail { get; set; }
            public DateTime cErstellt { get; set; }
            public string cBemerkung { get; set; }
            public string cAblage { get; set; }
            public DateTime cGeaendert { get; set; }
            public Int32 cImgCount { get; set; }
            public Int32 cAblage_neu { get; set; }
            public bool cChecked { get; set; }
            public int cLfdNr { get; set; }

            public cGrunddaten(Int32 _cid)
            {
                using (DataClassesSammlungenDataContext conn = new DataClassesSammlungenDataContext())
                {
                    var cgd = from g in conn.Grunddaten where g.ID == cID select g;
                    foreach (var gd in cgd)
                    {
                        cLfdNr = gd.LfdNr;
                        cModul = gd.Modul;
                        cNr = gd.Nr;
                        cObjekt = gd.Objekt;
                        cDetail = gd.Detail;
                        cBemerkung = gd.Bemerkung;
                        cAblage_neu = gd.Ablageort_neu;
                        cErstellt = (DateTime)gd.Erstellt;
                        cGeaendert = (DateTime)gd.Geaendert;
                        cChecked = (bool)gd.Checked;
                        cImgCount = gd.ImgCount;
                    }
                }
            }
        }
            public static void AddGrunddaten(Grunddaten grunddaten)
            {
                using (DataClassesSammlungenDataContext conn = new DataClassesSammlungenDataContext())
                {
                    conn.Grunddaten.InsertOnSubmit(grunddaten);
                    conn.SubmitChanges();
                }

            }

            public static void EditGrunddaten(Grunddaten grunddaten)
            {
                using (DataClassesSammlungenDataContext conn = new DataClassesSammlungenDataContext())
                {
                    Grunddaten gd = (from g in conn.Grunddaten where g.ID == grunddaten.ID select g).FirstOrDefault();
                    gd.LfdNr = grunddaten.LfdNr;
                    gd.Modul = grunddaten.Modul;
                    gd.Nr = grunddaten.Nr;
                    gd.Objekt = grunddaten.Objekt;
                    gd.Detail = grunddaten.Detail;
                    gd.Bemerkung = grunddaten.Bemerkung;
                    gd.Ablageort = grunddaten.Ablageort;
                    gd.Ablageort_neu = grunddaten.Ablageort_neu;
                    gd.Erstellt = grunddaten.Erstellt;
                    gd.Geaendert = grunddaten.Geaendert;
                    gd.Checked = grunddaten.Checked;
                    gd.ImgCount = grunddaten.ImgCount;
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
            public static void AddMikro(ModulMikro _ModulMikro)
            {
                using (DataClassesSammlungenDataContext conn = new DataClassesSammlungenDataContext())
                {
                    conn.ModulMikro.InsertOnSubmit(_ModulMikro);
                    conn.SubmitChanges();
                }

            }

            public static void EditMikro(ModulMikro _ModulMikro)
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
            public static void DeleteMikro(ModulMikro _ModulMikro)
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
            #region Settings
            public static string ImgPath;
            public static string cName; //Original Dateiname der übernommenen Bilder

            public static void GetSettings()
            {
                DataClassesSammlungenDataContext conn = new DataClassesSammlungenDataContext();
                var st = from s in conn.Settings select s;
                foreach (var item in st)
                {
                    ImgPath = item.Bildpfad;
                }
            }
            #endregion


        }
        #region Images
        public class Picture
        {
            public string Name { get; set; }
            public BitmapImage BitmapImage { get; set; }
            public DateTime DateCreated { get; set; }
            public string Path { get; set; }
            public string FileType { get; set; }

        }


        public class PictureList : ObservableCollection<Picture>
        {
            public PictureList(string myImges) //selPicture muss den Stamm des FileNmaens enthalten (z.B. Filename = 12#1.jpg, Stamm = 12)
            {
                Refresh(myImges);
            }

            private void Refresh(string _myImges)
            {
                string cPath = null;
                string pattern = null;
                String[] stamm = _myImges.Split('#'); //Wenn _myImages "*#<Path>" enthält, dann erfolgt Auflistung alle Bilder im Pfad <Path>
                                                      //string pathPictures =@"H:\Mikro-Makro\TestOrdner";
                if (stamm[0] == "*")
                {
                    cPath = stamm[1];
                    pattern = "*.jpg";
                }
                else
                {
                    pattern = stamm[0] + "#*.jpg";
                    cPath = Admin.ImgPath;
                }
                string[] fileNames = Directory.GetFiles(cPath, pattern);
                //.Union(Directory.GetFiles(pathPictures, imgPath + "*.jpg"))
                //                           .Union(Directory.GetFiles(pathPictures,imgPath + "*.jpeg"))
                //                           .OrderBy(f => f)
                //                           .ToArray();
                foreach (string fileName in fileNames)
                {

                    //if (fileName.Contains(stamm[0]) == true)
                    //{

                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.UriSource = new Uri(fileName);
                    bitmap.DecodePixelWidth = 150;
                    bitmap.EndInit();

                    this.Add(new Picture()
                    {
                        Name = Path.GetFileNameWithoutExtension(fileName),
                        BitmapImage = bitmap,
                        DateCreated = File.GetCreationTime(fileName),
                        Path = fileName,
                        FileType = Path.GetExtension(fileName),

                    });
                }
            }
            public class delImg
            {
                public static bool ImgDel(string args)
                {
                    string curPath = Admin.ImgPath;
                    // Files to be deleted   
                    try
                    {
                        foreach (var myFile in Directory.GetFiles(curPath))
                        {
                            if (myFile.Contains(args) == true)
                            {
                                //System.Windows.Forms.MessageBox.Show(myFile);
                                File.Delete(myFile);
                            }
                        }
                        return true;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }
            #endregion images

        }
        #region Allgemeines
        public class Allgemeines
        {

            public static bool FileIsLocked(string strFullFileName)
            {
                bool blnReturn = false;
                System.IO.FileStream fs;
                try
                {
                    fs = File.Open(strFullFileName, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None);
                    fs.Close();
                }
                catch (System.IO.IOException ex)
                {
                    blnReturn = true;
                }
                return blnReturn;
            }

            #endregion
        }
  
}

