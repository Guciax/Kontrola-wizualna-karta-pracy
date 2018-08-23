using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontrola_wizualna_karta_pracy
{
    public class RecordToSave
    {
        public RecordToSave(string Operator,int iloscDobrych,string numerZlecenia, DateTime Data_czas, int ngBrakLutowia=0, int ngBrakDiodyLed=0, int ngBrakResConn = 0, int ngPrzesuniecieLed = 0, int ngPrzesuniecieResConn = 0, int ngZabrudzenieLed = 0, int ngUszkodzenieMechaniczneLed = 0, int ngUszkodzenieConn = 0, int ngWadaFabrycznaDiody = 0, int ngUszkodzonePcb = 0, int ngWadaNaklejki = 0, int ngSpalonyConn = 0, int ngInne = 0, int scrapBrakLutowia = 0, int scrapBrakDiodyLed = 0, int scrapBrakResConn = 0, int scrapPrzesuniecieLed = 0, int scrapPrzesuniecieResConn = 0, int scrapZabrudzenieLed = 0, int scrapUszkodzenieMechaniczneLed = 0, int scrapUszkodzenieConn = 0, int scrapWadaFabrycznaDiody = 0, int scrapUszkodzonePcb = 0, int scrapWadaNaklejki = 0, int scrapSpalonyConn = 0, int scrapInne = 0, int ngTestElektryczny = 0)
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
        }

        public DateTime Data_Czas { get; set; }
        public string Operator { get; set; }
        public int IloscDobrych { get; set; }
        public string NumerZlecenia { get; set; }
        public int NgBrakLutowia
        {
            get { return _ngBrakLutowia; }
            set
            {
                if (IloscDobrych > 0)
                {
                    IloscDobrych += _ngBrakLutowia - value;
                }
                _ngBrakLutowia = value;
            }
        }

        public int NgBrakDiodyLed
        {
            get { return _ngBrakDiodyLed; }
            set
            {
                if (IloscDobrych > 0)
                {
                    IloscDobrych += _ngBrakDiodyLed - value;
                }
                _ngBrakDiodyLed = value;
            }
        }
        public int NgBrakResConn
        {
            get { return _ngBrakResConn; }
            set
            {
                if (IloscDobrych > 0)
                {
                    IloscDobrych += _ngBrakResConn - value;
                }
                _ngBrakResConn = value;
            }
        }
        public int NgPrzesuniecieLed
        {
            get { return _ngPrzesuniecieLed; }
            set
            {
                if (IloscDobrych > 0)
                {
                    IloscDobrych += _ngPrzesuniecieLed - value;
                }
                _ngPrzesuniecieLed = value;
            }
        }
        public int NgPrzesuniecieResConn
        {
            get { return _ngPrzesuniecieResConn; }
            set
            {
                if (IloscDobrych > 0)
                {
                    IloscDobrych += _ngPrzesuniecieResConn - value;
                }
                _ngPrzesuniecieResConn = value;
            }
        }
        public int NgZabrudzenieLed
        {
            get { return _ngZabrudzenieLed; }
            set
            {
                if (IloscDobrych > 0)
                {
                    IloscDobrych += _ngZabrudzenieLed - value;
                }
                _ngZabrudzenieLed = value;
            }
        }
        public int NgUszkodzenieMechaniczneLed
        {
            get { return _ngUszkodzenieMechaniczneLed; }
            set
            {
                if (IloscDobrych > 0)
                {
                    IloscDobrych += _ngUszkodzenieMechaniczneLed - value;
                }
                _ngUszkodzenieMechaniczneLed = value;
            }
        }
        public int NgUszkodzenieConn
        {
            get { return _ngUszkodzenieConn; }
            set
            {
                if (IloscDobrych > 0)
                {
                    IloscDobrych += _ngUszkodzenieConn - value;
                }
                _ngUszkodzenieConn = value;
            }
        }
        public int NgWadaFabrycznaDiody
        {
            get { return _ngWadaFabrycznaDiody; }
            set
            {
                if (IloscDobrych > 0)
                {
                    IloscDobrych += _ngWadaFabrycznaDiody - value;
                }
                _ngWadaFabrycznaDiody = value;
            }
        }
        public int NgUszkodzonePcb
        {
            get { return _ngUszkodzonePcb; }
            set
            {
                if (IloscDobrych > 0)
                {
                    IloscDobrych += _ngUszkodzonePcb - value;
                }
                _ngUszkodzonePcb = value;
            }
        }
        public int NgWadaNaklejki
        {
            get { return _ngWadaNaklejki; }
            set
            {
                if (IloscDobrych > 0)
                {
                    IloscDobrych += _ngWadaNaklejki - value;
                }
                _ngWadaNaklejki = value;
            }
        }
        public int NgSpalonyConn
        {
            get { return _ngSpalonyConn; }
            set
            {
                if (IloscDobrych > 0)
                {
                    IloscDobrych += _ngSpalonyConn - value;
                }
                _ngSpalonyConn = value;
            }
        }
        public int NgInne
        {
            get { return _ngInne; }
            set
            {
                if (IloscDobrych > 0)
                {
                    IloscDobrych += _ngInne - value;
                }
                _ngInne = value;
            }
        }
        public int ScrapBrakLutowia
        {
            get { return _scrapBrakLutowia; }
            set
            {
                if (IloscDobrych > 0)
                {
                    IloscDobrych += _scrapBrakLutowia - value;
                }
                _scrapBrakLutowia = value;
            }
        }
        public int ScrapBrakDiodyLed
        {
            get { return _scrapBrakDiodyLed; }
            set
            {
                if (IloscDobrych > 0)
                {
                    IloscDobrych += _scrapBrakDiodyLed - value;
                }
                _scrapBrakDiodyLed = value;
            }
        }
        public int ScrapBrakResConn
        {
            get { return _scrapBrakResConn; }
            set
            {
                if (IloscDobrych > 0)
                {
                    IloscDobrych += _scrapBrakResConn - value;
                }
                _scrapBrakResConn = value;
            }
        }
        public int ScrapPrzesuniecieLed
        {
            get { return _scrapPrzesuniecieLed; }
            set
            {
                if (IloscDobrych > 0)
                {
                    IloscDobrych += _scrapPrzesuniecieLed - value;
                }
                _scrapPrzesuniecieLed = value;
            }
        }
        public int ScrapPrzesuniecieResConn
        {
            get { return _scrapPrzesuniecieResConn; }
            set
            {
                if (IloscDobrych > 0)
                {
                    IloscDobrych += _scrapPrzesuniecieResConn - value;
                }
                _scrapPrzesuniecieResConn = value;
            }
        }
        public int ScrapZabrudzenieLed
        {
            get { return _scrapZabrudzenieLed; }
            set
            {
                if (IloscDobrych > 0)
                {
                    IloscDobrych += _scrapZabrudzenieLed - value;
                }
                _scrapZabrudzenieLed = value;
            }
        }
        public int ScrapUszkodzenieMechaniczneLed
        {
            get { return _scrapUszkodzenieMechaniczneLed; }
            set
            {
                if (IloscDobrych > 0)
                {
                    IloscDobrych += _scrapUszkodzenieMechaniczneLed - value;
                }
                _scrapUszkodzenieMechaniczneLed = value;
            }
        }
        public int ScrapUszkodzenieConn
        {
            get { return _scrapUszkodzenieConn; }
            set
            {
                if (IloscDobrych > 0)
                {
                    IloscDobrych += _scrapUszkodzenieConn - value;
                }
                _scrapUszkodzenieConn = value;
            }
        }
        public int ScrapWadaFabrycznaDiody
        {
            get { return _scrapWadaFabrycznaDiody; }
            set
            {
                if (IloscDobrych > 0)
                {
                    IloscDobrych += _scrapWadaFabrycznaDiody - value;
                }
                _scrapWadaFabrycznaDiody = value;
            }
        }
        public int ScrapUszkodzonePcb
        {
            get { return _scrapUszkodzonePcb; }
            set
            {
                if (IloscDobrych > 0)
                {
                    IloscDobrych += _scrapUszkodzonePcb - value;
                }
                _scrapUszkodzonePcb = value;
            }
        }
        public int ScrapWadaNaklejki
        {
            get { return _scrapWadaNaklejki; }
            set
            {
                if (IloscDobrych > 0)
                {
                    IloscDobrych += _scrapWadaNaklejki - value;
                }
                _scrapWadaNaklejki = value;
            }
        }
        public int ScrapSpalonyConn
        {
            get { return _scrapSpalonyConn; }
            set
            {
                if (IloscDobrych > 0)
                {
                    IloscDobrych += _scrapSpalonyConn - value;
                }
                _scrapSpalonyConn = value;
            }
        }
        public int ScrapInne
        {
            get { return _scrapInne; }
            set
            {
                if (IloscDobrych > 0)
                {
                    IloscDobrych += _scrapInne - value;
                }
                _scrapInne = value;
            }
        }
        public int NgTestElektryczny
        {
            get { return _ngTestElektryczny; }
            set
            {
                if (IloscDobrych > 0)
                {
                    IloscDobrych += _ngTestElektryczny - value;
                }
                _ngTestElektryczny = value;
            }
        }


        private int _ngBrakLutowia=0;
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
    }
}
