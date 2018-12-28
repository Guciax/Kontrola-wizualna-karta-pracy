using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontrola_wizualna_karta_pracy.DataStructures
{
    public class imageFailureTag
    {
        public imageFailureTag(string serial, string ngType, string index, string result)
        {
            Serial = serial;
            NgType = ngType;
            Index = index;
            Result = result;
        }

        public string Serial { get; }
        public string NgType { get; }
        public string Index { get; }
        public string Result { get; }
    }
}
