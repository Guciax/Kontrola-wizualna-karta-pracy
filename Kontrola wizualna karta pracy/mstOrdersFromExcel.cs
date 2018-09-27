using Kontrola_wizualna_karta_pracy.DataStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kontrola_wizualna_karta_pracy
{
    class mstOrdersFromExcel
    {
        public struct mstOrders
        {
            public string nc12;
            public string order;
            public string quantity;
            public DateTime endDate;
        }

        public static void loadExcel(ref Dictionary<string,SmtInfo > smtInfo)
        {
            List<mstOrders> result = new List<mstOrders>();
            string FilePath = @"Y:\Manufacturing_Center\Manufacturing HID EM\weinne\woto\elektronika\ZLECENIA MST\2018\zlecenia MST.xlsx";

            if (File.Exists(FilePath))
            {
                var fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                var pck = new OfficeOpenXml.ExcelPackage();
                try
                {
                    pck = new OfficeOpenXml.ExcelPackage(fs);
                }
                catch (Exception e) { MessageBox.Show(e.Message); }

                if (pck.Workbook.Worksheets.Count != 0)
                {
                    foreach (OfficeOpenXml.ExcelWorksheet worksheet in pck.Workbook.Worksheets)
                    {
                        if (worksheet.Dimension == null) continue;
                        int orderColIndex = 0;
                        int nc12ColIndex = 0;
                        int qtyColIndex = 0;
                        int firstRowData = 0;
                        int dateIndex = 0;

                        for (int row = 1; row < 11; row++)
                        {
                            for (int col = 1; col < worksheet.Dimension.End.Column; col++)
                            {
                                if (worksheet.Cells[row, col].Value != null)
                                {
                                    if (worksheet.Cells[row, col].Value.ToString().Trim().ToUpper().Replace(" ", "") == "NRZLECENIA")
                                    {
                                        orderColIndex = col;
                                    }
                                    if (worksheet.Cells[row, col].Value.ToString().Trim().ToUpper().Replace(" ", "") == "12NC")
                                    {
                                        nc12ColIndex = col;
                                    }
                                    if (worksheet.Cells[row, col].Value.ToString().Trim().ToUpper().Replace(" ", "") == "ILOŚĆ")
                                    {
                                        qtyColIndex = col;
                                    }

                                    if (worksheet.Cells[row, col].Value.ToString().Trim().ToUpper().Replace(" ", "") == "DATAWEJŚCIA")
                                    {
                                        dateIndex = col;
                                    }

                                }
                            }
                            if (orderColIndex > 0)
                            {
                                firstRowData = row + 1;
                                break;
                            }
                        }
                        // Debug.WriteLine("przes: " + endOrderIndex);

                        for (int row = firstRowData; row < worksheet.Dimension.End.Row; row++)
                        {
                            if (worksheet.Cells[row, nc12ColIndex].Value != null)
                            {
                                if (worksheet.Cells[row, dateIndex].Value == null) continue;
                                string nc12 = worksheet.Cells[row, nc12ColIndex].Value.ToString().Replace(" ", "").Trim();
                                string orderNo = worksheet.Cells[row, orderColIndex].Value.ToString().Replace(" ", "").Trim();
                                string qty = worksheet.Cells[row, qtyColIndex].Value.ToString().Replace(" ", "").Trim();

                                mstOrders newItem = new mstOrders();
                                newItem.order = orderNo;
                                newItem.nc12 = nc12;
                                newItem.quantity = qty;

                                if (dateIndex > 0)
                                {
                                    DateTime endDate = new DateTime(0001, 01, 01);
                                    DateTime.TryParse(fixDateStringFormat(worksheet.Cells[row, dateIndex].Value.ToString().Replace(" ", "").Trim().Replace(".", "-")), out endDate);
                                    newItem.endDate = endDate;
                                    //Debug.WriteLine(endDate.ToShortDateString());
                                }

                                result.Add(newItem);
                            }
                        }
                    }
                }
            }

            foreach (var item in result)
            {
                if (smtInfo.ContainsKey(item.order)) continue;
                int qty = 0;
                int.TryParse(item.quantity, out qty);
                smtInfo.Add(item.order, new SmtInfo("", item.endDate.ToString("dd.MM.yyyy"), qty, item.nc12, true));
            }

            //return result;
        }

        public static string fixDateStringFormat(string inputDate)
        {
            string result = "";
            try
            {
                string onlyDate = inputDate.Substring(0, 10);
                Char splitChar;
                if (onlyDate.Contains("."))
                {
                    splitChar = '.';
                }
                else
                {
                    splitChar = '-';
                }
                string[] splittedDate = onlyDate.Split(splitChar);
                if (splittedDate[0].Length == 4)
                {
                    result = splittedDate[2] + "-" + splittedDate[1] + "-" + splittedDate[0];
                }
                else
                {
                    result = splittedDate[0] + "-" + splittedDate[1] + "-" + splittedDate[2];
                }
                //Debug.WriteLine("fixed Date: " + result);
            }
            catch { return inputDate; }
            return result;
        }
    }
}
