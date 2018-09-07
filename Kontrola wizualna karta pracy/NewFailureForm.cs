using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using ZXing.Maxicode;
using ZXing.Common;
using ZXing.Datamatrix;
using ZXing.Multi;
using System.Collections;

namespace Kontrola_wizualna_karta_pracy
{
    public partial class NewFailureForm : Form
    {
        FilterInfoCollection CaptureDevice;
        VideoCaptureDevice FinalFrame;
        Bitmap bitmap;
        public string buttonClicked { get; set; }
        public string LotNumber { get; }
        public string FixedDate { get; }
        public string[] PcbsInCurrentLot { get; }
        public bool LangPolish { get; }
        public string DeviceMonikerString { get; }
        public string serialNo = "";
        public List<Image> imageList = new List<Image>();

        public NewFailureForm(string[] ngButtons, string[] scrapButtons, string lotNumber, string fixedDate, string[] pcbsInCurrentLot,bool langPolish, string deviceMonikerString, bool rotateCam180, List<string> listOfNgPcbSerials)
        {
            InitializeComponent();
            this.ngButtons = ngButtons;
            this.scrapButtons = scrapButtons;
            LotNumber = lotNumber;
            FixedDate = fixedDate;
            PcbsInCurrentLot = pcbsInCurrentLot;
            if (PcbsInCurrentLot==null)
            {
                PcbsInCurrentLot = new string[0];
            }
            LangPolish = langPolish;
            DeviceMonikerString = deviceMonikerString;
            this.rotateCam180 = rotateCam180;
            this.listOfNgPcbSerials = listOfNgPcbSerials;
            //CreateWasteReasonButtons(ngButtons, scrapButtons);
        }

        private void NewFailureForm_Load(object sender, EventArgs e)
        {
            CaptureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            FinalFrame = new VideoCaptureDevice();
            FinalFrame = new VideoCaptureDevice(DeviceMonikerString);
            FinalFrame.NewFrame += new NewFrameEventHandler(FinalFrame_NewFrame);
            FinalFrame.NewFrame -= Handle_New_Frame;
            Thread.Sleep(1000);
            FinalFrame.Start();

            labelDecodedQr.Text = LanguangeTranslation.Translate("Zeskanuj kod Qr", LangPolish);
            button1.Text = LanguangeTranslation.Translate("Zapisz", LangPolish);
            btnTakePic.Text = LanguangeTranslation.Translate("+zdjęcie", LangPolish);
            //this.ActiveControl = btnTakePic;
        }


        Button prevBtn = null;
        private void btn_MouseClick(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            btn.BackColor = Color.White;
            btn.ForeColor = Color.Black;
            btn.Font = new Font(btn.Font, FontStyle.Bold);

            if (prevBtn != null)
            {
                prevBtn.BackColor = prevBtn.Parent.BackColor;
                prevBtn.ForeColor = prevBtn.Parent.ForeColor;
                prevBtn.Font = new Font(prevBtn.Font, FontStyle.Regular);

            }
            prevBtn = btn;
            this.buttonClicked = btn.Name;
            btnTakePic.Visible = true;
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
            if (rotateCam180)
            {
                bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
            }
            pictureBox1.Image = bitmap;
        }

