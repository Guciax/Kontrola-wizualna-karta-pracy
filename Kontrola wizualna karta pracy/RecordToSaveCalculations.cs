using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kontrola_wizualna_karta_pracy
{
    class RecordToSaveCalculations
    {
        public RecordToSaveCalculations(RecordToSave recordToSave)
        {
            RecordToSave = recordToSave;
        }

        public RecordToSave RecordToSave { get; }

        public int GetAllNg()
        {
            int result = 0;

            PropertyInfo[] properties = typeof(RecordToSave).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.Name.ToLower().Contains("ng") || property.Name.ToLower().Contains("scrap"))
                {
                    result += (int)property.GetValue(RecordToSave);
                }
            }

            return result;
        }

        public static int GetAllNg2(RecordToSave RecordToSave)
        {
            int result = 0;

            PropertyInfo[] properties = typeof(RecordToSave).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.Name.ToLower().Contains("ng") || property.Name.ToLower().Contains("scrap"))
                {
                    result += (int)property.GetValue(RecordToSave);
                }
            }

            return result;
        }
    }
}
