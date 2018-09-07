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
    public partial class ShowDefectsForm : Form
    {
        private readonly List<ngBoxTag> listOfDefects;
        private readonly bool langPolish;
        private readonly string failureName;
        public List<ngBoxTag> newListOfDefects;

        public ShowDefectsForm(List<ngBoxTag> listOfDefects, bool langPolish, string failureName)
        {
            InitializeComponent();
            this.listOfDefects = listOfDefects;
            this.langPolish = langPolish;
            this.failureName = failureName;
        }

        private void ShowDefectsForm_Load(object sender, EventArgs e)
        {
            var maxImages = listOfDefects.Select(list => list.Images.Count).Max();
            DataGridViewButtonColumn buttonCol = new DataGridViewButtonColumn();
            buttonCol.Name = "buttonCol";
            buttonCol.HeaderText = "Usuń";
            dataGridView1.Columns.Add(buttonCol);
            dataGridView1.Columns.Add("serial", "serial");
            dataGridView1.Columns.Add("data", "Data");
            DataGridViewImageColumn imageCol = new DataGridViewImageColumn();
            imageCol.Name = "imageCol";
            imageCol.HeaderText = "Zdjęcia";
            imageCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
            dataGridView1.Columns.Add(imageCol);
            labelFailureName.Text = failureName;
            

            foreach (var defect in listOfDefects)
            {
                Bitmap bitmap = new Bitmap(defect.Images[0].Width * defect.Images.Count, defect.Images[0].Height);

                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    for (int i = 0; i < defect.Images.Count; i++)
                    {
                        g.DrawImage(defect.Images[i], i* defect.Images[0].Width, 0);
                    }
                   
                }
                DataGridViewButtonCell buttonCell = new DataGridViewButtonCell();

                
                dataGridView1.Rows.Add(buttonCell,defect.SerialNo, defect.DateTimeString, bitmap);
                dataGridView1.Rows[dataGridView1.Rows.GetLastRow(DataGridViewElementStates.None)].Height = 150;
                dataGridView1.Rows[dataGridView1.Rows.GetLastRow(DataGridViewElementStates.None)].Cells[0].Value = LanguangeTranslation.Translate("USUŃ", langPolish);
            }
            DgvTools.ColumnsAutoSize(dataGridView1, DataGridViewAutoSizeColumnMode.AllCellsExceptHeader);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex==0)
            {
                dataGridView1.Rows.RemoveAt(e.RowIndex);
                listOfDefects.RemoveAt(e.RowIndex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            newListOfDefects = listOfDefects;
        }
    }
}