        private void btnTakePic_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Controls.Count < 11)
            {
                Image img = pictureBox1.Image;
                img.Tag = serialNo + "_" + buttonClicked + '_' + flowLayoutPanel1.Controls.Count;
                imageList.Add(img);

                PictureBox picBx = new PictureBox();
                picBx.Name = (imageList.Count - 1).ToString();
                picBx.Margin = new Padding(2);
                picBx.Height = flowLayoutPanel1.Height - 4;
                picBx.Width = img.Width / img.Height * picBx.Height;
                picBx.Image = img;
                picBx.SizeMode = PictureBoxSizeMode.Zoom;
                picBx.MouseEnter += picBx_MouseEnter;
                picBx.MouseLeave += picBx_MouseLeave;
                picBx.MouseClick += picBx_MouseClick;
                picBx.BorderStyle = BorderStyle.FixedSingle;

                flowLayoutPanel1.Controls.Add(picBx);
                button1.Visible = true;
            }
            else
            {
                MessageBox.Show(LanguangeTranslation.Translate("Max 10 zdjęć. Skasuj zdjęcia aby dodać nowe.", LangPolish));
            }
        }

        private void picBx_MouseClick(object sender, MouseEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            int index = int.Parse(pb.Name);
            imageList.RemoveAt(index);
            flowLayoutPanel1.Controls.Remove(pb);
        }

        private void picBx_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            if (pb.Tag != null)
            {
                pb.Image = (Bitmap)pb.Tag;
                pb.SizeMode = PictureBoxSizeMode.Zoom;
            }
            FinalFrame.Start();
        }

        private void picBx_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            pb.Tag = pb.Image;
            pb.SizeMode = PictureBoxSizeMode.CenterImage;
            pb.Image = Kontrola_wizualna_karta_pracy.Properties.Resources.delete_1_icon;
            FinalFrame.Stop();
            pictureBox1.Image = (Image)pb.Tag;
        }

        private void NewFailureForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FinalFrame.IsRunning)
            {
                FinalFrame.Stop();
            }
            timer1.Enabled = false;
            this.Dispose();
        }

        private bool IsFormReadyTosave()
        {
            if (labelDecodedQr.Text.Contains(" "))
            {
                MessageBox.Show(LanguangeTranslation.Translate("Zeskanuj kod Qr", LangPolish));
                return false;
            }
            if (buttonClicked == "")
            {
                MessageBox.Show(LanguangeTranslation.Translate("Wbierz przyczynę wady", LangPolish));
                return false;
            }
            if (flowLayoutPanel1.Controls.Count<2)
            {
                MessageBox.Show(LanguangeTranslation.Translate("Dodaj zdjęcia wad", LangPolish));
                return false;
            }
            
           return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsFormReadyTosave())
            {
                //SaveImagesToFiles(imageList);
                //Debug.WriteLine(buttonClicked);
                this.DialogResult = DialogResult.OK;
            }
            
        }
        Stopwatch stoper = new Stopwatch();
        private readonly string[] ngButtons;
        private readonly string[] scrapButtons;
        private readonly bool rotateCam180;
        private readonly List<string> listOfNgPcbSerials;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!stoper.IsRunning)
            {
                stoper.Start();
            }

            int waitTime = 30;
#if DEBUG
            waitTime = 30;
