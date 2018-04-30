using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontrola_wizualna_karta_pracy
{
    class EfficiencyTools
    {
        public static DataTable RecentJob(double lastLotQty = 0, string lastLotNo="", string oper="")
        {
            DataTable result = new DataTable();
            string fileName = "RecentJobs.txt";

            if (!File.Exists(fileName))
                    {
                File.Create(fileName);
            }

            List<string> textFileLines = File.ReadAllLines(fileName).ToList();
            if (lastLotQty > 0)
            {
                textFileLines.Add(DateTime.Now.ToString("dd-MM-yy HH:mm" + ";" + oper + ";" + lastLotNo + ":" + lastLotQty));
            }

            result.Columns.Add("Data");
            result.Columns.Add("Operator");
            result.Columns.Add("LOT");
            result.Columns.Add("Ilość");

            List<string> toRemove = new List<string>();

            for (int i=0;i<textFileLines.Count;i++)
            {
                string[] line = textFileLines[i].Split(';');
                result.Rows.Add(line);

                DateTime date = DateTime.ParseExact(line[0], "dd-MM-yy HH:mm", CultureInfo.InvariantCulture);
                if ((DateTime.Now - date).TotalHours > 24) 
                {
                    toRemove.Add(textFileLines[i]);
                }
            }

            textFileLines.RemoveAll(t => toRemove.Contains(t));
            File.WriteAllLines(fileName, textFileLines.ToArray());

            return result;
        }
    }
}
