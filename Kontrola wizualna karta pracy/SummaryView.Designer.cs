namespace Kontrola_wizualna_karta_pracy
{
    partial class SummaryView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelSummary = new System.Windows.Forms.Label();
            this.labelNg = new System.Windows.Forms.Label();
            this.labelScrap = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panelNgPercentage = new System.Windows.Forms.Panel();
            this.labelNgPercentage = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panelNgPercentage.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Left;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(357, 64);
            this.button1.TabIndex = 0;
            this.button1.Text = "TAK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 473);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(724, 64);
            this.panel1.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button2.Location = new System.Drawing.Point(357, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(367, 64);
            this.button2.TabIndex = 1;
            this.button2.Text = "NIE";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 162);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(724, 311);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // labelSummary
            // 
            this.labelSummary.AutoSize = true;
            this.labelSummary.BackColor = System.Drawing.Color.Transparent;
            this.labelSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelSummary.Location = new System.Drawing.Point(12, 9);
            this.labelSummary.Name = "labelSummary";
            this.labelSummary.Size = new System.Drawing.Size(399, 26);
            this.labelSummary.TabIndex = 7;
            this.labelSummary.Text = "SPRAWDŹ POPRAWNOŚĆ DANYCH:";
            // 
            // labelNg
            // 
            this.labelNg.AutoSize = true;
            this.labelNg.BackColor = System.Drawing.Color.Transparent;
            this.labelNg.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelNg.ForeColor = System.Drawing.Color.White;
            this.labelNg.Location = new System.Drawing.Point(9, 10);
            this.labelNg.Name = "labelNg";
            this.labelNg.Size = new System.Drawing.Size(51, 26);
            this.labelNg.TabIndex = 8;
            this.labelNg.Text = "NG:";
            // 
            // labelScrap
            // 
            this.labelScrap.AutoSize = true;
            this.labelScrap.BackColor = System.Drawing.Color.Transparent;
            this.labelScrap.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelScrap.ForeColor = System.Drawing.Color.White;
            this.labelScrap.Location = new System.Drawing.Point(13, 10);
            this.labelScrap.Name = "labelScrap";
            this.labelScrap.Size = new System.Drawing.Size(95, 26);
            this.labelScrap.TabIndex = 9;
            this.labelScrap.Text = "SCRAP:";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Red;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.labelNg);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(356, 305);
            this.panel2.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.labelScrap);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(365, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(356, 305);
            this.panel3.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.labelSummary);
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(550, 154);
            this.panel4.TabIndex = 8;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 158F));
            this.tableLayoutPanel2.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panelNgPercentage, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(724, 160);
            this.tableLayoutPanel2.TabIndex = 9;
            // 
            // panelNgPercentage
            // 
            this.panelNgPercentage.BackColor = System.Drawing.Color.Lime;
            this.panelNgPercentage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNgPercentage.Controls.Add(this.labelNgPercentage);
            this.panelNgPercentage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNgPercentage.Location = new System.Drawing.Point(559, 3);
            this.panelNgPercentage.Name = "panelNgPercentage";
            this.panelNgPercentage.Size = new System.Drawing.Size(162, 154);
            this.panelNgPercentage.TabIndex = 9;
            // 
            // labelNgPercentage
            // 
            this.labelNgPercentage.BackColor = System.Drawing.Color.Transparent;
            this.labelNgPercentage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelNgPercentage.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelNgPercentage.ForeColor = System.Drawing.Color.White;
            this.labelNgPercentage.Location = new System.Drawing.Point(0, 0);
            this.labelNgPercentage.Name = "labelNgPercentage";
            this.labelNgPercentage.Size = new System.Drawing.Size(160, 152);
            this.labelNgPercentage.TabIndex = 10;
            this.labelNgPercentage.Text = "ODPAD";
            this.labelNgPercentage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SummaryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 537);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Name = "SummaryView";
            this.Text = "Podsumowanie";
            this.Load += new System.EventHandler(this.SummaryView_Load);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panelNgPercentage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label labelScrap;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelNg;
        private System.Windows.Forms.Label labelSummary;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panelNgPercentage;
        private System.Windows.Forms.Label labelNgPercentage;
    }
}