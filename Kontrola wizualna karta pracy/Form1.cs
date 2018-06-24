using Kontrola_wizualna_karta_pracy.DataStructures;
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
using static Kontrola_wizualna_karta_pracy.DynamicControls;
using System.IO;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;
using ZXing.Aztec;
using System.Diagnostics;

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
        Dictionary<string, SmtInfo> smtInfo = new Dictionary<string, SmtInfo>();
        FilterInfoCollection CaptureDevice;
        VideoCaptureDevice FinalFrame;
        Bitmap bitmap;

        private void Form1_Load(object sender, EventArgs e)
        {
            //imagesList = ImagesTools.CreateListOfImages(@"Zdjecia\PL");
            CaptureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo item in CaptureDevice)
            {
                Debug.WriteLine(item.Name);
            }

            Debug.WriteLine(CaptureDevice[0].ToString());

            textBoxLotNumber.DataBindings.Add("Text", recordToSave, "NumerZlecenia", false, DataSourceUpdateMode.OnPropertyChanged);
            comboBoxOperator.DataBindings.Add("Text", recordToSave, "Operator", false, DataSourceUpdateMode.OnPropertyChanged);
            textBoxGoodQty.DataBindings.Add("Text", recordToSave, "IloscDobrych", false, DataSourceUpdateMode.OnPropertyChanged);

            //comboBoxOperator.Items.AddRange(SqlOperations.RecentOperatorsList(90).ToArray());
            //lotToModelDictionary = SqlOperations.GetLotToModelDictionary();
            //smtInfo = SqlOperations.GetSmtInfo();

            DynamicControls.CreateControls(flpNgBox, flpScrapBox, flowLayoutPanel1, SqlOperations.GetWasteColumnNames(), recordToSave);

            FinalFrame = new VideoCaptureDevice();
            FinalFrame = new VideoCaptureDevice(CaptureDevice[2].MonikerString);
            FinalFrame.NewFrame += new NewFrameEventHandler(FinalFrame_NewFrame);
            FinalFrame.NewFrame -= Handle_New_Frame;
        }

        private void Handle_New_Frame(object sender, NewFrameEventArgs eventArgs)
        {
            if (bitmap != null)
                bitmap.Dispose();
            bitmap = new Bitmap(eventArgs.Frame);

            if (pictureBox1.Image != null)
                this.Invoke(new MethodInvoker(delegate () { pictureBox1.Image.Dispose(); }));
            pictureBox1.Image = bitmap;
        }

        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            bitmap = (Bitmap)eventArgs.Frame.Clone();
            pictureBox1.Image = bitmap;
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
            
        }

        private void textBoxGoodQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\\d+"))
                e.Handled = true;
        }

        private void textBoxGoodQty_Enter(object sender, EventArgs e)
        {
            textBoxGoodQty.SelectAll();
            virtualKeyboard kbForm = new virtualKeyboard(textBoxGoodQty);
            kbForm.Location = new System.Drawing.Point(textBoxGoodQty.Location.X, textBoxGoodQty.Location.Y + textBoxGoodQty.Height);
            kbForm.Show();
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
            SmtInfo smtIfo;
            string smtLine = "";
            string smtDate = "";
            string connInfo = "";

            lotToModelDictionary.TryGetValue(textBoxLotNumber.Text, out model);
            smtInfo.TryGetValue(textBoxLotNumber.Text, out smtIfo);

            if (model!=null)
            {
                connInfo = GetNumberOfConnectors(model);
            }

            if (smtIfo != null)
            {
                smtDate = smtIfo.CompletitionDate;
                smtLine = smtIfo.SmtLine;
            }

            labelLotInfo.Text = "Model: " + model + Environment.NewLine
                + "Produkcja: " + smtDate + " " + smtLine + Environment.NewLine;
            if (connInfo!="")
            {
                labelLotInfo.Text += "Ilość złączek: " + connInfo;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            FinalFrame.Stop();
        }

        private void buttonCamStartStop_Click(object sender, EventArgs e)
        {

            if (FinalFrame.IsRunning)
            {
                FinalFrame.Stop();
                pictureBox1.Image = null;
                //pictureBoxCaptured.Image = null;
                pictureBox1.Invalidate();
                //pictureBoxCaptured.Invalidate();

                buttonCamStartStop.Text = "Cam Start";

            }
            else
            {
                
                FinalFrame.Start();

                buttonCamStartStop.Text = "Cam Stop";

            }
        }

        private void buttonAddFailure_Click(object sender, EventArgs e)
        {
            List<string> ngButtons = new List<string>();
            List<string> scrapButtons = new List<string>();
            foreach (Control ctrl in flpNgBox.Controls)
            {
                ngButtons.Add(ctrl.Name);
            }
            foreach (Control ctrl in flpScrapBox.Controls)
            {
                scrapButtons.Add(ctrl.Name);
            }

            FinalFrame.Stop();
            NewFailureForm failForm = new NewFailureForm(ngButtons.ToArray(), scrapButtons.ToArray(), textBoxLotNumber.Text, DateTime.Now.ToString("dd-MM-yyyy"));
            failForm.ShowDialog();
            string buttonClicked = failForm.buttonClicked;
            if (buttonClicked != null)
            {
                if (buttonClicked.StartsWith("ng"))
                {
                    foreach (Control tb in flpNgBox.Controls)
                    {
                        if (tb.Name == buttonClicked)
                        {
                            int val = int.Parse(tb.Text);
                            val++;
                            tb.Text = val.ToString();
                        }
                    }
                }
                else if (buttonClicked.StartsWith("scrap"))
                {
                    foreach (Control tb in flpScrapBox.Controls)
                    {
                        if (tb.Name == buttonClicked)
                        {
                            int val = int.Parse(tb.Text);
                            val++;
                            tb.Text = val.ToString();
                        }
                    }
                }
            }
        }
    }
    }
