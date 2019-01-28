using Kontrola_wizualna_karta_pracy.DataStructures;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontrola_wizualna_karta_pracy
{
    class SqlOperations
    {
        public static void SaveRecordToVisualInspectionDb(RecordToSave saveData)
        {
            using (SqlConnection openCon = new SqlConnection(@"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;"))
            {
                string save = "INSERT into tb_Kontrola_Wizualna_Karta_Pracy (Data_czas,Operator,iloscDobrych,numerZlecenia,ngBrakLutowia,ngBrakDiodyLed,ngBrakResConn,ngPrzesuniecieLed,ngPrzesuniecieResConn,ngZabrudzenieLed,ngUszkodzenieMechaniczneLed,ngUszkodzenieConn,ngWadaFabrycznaDiody,ngUszkodzonePcb,ngWadaNaklejki,ngSpalonyConn,ngInne,scrapBrakLutowia,scrapBrakDiodyLed,scrapBrakResConn,scrapPrzesuniecieLed,scrapPrzesuniecieResConn,scrapZabrudzenieLed,scrapUszkodzenieMechaniczneLed,scrapUszkodzenieConn,scrapWadaFabrycznaDiody,scrapUszkodzonePcb,scrapWadaNaklejki,scrapSpalonyConn,scrapInne,ngTestElektryczny) VALUES (@Data_czas,@Operator,@iloscDobrych,@numerZlecenia,@ngBrakLutowia,@ngBrakDiodyLed,@ngBrakResConn,@ngPrzesuniecieLed,@ngPrzesuniecieResConn,@ngZabrudzenieLed,@ngUszkodzenieMechaniczneLed,@ngUszkodzenieConn,@ngWadaFabrycznaDiody,@ngUszkodzonePcb,@ngWadaNaklejki,@ngSpalonyConn,@ngInne,@scrapBrakLutowia,@scrapBrakDiodyLed,@scrapBrakResConn,@scrapPrzesuniecieLed,@scrapPrzesuniecieResConn,@scrapZabrudzenieLed,@scrapUszkodzenieMechaniczneLed,@scrapUszkodzenieConn,@scrapWadaFabrycznaDiody,@scrapUszkodzonePcb,@scrapWadaNaklejki,@scrapSpalonyConn,@scrapInne,@ngTestElektryczny)";
                using (SqlCommand querySave = new SqlCommand(save))
                {
                    querySave.Connection = openCon;
                    querySave.Parameters.Add("@Data_czas", SqlDbType.SmallDateTime).Value = saveData.Data_Czas;
                    querySave.Parameters.Add("@Operator", SqlDbType.NVarChar).Value = saveData.Operator;
                    querySave.Parameters.Add("@iloscDobrych", SqlDbType.Int).Value = saveData.IloscDobrych;
                    querySave.Parameters.Add("@numerZlecenia", SqlDbType.NVarChar).Value = saveData.NumerZlecenia;
                    querySave.Parameters.Add("@ngBrakLutowia", SqlDbType.Int).Value = saveData.NgBrakLutowia;
                    querySave.Parameters.Add("@ngBrakDiodyLed", SqlDbType.Int).Value = saveData.NgBrakDiodyLed;
                    querySave.Parameters.Add("@NgBrakResConn", SqlDbType.Int).Value = saveData.NgBrakResConn;
                    querySave.Parameters.Add("@NgPrzesuniecieLed", SqlDbType.Int).Value = saveData.NgPrzesuniecieLed;
                    querySave.Parameters.Add("@NgPrzesuniecieResConn", SqlDbType.Int).Value = saveData.NgPrzesuniecieResConn;
                    querySave.Parameters.Add("@NgZabrudzenieLed", SqlDbType.Int).Value = saveData.NgZabrudzenieLed;
                    querySave.Parameters.Add("@NgUszkodzenieMechaniczneLed", SqlDbType.Int).Value = saveData.NgUszkodzenieMechaniczneLed;
                    querySave.Parameters.Add("@NgUszkodzenieConn", SqlDbType.Int).Value = saveData.NgUszkodzenieConn;
                    querySave.Parameters.Add("@NgWadaFabrycznaDiody", SqlDbType.Int).Value = saveData.NgWadaFabrycznaDiody;
                    querySave.Parameters.Add("@NgUszkodzonePcb", SqlDbType.Int).Value = saveData.NgUszkodzonePcb;
                    querySave.Parameters.Add("@NgWadaNaklejki", SqlDbType.Int).Value = saveData.NgWadaNaklejki;
                    querySave.Parameters.Add("@NgSpalonyConn", SqlDbType.Int).Value = saveData.NgSpalonyConn;
                    querySave.Parameters.Add("@NgInne", SqlDbType.Int).Value = saveData.NgInne;
                    querySave.Parameters.Add("@ScrapBrakLutowia", SqlDbType.Int).Value = saveData.ScrapBrakLutowia;
                    querySave.Parameters.Add("@ScrapBrakDiodyLed", SqlDbType.Int).Value = saveData.ScrapBrakDiodyLed;
                    querySave.Parameters.Add("@ScrapBrakResConn", SqlDbType.Int).Value = saveData.ScrapBrakResConn;
                    querySave.Parameters.Add("@ScrapPrzesuniecieLed", SqlDbType.Int).Value = saveData.ScrapPrzesuniecieLed;
                    querySave.Parameters.Add("@ScrapPrzesuniecieResConn", SqlDbType.Int).Value = saveData.ScrapPrzesuniecieResConn;
                    querySave.Parameters.Add("@ScrapZabrudzenieLed", SqlDbType.Int).Value = saveData.ScrapZabrudzenieLed;
                    querySave.Parameters.Add("@ScrapUszkodzenieMechaniczneLed", SqlDbType.Int).Value = saveData.ScrapUszkodzenieMechaniczneLed;
                    querySave.Parameters.Add("@ScrapUszkodzenieConn", SqlDbType.Int).Value = saveData.ScrapUszkodzenieConn;
                    querySave.Parameters.Add("@ScrapWadaFabrycznaDiody", SqlDbType.Int).Value = saveData.ScrapWadaFabrycznaDiody;
                    querySave.Parameters.Add("@ScrapUszkodzonePcb", SqlDbType.Int).Value = saveData.ScrapUszkodzonePcb;
                    querySave.Parameters.Add("@ScrapWadaNaklejki", SqlDbType.Int).Value = saveData.ScrapWadaNaklejki;
                    querySave.Parameters.Add("@ScrapSpalonyConn", SqlDbType.Int).Value = saveData.ScrapSpalonyConn;
                    querySave.Parameters.Add("@ScrapInne", SqlDbType.Int).Value = saveData.ScrapInne;
                    querySave.Parameters.Add("@NgTestElektryczny", SqlDbType.Int).Value = saveData.NgTestElektryczny;
                    openCon.Open();
                    querySave.ExecuteNonQuery();
                }
            }
        }

        public static List<string> RecentOperatorsList(int daysAgo)
        {
            DateTime tillDate = System.DateTime.Now.AddDays(daysAgo * (-1));
            HashSet<string> result = new HashSet<string>();
            DataTable tabletoFill = new DataTable();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;";

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = @"SELECT Data_czas,Operator FROM tb_Kontrola_Wizualna_Karta_Pracy where Data_czas>@dataCzas;";
            //@"SELECT Data_czas,Operator,iloscDobrych,numerZlecenia,ngBrakLutowia,ngBrakDiodyLed,ngBrakResConn,ngPrzesuniecieLed,ngPrzesuniecieResConn,ngZabrudzenieLed,ngUszkodzenieMechaniczneLed,ngUszkodzenieConn,ngWadaFabrycznaDiody,ngUszkodzonePcb,ngWadaNaklejki,ngSpalonyConn,ngInne,scrapBrakLutowia,scrapBrakDiodyLed,scrapBrakResConn,scrapPrzesuniecieLed,scrapPrzesuniecieResConn,scrapZabrudzenieLed,scrapUszkodzenieMechaniczneLed,scrapUszkodzenieConn,scrapWadaFabrycznaDiody,scrapUszkodzonePcb,scrapWadaNaklejki,scrapSpalonyConn,scrapInne,ngTestElektryczny FROM tb_Kontrola_Wizualna_Karta_Pracy WHERE Data_czas > '" + DateTime.Now.AddDays(-90).ToShortDateString() + "';";
            command.Parameters.AddWithValue("@dataCzas", tillDate);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            //try
            {
                adapter.Fill(tabletoFill);
            }
            // catch (Exception e)
            HashSet<string> tempHash = new HashSet<string>();
            foreach (DataRow row in tabletoFill.Rows)
            {
                tempHash.Add(row["Operator"].ToString());
            }

            return tempHash.OrderBy(op=>op).ToList();
        }

        internal static void UpdateNgAfterRework(string serial, string result, string viOperator)
        {
            string connectionString = @"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE MES.dbo.tb_NG_tracking SET post_rework_vi_result=@result ,vi_Operator=@vi_Operator WHERE serial_no = @serial";

                    command.Parameters.AddWithValue("@result", result);
                    command.Parameters.AddWithValue("@serial", serial);
                    command.Parameters.AddWithValue("@vi_Operator", viOperator);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public static Dictionary<string, List<string>> HowManyModulesTested(string lotId)
        {
            DataTable sqlTable = new DataTable();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;";

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText =
                @"SELECT serial_no,result,wip_entity_name FROM tb_tester_measurements WHERE wip_entity_name=@lotId AND result<>'NGV' ORDER BY inspection_time DESC;";
            command.Parameters.AddWithValue("@lotId", lotId);

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(sqlTable);

            Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();
            result.Add("OK", new List<string>());
            result.Add("NG", new List<string>());

            
            foreach (DataRow row in sqlTable.Rows)
            {
                string serial = row["serial_no"].ToString();
                string testResult = row["result"].ToString();

                if (testResult != "OK" & testResult != "NG") continue;
                if (result["OK"].Contains(serial) || result["NG"].Contains(serial)) continue;
                result[testResult].Add(serial);
            }

            return result;
        }

        public static Dictionary<string,CurrentLotInfo> LotToModelDictionaryFromMesZlecProd()
        {
            Dictionary<string, CurrentLotInfo> result = new Dictionary<string, CurrentLotInfo>();

            DataTable sqlTable = new DataTable();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;";

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText =
                @"SELECT Nr_Zlecenia_Produkcyjnego,NC12_wyrobu,Ilosc_wyrobu_zlecona FROM tb_Zlecenia_produkcyjne;";

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(sqlTable);

            foreach (DataRow row in sqlTable.Rows)
            {
                if (result.ContainsKey(row["Nr_Zlecenia_Produkcyjnego"].ToString())) continue;
                string lotId = row["Nr_Zlecenia_Produkcyjnego"].ToString();
                string model = row["NC12_wyrobu"].ToString().Replace("LLFML", "");
                int orderedQty = int.Parse( row["Ilosc_wyrobu_zlecona"].ToString());

                CurrentLotInfo nfo = new CurrentLotInfo(lotId, model, orderedQty);
                result.Add(lotId, nfo);
            }
            return result;
        }

        public static CurrentLotInfo LotNoToModelIdFromMes(string lotNo)
        {

            DataTable sqlTable = new DataTable();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;";

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText =
                @"SELECT Nr_Zlecenia_Produkcyjnego,NC12_wyrobu,Ilosc_wyrobu_zlecona FROM tb_Zlecenia_produkcyjne WHERE Nr_Zlecenia_Produkcyjnego=@lot;";
            command.Parameters.AddWithValue("@lot", lotNo);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(sqlTable);

            string model = "";
            int qty = 0;
            if (sqlTable.Rows.Count>0)
            {
                model = sqlTable.Rows[0]["NC12_wyrobu"].ToString();
                qty = int.Parse(sqlTable.Rows[0]["Ilosc_wyrobu_zlecona"].ToString());
            }

            return new CurrentLotInfo(lotNo, model, qty);
        }

        public static Dictionary<string, SmtInfo> GetSmtInfo()
        {
            Dictionary<string, SmtInfo> result = new Dictionary<string, SmtInfo>();

            DataTable sqlTable = new DataTable();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;";

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText =
                @"SELECT DataCzasKoniec,LiniaSMT,NrZlecenia,IloscWykonana,Model FROM tb_SMT_Karta_Pracy;";

            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(sqlTable);

            foreach (DataRow row in sqlTable.Rows)
            {
                if (result.ContainsKey(row["NrZlecenia"].ToString())) continue;
                string completitionDate = row["DataCzasKoniec"].ToString();
                string smtLine = row["LiniaSMT"].ToString();
                string model = row["Model"].ToString();
                int qty = 0;
                if (!int.TryParse(row["IloscWykonana"].ToString(), out qty))
                {
                    qty = 0;
                }

                SmtInfo newItem = new SmtInfo(smtLine, completitionDate, qty, model, true);
                result.Add(row["NrZlecenia"].ToString(), newItem );
            }
            return result;
        }

        public static DataTable DownloadVisInspFromSQL(string[] lots)
        {
            DataTable tabletoFill = new DataTable();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;";

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = @"SELECT Id,Data_czas,Operator,iloscDobrych,numerZlecenia,ngBrakLutowia,ngBrakDiodyLed,ngBrakResConn,ngPrzesuniecieLed,ngPrzesuniecieResConn,ngZabrudzenieLed,ngUszkodzenieMechaniczneLed,ngUszkodzenieConn,ngWadaFabrycznaDiody,ngUszkodzonePcb,ngWadaNaklejki,ngSpalonyConn,ngInne,scrapBrakLutowia,scrapBrakDiodyLed,scrapBrakResConn,scrapPrzesuniecieLed,scrapPrzesuniecieResConn,scrapZabrudzenieLed,scrapUszkodzenieMechaniczneLed,scrapUszkodzenieConn,scrapWadaFabrycznaDiody,scrapUszkodzonePcb,scrapWadaNaklejki,scrapSpalonyConn,scrapInne,ngTestElektryczny FROM tb_Kontrola_Wizualna_Karta_Pracy WHERE ";

            for (int i = 0; i < lots.Length; i++)
            {
                if (i>0)
                {
                    command.CommandText += " OR ";
                }
                command.CommandText += "numerZlecenia='" + lots[i] + "'";
            }
            command.CommandText += ";";

            SqlDataAdapter adapter = new SqlDataAdapter(command);

            adapter.Fill(tabletoFill);

            return tabletoFill;
        }

        public static string[] GetWasteColumnNames()
        {

            return new string[] { "ngBrakLutowia", "ngBrakDiodyLed", "ngBrakResConn", "ngPrzesuniecieLed", "ngPrzesuniecieResConn", "ngZabrudzenieLed", "ngUszkodzenieMechaniczneLed", "ngUszkodzenieConn", "ngWadaFabrycznaDiody", "ngUszkodzonePcb", "ngWadaNaklejki", "ngSpalonyConn", "ngInne", "scrapBrakLutowia", "scrapBrakDiodyLed", "scrapBrakResConn", "scrapPrzesuniecieLed", "scrapPrzesuniecieResConn", "scrapZabrudzenieLed", "scrapUszkodzenieMechaniczneLed", "scrapUszkodzenieConn", "scrapWadaFabrycznaDiody", "scrapUszkodzonePcb", "scrapWadaNaklejki", "scrapSpalonyConn", "scrapInne", "ngTestElektryczny" };
        }

        public static bool CheckIfLotIsAlreadyAdded(string lot)
        {
            DataTable tabletoFill = new DataTable();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;";

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = @"SELECT numerZlecenia FROM tb_Kontrola_Wizualna_Karta_Pracy WHERE numerZlecenia=@lot;";
            command.Parameters.AddWithValue("@lot", lot);

            SqlDataAdapter adapter = new SqlDataAdapter(command);

            adapter.Fill(tabletoFill);

            return tabletoFill.Rows.Count > 0 ? true : false;
        }

        public static void InsertPcbToNgTable(List<Image> imagesTosave, string orderNo)
        {
            List<imageFailureTag> pcbToSave = new List<imageFailureTag>();
            List<string> controlSerialList = new List<string>();
            foreach (var pcb in imagesTosave)
            {
                imageFailureTag tag = (imageFailureTag)pcb.Tag;
                if (controlSerialList.Contains(tag.Serial)) continue;
                controlSerialList.Add(tag.Serial);
                pcbToSave.Add(tag);
            }



            using (SqlConnection openCon = new SqlConnection(@"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;"))
            {
                openCon.Open();
                foreach (var imgTag in pcbToSave)
                {
                    string save = "INSERT into tb_NG_tracking (serial_no, order_no, result, ng_type, datetime) VALUES (@serial_no, @order_no, @result, @ng_type, @datetime)";
                    using (SqlCommand querySave = new SqlCommand(save))
                    {
                        querySave.Connection = openCon;
                        querySave.Parameters.Add("@serial_no", SqlDbType.NVarChar).Value = imgTag.Serial;
                        querySave.Parameters.Add("@order_no", SqlDbType.NVarChar).Value = orderNo;
                        querySave.Parameters.Add("@result", SqlDbType.NVarChar).Value = imgTag.Result;
                        querySave.Parameters.Add("@ng_type", SqlDbType.NVarChar).Value = imgTag.NgType;
                        querySave.Parameters.Add("@datetime", SqlDbType.SmallDateTime).Value = DateTime.Now;
                        querySave.ExecuteNonQuery();
                    }
                }
            }
        }

        public static DataTable CheckIfSerialIsInNgTable(string serial)
        {
            DataTable tabletoFill = new DataTable();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=MSTMS010;Initial Catalog=MES;User Id=mes;Password=mes;";

            SqlCommand command = new SqlCommand();
            command.Connection = conn;
            command.CommandText = @"SELECT serial_no,result,ng_type,datetime,rework_result,rework_datetime,post_rework_vi_result,post_rework_OQA_result FROM tb_NG_tracking WHERE serial_no=@serial";
            command.Parameters.AddWithValue("@serial", serial);
            SqlDataAdapter adapter = new SqlDataAdapter(command);

            adapter.Fill(tabletoFill);

            return tabletoFill;
        }
    }
}
