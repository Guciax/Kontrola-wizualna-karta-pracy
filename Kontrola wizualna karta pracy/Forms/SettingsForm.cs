using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kontrola_wizualna_karta_pracy
{
    public partial class SettingsForm : Form
    {
        private readonly FilterInfoCollection captureDevice;
        public string deviceMonikerString = "";
        string cameraState = "";
        string serialToLotConnection = "";

        public SettingsForm(FilterInfoCollection CaptureDevice)
        {
            InitializeComponent();
            captureDevice = CaptureDevice;

        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            foreach (FilterInfo device in captureDevice)
            {
                comboBox1.Items.Add(device.Name);
            }
            if (comboBox1.Items.Count==0)
            {
                MessageBox.Show("Nie wykryto kamery!");
            }

            
            string rotateSettings = AppSettings.GetSettings("camera180Rotate");

            if (rotateSettings == "ON") 
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }

            cameraState = AppSettings.GetSettings("Camera_ON_OFF");
            if(cameraState=="ON")
            {
                checkBox2.Checked = true;
            }
            else
            {
                checkBox2.Checked = false;
            }

            serialToLotConnection = AppSettings.GetSettings("SprawdzajSerial");
            if (serialToLotConnection == "ON")
            {
                checkBox3.Checked = true;
            }
            else
            {
                checkBox3.Checked = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0)
            {
                deviceMonikerString = captureDevice[comboBox1.SelectedIndex].MonikerString;
                AppSettings.AddOrUpdateAppSettings("deviceMonikerString", deviceMonikerString);
            }

            string rotationSetting = "OFF";
            if (checkBox1.Checked)
            {
                rotationSetting = "ON";
            }
            AppSettings.AddOrUpdateAppSettings("camera180Rotate", rotationSetting);

            string cameraOn = "OFF";
            if (checkBox2.Checked)
            {
                cameraOn = "ON";
            }
            AppSettings.AddOrUpdateAppSettings("Camera_ON_OFF", cameraOn);
            if (cameraOn != cameraState)
            {
                MessageBox.Show("Uruchom ponownie aplikację");
            }

            if (checkBox3.Checked)
            {
                AppSettings.AddOrUpdateAppSettings("SprawdzajSerial", "ON");
            }
            else
            {
                AppSettings.AddOrUpdateAppSettings("SprawdzajSerial", "OFF");
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            VideoCaptureDevice dev = new VideoCaptureDevice(captureDevice[comboBox1.SelectedIndex].MonikerString);
            foreach (var res in dev.VideoCapabilities)
            {
                comboBox2.Items.Add(res.FrameSize.Width + "x" + res.FrameSize.Width);
            }
        }
    }
}
