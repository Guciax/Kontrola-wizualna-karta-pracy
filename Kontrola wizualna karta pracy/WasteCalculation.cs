using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kontrola_wizualna_karta_pracy
{
    class WasteCalculation
    {
        public static List<WasteDataStructure> LoadData(DataTable inputTable, Dictionary<string, string> lotModelDictionary)
        {
            List<WasteDataStructure> result = new List<WasteDataStructure>();

            foreach (DataRow row in inputTable.Rows)
            {
                int id;
                DateTime realDateTime;
                int shiftNumber;
                string oper;
                int goodQty;
                int allQty = 0;
                string numerZlecenia;
                int allNg = 0;
                int allScrap = 0;

                Dictionary<string, int> wastePerReason = new Dictionary<string, int>();

                id = int.Parse(row["Id"].ToString());
                realDateTime = TimeTools.ParseExact(row["Data_czas"].ToString());

                shiftNumber = TimeTools.whatDayShiftIsit(realDateTime).shiftNumber;
                oper = row["Operator"].ToString();
                goodQty = int.Parse(row["iloscDobrych"].ToString());
                allQty = goodQty;
                string model = "???";


                Regex rgx = new Regex("[^a-zA-Z0-9 -]");
                string digitsLot = rgx.Replace(row["numerZlecenia"].ToString(), "");
                numerZlecenia = digitsLot.Trim();
                lotModelDictionary.TryGetValue(numerZlecenia, out model);
                
                for (int c = 0; c < inputTable.Columns.Count; c++)
                {
                    string wasteReason = inputTable.Columns[c].ColumnName;
                    if (wasteReason.Contains("ng"))
                    {
                        if (!wastePerReason.ContainsKey(wasteReason))
                        {
                            wastePerReason.Add(wasteReason, 0);
                        }

                        int wasteQty = int.Parse(row[c].ToString());
                        wastePerReason[wasteReason] = wasteQty;
                        allNg += wasteQty;

                    }
                    else if (wasteReason.Contains("scrap"))
                    {
                        if (!wastePerReason.ContainsKey(wasteReason))
                        {
                            wastePerReason.Add(wasteReason, 0);
                        }

                        int wasteQty = int.Parse(row[c].ToString());
                        wastePerReason[wasteReason] = wasteQty;
                        allScrap += wasteQty;
                    }
                }


                WasteDataStructure recordToAdd = new WasteDataStructure(id,  realDateTime, shiftNumber, oper, goodQty, allQty, allNg, allScrap, numerZlecenia, model, wastePerReason);
                result.Add(recordToAdd);
            }

            return result;
        }

        
    }
}
