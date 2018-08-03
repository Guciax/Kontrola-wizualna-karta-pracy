using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kontrola_wizualna_karta_pracy
{
    public partial class SummaryView : Form
    {
        private readonly RecordToSave recordToSave;

        public bool OK { get; set; }

        public SummaryView(RecordToSave recordToSave)
        {
            InitializeComponent();
            this.recordToSave = recordToSave;
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
            int allNg = RecordToSaveCalculations.GetAllNg2(recordToSave);
            double ngPercentage = Math.Round((double)allNg / (double)(recordToSave.IloscDobrych + allNg)*100,2);

            labelNgPercentage.Text = "ODPAD" +Environment.NewLine+ngPercentage + "%";

            if (ngPercentage>0.7)
            {
                panelNgPercentage.BackColor = Color.Red;
                panelNgPercentage.ForeColor = Color.White;
            }
            else
            {
                panelNgPercentage.BackColor = Color.Lime;
                panelNgPercentage.ForeColor = Color.Black;
            }

            labelSummary.Text   += Environment.NewLine + "Ilość dobrych: " + recordToSave.IloscDobrych
                                + Environment.NewLine + "Numer zlecenia: " + recordToSave.NumerZlecenia
                                + Environment.NewLine + "Operator: " + recordToSave.Operator;

            PropertyInfo[] properties = typeof(RecordToSave).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.Name.ToLower().StartsWith("ng"))
                {
                    if ((int)property.GetValue(recordToSave) > 0)
                    {
                        labelNg.Text += Environment.NewLine + controlToDbTranslation.GetLabelCaptionFromDbColumn(property.Name) + ": " + property.GetValue(recordToSave);
                    }
                }
                else if (property.Name.ToLower().StartsWith("scrap"))
                {
                    if ((int)property.GetValue(recordToSave) > 0)
                    {
                        labelScrap.Text += Environment.NewLine + controlToDbTranslation.GetLabelCaptionFromDbColumn( property.Name) + ": " + property.GetValue(recordToSave);
                    }
                }
            }
        }
    }
}
