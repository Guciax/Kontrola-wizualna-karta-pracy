using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kontrola_wizualna_karta_pracy
{
    class controlToDbTranslation
    {
        public static string GetLabelCaptionFromDbColumn(string colName)
        {
            switch (colName)
            {
                case "ngBrakLutowia": return "Brak Lutowia";
                case "ngBrakDiodyLed": return "Brak LED";
                case "ngBrakResConn": return "Brak RES/CONN";
                case "ngPrzesuniecieLed": return "Przesunięcie LED";
                case "ngPrzesuniecieResConn": return "Przesunięcie CONN";
                case "ngZabrudzenieLed": return "Zabrudzenie LED";
                case "ngUszkodzenieMechaniczneLed": return "Uszkodzenie mech. LED";
                case "ngUszkodzenieConn": return "Uszkodzenie CONN";
                case "ngWadaFabrycznaDiody": return "Wada fabryczna LED";
                case "ngUszkodzonePcb": return "Uszkodzone PCB";
                case "ngWadaNaklejki": return "Wada naklejki";
                case "ngSpalonyConn": return "Spalony CONN";
                case "ngInne": return "Inne";
                case "scrapBrakLutowia": return "Brak Lutowia";
                case "scrapBrakDiodyLed": return "Brak LED";
                case "scrapBrakResConn": return "Brak RES/CONN";
                case "scrapPrzesuniecieLed": return "Przesunięcie LED";
                case "scrapPrzesuniecieResConn": return "Przesunięcie CONN";
                case "scrapZabrudzenieLed": return "Zabrudzenie LED";
                case "scrapUszkodzenieMechaniczneLed": return "Uszkodzenie mech. LED";
                case "scrapUszkodzenieConn": return "Uszkodzenie CONN";
                case "scrapWadaFabrycznaDiody": return "Wada fabryczna LED";
                case "scrapUszkodzonePcb": return "Uszkodzone PCB";
                case "scrapWadaNaklejki": return "Wada naklejki";
                case "scrapSpalonyConn": return "Spalony CONN";
                case "scrapInne": return "Inne";
                case "ngTestElektryczny": return "Test elektryczny";
                default: return colName;
            }
        }

        
    }
}
