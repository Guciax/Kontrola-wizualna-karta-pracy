using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;
using ZXing.Datamatrix;

namespace Kontrola_wizualna_karta_pracy.Forms
{
    public partial class CheckRework : Form
    {
        private readonly string deviceMonikerString;
        private readonly bool langPolish;
        private readonly string[] operatorsList;
        FilterInfoCollection CaptureDevice;
        VideoCaptureDevice FinalFrame;
        Bitmap bitmap;

        public CheckRework(string deviceMonikerString, bool langPolish, string[] operatorsList)
        {
            InitializeComponent();
            this.deviceMonikerString = deviceMonikerString;
            this.langPolish = langPolish;
            this.operatorsList = operatorsList;
        }

        private void CheckRework_Load(object sender, EventArgs e)
        {
            CaptureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            FinalFrame = new VideoCaptureDevice();
            FinalFrame = new VideoCaptureDevice(deviceMonikerString);
            FinalFrame.NewFrame += new NewFrameEventHandler(FinalFrame_NewFrame);
            FinalFrame.NewFrame -= Handle_New_Frame;
            Thread.Sleep(1000);
            FinalFrame.Start();

            labelDecodedQr.Text = LanguangeTranslation.Translate("Zeskanuj kod Qr", langPolish);
            comboBox1.Items.AddRange(operatorsList);
        }

        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            bitmap = (Bitmap)eventArgs.Frame.Clone();
            pictureBox1.Image = bitmap;
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

        Stopwatch stoper = new Stopwatch();
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!stoper.IsRunning)
            {
                stoper.Start();
            }

            int waitTime = 30;

            BarcodeReader Reader = new BarcodeReader();
            MultiFormatReader multiReader = new MultiFormatReader();
            DataMatrixReader dataMatrixReader = new DataMatrixReader();

            Bitmap bitmap = (Bitmap)pictureBox1.Image;
            if (bitmap != null)
            {

                LuminanceSource source = new BitmapLuminanceSource(bitmap);
                BinaryBitmap binaryBitmap = new BinaryBitmap(new HybridBinarizer(source));

                var hints = new Dictionary<DecodeHintType, object>();
                var fmts = new List<BarcodeFormat>();


                fmts.Add(BarcodeFormat.DATA_MATRIX);
                fmts.Add(BarcodeFormat.QR_CODE);
                hints.Add(DecodeHintType.TRY_HARDER, true);
                hints.Add(DecodeHintType.POSSIBLE_FORMATS, fmts);
                multiReader.Hints = hints;

                Result qrResult = multiReader.decode(binaryBitmap);
                Result dataMatrixResult = dataMatrixReader.decode(binaryBitmap);
                Result result = null;

                if (qrResult != null)
                {
                    result = qrResult;
                }
                else
                {
                    result = dataMatrixResult;
                }
                //Result result = Reader.Decode(bitmap);
                //Result result = multiReader.decode(barcodeBitmap);

                if (result != null)
                {
                        string decoded = result.ToString().Trim();
                        if (decoded != "")
                        {
                            CheckDecodedSerial(decoded);
                        }
                }
                else
                {
                    labelDecodedQr.Text = "Zeskanuj kod Qr  ......" + Math.Round(waitTime - stoper.Elapsed.TotalMilliseconds / 1000, 1).ToString() + " sek.";
                    if (stoper.Elapsed.Seconds >= waitTime)
                    {
                        timer1.Stop();
                        stoper.Stop();
                        stoper.Reset();
                        labelDecodedQr.Text = "Nie można odczytać kodu, wpisz ręcznie:";
                        // btnTakePic.Visible = true;
                        textBox1.Visible = true;
                    }
                }

            }
        }

        private void CheckDecodedSerial(string decoded)
        {
            DataTable ngTable = SqlOperations.CheckIfSerialIsInNgTable(decoded);

            timer1.Stop();
            stoper.Stop();
            if (ngTable.Rows.Count == 0)
            {
                MessageBox.Show("Ten numer PCB nie jest zarejestrowany w bazie!");
                ResetForm();
            }
            else
            {
                //serial_no,result,ng_type,datetime,rework_result,rework_datetime,post_rework_vi_result,post_rework_OQA_result
                dataGridView1.Rows.Add("Pierwsza kontrola", ngTable.Rows[0]["datetime"]);
                dataGridView1.Rows.Add("Przyczyna NG", ngTable.Rows[0]["ng_type"]);
                dataGridView1.Rows.Add("Data naprawy", ngTable.Rows[0]["rework_datetime"]);
                dataGridView1.Rows.Add("Wynik naprawy", ngTable.Rows[0]["rework_result"]);
                dataGridView1.Rows.Add("Kontrola po naprawie", ngTable.Rows[0]["post_rework_vi_result"]);
                dataGridView1.Rows.Add("OQA", ngTable.Rows[0]["post_rework_OQA_result"]);
                DgvTools.ColumnsAutoSize(dataGridView1, DataGridViewAutoSizeColumnMode.AllCells);

                string reworkResult = ngTable.Rows[0]["rework_result"].ToString().Trim();
                if (reworkResult == "") { reworkResult = "BRAK"; }

                if (ngTable.Rows[0]["rework_result"].ToString() != "OK")
                {
                    if (MessageBox.Show("Nieprawidłowy wynik naprawy: " + reworkResult) == DialogResult.OK)
                    {
                        ResetForm();
                    }
                }
                else
                {
                    if (ngTable.Rows[0]["post_rework_vi_result"].ToString().Trim() != "")
                    {
                        DialogResult dialogResult = MessageBox.Show("Ten panel ma już status KONTROLA PO NAPRAWIE = " + ngTable.Rows[0]["post_rework_vi_result"] + Environment.NewLine + "Chcesz to zmienić?", "Powtórne sprawdzenie", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.No)
                        {
                            ResetForm();
                        }
                        else
                        {
                            labelDecodedQr.Text = decoded;
                            buttonOK.Visible = true;
                            buttonNG.Visible = true;
                            panel1.BackColor = Color.Lime;
                        }
                    }
                    else
                    {
                        labelDecodedQr.Text = decoded;
                        buttonOK.Visible = true;
                        buttonNG.Visible = true;
                        panel1.BackColor = Color.Lime;
                    }
                }
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Trim() != "")
            {
                SqlOperations.UpdateNgAfterRework(labelDecodedQr.Text, "OK", comboBox1.Text);
                ResetForm();
            }
            else
            {
                MessageBox.Show("Pole operator puste, usupełnij.");
            }
        }

        private void buttonNG_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Trim() != "")
            {
                SqlOperations.UpdateNgAfterRework(labelDecodedQr.Text, "NG", comboBox1.Text);
                ResetForm();
            }
            else
            {
                MessageBox.Show("Pole operator puste, usupełnij.");
            }
        }

        private void CheckRework_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FinalFrame.IsRunning)
            {
                FinalFrame.Stop();
            }
            timer1.Enabled = false;
            this.Dispose();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                CheckDecodedSerial(textBox1.Text);
            }
        }

        private void ResetForm()
        {
            stoper.Reset();
            stoper.Start();
            timer1.Enabled = true;
            dataGridView1.Rows.Clear();
            panel1.BackColor = Color.White;
            buttonNG.Visible = false;
            buttonOK.Visible = false;
        }
    }
}
