using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontrola_wizualna_karta_pracy
{
    class LanguangeTranslation
    {
        public static string Translate(string input, bool langPolish)
        {
            string result = "";

            if (langPolish)
            {
                switch (input)
                {
                    case "labelSolder": return "Wady lutowia";
                    case "labelMissingLed": return "Brak LED/polaryzacja/podniesiona";
                    case "labelMissingRes": return "Brak RES/CONN";
                    case "labelShiftedLed": return "Przesunięcie diody LED";
                    case "labelShiftedRes": return "Przesunięcie RES/CONN";
                    case "labelDirtyLed": return "Zabrudzona dioda LED";
                    case "labelDmgLed": return "Uszkodzenie mechaniczne LED";
                    case "labelDmgConn": return "Uszkodzenie mechaniczne CONN";
                    case "labelBadLed": return "Wada fabryczna diody LED";
                    case "labelDmgPcb": return "Uszkodzenie PCB";
                    case "labelLabel": return "Wada naklejki";
                    case "labelBurnedConn": return "Spalony CONN";
                    case "labelOther": return "Inne";
                    case "labelElecTest": return "Test elektryczny";
                    case "labelLotNo": return "Numer zlecenia";
                    case "labelOperator": return "Operator";
                    case "labelGoodQty": return "Ilość dobrych";
                    case "buttonSave": return "Zapisz";


                default: return null;
                }
            }
            else
            {

            }

            return result;
        }
    }
}
