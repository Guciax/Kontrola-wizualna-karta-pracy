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
        public static string GetSqlColumnName(NumericUpDown numUpD)
        {
            switch (numUpD.Name)
            {
                case "Ng0BWadyLutowia" : return "ngBrakLutowia";
                case "Ng0BrakDiodyLed" : return "ngBrakDiodyLed";
                case "Ng0BrakResConn" : return "ngBrakResConn";
                case "Ng0PrzesuniecieDiodyLed" : return "ngPrzesuniecieLed";
                case "Ng0PrzesuniecieResConn": return "ngPrzesuniecieResConn";
                case "Ng0ZabrudzonaDiodaLed" : return "ngZabrudzenieLed";
                case "Ng0UszkodzenieDiodyLed" : return "ngUszkodzenieMechaniczneLed";
                case "Ng0UszkodzenieConn" : return "ngUszkodzenieConn";
                case "Ng0WadaFbrycznaLed" : return "ngWadaFabrycznaDiody";
                case "Ng0UszkodzeniePcb" : return "ngUszkodzonePcb";
                case "Ng0WadaNaklejki": return "ngWadaNaklejki";
                case "Ng0SpalonyConn" : return "ngSpalonyConn";
                case "Ng0Inne" : return "ngInne";
                case "Scrap0WadyLutowia" : return "scrapBrakLutowia";
                case "Scrap0BrakDiodyLed" : return "scrapBrakDiodyLed";
                case "Scrap0BrakResConn": return "scrapBrakResConn";
                case "Scrap0PrzesuniecieDiodyLed" : return "scrapPrzesuniecieLed";
                case "Scrap0PrzesuniecieResConn" : return "scrapPrzesuniecieResConn";
                case "Scrap0ZabrudzonaDiodaLed" : return "scrapZabrudzenieLed";
                case "Scrap0UszkodzenieDiodyLed" : return "scrapUszkodzenieMechaniczneLed";
                case "Scrap0UszkodzenieConn" : return "scrapUszkodzenieConn";
                case "Scrap0WadaFabrycznaLed" : return "scrapWadaFabrycznaDiody";
                case "Scrap0UszkodzeniePcb" : return "scrapUszkodzonePcb";
                case "Scrap0WadaNaklejki" : return "scrapWadaNaklejki";
                case "Scrap0SpalonyConn" : return "scrapSpalonyConn";
                case "Scrap0Inne" : return "scrapInne";
                case "Ng0Test" : return "ngTestElektryczny";
                default: return null;
            }
        }

        
    }
}
