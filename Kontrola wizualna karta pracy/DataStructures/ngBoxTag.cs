using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontrola_wizualna_karta_pracy
{
    public class ngBoxTag
    {
        public ngBoxTag(string serialNo, string dateTimeString, List<Image> images)
        {
            SerialNo = serialNo;
            DateTimeString = dateTimeString;
            Images = images;
        }

        public string SerialNo { get; }
        public string DateTimeString { get; }
        public List<Image> Images { get; }
    }
}
