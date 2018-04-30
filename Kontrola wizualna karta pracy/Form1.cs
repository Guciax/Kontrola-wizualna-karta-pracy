using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kontrola_wizualna_karta_pracy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<Image> imagesList = new List<Image>();
        RecordToSave recordToSave = new RecordToSave("", 0, "", DateTime.Now);
        Dictionary<string, string> lotToModelDictionary = new Dictionary<string, string>();
        Dictionary<string, string> smtInfo = new Dictionary<string, string>();
        private void Form1_Load(object sender, EventArgs e)
        {
            imagesList = ImagesTools.CreateListOfImages(@"Zdjecia\PL");
            Ng0BWadyLutowia.Tag = labelSolder;
            Ng0BrakDiodyLed.Tag = labelMissingLed;
            Ng0BrakResConn.Tag = labelMissingRes;
            Ng0PrzesuniecieDiodyLed.Tag = labelShiftedLed;
            Ng0PrzesuniecieResConn.Tag = labelShiftedRes;
            Ng0ZabrudzonaDiodaLed.Tag = labelDirtyLed;
            Ng0UszkodzenieDiodyLed.Tag = labelDmgLed;
            Ng0UszkodzenieConn.Tag = labelDmgConn;
            Ng0WadaFbrycznaLed.Tag = labelBadLed;
            Ng0UszkodzeniePcb.Tag = labelDmgPcb;
            Ng0WadaNaklejki.Tag = labelLabel;
            Ng0SpalonyConn.Tag = labelBurnedConn;
            Ng0Inne.Tag = labelOther;
            Scrap0WadyLutowia.Tag = labelSolder;
            Scrap0BrakDiodyLed.Tag = labelMissingLed;
            Scrap0BrakResConn.Tag = labelMissingRes;
            Scrap0PrzesuniecieDiodyLed.Tag = labelShiftedLed;
            Scrap0PrzesuniecieResConn.Tag = labelShiftedRes;
            Scrap0ZabrudzonaDiodaLed.Tag = labelDirtyLed;
            Scrap0UszkodzenieDiodyLed.Tag = labelDmgLed;
            Scrap0UszkodzenieConn.Tag = labelDmgConn;
            Scrap0WadaFabrycznaLed.Tag = labelBadLed;
            Scrap0UszkodzeniePcb.Tag = labelDmgPcb;
            Scrap0WadaNaklejki.Tag = labelLabel;
            Scrap0SpalonyConn.Tag = labelBurnedConn;
            Scrap0Inne.Tag = labelOther;
            Ng0Test.Tag = labelElecTest;

            bindRecordToControls();

            comboBoxOperator.Items.AddRange(SqlOperations.RecentOperatorsList(90).ToArray());
            lotToModelDictionary = SqlOperations.GetLotToModelDictionary();
            smtInfo = SqlOperations.GetSmtInfo();
        }

        private void bindRecordToControls()
        {
            textBoxLotNumber.DataBindings.Add("Text", recordToSave, "NumerZlecenia", false, DataSourceUpdateMode.OnPropertyChanged);
            comboBoxOperator.DataBindings.Add("Text", recordToSave, "Operator", false, DataSourceUpdateMode.OnPropertyChanged);
            textBoxGoodQty.DataBindings.Add("Text", recordToSave, "IloscDobrych", false, DataSourceUpdateMode.OnPropertyChanged);
            Ng0BWadyLutowia.DataBindings.Add("Value", recordToSave, "NgBrakLutowia", false, DataSourceUpdateMode.OnPropertyChanged);
            Ng0BrakDiodyLed.DataBindings.Add("Value", recordToSave, "NgBrakDiodyLed", false, DataSourceUpdateMode.OnPropertyChanged);
            Ng0BrakResConn.DataBindings.Add("Value", recordToSave, "NgBrakResConn", false, DataSourceUpdateMode.OnPropertyChanged);
            Ng0PrzesuniecieDiodyLed.DataBindings.Add("Value", recordToSave, "NgPrzesuniecieLed", false, DataSourceUpdateMode.OnPropertyChanged);
            Ng0PrzesuniecieResConn.DataBindings.Add("Value", recordToSave, "NgPrzesuniecieResConn", false, DataSourceUpdateMode.OnPropertyChanged);
            Ng0ZabrudzonaDiodaLed.DataBindings.Add("Value", recordToSave, "NgZabrudzenieLed", false, DataSourceUpdateMode.OnPropertyChanged);
            Ng0UszkodzenieDiodyLed.DataBindings.Add("Value", recordToSave, "NgUszkodzenieMechaniczneLed", false, DataSourceUpdateMode.OnPropertyChanged);
            Ng0UszkodzenieConn.DataBindings.Add("Value", recordToSave, "NgUszkodzenieConn", false, DataSourceUpdateMode.OnPropertyChanged);
            Ng0WadaFbrycznaLed.DataBindings.Add("Value", recordToSave, "NgWadaFabrycznaDiody", false, DataSourceUpdateMode.OnPropertyChanged);
            Ng0UszkodzeniePcb.DataBindings.Add("Value", recordToSave, "NgUszkodzonePcb", false, DataSourceUpdateMode.OnPropertyChanged);
            Ng0WadaNaklejki.DataBindings.Add("Value", recordToSave, "NgWadaNaklejki", false, DataSourceUpdateMode.OnPropertyChanged);
            Ng0SpalonyConn.DataBindings.Add("Value", recordToSave, "NgSpalonyConn", false, DataSourceUpdateMode.OnPropertyChanged);
            Ng0Inne.DataBindings.Add("Value", recordToSave, "NgInne", false, DataSourceUpdateMode.OnPropertyChanged);
            Scrap0WadyLutowia.DataBindings.Add("Value", recordToSave, "ScrapBrakLutowia", false, DataSourceUpdateMode.OnPropertyChanged);
            Scrap0BrakDiodyLed.DataBindings.Add("Value", recordToSave, "ScrapBrakDiodyLed", false, DataSourceUpdateMode.OnPropertyChanged);
            Scrap0BrakResConn.DataBindings.Add("Value", recordToSave, "ScrapBrakResConn", false, DataSourceUpdateMode.OnPropertyChanged);
            Scrap0PrzesuniecieDiodyLed.DataBindings.Add("Value", recordToSave, "ScrapPrzesuniecieLed", false, DataSourceUpdateMode.OnPropertyChanged);
            Scrap0PrzesuniecieResConn.DataBindings.Add("Value", recordToSave, "ScrapPrzesuniecieResConn", false, DataSourceUpdateMode.OnPropertyChanged);
            Scrap0ZabrudzonaDiodaLed.DataBindings.Add("Value", recordToSave, "ScrapZabrudzenieLed", false, DataSourceUpdateMode.OnPropertyChanged);
            Scrap0UszkodzenieDiodyLed.DataBindings.Add("Value", recordToSave, "ScrapUszkodzenieMechaniczneLed", false, DataSourceUpdateMode.OnPropertyChanged);
            Scrap0UszkodzenieConn.DataBindings.Add("Value", recordToSave, "ScrapUszkodzenieConn", false, DataSourceUpdateMode.OnPropertyChanged);
            Scrap0WadaFabrycznaLed.DataBindings.Add("Value", recordToSave, "ScrapWadaFabrycznaDiody", false, DataSourceUpdateMode.OnPropertyChanged);
            Scrap0UszkodzeniePcb.DataBindings.Add("Value", recordToSave, "ScrapUszkodzonePcb", false, DataSourceUpdateMode.OnPropertyChanged);
            Scrap0WadaNaklejki.DataBindings.Add("Value", recordToSave, "ScrapWadaNaklejki", false, DataSourceUpdateMode.OnPropertyChanged);
            Scrap0SpalonyConn.DataBindings.Add("Value", recordToSave, "ScrapSpalonyConn", false, DataSourceUpdateMode.OnPropertyChanged);
            Scrap0Inne.DataBindings.Add("Value", recordToSave, "ScrapInne", false, DataSourceUpdateMode.OnPropertyChanged);
            Ng0Test.DataBindings.Add("Value", recordToSave, "NgTestElektryczny", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        NumericUpDown previousUpDown = null;
        private void numUpDownEnter(object sender, EventArgs e)
        {
            if (previousUpDown != null)
            {
                highlightSelected(previousUpDown, false);
            }

            NumericUpDown numControl = (NumericUpDown)sender;

            string failureName = numControl.Name.Split('0')[1];
            Image failureImg = ImagesTools.GetImageByName(imagesList, failureName);
            //if (failureImg!=null)
            {
                pictureBox1.Image = failureImg;
            }

            highlightSelected(numControl, true);
            previousUpDown = numControl;
        }

        private void highlightSelected(NumericUpDown numUp, bool state)
        {
            if (state)
            {
                Label lbl = ((Label)numUp.Tag);
                lbl.Font = new Font(lbl.Font, FontStyle.Bold);
                numUp.BackColor = Color.LightYellow;
            }
            else
            {
                Label lbl = ((Label)numUp.Tag);
                lbl.Font = new Font(lbl.Font, FontStyle.Regular);
                numUp.BackColor = Color.White;
            }
        }

        private string CreateSummaryText()
        {
            string result = labelOperator.Text + ": " + comboBoxOperator.Text + Environment.NewLine;
            result += labelGoodQty.Text + ": " + textBoxGoodQty.Text + Environment.NewLine;
            result += Environment.NewLine + "____________________________________________________" + Environment.NewLine;

            string ngSummary = "";
            foreach (NumericUpDown num in panelNg.Controls.OfType<NumericUpDown>())
            {
                if (num.Value > 0)
                {
                    ngSummary += ((Label)num.Tag).Text + ": " + num.Value + Environment.NewLine;
                }
            }
            if (ngSummary != "")
            {
                result += Environment.NewLine + "NG:" + Environment.NewLine+ ngSummary;
            }

            string scrapSummary = "";
            foreach (NumericUpDown num in panelScrap.Controls.OfType<NumericUpDown>())
            {
                if (num.Value > 0)
                {
                    scrapSummary += ((Label)num.Tag).Text + ": " + num.Value + Environment.NewLine;
                }
            }

            if (scrapSummary != "")
            {
                result += Environment.NewLine + "SCRAP:" + Environment.NewLine + scrapSummary;
            }

            if (Ng0Test.Value>0)
            {
                result += Environment.NewLine + "TEST NG:" + Ng0Test.Value;
            }
            return result;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            using (SummaryView summaryForm = new SummaryView(CreateSummaryText()))
            {
                summaryForm.ShowDialog();
                if (summaryForm.DialogResult == DialogResult.OK)
                {
                    SqlOperations.SaveRecordToDb(recordToSave);

                    Efficiency.SaveToTetFile(DateTime.Now.ToLongDateString() + ";" + "Model" + ";" + textBoxLotNumber.Text + ";" + textBoxGoodQty.Text);

                    textBoxLotNumber.Text = "";
                    comboBoxOperator.Items.Clear();
                    comboBoxOperator.Items.AddRange(SqlOperations.RecentOperatorsList(90).ToArray());
                    textBoxGoodQty.Text = "0";
                    foreach (NumericUpDown num in panelNg.Controls.OfType<NumericUpDown>())
                    {
                        num.Value = 0;
                    }

                    foreach (NumericUpDown num in panelScrap.Controls.OfType<NumericUpDown>())
                    {
                        num.Value = 0;
                    }

                    Ng0Test.Value = 0;

                    Efficiency.AddRecentOrdersToGrid(dataGridView1);
                }
            }
        }

        private NumericUpDown DbFieldToNumeric(string dbField)
        {
            switch (dbField)
            {
                case "ngBrakLutowia": return Ng0BWadyLutowia;
                case "ngBrakDiodyLed": return Ng0BrakDiodyLed;
                case "ngBrakResConn": return Ng0BrakResConn;
                case "ngPrzesuniecieLed": return Ng0PrzesuniecieDiodyLed;
                case "ngPrzesuniecieResConn": return Ng0PrzesuniecieResConn;
                case "ngZabrudzenieLed": return Ng0ZabrudzonaDiodaLed;
                case "ngUszkodzenieMechaniczneLed": return Ng0UszkodzenieDiodyLed;
                case "ngUszkodzenieConn": return Ng0UszkodzenieConn;
                case "ngWadaFabrycznaDiody": return Ng0WadaFbrycznaLed;
                case "ngUszkodzonePcb": return Ng0UszkodzeniePcb;
                case "ngWadaNaklejki": return Ng0WadaNaklejki;
                case "ngSpalonyConn": return Ng0SpalonyConn;
                case "ngInne": return Ng0Inne;
                case "scrapBrakLutowia": return Scrap0WadyLutowia;
                case "scrapBrakDiodyLed": return Scrap0BrakDiodyLed;
                case "scrapBrakResConn": return Scrap0BrakResConn;
                case "scrapPrzesuniecieLed": return Scrap0PrzesuniecieDiodyLed;
                case "scrapPrzesuniecieResConn": return Scrap0PrzesuniecieResConn;
                case "scrapZabrudzenieLed": return Scrap0ZabrudzonaDiodaLed;
                case "scrapUszkodzenieMechaniczneLed": return Scrap0UszkodzenieDiodyLed;
                case "scrapUszkodzenieConn": return Scrap0UszkodzenieConn;
                case "scrapWadaFabrycznaDiody": return Scrap0WadaFabrycznaLed;
                case "scrapUszkodzonePcb": return Scrap0UszkodzeniePcb;
                case "scrapWadaNaklejki": return Scrap0WadaNaklejki;
                case "scrapSpalonyConn": return Scrap0SpalonyConn;
                case "scrapInne": return Scrap0Inne;
                case "ngTestElektryczny": return Ng0Test;
                default: return null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //bindRecordToControls();
            MessageBox.Show(recordToSave.IloscDobrych.ToString());
        }

        private void textBoxGoodQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\\d+"))
                e.Handled = true;
        }

        private void textBoxGoodQty_Enter(object sender, EventArgs e)
        {
            textBoxGoodQty.SelectAll();
        }

        private void textBoxGoodQty_MouseClick(object sender, MouseEventArgs e)
        {
            textBoxGoodQty.SelectAll();

            
        }

        private void timerLotToModel_Tick(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                lotToModelDictionary = SqlOperations.GetLotToModelDictionary();
                smtInfo = SqlOperations.GetSmtInfo();
            }).Start();
        }

        private string GetNumberOfConnectors(string model)
        {
            string result = "";
            if (model.Contains("LLFML"))
            {
                //exceptions....
                if (model.Replace("LLFML", "") == "G2-08L404B")
                    return "0";

                string family = model.Split('-')[0].Replace("LLFML", "").Substring(0, 1).ToUpper();
                string connCode = model.Split('-')[1].Substring(5, 1);
                if ((family == "K" || family == "G") & (connCode == "2" || connCode == "4"))
                {
                    result = "4";
                }
                else
                {
                    result = "2";
                }
            }
            return result;
        }

        private void textBoxLotNumber_TextChanged(object sender, EventArgs e)
        {
            string model = "";
            string smtString = "";
            string smtLine = "";
            string smtDate = "";
            string connInfo = "";

            lotToModelDictionary.TryGetValue(textBoxLotNumber.Text, out model);
            smtInfo.TryGetValue(textBoxLotNumber.Text, out smtString);

            if (model!=null)
            {
                connInfo = GetNumberOfConnectors(model);
            }

            if (smtString!=null)
            {
                smtDate = smtString.Split(';')[0];
                smtLine = smtString.Split(';')[1];
            }

            labelLotInfo.Text = "Model: " + model + Environment.NewLine
                + "Produkcja: " + smtDate + " " + smtLine + Environment.NewLine;
            if (connInfo!="")
            {
                labelLotInfo.Text += "Ilość złączek: " + connInfo;
            }
        }
    }
}
