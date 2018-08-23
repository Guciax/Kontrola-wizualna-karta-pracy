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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            deviceMonikerString = captureDevice[comboBox1.SelectedIndex].MonikerString;
            AppSettings.AddOrUpdateAppSettings("deviceMonikerString", deviceMonikerString);
        }
    }
}
