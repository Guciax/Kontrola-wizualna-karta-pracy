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
    public partial class TestResults : Form
    {
        private readonly Dictionary<string, List<string>> testResults;
        private readonly string lotId;

        public TestResults(Dictionary<string, List<string>> testResults, string lotId)
        {
            InitializeComponent();
            this.testResults = testResults;
            this.lotId = lotId;
        }

        private void TestResults_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("OK", "OK");
            dataGridView1.Columns.Add("NG", "NG");
            
            dataGridView1.Rows.Add(Math.Max(testResults["OK"].Count, testResults["NG"].Count));
            for (int i = 0; i < testResults["OK"].Count; i++)
            {
                dataGridView1.Rows[i].Cells["OK"].Value = testResults["OK"][i];
            }

            for (int i = 0; i < testResults["NG"].Count; i++)
            {
                dataGridView1.Rows[i].Cells["NG"].Value = testResults["NG"][i];
            }

            int ngCount = testResults["NG"].Count;
            int okCount = testResults["OK"].Count;
            label1.Text = "LOT: " + lotId + "          OK: " + okCount  + "          NG: " + ngCount;

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        
    }
}
