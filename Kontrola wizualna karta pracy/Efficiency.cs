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
            System.IO.File.AppendAllText("recentOrders.txt", textLine + Environment.NewLine);
        }

        public static void AddRecentOrdersToGrid(DataGridView grid)
        {
            DataTable source = new DataTable();
            source.Columns.Add("Data");
            source.Columns.Add("Model");
            source.Columns.Add("Zlecenie");
            source.Columns.Add("Ilosc");
            string[] fileLines = System.IO.File.ReadAllLines("recentOrders.txt");

            foreach (var line in fileLines)
            {
                string[] lineSplitted = line.Split(';');
                string date = lineSplitted[0];
                string model = lineSplitted[1];
                string zlecenie = lineSplitted[2];
                string ilosc = lineSplitted[3];
                source.Rows.Add(date, model, zlecenie, ilosc);
            }

            grid.DataSource = source;

            foreach (DataGridViewColumn col in grid.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        public static double CalculateWasteThisShift(DataGridView grid, bool polish)
        {
            int qty = Efficiency.HowManyInspectedThisShift(grid);
            return Math.Round((double)qty / (double)LanguangeTranslation.NormPerShift(polish) * 100, 2);
        }

        public static int HowManyInspectedThisShift(DataGridView grid)
        {
            int result = 0;
            foreach (DataGridViewRow row in grid.Rows)
            {
                DateTime inspTime = DateTime.ParseExact(row.Cells["Data"].Value.ToString(), "dd-MM-yyyy", CultureInfo.CurrentCulture);
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
