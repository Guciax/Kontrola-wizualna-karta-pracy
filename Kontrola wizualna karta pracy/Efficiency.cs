using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Kontrola_wizualna_karta_pracy.DateOperations;

namespace Kontrola_wizualna_karta_pracy
{
    class Efficiency
    {
        public static void SaveToTetFile(string textLine)
        {
            List<string> file = System.IO.File.ReadAllLines("recentOrders.txt").ToList();
            int startline = file.Count - 10;
            if (startline < 0) startline = 0;
            List<string> outputFile = new List<string>();
            for (int i = startline; i < file.Count; i++) 
            {
                outputFile.Add(file[startline]);
            }
            outputFile.Add(textLine);

            System.IO.File.AppendAllText("recentOrders.txt", textLine + Environment.NewLine);
        }

        public static void AddRecentOrdersToGrid(DataGridView grid, ref Dictionary<string, string> lotModelDict)
        {
            DataTable source = new DataTable();
            source.Columns.Add("Data");
            source.Columns.Add("Model");
            source.Columns.Add("Zlecenie");
            source.Columns.Add("Ilosc");
            source.Columns.Add("NG");

            string[] fileLines = System.IO.File.ReadAllLines("recentOrders.txt");

            foreach (var line in fileLines)
            {
                string[] lineSplitted = line.Split(';');
                if (lineSplitted.Length < 4) continue;

                string date = lineSplitted[0];
                string model = lineSplitted[1];
                string zlecenie = lineSplitted[2];

                if (model.Trim().Length == 0 || model == "Model") continue;
                if(!lotModelDict.ContainsKey(zlecenie))
                {
                    lotModelDict.Add(zlecenie, model);
                }
                
                string ilosc = lineSplitted[3];
                string ng = lineSplitted[4];
                DataRow newRow = source.NewRow();
                newRow[0] = date;
                newRow[1] = model;
                newRow[2] = zlecenie;
                newRow[3] = ilosc;
                newRow[4] = ng;

                source.Rows.InsertAt(newRow, 0);
            }

            grid.DataSource = source;

            DgvTools.ColumnsAutoSize(grid, DataGridViewAutoSizeColumnMode.AllCellsExceptHeader);
        }

        public static double CalculateWasteThisShift(DataGridView grid)
        {
            double ng = 0;
            double total = 0;
            foreach (DataGridViewRow row in grid.Rows)
            {
                DateTime inspectionTime = DateTime.ParseExact(row.Cells["Data"].Value.ToString(),"HH:mm dd-MMM", CultureInfo.CurrentCulture);
                if (TimeTools.whatDayShiftIsit(inspectionTime).fixedDate != TimeTools.whatDayShiftIsit(DateTime.Now).fixedDate || TimeTools.whatDayShiftIsit(inspectionTime).shift != TimeTools.whatDayShiftIsit(DateTime.Now).shift) continue;
                ng += double.Parse(row.Cells["NG"].Value.ToString());
                total += double.Parse(row.Cells["Ilosc"].Value.ToString());
            }

            return total>0?  Math.Round(ng / total * 100, 2): 0;
        }

        public static double CalculateEfficiencyThisShift(DataGridView grid, bool polish)
        {
            int qty = Efficiency.HowManyInspectedThisShift(grid);
            return Math.Round((double)qty / (double)LanguangeTranslation.NormPerShift(polish) * 100, 2);
        }

        public static int HowManyInspectedThisShift(DataGridView grid)
        {
            int result = 0;
            foreach (DataGridViewRow row in grid.Rows)
            {
                DateTime inspTime = DateTime.ParseExact(row.Cells["Data"].Value.ToString(), "HH:mm dd-MMM", CultureInfo.CurrentCulture);
                int qty = 0;
                if (!int.TryParse(row.Cells["Ilosc"].Value.ToString(), out qty))
                {
                    qty = 0;
                }

                dateShiftNo rowShift = DateOperations.whatDayShiftIsit(inspTime);
                dateShiftNo thisShift = DateOperations.whatDayShiftIsit(DateTime.Now);

                if (rowShift.shiftStartDate == thisShift.shiftStartDate & rowShift.shift == thisShift.shift)
                {
                    result += qty;
                }
            }
            return result;
        }
    }
}
