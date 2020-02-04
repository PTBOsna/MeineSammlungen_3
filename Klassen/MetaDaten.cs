using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;

using System.Globalization;
using System.Windows;

namespace MeineSammlungen_3
{
    public class ExifDaten
    {
        public string Kamera { get; set; }
        public string AufnahmeDat { get; set; }
        public string Blende { get; set; }
        public string Belichtung { get; set; }
        public string ISO { get; set; }
        public string Brennweite { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Altitude { get; set; }
        public string LongitudeDez { get; set; }
        public string LatitudeDez { get; set; }

        public ExifDaten(string curImg)
        {
            //curImg = _curImg;
            //ExifDaten currExif = new ExifDaten();
            string sDocument = curImg;
            if (File.Exists(sDocument) == false)
            {
                return;
            }
            //Klassen.Admin.FileIsLocked(sDocument);
            //FileStream myStream = new FileStream(sDocument, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            //JpegBitmapDecoder Decoder = new JpegBitmapDecoder(myStream, BitmapCreateOptions.None, BitmapCacheOption.None);
            //var metadata = 
            //Image img;
            //using (var bmpTemp = new Bitmap(sDocument))
            //{
            //    img = new Bitmap(bmpTemp);
            //}
            //Image imgLoaded = img;
            Image imgLoaded = Image.FromFile(sDocument);
            Encoding enc = Encoding.Default;
            string lat_Ref = null;
            string long_Ref = null;
            foreach (PropertyItem item in imgLoaded.PropertyItems)
            {
                //alle Infos auslesen - item.Id beinhaltet die Position der spezifischen Information
                //Geografische Position
                //Case 1
                //    lat_Ref = Replace(System.Text.Encoding.ASCII.GetString(Info.Value), vbNullChar, "")
                if (item.Id == 1)
                {
                    lat_Ref = enc.GetString(item.Value, 0, 1);
                    //MessageBox.Show(lat_Ref);
                }

                if (item.Id == 2)
                {
                    decimal degrees = (System.BitConverter.ToInt32(item.Value, 0) / System.BitConverter.ToInt32(item.Value, 4));
                    decimal minutes = (System.BitConverter.ToInt32(item.Value, 8) / System.BitConverter.ToInt32(item.Value, 12));
                    decimal seconds = (System.BitConverter.ToInt32(item.Value, 16) / System.BitConverter.ToInt32(item.Value, 20));

                    decimal lat_dd = System.Math.Round(degrees + (minutes / 60) + (seconds / 3600), 6);
                    Latitude = degrees.ToString() + "°" + minutes + "'" + seconds + (char)34 + " " + lat_Ref;
                    LatitudeDez = "Dezimal: " + lat_dd.ToString() + " " + lat_Ref;
                }

                if (item.Id == 3)
                {
                    long_Ref = enc.GetString(item.Value, 0, 1);
                }
                if (item.Id == 4)
                {
                    decimal degrees = (System.BitConverter.ToInt32(item.Value, 0) / System.BitConverter.ToInt32(item.Value, 4));
                    decimal minutes = (System.BitConverter.ToInt32(item.Value, 8) / System.BitConverter.ToInt32(item.Value, 12));
                    decimal seconds = (System.BitConverter.ToInt32(item.Value, 16) / System.BitConverter.ToInt32(item.Value, 20));

                    decimal long_dd = System.Math.Round(degrees + (minutes / 60) + (seconds / 3600), 6);
                    Longitude = degrees.ToString() + "°" + minutes + "'" + seconds + (char)34 + " " + long_Ref;
                    LongitudeDez = "Dezimal: " + long_dd.ToString() + " " + long_Ref;
                }


                if (item.Id == 6)
                {
                    Altitude = "Höhe über NN: " + (((Int32)System.BitConverter.ToInt32(item.Value, 0)) / 1000).ToString() + " Meter";
                }


                if (item.Id == 272) Kamera = enc.GetString(item.Value, 0, item.Len - 1);
                //if (item.Id == 36867) ExifDaten.AufnahmeDat = enc.GetString(item.Value, 0, item.Len - 1);
                if (item.Id == 33437) Blende = (BitConverter.ToInt32(item.Value, 0) / BitConverter.ToInt32(item.Value, 4)).ToString();

                if (item.Id == 33434) Belichtung = (BitConverter.ToInt32(item.Value, 0)).ToString() + "/" + (BitConverter.ToInt32(item.Value, 4)).ToString() + " sec.";
                if (item.Id == 34855) ISO = (BitConverter.ToInt16(item.Value, 0)).ToString();
                if (item.Id == 37386) Brennweite = (BitConverter.ToInt32(item.Value, 0) / BitConverter.ToInt32(item.Value, 4)).ToString() + " mm";
                // Datum und Uhrzeit auslesen
                if (item.Id == 306) AufnahmeDat = System.Text.Encoding.Default.GetString(item.Value);

            }
            imgLoaded.Dispose();
            //MessageBox.Show(ExifDaten.Latitude + "\r\n" + ExifDaten.LatitudeDez + "\r\n" + ExifDaten.Longitude + "\r\n" + ExifDaten.LongitudeDez + "\r\n" + ExifDaten.Altitude);

        }
        public void clearExif()
        {
            Altitude = null;
            AufnahmeDat = null;
            Belichtung = null;
            Blende = null;
            Brennweite = null;
            ISO = null;
            Kamera = null;
            Latitude = null;
            LatitudeDez = null;
            Longitude = null;
            LongitudeDez = null;

        }
    }

