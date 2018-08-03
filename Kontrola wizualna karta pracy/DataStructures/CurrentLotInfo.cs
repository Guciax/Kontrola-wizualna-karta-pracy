using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontrola_wizualna_karta_pracy.DataStructures
{
    class CurrentLotInfo
    {
        public CurrentLotInfo(string lotNo, string model, int orderedQty)
        {
            LotNo = lotNo;
            Model = model;
            OrderedQty = orderedQty;
        }

        public string LotNo { get; }
        public string Model { get; }
        public int OrderedQty { get; }
    }
}
