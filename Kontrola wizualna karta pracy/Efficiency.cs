using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kontrola_wizualna_karta_pracy
{
    class Efficiency
    {
        public static void SaveToTetFile(string textLine)
        {
            System.IO.File.AppendAllText("recentOrders.txt", textLine+Environment.NewLine);
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
    }
}
