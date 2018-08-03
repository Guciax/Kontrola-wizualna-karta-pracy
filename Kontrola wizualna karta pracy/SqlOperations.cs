using Kontrola_wizualna_karta_pracy.DataStructures;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontrola_wizualna_karta_pracy
{
    class SqlOperations
    {
        public static void SaveRecordToDb(RecordToSave saveData)
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

        public static Dictionary<string,CurrentLotInfo> GetLotToModelDictionary()
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

                SmtInfo newItem = new SmtInfo(smtLine, completitionDate, qty, model);
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
    }
}
