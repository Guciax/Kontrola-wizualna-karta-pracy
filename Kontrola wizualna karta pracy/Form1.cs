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
using System.Drawing.Text;
using System.Globalization;
using static Kontrola_wizualna_karta_pracy.DateOperations;
using System.Configuration;

namespace Kontrola_wizualna_karta_pracy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            byte[] fontData = Properties.Resources.digital_7;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources.digital_7.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.digital_7.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);
            
            myFont = new Font(fonts.Families[0], 56.0F);

            recordToSaceCalculation = new RecordToSaveCalculations(recordToSave);
            summaryView = new SummaryView(recordToSave, radioButtonPolish.Checked);

            panelClock.Width = panel5.Width+2;
            panelNumerics.Width = panel5.Width+2;
            panelClock.Location = new System.Drawing.Point(0, this.Height - panelClock.Height-15);
            labelClock.Font = myFont;
        }

        List<Image> imagesList = new List<Image>();
        RecordToSave recordToSave = new RecordToSave("", 0, "", DateTime.Now);
        string appPath = AppSettings.GetSettings("AppPath");
        Dictionary<string, CurrentLotInfo> currentLotDictionary = new Dictionary<string, CurrentLotInfo>();
        Dictionary<string, SmtInfo> smtInfo = new Dictionary<string, SmtInfo>();

        FilterInfoCollection CaptureDevice;
        VideoCaptureDevice FinalFrame;
        string deviceMonikerString = "";

        Bitmap bitmap;
        List<WasteDataStructure> inspectionData = new List<WasteDataStructure>();
        Dictionary<string, string> lotModelDict = new Dictionary<string, string>();
        CurrentLotInfo currentLotInfo;
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
        IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);
        Font myFont;
        private RecordToSaveCalculations recordToSaceCalculation;
        private PrivateFontCollection fonts = new PrivateFontCollection();
        private SummaryView summaryView;
        bool cameraEnabled = false;
        string[] pcbsInCurrentLot = null;
        

        private void Form1_Load(object sender, EventArgs e)
        {
            //label1.Font = myFont;
            ClearRecordToSave();
            imagesList = ImagesTools.CreateListOfImages(Path.Combine(AppSettings.GetSettings("AppPath"), @"Zdjecia\PL"));

            CaptureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            

            comboBoxOperator.Items.AddRange(SqlOperations.RecentOperatorsList(45).ToArray());
            smtInfo = SqlOperations.GetSmtInfo();
            mstOrdersFromExcel.loadExcel(ref smtInfo);

            CalculateWasteAndEff();
            //string cameraConfig = ConfigurationManager.AppSettings["Camera_ON_OFF"];
            string cameraConfig = AppSettings.GetSettings("Camera_ON_OFF");
            if (cameraConfig == "ON")
            {
                cameraEnabled = true;
            }

            FinalFrame = new VideoCaptureDevice();

            if (cameraEnabled)
            {
                deviceMonikerString = CheckDeviceMonikerString();
                if (deviceMonikerString != "")
                {
                    FinalFrame = new VideoCaptureDevice(deviceMonikerString);
                    FinalFrame.NewFrame += new NewFrameEventHandler(FinalFrame_NewFrame);
                    FinalFrame.NewFrame -= Handle_New_Frame;
                    buttonCamStartStop.Visible = true;
                    panelNumerics.Visible = false;
                }
            }

            panelVirtualKeyboard.Parent = this;
            panelVirtualKeyboard.BringToFront();

            DynamicControls.CreateControls(flpNgBox, flpScrapBox, labelPanel, SqlOperations.GetWasteColumnNames(), recordToSave, FinalFrame, imagesList, pictureBox1);

            textBoxLotNumber.DataBindings.Add("Text", recordToSave, "NumerZlecenia", false, DataSourceUpdateMode.OnPropertyChanged);
            comboBoxOperator.DataBindings.Add("Text", recordToSave, "Operator", false, DataSourceUpdateMode.OnPropertyChanged);
            textBoxGoodQty.DataBindings.Add("Text", recordToSave, "IloscDobrych", false, DataSourceUpdateMode.OnPropertyChanged);

            Ng0BrakLutowia.DataBindings.Add("Value", recordToSave, "NgBrakLutowia", false, DataSourceUpdateMode.OnPropertyChanged);
            Ng0BrakDiodyLed.DataBindings.Add("Value", recordToSave, "NgBrakDiodyLed", false, DataSourceUpdateMode.OnPropertyChanged);
            Ng0BrakResConn.DataBindings.Add("Value", recordToSave, "NgBrakResConn", false, DataSourceUpdateMode.OnPropertyChanged);
            Ng0PrzesuniecieLed.DataBindings.Add("Value", recordToSave, "NgPrzesuniecieLed", false, DataSourceUpdateMode.OnPropertyChanged);
            Ng0PrzesuniecieResConn.DataBindings.Add("Value", recordToSave, "NgPrzesuniecieResConn", false, DataSourceUpdateMode.OnPropertyChanged);
            Ng0ZabrudzenieLed.DataBindings.Add("Value", recordToSave, "NgZabrudzenieLed", false, DataSourceUpdateMode.OnPropertyChanged);
            Ng0UszkodzenieMechaniczneLed.DataBindings.Add("Value", recordToSave, "NgUszkodzenieMechaniczneLed", false, DataSourceUpdateMode.OnPropertyChanged);
            Ng0UszkodzenieConn.DataBindings.Add("Value", recordToSave, "NgUszkodzenieConn", false, DataSourceUpdateMode.OnPropertyChanged);
            Ng0WadaFabrycznaDiody.DataBindings.Add("Value", recordToSave, "NgWadaFabrycznaDiody", false, DataSourceUpdateMode.OnPropertyChanged);
            Ng0UszkodzonePcb.DataBindings.Add("Value", recordToSave, "NgUszkodzonePcb", false, DataSourceUpdateMode.OnPropertyChanged);
            Ng0WadaNaklejki.DataBindings.Add("Value", recordToSave, "NgWadaNaklejki", false, DataSourceUpdateMode.OnPropertyChanged);
            Ng0SpalonyConn.DataBindings.Add("Value", recordToSave, "NgSpalonyConn", false, DataSourceUpdateMode.OnPropertyChanged);
            Ng0Inne.DataBindings.Add("Value", recordToSave, "NgInne", false, DataSourceUpdateMode.OnPropertyChanged);
            Scrap0BrakLutowia.DataBindings.Add("Value", recordToSave, "ScrapBrakLutowia", false, DataSourceUpdateMode.OnPropertyChanged);
            Scrap0BrakDiodyLed.DataBindings.Add("Value", recordToSave, "ScrapBrakDiodyLed", false, DataSourceUpdateMode.OnPropertyChanged);
            Scrap0BrakResConn.DataBindings.Add("Value", recordToSave, "ScrapBrakResConn", false, DataSourceUpdateMode.OnPropertyChanged);
            Scrap0PrzesuniecieLed.DataBindings.Add("Value", recordToSave, "ScrapPrzesuniecieLed", false, DataSourceUpdateMode.OnPropertyChanged);
            Scrap0PrzesuniecieResConn.DataBindings.Add("Value", recordToSave, "ScrapPrzesuniecieResConn", false, DataSourceUpdateMode.OnPropertyChanged);
            Scrap0ZabrudzenieLed.DataBindings.Add("Value", recordToSave, "ScrapZabrudzenieLed", false, DataSourceUpdateMode.OnPropertyChanged);
            Scrap0UszkodzenieMechaniczneLed.DataBindings.Add("Value", recordToSave, "ScrapUszkodzenieMechaniczneLed", false, DataSourceUpdateMode.OnPropertyChanged);
            Scrap0UszkodzenieConn.DataBindings.Add("Value", recordToSave, "ScrapUszkodzenieConn", false, DataSourceUpdateMode.OnPropertyChanged);
            Scrap0WadaFabrycznaDiody.DataBindings.Add("Value", recordToSave, "ScrapWadaFabrycznaDiody", false, DataSourceUpdateMode.OnPropertyChanged);
            Scrap0UszkodzonePcb.DataBindings.Add("Value", recordToSave, "ScrapUszkodzonePcb", false, DataSourceUpdateMode.OnPropertyChanged);
            Scrap0WadaNaklejki.DataBindings.Add("Value", recordToSave, "ScrapWadaNaklejki", false, DataSourceUpdateMode.OnPropertyChanged);
            Scrap0SpalonyConn.DataBindings.Add("Value", recordToSave, "ScrapSpalonyConn", false, DataSourceUpdateMode.OnPropertyChanged);
            Scrap0Inne.DataBindings.Add("Value", recordToSave, "ScrapInne", false, DataSourceUpdateMode.OnPropertyChanged);
            Ng0TestElektryczny.DataBindings.Add("Value", recordToSave, "NgTestElektryczny", false, DataSourceUpdateMode.OnPropertyChanged);

            


            bool release = true;
#if DEBUG
            release=false;
#endif
            if (release)
            {
                var locationOnForm = flpNgBox.FindForm().PointToClient(flpNgBox.Parent.PointToScreen(flpNgBox.Location));
                panelNumerics.Location = new System.Drawing.Point(locationOnForm.X-3, locationOnForm.Y-3);
            }

            this.Size = new Size(SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height);
            this.Location = new System.Drawing.Point(0, 0);

            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            labelAppVersion.Text = fvi.FileVersion;
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
            if (failureImg != null)
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

            string testSummary = "";
            string ngSummary = "";

            foreach (object ctrl in flpNgBox.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox box = (TextBox)ctrl;
                    if (box.Text != "0")
                    {
                        if (box.Tag.ToString().ToLower().Contains("test"))
                        {
                            testSummary = box.Tag.ToString() + " - " + box.Text+Environment.NewLine;
                        }
                        else
                        {
                            ngSummary += box.Tag.ToString() + " - " + box.Text + Environment.NewLine;
                        }
                    }
                }
            }

            if (ngSummary != "")
            {
                result += Environment.NewLine + "NG:" + Environment.NewLine + ngSummary;
            }
            result += Environment.NewLine + testSummary;
            string scrapSummary = "";
            foreach (object ctrl in flpScrapBox.Controls)
            {
                if (ctrl is TextBox)
                {
                    TextBox box = (TextBox)ctrl;
                    if (box.Text != "0")
                    {
                        scrapSummary += box.Tag.ToString() + " - " + box.Text + Environment.NewLine;
                    }
                }
            }

            if (scrapSummary != "")
            {
                result += Environment.NewLine + Environment.NewLine + "SCRAP:" + Environment.NewLine + scrapSummary;
            }

            return result;
        }

        private bool AllDataFilledCorrectly()
        {
            bool result = true;

            if (textBoxLotNumber.Text.Length < 5) 
            {
                result = false;
                MessageBox.Show(LanguangeTranslation.Translate("Nieprawidłowy numer zlecenia", radioButtonPolish.Checked));
            }

            if (comboBoxOperator.Text.Length<4)
            {
                result = false;
                MessageBox.Show(LanguangeTranslation.Translate( "Nieprawidłowa nazwa operatora", radioButtonPolish.Checked));
            }

            if (textBoxGoodQty.Text=="0")
            {
                result = false;
                MessageBox.Show(LanguangeTranslation.Translate("Nieprawidłowa ilość", radioButtonPolish.Checked));
            }
            return result;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (AllDataFilledCorrectly())
            {
                using (SummaryView summaryForm = new SummaryView(recordToSave, radioButtonPolish.Checked))
                {
                    summaryForm.ShowDialog();
                    if (summaryForm.DialogResult == DialogResult.OK)
                    {
                        recordToSave.Data_Czas = DateTime.Now;
                        SqlOperations.SaveRecordToDb(recordToSave);
                        string model = LanguangeTranslation.Translate("Nieznany", radioButtonPolish.Checked);

                        if (smtCurrentLotInfo != null)
                        {
                            if (smtCurrentLotInfo.Model != null)
                            {
                                model = smtCurrentLotInfo.Model;
                            }
                        }

                        var allNg = recordToSaceCalculation.GetAllNg();
                        Efficiency.SaveToTextFile(DateTime.Now.ToString("HH:mm dd-MMM") + ";" + smtCurrentLotInfo.Model + ";" + textBoxLotNumber.Text + ";" + (int.Parse(textBoxGoodQty.Text) + allNg).ToString() + ";" + allNg, appPath);

                        labelLotInfo.Text = LanguangeTranslation.Translate("Dane zlecenia", radioButtonPolish.Checked);

                        CalculateWasteAndEff();

                        ClearRecordToSave();
                        currentLotInfo = null;

                        comboBoxOperator.Items.AddRange(SqlOperations.RecentOperatorsList(45).ToArray());
                    }
                }
            }
        }

        private void ClearRecordToSave()
        {
            //recordToSave = new RecordToSave("", 0, "", DateTime.Now, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            foreach (object control in flpNgBox.Controls)
            {
                if (control is TextBox)
                {
                    TextBox txtB = (TextBox)control;
                    txtB.Text = "0";
                }
            }

            foreach (object control in flpScrapBox.Controls)
            {
                if (control is TextBox)
                {
                    TextBox txtB = (TextBox)control;
                    txtB.Text = "0";
                }
            }

            textBoxLotNumber.Text = "";
            comboBoxOperator.Items.Clear();
            textBoxGoodQty.Text = "0";
        }

        private NumericUpDown DbFieldToNumeric(string dbField)
        {
            switch (dbField)
            {
                case "ngBrakLutowia": return Ng0BrakLutowia;
                case "ngBrakDiodyLed": return Ng0BrakDiodyLed;
                case "ngBrakResConn": return Ng0BrakResConn;
                case "ngPrzesuniecieLed": return Ng0PrzesuniecieLed;
                case "ngPrzesuniecieResConn": return Ng0PrzesuniecieResConn;
                case "ngZabrudzenieLed": return Ng0ZabrudzenieLed;
                case "ngUszkodzenieMechaniczneLed": return Ng0UszkodzenieMechaniczneLed;
                case "ngUszkodzenieConn": return Ng0UszkodzenieConn;
                case "ngWadaFabrycznaDiody": return Ng0WadaFabrycznaDiody;
                case "ngUszkodzonePcb": return Ng0UszkodzonePcb;
                case "ngWadaNaklejki": return Ng0WadaNaklejki;
                case "ngSpalonyConn": return Ng0SpalonyConn;
                case "ngInne": return Ng0Inne;
                case "scrapBrakLutowia": return Scrap0BrakLutowia;
                case "scrapBrakDiodyLed": return Scrap0BrakDiodyLed;
                case "scrapBrakResConn": return Scrap0BrakResConn;
                case "scrapPrzesuniecieLed": return Scrap0PrzesuniecieLed;
                case "scrapPrzesuniecieResConn": return Scrap0PrzesuniecieResConn;
                case "scrapZabrudzenieLed": return Scrap0ZabrudzenieLed;
                case "scrapUszkodzenieMechaniczneLed": return Scrap0UszkodzenieMechaniczneLed;
                case "scrapUszkodzenieConn": return Scrap0UszkodzenieConn;
                case "scrapWadaFabrycznaDiody": return Scrap0WadaFabrycznaDiody;
                case "scrapUszkodzonePcb": return Scrap0UszkodzonePcb;
                case "scrapWadaNaklejki": return Scrap0WadaNaklejki;
                case "scrapSpalonyConn": return Scrap0SpalonyConn;
                case "scrapInne": return Scrap0Inne;
                case "ngTestElektryczny": return Ng0TestElektryczny;
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
            panelVirtualKeyboard.Visible = true;
            panelVirtualKeyboard.Location = new System.Drawing.Point(textBoxGoodQty.Location.X + 3, textBoxGoodQty.Location.Y + textBoxGoodQty.Height + 3);
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
                //currentLotDictionary = SqlOperations.GetLotToModelDictionary();
                smtInfo = SqlOperations.GetSmtInfo();
                if (DateTime.Now.Minute == 30)//one per hour
                {
                    mstOrdersFromExcel.loadExcel(ref smtInfo);
                }
            }).Start();
        }

        public static string GetNumberOfConnectors(string model)
        {
            string result = "";
            if (model.Contains("-"))
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
            else
            {
                result = "?";
            }
            return result;
        }

        SmtInfo smtCurrentLotInfo = new SmtInfo("", "", 0, "Nieznany", true);
        private void textBoxLotNumber_TextChanged(object sender, EventArgs e)
        {
            smtCurrentLotInfo = new SmtInfo("", "", 0, "Nieznany", radioButtonPolish.Checked);
            string lot = textBoxLotNumber.Text;

            if (textBoxLotNumber.Text.Length > 6)
            {
                if (smtInfo.TryGetValue(lot, out smtCurrentLotInfo))
                {
                    BackgroundWorker worker = new BackgroundWorker();
                    worker.DoWork += worker_DoWork;
                    worker.RunWorkerAsync();
                }
                else
                {
                    CurrentLotInfo tryMesToGetLotInfo = SqlOperations.LotNoToModelIdFromMes(textBoxLotNumber.Text);
                    if (tryMesToGetLotInfo.Model != "")
                    {
                        smtCurrentLotInfo = new SmtInfo("", "Brak danych SMT", tryMesToGetLotInfo.OrderedQty, tryMesToGetLotInfo.Model, radioButtonPolish.Checked);
                    }
                    else
                    {
                        smtCurrentLotInfo = new SmtInfo("", "", 0, "Nieznany", radioButtonPolish.Checked);
                    }
                }
            }

            labelLotInfo.Text = smtCurrentLotInfo.infoToDisplay;
            //recordToSave.IloscDobrych = smtCurrentLotInfo.OrderedQty;
            textBoxGoodQty.Text = smtCurrentLotInfo.OrderedQty.ToString();
            textBoxLotNumber.Text = lot;
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e) //to know what pcbSerials are in this lot
        {
            Dictionary<string, List<string>> testResult = SqlOperations.HowManyModulesTested(textBoxLotNumber.Text);
            pcbsInCurrentLot = testResult.SelectMany(s => s.Value).ToArray();
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
                pictureBox1.Invalidate();
            }
            else
            {
                FinalFrame.Start();

            }
        }

        private void buttonAddFailure_Click(object sender, EventArgs e)
        {
            if (textBoxLotNumber.Text.Trim() == "")
            {
                MessageBox.Show(LanguangeTranslation.Translate("Najpierw wpisz numer zlecenia", radioButtonPolish.Checked));
            }
            else
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
                NewFailureForm failForm = new NewFailureForm(ngButtons.ToArray(), scrapButtons.ToArray(), textBoxLotNumber.Text, DateTime.Now.ToString("dd-MM-yyyy"), pcbsInCurrentLot, radioButtonPolish.Checked, deviceMonikerString);
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

        private void button13_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (textBoxGoodQty.Text == "0")
            {
                textBoxGoodQty.Text = btn.Text;
            }
            else
            {
                textBoxGoodQty.Text += btn.Text;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            textBoxGoodQty.Text = "0";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (textBoxGoodQty.Text.Length > 1)
            {
                textBoxGoodQty.Text = textBoxGoodQty.Text.Substring(0, textBoxGoodQty.Text.Length - 1);
            }
            else
            {
                textBoxGoodQty.Text = "0";
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            Debug.WriteLine(GetChildAtPoint(new System.Drawing.Point(e.X, e.Y)).Name);
        }

        private void timerHideKeyboard_Tick(object sender, EventArgs e)
        {

            if (panelVirtualKeyboard.Visible)
            {
                if (!panelVirtualKeyboard.ClientRectangle.Contains(panelVirtualKeyboard.PointToClient(Cursor.Position)) & !textBoxGoodQty.ClientRectangle.Contains(textBoxGoodQty.PointToClient(Cursor.Position)))
                {
                    panelVirtualKeyboard.Visible = false;
                    this.ActiveControl = pictureBox1;
                }
            }
        }

        private void dataGridViewHistory_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DgvTools.ColorShifts(dataGridViewHistory, "Data");
            //labelQtySinceShiftStart.Text = LanguangeTranslation.Translate("Ilość od początku zmiany:", radioButtonPolish.Checked) + Efficiency.HowManyInspectedThisShift(dataGridViewHistory) + LanguangeTranslation.Translate("szt", radioButtonPolish.Checked);
            //labelWasteLevel.Text = Efficiency.CalculateEfficiencyThisShift(dataGridViewHistory, radioButtonPolish.Checked) + "%";
            //labelWasteLevel.Text = Efficiency.CalculateWasteThisShift(dataGridViewHistory)+"%";
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //if(!FinalFrame.IsRunning)
            //{
            //    buttonCamStartStop.Text = "Cam Start";
            //}
            //else
            //{
            //    buttonCamStartStop.Text = "Cam Stop";
            //}
        }

        private void button14_Click(object sender, EventArgs e)
        {

        }

        private void textBoxLotNumber_Enter(object sender, EventArgs e)
        {
            //if (textBoxLotNumber.ForeColor == Color.Silver)
            //{
            //    textBoxLotNumber.Text = "";
            //    textBoxLotNumber.ForeColor = Color.Black;
            //}
        }

        private void textBoxLotNumber_Leave(object sender, EventArgs e)
        {
            //if (textBoxLotNumber.Text.Trim()=="")
            //{
            //    textBoxLotNumber.Text = "Numer Zlecenia";
            //    textBoxLotNumber.ForeColor = Color.Silver;
            //}
        }

        private void CalculateWasteAndEff()
        {
                Efficiency.AddRecentOrdersToGrid(dataGridViewHistory, ref lotModelDict, appPath);
                List<string> inspectedLots = new List<string>();

            foreach (DataGridViewRow row in dataGridViewHistory.Rows)
            {
                DateTime inspectionTime = DateTime.ParseExact(row.Cells["Data"].Value.ToString(), "HH:mm dd-MMM", CultureInfo.CurrentCulture);
                if ((DateTime.Now - inspectionTime).TotalHours > 8) continue;
                inspectedLots.Add(row.Cells["Zlecenie"].Value.ToString());
            }

                if (inspectedLots.Count > 0)
                {
                    chart1.Visible = true;
                    DataTable inspectionTable = SqlOperations.DownloadVisInspFromSQL(inspectedLots.ToArray());
                    inspectionData = WasteCalculation.LoadData(inspectionTable, lotModelDict);
                    Charting.DrawChartWasteReasons(inspectionData, chart1);
                    double waste = Efficiency.CalculateWasteLastXXh(dataGridViewHistory, 8);
                    labelWasteLevel.Text = waste.ToString().Replace(",", ".") + "%";

                    //int currentShift = DateOperations.whatDayShiftIsit(DateTime.Now).shift;
                    //labelCurrentShift.Text = "Odpad od początku zmiany " + currentShift + ":";
                    if (waste > 0.7)
                    {
                        panelWasteLevel.Tag = "Alarm";
                    }
                    else
                    {
                        panelWasteLevel.Tag = "";
                    }

                    int qtyThisShift = Efficiency.CalculateQuantityThisShift(dataGridViewHistory);
                    labelQtySinceShiftBegining.Text = qtyThisShift.ToString() + LanguangeTranslation.Translate("szt",radioButtonPolish.Checked);

                    DateTime shiftStart = TimeTools.whatDayShiftIsit(DateTime.Now).shiftStartDate;
                    double qtyPerHour = (double)qtyThisShift / (DateTime.Now - shiftStart).TotalHours;

                    labelQtyPerHour.Text = Math.Round(qtyPerHour, 1).ToString() + LanguangeTranslation.Translate("szt/godz",radioButtonPolish.Checked);
                }
                else
                {
                    chart1.Visible = false;
                }

            
        }

        private void timerAlarm_Tick(object sender, EventArgs e)
        {
            if (panelWasteLevel.Tag != null)
            {
                if (panelWasteLevel.Tag.ToString() == "Alarm")
                {
                    if (panelWasteLevel.BackColor == Color.White)
                    {
                        panelWasteLevel.BackColor = Color.Red;
                    }
                    else
                    {
                        panelWasteLevel.BackColor = Color.White;
                    }
                }
                else
                {
                    panelWasteLevel.BackColor = Color.Lime;
                }
            }
        }

        private void numUpDown_valueChange_shared(object sender, EventArgs e)
        {
            
            NumericUpDown ctrl = (NumericUpDown)sender;
            if (ctrl.Value > 0)
            {
                ctrl.BackColor = ctrl.Parent.BackColor;
                ctrl.ForeColor = Color.White;
            }
            else
            {
                ctrl.BackColor = Color.White;
                ctrl.ForeColor = Color.Black;
            }

            //textBoxGoodQty.Text = recordToSave.IloscDobrych.ToString();
        }

        private void numUpDown_Validated_shared(object sender, EventArgs e)
        {
            
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Dictionary<string, List<string>> testResult = SqlOperations.HowManyModulesTested(textBox1.Text);
                if (testResult["OK"].Count + testResult["NG"].Count > 0) 
                {
                    TestResults testResultForm = new TestResults(testResult, textBox1.Text);
                    testResultForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show(LanguangeTranslation.Translate("Brak danych o testach", radioButtonPolish.Checked));
                }
                textBox1.Text= LanguangeTranslation.Translate("Wyniki testu", radioButtonPolish.Checked);
                textBox1.ForeColor = Color.Silver;
                this.ActiveControl = radioButton2;
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.ForeColor == Color.Silver)
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
            
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text=="")
            {
                textBox1.Text = LanguangeTranslation.Translate("Wyniki testu", radioButtonPolish.Checked);
                textBox1.ForeColor = Color.Silver;
            }
        }

        private void timerEfficiencyAndWaste_Tick(object sender, EventArgs e)
        {
            CalculateWasteAndEff();
        }

        private void UpdateLanguage()
        {
            labelLotNo.Text = LanguangeTranslation.Translate("Numer zlecenia", radioButtonPolish.Checked);
            labelOperator.Text = LanguangeTranslation.Translate("Operator", radioButtonPolish.Checked);
            labelGoodQty.Text = LanguangeTranslation.Translate("Ilość dobrych", radioButtonPolish.Checked);
            buttonSave.Text = LanguangeTranslation.Translate("Zapisz", radioButtonPolish.Checked);
            buttonAddFailure.Text = LanguangeTranslation.Translate("Dodaj wadę", radioButtonPolish.Checked);
            labelOdpadTitle.Text = LanguangeTranslation.Translate("ODPAD", radioButtonPolish.Checked);
            labelLast8h.Text= LanguangeTranslation.Translate("ostatnie 8h:", radioButtonPolish.Checked);
            labelWasteTop5.Text= LanguangeTranslation.Translate("Odpad TOP 5 przyczyn odpadu:", radioButtonPolish.Checked);
            labelEffTitle.Text= LanguangeTranslation.Translate("WYDAJNOŚĆ", radioButtonPolish.Checked);
            labelQtySinceShiftStart.Text= LanguangeTranslation.Translate("Ilość od początku zmiany:", radioButtonPolish.Checked);
            labelEffNorm.Text= LanguangeTranslation.Translate("Norma: 2500 szt./ zm.", radioButtonPolish.Checked);
            labelEffNormHour.Text= LanguangeTranslation.Translate("312 szt/godz", radioButtonPolish.Checked);
            label23.Text= LanguangeTranslation.Translate("Aktualnie średnio", radioButtonPolish.Checked);
            labelLotInfo.Text = labelLotInfo.Text
                .Replace("Dane zlecenia", LanguangeTranslation.Translate("Dane zlecenia", radioButtonPolish.Checked))
                .Replace("Model", LanguangeTranslation.Translate("Model", radioButtonPolish.Checked))
                .Replace("Ilość złączek", LanguangeTranslation.Translate("Ilość złączek", radioButtonPolish.Checked))
                .Replace("Nieznany", LanguangeTranslation.Translate("Nieznany", radioButtonPolish.Checked))
                .Replace("Produkcja", LanguangeTranslation.Translate("Produkcja", radioButtonPolish.Checked));

            foreach (var control in panel2.Controls)
            {
                if (control is Label)
                {
                    Label lbl = (Label)control;
                    lbl.Text = LanguangeTranslation.Translate(lbl.Name, radioButtonPolish.Checked);
                }
            }

            foreach (var control in labelPanel.Controls)
            {
                if (control is TextBox)
                {
                    TextBox txtB = (TextBox)control;
                    txtB.Text = LanguangeTranslation.Translate(txtB.Name, radioButtonPolish.Checked);
                }
            }
        }

        private void radioButtonPolish_CheckedChanged(object sender, EventArgs e)
        {
            UpdateLanguage();
        }

        private string CheckDeviceMonikerString()
        {
            string result = "";
            CaptureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            List<string> devices = new List<string>();
            foreach (FilterInfo dev in CaptureDevice)
            {
                devices.Add(dev.MonikerString);
            }
            string deviceFromSettings = AppSettings.GetSettings("deviceMonikerString");

            if (devices.Contains(deviceFromSettings))
            {
                return deviceFromSettings;
            }
            else
            {
                using (SettingsForm setF = new SettingsForm(CaptureDevice))
                {
                    if (setF.ShowDialog() == DialogResult.OK)
                    {
                        return setF.deviceMonikerString;
                    }
                    else
                    {
                        return "";
                    }
                }
            }
        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            using (SettingsForm setF = new SettingsForm(CaptureDevice))
            {
                if (setF.ShowDialog()== DialogResult.OK)
                {
                    MessageBox.Show(LanguangeTranslation.Translate("Uruchom ponownie aplikację aby użyć nowej kamery", radioButtonPolish.Checked));
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(LanguangeTranslation.Translate("Zamknąć program?", radioButtonPolish.Checked), LanguangeTranslation.Translate("Zakończenie pracy", radioButtonPolish.Checked), MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void timerClock_Tick(object sender, EventArgs e)
        {
            labelClock.Text = System.DateTime.Now.ToString("HH:mm:ss");
        }
    }
}