    //public static class IPTC_Const
    //{
    //    static string IPTC_Headline = @"/app13/irb/8bimiptc/iptc/Headline"; // ; //Detail
    //    static string IPTC_Caption = @"/app13/irb/8bimiptc/iptc/Caption"; // ; //Bemerkung
    //    static string IPTC_Object = @"/app13/irb/8bimiptc/iptc/Object name"; //Objekt
    //    static string IPTC_City = @"/app13/irb/8bimiptc/iptc/city"; //Fundstelle Ort
    //    static string IPTC_Loacation = @"/app13/irb/8bimiptc/iptc/sub-location"; //Position
    //    static string IPTC_State = @"/app13/irb/8bimiptc/iptc/Province\/State"; // Land
    //    static string IPTC_Country = @"/app13/irb/8bimiptc/iptc/Country\/Primary Location Name"; // Country
    //    static string IPTC_Quelle = @"/app13/irb/8bimiptc/iptc/source"; //Quelle (Negativ)
    //    static string IPTC_Copyright = @"/app13/irb/8bimiptc/iptc/copyright notice"; //Copyright
    //    static string IPTC_Author = @"/app13/irb/8bimiptc/iptc/by-line"; //Autor
    //    static string IPTC_Instr = @"/app13/irb/8bimiptc/iptc/Edit Status"; //Hinweis (in DB aufgenommen)
    //    static string IPTC_DigitCreateDate = @"/app13/irb/8bimiptc/iptc/Digital Creation Date"; //Form yyyyMMdd
    //    static string IPTC_Created = @"/app13/irb/8bimiptc/iptc/Date Created"; //Form yyyyMMdd
    //    static string IPTC_Special = @"/app13/irb/8bimiptc/iptc/Special Instructions";
    //}

