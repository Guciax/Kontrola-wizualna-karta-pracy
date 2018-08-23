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
        private readonly bool languagePolish;
        int allNg = 0;
        public bool OK { get; set; }

        public SummaryView(RecordToSave recordToSave, bool languagePolish)
        {
            InitializeComponent();
            this.recordToSave = recordToSave;
            this.languagePolish = languagePolish;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (allNg > 5) 
            {
                MessageBox.Show("Odpad powyżej 5NG" + Environment.NewLine + "Powiadom Lidera Zmiany!");
            }
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
            labelSummary.Text = LanguangeTranslation.Translate("SPRAWDŹ POPRAWNOŚĆ DANYCH:", languagePolish);

            allNg = RecordToSaveCalculations.GetAllNg2(recordToSave);
            
            double ngPercentage = Math.Round((double)allNg / (double)(recordToSave.IloscDobrych + allNg)*100,2);

            string wasteString = LanguangeTranslation.Translate("ODPAD", languagePolish);
            labelNgPercentage.Text = wasteString + Environment.NewLine+ngPercentage + "%";

            buttonYes.Text = LanguangeTranslation.Translate("TAK", languagePolish);
            buttonNo.Text = LanguangeTranslation.Translate("NIE", languagePolish);

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

            string gooQtyString = LanguangeTranslation.Translate("Ilość dobrych", languagePolish);
            string lotNoString = LanguangeTranslation.Translate("Numer zlecenia", languagePolish);
            string operatorString = LanguangeTranslation.Translate("Operator", languagePolish);

            labelSummary.Text   += Environment.NewLine + gooQtyString + ": " + recordToSave.IloscDobrych
                                + Environment.NewLine + lotNoString + ": " + recordToSave.NumerZlecenia
                                + Environment.NewLine + operatorString+": " + recordToSave.Operator;

            string ngSummary = "";
            string scrapSummary = "";
            int totalNg = 0;
            int totalScrap = 0;
            PropertyInfo[] properties = typeof(RecordToSave).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.Name.ToLower().StartsWith("ng"))
                {
                    if ((int)property.GetValue(recordToSave) > 0)
                    {
                        ngSummary +=Environment.NewLine+ controlToDbTranslation.GetLabelCaptionFromDbColumn(property.Name) + ": " + property.GetValue(recordToSave);
                        totalNg += (int)property.GetValue(recordToSave);
                    }
                }
                else if (property.Name.ToLower().StartsWith("scrap"))
                {
                    if ((int)property.GetValue(recordToSave) > 0)
                    {
                        scrapSummary+= Environment.NewLine + controlToDbTranslation.GetLabelCaptionFromDbColumn( property.Name) + ": " + property.GetValue(recordToSave);
                        totalScrap += (int)property.GetValue(recordToSave);
                    }
                }
            }

            labelNg.Text = "NG - " + totalNg + ngSummary;
            labelScrap.Text = "SCRAP - " + totalScrap + scrapSummary;
        }
    }
}
