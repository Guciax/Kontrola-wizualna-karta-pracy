using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;

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
        List<Image> imageList = new List<Image>();

        public NewFailureForm(string[] ngButtons, string[] scrapButtons, string lotNumber, string fixedDate)
        {
            InitializeComponent();
            CreateWasteReasonButtons(ngButtons, scrapButtons);

            LotNumber = lotNumber;
            FixedDate = fixedDate;
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
        }

        private void NewFailureForm_Load(object sender, EventArgs e)
        {
            CaptureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            FinalFrame = new VideoCaptureDevice();
            FinalFrame = new VideoCaptureDevice(CaptureDevice[0].MonikerString);
            FinalFrame.NewFrame += new NewFrameEventHandler(FinalFrame_NewFrame);
            FinalFrame.NewFrame -= Handle_New_Frame;
            Thread.Sleep(1000);
            FinalFrame.Start();
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

        private void btnTakePic_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Controls.Count < 6)
            {
                Image img = pictureBox1.Image;

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
            }
            else
            {
                MessageBox.Show("Max 5 zdjęć. Skasuj zdjęcia aby dodać nowe.");
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
            if (labelDecodedQr.Text == "Zeskanuj kod Qr")
            {
                MessageBox.Show("Zeskanuj kod Qr");
                return false;
            }
            if (buttonClicked == "")
            {
                MessageBox.Show("Wbierz przyczynę błędu");
                return false;
            }
            if (flowLayoutPanel1.Controls.Count<2)
            {
                MessageBox.Show("Dodaj zdjęcia wad");
                return false;
            }
            
                        return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsFormReadyTosave())
            {
                SaveImagesToFiles(imageList);
                Debug.WriteLine(buttonClicked);
                this.DialogResult = DialogResult.OK;
            }
        }
        Stopwatch stoper = new Stopwatch();
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!stoper.IsRunning)
            {
                stoper.Start();
            }

            BarcodeReader Reader = new BarcodeReader();
            Bitmap bitmap = (Bitmap)pictureBox1.Image;
            if (bitmap != null)
            {
                Result result = Reader.Decode(bitmap);
                if (result != null)
                {
                    try
                    {
                        string decoded = result.ToString().Trim();
                        if (decoded != "")
                        {
                            timer1.Stop();
                            stoper.Stop();
                            stoper.Reset();
                            labelDecodedQr.Text = decoded;
                            btnTakePic.Visible = true;
                            panelQr.BackColor = Color.Lime;
                        }
                    }

                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message + " - " + ex.HResult);
                    }
                }
                else
                {
                    if (stoper.Elapsed.Seconds > 15) 
                    {
                        timer1.Stop();
                        stoper.Stop();
                        stoper.Reset();
                        labelDecodedQr.Text = "Nie można odczytać kodu, wpisz ręcznie:";
                        btnTakePic.Visible = true;
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
                btn.Text = controlToDbTranslation.GetLabelCaptionFromDbColumn(ngButton);
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
                btn.Text = controlToDbTranslation.GetLabelCaptionFromDbColumn(scrapButton);
                btn.Width = flpScrapButtons.Width - 6;
                btn.Margin = new Padding(3);
                btn.Height = 50;
                btn.ForeColor = Color.White;
                btn.FlatStyle = FlatStyle.Flat;
                btn.MouseClick += btn_MouseClick;

                flpScrapButtons.Controls.Add(btn);
            }
        }

        private void SaveImagesToFiles(List<Image> imgList)
        {
            var imgFolderPath = ConfigurationManager.AppSettings["ImgPath"] + "\\" + FixedDate + "\\" + LotNumber+"\\";
            System.IO.Directory.CreateDirectory(imgFolderPath);
            string pcbId = labelDecodedQr.Text;
            if (textBox1.Text != "") 
            {
                pcbId = textBox1.Text;
            }
            for(int i=0;i<imgList.Count;i++)
            {
                Image img = imgList[i];
                var saveBmp = new Bitmap(img);
                saveBmp.Save(imgFolderPath + pcbId + "_" + i + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }
    }
}