    //public class myIPTCDaten
    //{
    //    public static string iObjekt; //Feld Objekt in Grunddaten
    //    public static string iDeteil; //Feld Detail in Grunddaten
    //    public static string iBemerkung; //Feld Bemerkung in Grunddaten
    //    public static string iFundstelleOrt; //Feld Fundstelle Ort in Exponate
    //    public static string iPostition; //Feld Postition in Exponate (ggf. auf GPS-Daten)
    //    public static string iFundstelleLand; //Feld Fundstelle Land in Exponate
    //    public static string iFundstelleCountry; //Feld Fundstelle Bundesland in Exponate
    //    public static string iQuelle; //Herkunft (Digitalbild, Analolgbild, Internet, Scann) aus Externer Tabelle
    //    public static string iSpezial; //nähere Bezeichung der Quelle (Negativ-Nr, Datei-Name des Originals, Webseite bei Internet, Scann-Quelle)
    //    public static string iCopyright; //PTBBerlin    
    //    public static string iAutor; //PTBBerlin
    //    public static string iHinweise;//Text: In DB als Objekt Nr. xxx aufgenommen.
    //    public static string iDigitalErstellt;
    //    public static string iErstellt;
    //    public static List<string> iStichworte;//
    //    public static string iStichwortText; //iStichworte als ein String kombiniert
    //}
    public class IPTCDaten
    {
        string IPTC_Headline = @"/app13/irb/8bimiptc/iptc/Headline";
        string IPTC_Caption = @"/app13/irb/8bimiptc/iptc/Caption";
        string IPTC_Object = @"/app13/irb/8bimiptc/iptc/Object name"; //Objekt
        string IPTC_City = @"/app13/irb/8bimiptc/iptc/city"; //Fundstelle Ort
        string IPTC_Loacation = @"/app13/irb/8bimiptc/iptc/sub-location"; //Position
        string IPTC_State = @"/app13/irb/8bimiptc/iptc/Province\/State"; // Land
        string IPTC_Country = @"/app13/irb/8bimiptc/iptc/Country\/Primary Location Name"; // Country
        string IPTC_Quelle = @"/app13/irb/8bimiptc/iptc/source"; //Quelle (Negativ)
        string IPTC_Copyright = @"/app13/irb/8bimiptc/iptc/copyright notice"; //Copyright
        string IPTC_Author = @"/app13/irb/8bimiptc/iptc/by-line"; //Autor
        string IPTC_Instr = @"/app13/irb/8bimiptc/iptc/Edit Status"; //Hinweis (in DB aufgenommen)
        string IPTC_DigitCreateDate = @"/app13/irb/8bimiptc/iptc/Digital Creation Date"; //Form yyyyMMdd
        string IPTC_Created = @"/app13/irb/8bimiptc/iptc/Date Created"; //Form yyyyMMdd
        string IPTC_Special = @"/app13/irb/8bimiptc/iptc/Special Instructions";
        public string iObjekt { get; set; }
        public string iDeteil { get; set; }
        public string iBemerkung { get; set; }
        public string iFundstelleOrt { get; set; }
        public string iPostition { get; set; }
        public string iFundstelleLand { get; set; }
        public string iFundstelleCountry { get; set; }
        public string iQuelle { get; set; }
        public string iSpezial { get; set; }
        public string iCopyright { get; set; }
        public string iAutor { get; set; }
        public string iHinweise { get; set; }
        public string iDigitalErstellt { get; set; }
        public string iErstellt { get; set; }
        public List<string> iStichworte { get; set; }
        public string iStichwortText { get; set; }
        /// <summary>
        /// Konstrukton IPTCDaten
        /// mit curImg als Übergabeparameter
        /// </summary>
        /// <param name="curImg"></param>
        public IPTCDaten(string curImg)
        {
            //if (curImg == null)
            // {
            //     return;
            // }
            string dummy = null;
            string sDocument = curImg;
            if (File.Exists(sDocument) == false)
            {
                return;
            }
            var stream = new FileStream(sDocument, FileMode.Open, FileAccess.Read);
            var decoder = new JpegBitmapDecoder(stream, BitmapCreateOptions.None, BitmapCacheOption.None);
            var metadata = decoder.Frames[0].Metadata as BitmapMetadata;
            if (metadata != null)
            {
                //Anzeige StichworteText
                try
                {
                    dummy = metadata.Keywords.Aggregate((old, val) => old + "; " + val);
                    iStichwortText = dummy;
                }
                catch (Exception)
                {
                    iStichwortText = "keine Angaben";
                }
                //Anzeige Stichworte (Liste)

                List<string> lst = new List<string>();
                if (metadata.Keywords != null)
                {
                    //iStichworte = metadata.Keywords.ToList();
                    lst = metadata.Keywords.ToList();
                    iStichworte = lst.ToList();
                }
                else
                {
                    //lst.Add("ohne");
                    //iStichworte = lst.ToList();
                }
                //Anzeige Beschreibung
                dummy = (string)metadata.GetQuery(IPTC_Headline);
                if (String.IsNullOrEmpty(dummy) == false)
                {
                    iDeteil = dummy;
                }
                else
                    iDeteil = "keine Angaben";
                //Anzeige Detail
                dummy = (string)metadata.GetQuery(IPTC_Caption);
                if (String.IsNullOrEmpty(dummy) == false)
                {
                    iBemerkung = dummy;
                }
                else
                    iBemerkung = "keine Angaben";
                //Anzeige Art
                dummy = (string)metadata.GetQuery(IPTC_Object);
                if (String.IsNullOrEmpty(dummy) == false)
                {
                    iObjekt = dummy;
                }
                else
                    iObjekt = "keine Angaben";
                //Anzeige Fundstelle Ort
                dummy = (string)metadata.GetQuery(IPTC_City);
                if (String.IsNullOrEmpty(dummy) == false)
                {
                    iFundstelleOrt = dummy;
                }
                else
                    iFundstelleOrt = "keine Angaben";
                //Anzeige Position Fundstelle
                dummy = (string)metadata.GetQuery(IPTC_Loacation);
                if (String.IsNullOrEmpty(dummy) == false)
                {
                    iPostition = dummy;
                }
                else
                    iPostition = "keine Angaben";
                //Anzeige Land
                dummy = (string)metadata.GetQuery(IPTC_State);
                if (String.IsNullOrEmpty(dummy) == false)
                {
                    iFundstelleLand = dummy;
                }
                else
                    iFundstelleLand = "keine Angaben";
                //Aneige Country/Bundesland
                dummy = (string)metadata.GetQuery(IPTC_Country);
                if (String.IsNullOrEmpty(dummy) == false)
                {
                    iFundstelleCountry = dummy;
                }
                else
                    iFundstelleCountry = "keine Angaben";
                //Anzeige Quelle (z.B.Digaitalbild...)
                dummy = (string)metadata.GetQuery(IPTC_Quelle);
                if (String.IsNullOrEmpty(dummy) == false)
                {
                    iQuelle = dummy;
                }
                else
                    iQuelle = "keine Angaben";
                //Anzeige Spezial (z.B. Negati Nr.)
                dummy = (string)metadata.GetQuery(IPTC_Special);
                if (String.IsNullOrEmpty(dummy) == false)
                {
                    iSpezial = dummy;
                }
                else
                    iSpezial = "keine Angaben";
                //Anzeige Copyright
                dummy = (string)metadata.GetQuery(IPTC_Copyright);
                if (String.IsNullOrEmpty(dummy) == false)
                {
                    iCopyright = dummy;
                }
                else
                    iCopyright = "keine Angaben";
                //Anzeige Autor
                dummy = (string)metadata.GetQuery(IPTC_Author);
                if (String.IsNullOrEmpty(dummy) == false)
                {
                    iAutor = dummy;
                }
                else
                    iAutor = "keine Angaben";
                //Anzeige Hinweise (z.B. in DB aufgenommen)
                dummy = (string)metadata.GetQuery(IPTC_Instr);
                if (String.IsNullOrEmpty(dummy) == false)
                {
                    iHinweise = dummy;
                }
                else
                    iHinweise = null;
                // Anzeige Digital erstellt
                dummy = (string)metadata.GetQuery(IPTC_DigitCreateDate);
                if (String.IsNullOrEmpty(dummy) == false)
                {
                    try
                    {
                        iDigitalErstellt = DateTime.ParseExact(dummy, "yyyyMMdd", CultureInfo.CurrentCulture).ToString();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ungültiges Datumsformat. Datum 'Digital erstellt' wird als " + dummy + " eingetragen!");
                        iDigitalErstellt = dummy;
                    }
                }
                else
                    iDigitalErstellt = null;
                //Anzeige Erstellt
                dummy = (string)metadata.GetQuery(IPTC_Created);
                if (String.IsNullOrEmpty(dummy) == false)
                    try
                    {
                        iErstellt = DateTime.ParseExact(dummy, "yyyyMMdd", CultureInfo.CurrentCulture).ToString();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Ungültiges Datumsformat. Datum 'Erstellt' wird als " + dummy + " eingetragen!");
                        iErstellt = dummy;
                    }
                else
                    iErstellt = null;
            }


            stream.Close();
            stream = null;
        }

