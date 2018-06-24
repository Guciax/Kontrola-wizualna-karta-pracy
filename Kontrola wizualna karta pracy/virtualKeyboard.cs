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
    public partial class virtualKeyboard : Form
    {
        private readonly TextBox targetBox;

        public virtualKeyboard(TextBox targetBox)
        {
            InitializeComponent();
            this.targetBox = targetBox;
        }

        private void virtualKeyboard_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            targetBox.Text += btn.Text;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (targetBox.Text.Length > 1)
            {
                targetBox.Text = targetBox.Text.Substring(0, targetBox.Text.Length - 1);
            }
            else
            {
                targetBox.Text = "";
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            targetBox.Text += "";
        }

        private void virtualKeyboard_Leave(object sender, EventArgs e)
        {
            if (!this.ContainsFocus)
            {
                MessageBox.Show("lost");
            }
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button)
                {
                    
                }
            }
        }
    }
}
