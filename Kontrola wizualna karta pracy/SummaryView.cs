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
    public partial class SummaryView : Form
    {
        private readonly string summary;

        public bool OK { get; set; }

        public SummaryView(string summary)
        {
            InitializeComponent();
            this.summary = summary;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.OK = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.OK = false;
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void SummaryView_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = summary;
        }
    }
}
