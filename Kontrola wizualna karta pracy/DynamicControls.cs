using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kontrola_wizualna_karta_pracy
{
    public class DynamicControls
    {
        public static void CreateControls(FlowLayoutPanel ngPanel, FlowLayoutPanel scrapPanel, FlowLayoutPanel labelPanel, string[] dbColumns, RecordToSave recordToSave, VideoCaptureDevice FinalFrame, List<Image> imagesList, PictureBox mainPicBox, Button camStartStopButton)
        {
            List<string> uniqeColumns = new List<string>();
            foreach (var col in dbColumns)
            {
                string failureName = col.Replace("ng", "").Replace("Ng", "").Replace("NG", "").Replace("scrap", "").Replace("Scrap", "").Replace("SCRAP", "");
                if (uniqeColumns.Contains(failureName)) continue;
                uniqeColumns.Add(failureName);
                Padding pad = new Padding((ngPanel.Width - 30) / 2, 1, (ngPanel.Width - 30) / 2, 1);

                MyTextBox lblBox = new MyTextBox();
                lblBox.Name = col.Replace("ng","").Replace("scrap","");
                Debug.WriteLine(lblBox.Name);
                lblBox.Text = controlToDbTranslation.GetLabelCaptionFromDbColumn(col);
                lblBox.TextAlign = HorizontalAlignment.Center;
                lblBox.Width = labelPanel.Width;
                lblBox.ReadOnly = true;
                lblBox.Cursor = Cursors.Arrow;
                lblBox.SelectionHighlightEnabled = false;
                lblBox.Margin = new Padding(0,1,0,1);
                lblBox.BackColor = System.Drawing.Color.Khaki;
                lblBox.MouseLeave += lblBox_MouseLeave;
                // lblBox.MouseEnter += lblBox_MouseEnter;

                lblBox.MouseEnter += (sender, EventArgs) =>
                {
                    MyTextBox box = (MyTextBox)sender;
                    box.BackColor = System.Drawing.Color.Orange;
                    
                    if (FinalFrame != null)
                    {
                        if (FinalFrame.IsRunning)
                        {
                            FinalFrame.Stop();
                            mainPicBox.Image = null;
                            camStartStopButton.BackgroundImage = Kontrola_wizualna_karta_pracy.Properties.Resources.microscope_OFF;
                        }
                    }

                    foreach (var img in imagesList)
                    {
                        if ((string)img.Tag == box.Name)
                        {
                            mainPicBox.Image = img;
                            break;
                        }
                    }
                };


                MyTextBox ngBox = new MyTextBox();
                ngBox.Name = "ng" + failureName;
                ngBox.Text = "0";
                ngBox.TextAlign = HorizontalAlignment.Center;
                ngBox.ReadOnly = true;
                ngBox.SelectionHighlightEnabled = false;
                ngBox.Width = 30;
                ngBox.Margin = pad;
                ngBox.DataBindings.Add("Text", recordToSave, ngBox.Name, false, DataSourceUpdateMode.OnPropertyChanged);
                ngBox.Tag = lblBox.Text;
                ngBox.Cursor = Cursors.Arrow;
                ngBox.Tag = new List<ngBoxTag>();
                ngBox.MouseClick += NgBox_MouseClick;
                ngBox.TextChanged += NgBox_TextChanged;

                if (!col.ToLower().Contains("elektrycz"))
                {
                    MyTextBox scrapBox = new MyTextBox();
                    scrapBox.Name = "scrap" + failureName;
                    scrapBox.Text = "0";
                    scrapBox.TextAlign = HorizontalAlignment.Center;
                    scrapBox.ReadOnly = true;
                    scrapBox.SelectionHighlightEnabled = false;
                    scrapBox.Width = 30;
                    scrapBox.Margin = pad;
                    scrapBox.DataBindings.Add("Text", recordToSave, scrapBox.Name, false, DataSourceUpdateMode.OnPropertyChanged);
                    scrapBox.Tag = lblBox.Text;
                    scrapBox.Cursor = Cursors.Arrow;
                    scrapBox.Tag = new List<ngBoxTag>();
                    scrapBox.MouseClick+= NgBox_MouseClick;
                    scrapBox.TextChanged += ScrapBox_TextChanged;
                    scrapPanel.Controls.Add(scrapBox);
                }
                ngPanel.Controls.Add(ngBox);
                labelPanel.Controls.Add(lblBox);
            }
        }

        private static void ScrapBox_TextChanged(object sender, EventArgs e)
        {
            MyTextBox scrapBox = (MyTextBox)sender;
            if (scrapBox.Text == "0")
            {
                scrapBox.BackColor = Color.White;
                scrapBox.ForeColor = Color.Black;
            }
            else
            {
                scrapBox.BackColor = Color.Black;
                scrapBox.ForeColor = Color.White;
            }
        }

        private static void NgBox_TextChanged(object sender, EventArgs e)
        {
            MyTextBox ngbox = (MyTextBox)sender;
            if (ngbox.Text=="0")
            {
                ngbox.BackColor = Color.White;
                ngbox.ForeColor = Color.Black;
            }
            else
            {
                ngbox.BackColor = Color.Red;
                ngbox.ForeColor = Color.White;
            }
        }

        private static void NgBox_MouseClick(object sender, MouseEventArgs e)
        {
            MyTextBox ngbox = (MyTextBox)sender;
            List<ngBoxTag> defectList = (List<ngBoxTag>)ngbox.Tag;
            if (defectList.Count > 0) 
            {
                ShowDefectsForm defectsForm = new ShowDefectsForm(defectList, true, ngbox.Name);
                if (defectsForm.ShowDialog()== DialogResult.OK)
                {
                    ngbox.Tag = defectsForm.newListOfDefects;
                    ngbox.Text = defectsForm.newListOfDefects.Count.ToString();
                }
            }
        }

        private static void lblBox_MouseLeave(object sender, EventArgs e)
        {
            MyTextBox box = (MyTextBox)sender;
            box.BackColor = System.Drawing.Color.Khaki;
        }

        private static void lblBox_MouseEnter(object sender, EventArgs e)
        {
            
            
        }

        public class MyTextBox : TextBox
        {
            public MyTextBox()
            {
                SelectionHighlightEnabled = true;
            }
            const int WM_SETFOCUS = 0x0007;
            const int WM_KILLFOCUS = 0x0008;
            [DefaultValue(true)]
            public bool SelectionHighlightEnabled { get; set; }
            protected override void WndProc(ref Message m)
            {
                if (m.Msg == WM_SETFOCUS && !SelectionHighlightEnabled)
                    m.Msg = WM_KILLFOCUS;

                base.WndProc(ref m);
            }
        }

    }
}
