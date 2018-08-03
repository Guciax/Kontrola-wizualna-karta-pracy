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
            switch (colName.ToLower())
            {
                case "ngbraklutowia": return "Brak Lutowia";
                case "ngbrakdiodyled": return "Brak LED";
                case "ngbrakresconn": return "Brak RES/CONN";
                case "ngprzesuniecieled": return "Przesunięcie LED";
                case "ngprzesuniecieresconn": return "Przesunięcie CONN";
                case "ngzabrudzenieled": return "Zabrudzenie LED";
                case "nguszkodzeniemechaniczneled": return "Uszkodzenie mech. LED";
                case "nguszkodzenieconn": return "Uszkodzenie CONN";
                case "ngwadafabrycznadiody": return "Wada fabryczna LED";
                case "nguszkodzonepcb": return "Uszkodzone PCB";
                case "ngwadanaklejki": return "Wada naklejki";
                case "ngspalonyconn": return "Spalony CONN";
                case "nginne": return "Inne";
                case "scrapbraklutowia": return "Brak Lutowia";
                case "scrapbrakdiodyled": return "Brak LED";
                case "scrapbrakresconn": return "Brak RES/CONN";
                case "scrapprzesuniecieled": return "Przesunięcie LED";
                case "scrapprzesuniecieresconn": return "Przesunięcie CONN";
                case "scrapzabrudzenieled": return "Zabrudzenie LED";
                case "scrapuszkodzeniemechaniczneled": return "Uszkodzenie mech. LED";
                case "scrapuszkodzenieconn": return "Uszkodzenie CONN";
                case "scrapwadafabrycznadiody": return "Wada fabryczna LED";
                case "scrapuszkodzonepcb": return "Uszkodzone PCB";
                case "scrapwadanaklejki": return "Wada naklejki";
                case "scrapspalonyconn": return "Spalony CONN";
                case "scrapinne": return "Inne";
                case "ngtestelektryczny": return "Test elektryczny";
                default: return colName;
            }
        }

        
    }
}
