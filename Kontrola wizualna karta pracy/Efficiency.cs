using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Kontrola_wizualna_karta_pracy.DateOperations;

namespace Kontrola_wizualna_karta_pracy
{
    class Efficiency
    {
        public static void SaveToTextFile(string textLine, string appPath)
        {
            //SaveToTextFile(DateTime.Now.ToString("HH:mm dd-MMM") + ";" + currentLotInfo.Model + ";" + textBoxLotNumber.Text + ";" + textBoxGoodQty.Text + ";" + recordToSaceCalculation.GetAllNg());
            List<string> fileLines = new List<string>();
            List<string> outputLines = new List<string>();
            string recentOrdersPath = Path.Combine(appPath, "recentOrders.txt");
            if (System.IO.File.Exists(recentOrdersPath))
            {
                List<string> file = System.IO.File.ReadAllLines(recentOrdersPath).ToList();
                foreach (var line in file)
                {
                    var splittedLine = line.Split(';');
                    DateTime inspectionDate = DateTime.ParseExact(splittedLine[0], "HH:mm dd-MMM", CultureInfo.CurrentCulture);
                    if ((DateTime.Now-inspectionDate).TotalHours<=24)
                    {
                        outputLines.Add(line);
                    }
                }
            }
            outputLines.Add(textLine);

            System.IO.File.WriteAllLines(recentOrdersPath, outputLines);
           
        }

        public static void LoadRecentOrdersToGrid(DataGridView grid, ref Dictionary<string, string> lotModelDict, string appPath)
        {
            DataTable source = new DataTable();
            source.Columns.Add("Data");
            source.Columns.Add("Model");
            source.Columns.Add("Zlecenie");
            source.Columns.Add("Ilosc");
            source.Columns.Add("NG");
            string recentOrdersPath = Path.Combine(appPath, "recentOrders.txt");

            if (System.IO.File.Exists(recentOrdersPath))
            {
                string[] fileLines = System.IO.File.ReadAllLines(recentOrdersPath);
                Dictionary<string, string> listOfLotsToModel = new Dictionary<string, string>();
                Dictionary<string, string> listOfLotsToDate = new Dictionary<string, string>();

                foreach (var line in fileLines)
                {
                    string[] lineSplitted = line.Split(';');
                    if (lineSplitted.Length < 4) continue;
                    string lot = lineSplitted[2];
                    string model = lineSplitted[1];
                    string date = lineSplitted[0];

                    if (listOfLotsToDate.ContainsKey(lot)) continue;
                    listOfLotsToModel.Add(lot, model);
                    listOfLotsToDate.Add(lot, date);
                }

                if (listOfLotsToModel.Count > 0)
                {
                    var inspectionTable = SqlOperations.DownloadVisInspFromSQL(listOfLotsToModel.Select(key => key.Key).ToArray());
                    //[id],[Data_czas],[Operator],[iloscDobrych],[numerZlecenia],[ngBrakLutowia],[ngBrakDiodyLed],[ngBrakResConn],[ngPrzesuniecieLed],[ngPrzesuniecieResConn]
                    //,[ngZabrudzenieLed],[ngUszkodzenieMechaniczneLed],[ngUszkodzenieConn],[ngWadaFabrycznaDiody],[ngUszkodzonePcb],[ngWadaNaklejki],[ngSpalonyConn]
                    //,[ngInne],[scrapBrakLutowia],[scrapBrakDiodyLed],[scrapBrakResConn],[scrapPrzesuniecieLed],[scrapPrzesuniecieResConn],[scrapZabrudzenieLed]
                    //,[scrapUszkodzenieMechaniczneLed],[scrapUszkodzenieConn],[scrapWadaFabrycznaDiody],[scrapUszkodzonePcb],[scrapWadaNaklejki],[scrapSpalonyConn]
                    //,[scrapInne],[ngTestElektryczny]

                    foreach (DataRow row in inspectionTable.Rows)
                    {
                        string lot = row["numerZlecenia"].ToString();
                        string model = listOfLotsToModel[lot];
                        DateTime date = DateTime.ParseExact(row["Data_czas"].ToString(), "dd.MM.yyyy HH:mm:ss", CultureInfo.CurrentCulture);

                        if (model.Trim().Length == 0 || model == "Model") continue;
                        if (!lotModelDict.ContainsKey(lot))
                        {
                            lotModelDict.Add(lot, model);
                        }

                        int scrapNg = 0;
                        for (int i = 5; i < inspectionTable.Columns.Count; i++)
                        {
                            if (row[i].ToString() != "0")
                            {
                                scrapNg += int.Parse(row[i].ToString());
                            }
                        }

                        int goodQty = Int32.Parse(row["iloscDobrych"].ToString()) + scrapNg;

                        DataRow newRow = source.NewRow();
                        newRow[0] = date.ToString("HH:mm dd-MMM");
                        newRow[1] = model;
                        newRow[2] = lot;
                        newRow[3] = goodQty;
                        newRow[4] = scrapNg;

                        source.Rows.InsertAt(newRow, 0);

                    }
                }
                //foreach (var line in fileLines)
                //{
                //    string[] lineSplitted = line.Split(';');
                //    if (lineSplitted.Length < 4) continue;

                //    string date = lineSplitted[0];
                //    string model = lineSplitted[1];
                //    string zlecenie = lineSplitted[2];

                //    if (model.Trim().Length == 0 || model == "Model") continue;
                //    if (!lotModelDict.ContainsKey(zlecenie))
                //    {
                //        lotModelDict.Add(zlecenie, model);
                //    }

                //    string ilosc = lineSplitted[3];
                //    string ng = lineSplitted[4];
                //    DataRow newRow = source.NewRow();
                //    newRow[0] = date;
                //    newRow[1] = model;
                //    newRow[2] = zlecenie;
                //    newRow[3] = ilosc;
                //    newRow[4] = ng;

                //    source.Rows.InsertAt(newRow, 0);
                //}

                grid.DataSource = source;
                DgvTools.ColumnsAutoSize(grid, DataGridViewAutoSizeColumnMode.AllCellsExceptHeader);
            }
            
        }