#endif

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

                if (qrResult!=null)
                {
                    result = qrResult;
                }else
                {
                    result = dataMatrixResult;
                }
                    //Result result = Reader.Decode(bitmap);
                    //Result result = multiReader.decode(barcodeBitmap);

                    if (result != null)
                    {
                        try
                        {
                            string decoded = result.ToString().Trim();
                            if (decoded != "")
                            {
                                bool checkSerials = AppSettings.GetSettings("SprawdzajSerial") == "ON";
                                if (!PcbsInCurrentLot.Contains(decoded) & PcbsInCurrentLot.Length > 0 & checkSerials)
                                {
                                    labelDecodedQr.Text = LanguangeTranslation.Translate("Ten numer PCB należy do innego zlecenia!", LangPolish);
                                    panelQr.BackColor = Color.Red;
                                    panelQr.ForeColor = Color.White;
                                    timer1.Enabled = false;
                                }
                                else
                                {
                                    if (listOfNgPcbSerials.Contains(decoded))
                                    {
                                        this.Close();
                                        MessageBox.Show(LanguangeTranslation.Translate("Ten numer PCB jest już dodany", LangPolish));
                                    }
                                    timer1.Stop();
                                    stoper.Stop();
                                    stoper.Reset();
                                    labelDecodedQr.Text = decoded;
                                    //btnTakePic.Visible = true;
                                    panelQr.BackColor = Color.Lime;
                                    panelQr.ForeColor = Color.Black;
                                    System.Windows.Forms.Clipboard.SetText(decoded);
                                    serialNo = decoded;
                                    
                                CreateWasteReasonButtons(ngButtons, scrapButtons);
                            }
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.Message + " - " + ex.HResult);
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
                            labelDecodedQr.Text = LanguangeTranslation.Translate("Nie można odczytać kodu, wpisz ręcznie:", LangPolish);
                           // btnTakePic.Visible = true;
                            textBox1.Visible = true;
                        }
                    }
                
            }
        }

        private void CreateWasteReasonButtons(string[] ngButtons, string[] scrapButtons)
        {
            foreach (var ngButton in ngButtons)
            {
                Button btn = new Button();
                btn.Name = ngButton;
                btn.Text = LanguangeTranslation.Translate(ngButton.Replace("ng", ""), LangPolish);//controlToDbTranslation.GetLabelCaptionFromDbColumn(ngButton);
                btn.Width = flpNgButtons.Width - 6;
                btn.Margin = new Padding(3);
                btn.Height = 50;
                btn.ForeColor = Color.White;
                btn.FlatStyle = FlatStyle.Flat;
                btn.MouseClick += btn_MouseClick;
                flpNgButtons.Controls.Add(btn);
            }

            foreach (var scrapButton in scrapButtons)
            {
                Button btn = new Button();
                btn.Name = scrapButton;
                btn.Text = LanguangeTranslation.Translate(scrapButton.Replace("scrap", ""), LangPolish);
                btn.Width = flpScrapButtons.Width - 6;
                btn.Margin = new Padding(3);
                btn.Height = 50;
                btn.ForeColor = Color.White;
                btn.FlatStyle = FlatStyle.Flat;
                btn.MouseClick += btn_MouseClick;
                flpScrapButtons.Controls.Add(btn);
            }
        }

        private static void ConnectPDrive()
        {
            Process myProcess = new Process();
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            myProcess.StartInfo.CreateNoWindow = true;
            myProcess.StartInfo.UseShellExecute = false;
            myProcess.StartInfo.FileName = "cmd.exe";
            myProcess.StartInfo.Arguments = @"/c net use P: \\mstms005\shared /user:eprod plfm!234 /PERSISTENT:NO";
            myProcess.EnableRaisingEvents = true;
            myProcess.Start();
            myProcess.WaitForExit();
        }

        private void SaveImagesToFiles(List<Image> imgList)
        {
            var imgFolderPath = Path.Combine(AppSettings.GetSettings("ImgPath"), FixedDate, LotNumber);
            if (Path.GetPathRoot(imgFolderPath).StartsWith("P"))
            {
                if (!Directory.Exists("P:\\"))
                {
                    ConnectPDrive();
                }
            }
            if (!Directory.Exists(imgFolderPath))
            {
                System.IO.Directory.CreateDirectory(imgFolderPath);
            }

            string pcbId = labelDecodedQr.Text;
            if (textBox1.Text != "")
            {
                pcbId = textBox1.Text;
            }
            for (int i = 0; i < imgList.Count; i++)
            {
                Image img = imgList[i];
                var saveBmp = new Bitmap(img);
                saveBmp.Save(imgFolderPath + "\\" + pcbId + "_" + i + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                if (listOfNgPcbSerials.Contains(textBox1.Text))
                {
                    MessageBox.Show(LanguangeTranslation.Translate("Ten numer PCB jest już dodany", LangPolish));
                    this.Close();
                }
                labelDecodedQr.Text = textBox1.Text;
                textBox1.Visible = false;
                serialNo = textBox1.Text;
                button1.Visible = true;
                CreateWasteReasonButtons(ngButtons, scrapButtons);
            }
        }

        private void btnTakePic_Leave(object sender, EventArgs e)
        {
            //this.ActiveControl = btnTakePic;
        }

        private void btnTakePic_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                btnTakePic.PerformClick();
            }
        }

        
    }
}
