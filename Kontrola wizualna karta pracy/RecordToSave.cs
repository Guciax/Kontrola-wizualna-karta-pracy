using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontrola_wizualna_karta_pracy
{
    public class RecordToSave
    {
        public RecordToSave(string Operator,int iloscDobrych,string numerZlecenia, DateTime Data_czas, int ngBrakLutowia=0, int ngBrakDiodyLed=0, int ngBrakResConn = 0, int ngPrzesuniecieLed = 0, int ngPrzesuniecieResConn = 0, int ngZabrudzenieLed = 0, int ngUszkodzenieMechaniczneLed = 0, int ngUszkodzenieConn = 0, int ngWadaFabrycznaDiody = 0, int ngUszkodzonePcb = 0, int ngWadaNaklejki = 0, int ngSpalonyConn = 0, int ngInne = 0, int scrapBrakLutowia = 0, int scrapBrakDiodyLed = 0, int scrapBrakResConn = 0, int scrapPrzesuniecieLed = 0, int scrapPrzesuniecieResConn = 0, int scrapZabrudzenieLed = 0, int scrapUszkodzenieMechaniczneLed = 0, int scrapUszkodzenieConn = 0, int scrapWadaFabrycznaDiody = 0, int scrapUszkodzonePcb = 0, int scrapWadaNaklejki = 0, int scrapSpalonyConn = 0, int scrapInne = 0, int ngTestElektryczny = 0, int iloscWszystkich=0)
        {
            Data_Czas = Data_czas;
            this.Operator = Operator;
            IloscDobrych = iloscDobrych;
            NumerZlecenia = numerZlecenia;
            NgBrakLutowia = ngBrakLutowia;
            NgBrakDiodyLed = ngBrakDiodyLed;
            NgBrakResConn = ngBrakResConn;
            NgPrzesuniecieLed = ngPrzesuniecieLed;
            NgPrzesuniecieResConn = ngPrzesuniecieResConn;
            NgZabrudzenieLed = ngZabrudzenieLed;
            NgUszkodzenieMechaniczneLed = ngUszkodzenieMechaniczneLed;
            NgUszkodzenieConn = ngUszkodzenieConn;
            NgWadaFabrycznaDiody = ngWadaFabrycznaDiody;
            NgUszkodzonePcb = ngUszkodzonePcb;
            NgWadaNaklejki = ngWadaNaklejki;
            NgSpalonyConn = ngSpalonyConn;
            NgInne = ngInne;
            ScrapBrakLutowia = scrapBrakLutowia;
            ScrapBrakDiodyLed = scrapBrakDiodyLed;
            ScrapBrakResConn = scrapBrakResConn;
            ScrapPrzesuniecieLed = scrapPrzesuniecieLed;
            ScrapPrzesuniecieResConn = scrapPrzesuniecieResConn;
            ScrapZabrudzenieLed = scrapZabrudzenieLed;
            ScrapUszkodzenieMechaniczneLed = scrapUszkodzenieMechaniczneLed;
            ScrapUszkodzenieConn = scrapUszkodzenieConn;
            ScrapWadaFabrycznaDiody = scrapWadaFabrycznaDiody;
            ScrapUszkodzonePcb = scrapUszkodzonePcb;
            ScrapWadaNaklejki = scrapWadaNaklejki;
            ScrapSpalonyConn = scrapSpalonyConn;
            ScrapInne = scrapInne;
            NgTestElektryczny = ngTestElektryczny;
            IloscWszystkich = iloscWszystkich;
        }

        public DateTime Data_Czas { get; set; }
        public string Operator { get; set; }
        public string NumerZlecenia { get; set; }

        public int IloscDobrych
        {
            get { return IloscWszystkich - GetAllScrap() - GetAllNg(); }
            set {; }
        }

        public int NgBrakLutowia
        {
            get;
            set;
        }

        public int NgBrakDiodyLed
        {
            get;
            set;
        }
        public int NgBrakResConn
        {
            get;
            set;
        }
        public int NgPrzesuniecieLed
        {
            get;
            set;
        }
        public int NgPrzesuniecieResConn
        {
            get;
            set;
        }
        public int NgZabrudzenieLed
        {
            get;
            set;
        }
        public int NgUszkodzenieMechaniczneLed
        {
            get;
            set;
        }
        public int NgUszkodzenieConn
        {
            get;
            set;
        }
        public int NgWadaFabrycznaDiody
        {
            get;
            set;
        }
        public int NgUszkodzonePcb
        {
            get;
            set;
        }
        public int NgWadaNaklejki
        {
            get;
            set;
        }
        public int NgSpalonyConn
        {
            get;
            set;
        }
        public int NgInne
        {
            get;
            set;
        }
        public int ScrapBrakLutowia
        {
            get;
            set;
        }
        public int ScrapBrakDiodyLed
        {
            get;
            set;
        }
        public int ScrapBrakResConn
        {
            get { return _scrapBrakResConn; }
            set
            {
                if (IloscDobrych > 0)
                {
                    //IloscDobrych += _scrapBrakResConn - value;
                }
                _scrapBrakResConn = value;
                //IloscDobrych = IloscWszystkich - GetAllNg() - GetAllScrap();
            }
        }
        public int ScrapPrzesuniecieLed
        {
            get;
            set;
        }
        public int ScrapPrzesuniecieResConn
        {
            get;
            set;
        }
        public int ScrapZabrudzenieLed
        {
            get;
            set;
        }
        public int ScrapUszkodzenieMechaniczneLed
        {
            get;
            set;
        }
        public int ScrapUszkodzenieConn
        {
            get;
            set;
        }
        public int ScrapWadaFabrycznaDiody
        {
            get;
            set;
        }
        public int ScrapUszkodzonePcb
        {
            get;
            set;
        }
        public int ScrapWadaNaklejki
        {
            get;
            set;
        }
        public int ScrapSpalonyConn
        {
            get;
            set;
        }
        public int ScrapInne
        {
            get;
            set;
        }
        public int NgTestElektryczny
        {
            get;
            set;
        }

        public int IloscWszystkich
        {
            get;
            set;
        }

        private int _IloscWszystkich = 0;
        private int _ngBrakLutowia = 0;
        private int _ngBrakDiodyLed = 0;
        private int _ngBrakResConn = 0;
        private int _ngPrzesuniecieLed = 0;
        private int _ngPrzesuniecieResConn = 0;
        private int _ngZabrudzenieLed = 0;
        private int _ngUszkodzenieMechaniczneLed = 0;
        private int _ngUszkodzenieConn = 0;
        private int _ngWadaFabrycznaDiody = 0;
        private int _ngUszkodzonePcb = 0;
        private int _ngWadaNaklejki = 0;
        private int _ngSpalonyConn = 0;
        private int _ngInne = 0;
        private int _scrapBrakLutowia ;
        private int _scrapBrakDiodyLed ;
        private int _scrapBrakResConn;
        private int _scrapPrzesuniecieLed;
        private int _scrapPrzesuniecieResConn;
        private int _scrapZabrudzenieLed;
        private int _scrapUszkodzenieMechaniczneLed;
        private int _scrapUszkodzenieConn;
        private int _scrapWadaFabrycznaDiody;
        private int _scrapUszkodzonePcb;
        private int _scrapWadaNaklejki;
        private int _scrapSpalonyConn;
        private int _scrapInne;
        private int _ngTestElektryczny;

        private int GetAllNg()
        {
            return NgBrakLutowia+ NgBrakDiodyLed + NgBrakResConn + NgPrzesuniecieLed + NgPrzesuniecieResConn + NgZabrudzenieLed + NgUszkodzenieMechaniczneLed + NgUszkodzenieConn + NgWadaFabrycznaDiody + NgUszkodzonePcb +NgWadaNaklejki + NgSpalonyConn + NgInne + NgTestElektryczny;
        }

        private int GetAllScrap()
        {
            return  ScrapBrakLutowia + ScrapBrakDiodyLed + ScrapBrakResConn + ScrapPrzesuniecieLed + ScrapPrzesuniecieResConn + ScrapZabrudzenieLed + ScrapUszkodzenieMechaniczneLed + ScrapUszkodzenieConn + ScrapWadaFabrycznaDiody + ScrapUszkodzonePcb + ScrapWadaNaklejki + ScrapSpalonyConn+ ScrapInne;
        }
    }
}