        public static double CalculateWasteLastXXh(DataGridView grid,int givenTime)
        {
            double ng = 0;
            double total = 0;
            foreach (DataGridViewRow row in grid.Rows)
            {
                DateTime inspectionTime = DateTime.ParseExact(row.Cells["Data"].Value.ToString(),"HH:mm dd-MMM", CultureInfo.CurrentCulture);
                if ((DateTime.Now-inspectionTime).TotalHours> givenTime) continue;
                Debug.WriteLine("LOT:" + row.Cells["Zlecenie"].Value.ToString() +" "+ row.Cells["Ilosc"].Value.ToString() + " NG:" + row.Cells["NG"].Value.ToString());
                ng += double.Parse(row.Cells["NG"].Value.ToString());
                total += double.Parse(row.Cells["Ilosc"].Value.ToString());
            }
            return total > 0 ? Math.Round(ng / total * 100, 2) : 0;
        }

        public static int CalculateQuantityThisShift(DataGridView grid)
        {
            int qty = 0;
            foreach (DataGridViewRow row in grid.Rows)
            {
                DateTime inspectionTime = DateTime.ParseExact(row.Cells["Data"].Value.ToString(), "HH:mm dd-MMM", CultureInfo.CurrentCulture);
                if (TimeTools.whatDayShiftIsit(inspectionTime).shiftStartDate != TimeTools.whatDayShiftIsit(DateTime.Now).shiftStartDate || TimeTools.whatDayShiftIsit(inspectionTime).shiftNumber != TimeTools.whatDayShiftIsit(DateTime.Now).shiftNumber) continue;
                qty += int.Parse(row.Cells["Ilosc"].Value.ToString());
            }
            return qty;
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
