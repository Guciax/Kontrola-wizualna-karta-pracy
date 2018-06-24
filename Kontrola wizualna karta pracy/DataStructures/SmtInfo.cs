using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontrola_wizualna_karta_pracy.DataStructures
{
    public class SmtInfo
    {
        public SmtInfo(string smtLine, string completitionDate)
        {
            SmtLine = smtLine;
            CompletitionDate = completitionDate;
        }

        public string SmtLine { get; }
        public string CompletitionDate { get; }
    }
}
