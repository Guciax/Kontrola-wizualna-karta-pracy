using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontrola_wizualna_karta_pracy.DataStructures
{
    public class SmtInfo
    {
        public SmtInfo(string smtLine, string completitionDate, int orderedQty, string model, bool polishLang)
        {
            SmtLine = smtLine;
            CompletitionDate = completitionDate;
            OrderedQty = orderedQty;
            Model = model;
            PolishLang = polishLang;
        }

        public string SmtLine { get; }
        public string CompletitionDate { get; }
        public int OrderedQty { get; }
        public string Model { get; }
        public bool PolishLang { get; set; }

        public string infoToDisplay
        {
            get
            {
                string connQtyString = Form1.GetNumberOfConnectors(Model);
                return LanguangeTranslation.Translate("Model", PolishLang) + ": " + Environment.NewLine + Model + Environment.NewLine + Environment.NewLine
                        + LanguangeTranslation.Translate("Produkcja", PolishLang) + ": " + Environment.NewLine + CompletitionDate + " " + SmtLine + Environment.NewLine
                        + Environment.NewLine + LanguangeTranslation.Translate("Ilość złączek", PolishLang) + ": " + connQtyString;
            }
        }
    }
}