        /// <summary>
        /// Speichern der IPTC-Daten
        /// der Bilddatei myFile
        /// </summary>
        /// <param name="myFile"></param>
        public void WriteIPTC(string myFile)
        {
            if (string.IsNullOrEmpty(myFile) == true)
            { return; }

            var originalImage = new FileInfo(myFile);
            BitmapFrame bitmapFrame = null;
            // load the jpg file with a PngBitmapDecoder    
            using (Stream pngStreamIn = File.Open(myFile, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            {
                var decoder = new JpegBitmapDecoder(pngStreamIn, BitmapCreateOptions.PreservePixelFormat,
                    BitmapCacheOption.OnLoad);
                bitmapFrame = decoder.Frames[0];
            }
            var metadata = bitmapFrame.Metadata.Clone() as BitmapMetadata;

            if ((iStichworte != null))
            {
                metadata.Keywords = new ReadOnlyCollection<string>(iStichworte);
                //replace keywords
            }
            if (!string.IsNullOrWhiteSpace(iObjekt))
            {
                metadata.SetQuery(IPTC_Object, iObjekt);
                //  set new description
            }
            if (!string.IsNullOrWhiteSpace(iBemerkung))
            {
                metadata.SetQuery(IPTC_Caption, iBemerkung);
            }
            if (!string.IsNullOrWhiteSpace(iDeteil))
            {
                metadata.SetQuery(IPTC_Headline, iDeteil);
            }
            if (!string.IsNullOrWhiteSpace(iAutor))
            {
                metadata.SetQuery(IPTC_Author, iAutor);
            }
            if (!string.IsNullOrWhiteSpace(iPostition))
            {
                metadata.SetQuery(IPTC_Loacation, iPostition);
            }
            if (!string.IsNullOrWhiteSpace(iFundstelleCountry))
            {
                metadata.SetQuery(IPTC_Country, iFundstelleCountry);
            }
            if (!string.IsNullOrWhiteSpace(iFundstelleOrt))
            {
                metadata.SetQuery(IPTC_City, iFundstelleOrt);
            }
            if (!string.IsNullOrWhiteSpace(iFundstelleLand))
            {
                metadata.SetQuery(IPTC_State, iFundstelleLand);
            }
            if (!string.IsNullOrWhiteSpace(iQuelle))
            {
                //MessageBox.Show(iQuelle);
                metadata.SetQuery(IPTC_Quelle, iQuelle);
            }
            if (!string.IsNullOrWhiteSpace(iSpezial))
            {
                //MessageBox.Show(iSpezial);
                metadata.SetQuery(IPTC_Special, iSpezial);
            }
            if (!string.IsNullOrWhiteSpace(iHinweise))
            {
                metadata.SetQuery(IPTC_Instr, iHinweise);
            }
            if (!string.IsNullOrWhiteSpace(iDigitalErstellt))
            {

                metadata.SetQuery(IPTC_DigitCreateDate, DateTime.Now.ToString("yyyyMMdd"));
            }
            if (!string.IsNullOrWhiteSpace(iErstellt))
            {
                metadata.SetQuery(IPTC_Created, iErstellt);
            }
            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapFrame, bitmapFrame.Thumbnail, metadata, bitmapFrame.ColorContexts));
            originalImage.Delete();
            using (Stream pngStreamOut = File.Open(myFile, FileMode.Create, FileAccess.ReadWrite))
            {
                encoder.Save(pngStreamOut);
            }
        }
    }
}

