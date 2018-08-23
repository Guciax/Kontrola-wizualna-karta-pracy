namespace Kontrola_wizualna_karta_pracy
{
    partial class NewFailureForm
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
            this.components = new System.ComponentModel.Container();
            this.panelQr = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.labelDecodedQr = new System.Windows.Forms.Label();
            this.btnTakePic = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.flpScrapButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.flpNgButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panelQr.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelQr
            // 
            this.panelQr.Controls.Add(this.textBox1);
            this.panelQr.Controls.Add(this.labelDecodedQr);
            this.panelQr.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelQr.Location = new System.Drawing.Point(116, 0);
            this.panelQr.Name = "panelQr";
            this.panelQr.Size = new System.Drawing.Size(1142, 66);
            this.panelQr.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBox1.Location = new System.Drawing.Point(698, 22);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(441, 35);
            this.textBox1.TabIndex = 1;
            this.textBox1.Visible = false;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // labelDecodedQr
            // 
            this.labelDecodedQr.AutoSize = true;
            this.labelDecodedQr.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelDecodedQr.Location = new System.Drawing.Point(17, 22);
            this.labelDecodedQr.Name = "labelDecodedQr";
            this.labelDecodedQr.Size = new System.Drawing.Size(189, 29);
            this.labelDecodedQr.TabIndex = 0;
            this.labelDecodedQr.Text = "Zeskanuj kod Qr";
            // 
            // btnTakePic
            // 
            this.btnTakePic.Location = new System.Drawing.Point(3, 3);
            this.btnTakePic.Name = "btnTakePic";
            this.btnTakePic.Size = new System.Drawing.Size(75, 77);
            this.btnTakePic.TabIndex = 0;
            this.btnTakePic.Text = "+ zdjęcie";
            this.btnTakePic.UseVisualStyleBackColor = true;
            this.btnTakePic.Visible = false;
            this.btnTakePic.Click += new System.EventHandler(this.btnTakePic_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnTakePic);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(116, 775);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1142, 80);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(116, 66);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1142, 709);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button1.Location = new System.Drawing.Point(116, 855);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(1142, 41);
            this.button1.TabIndex = 5;
            this.button1.Text = "Zapisz";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // flpScrapButtons
            // 
            this.flpScrapButtons.BackColor = System.Drawing.Color.Black;
            this.flpScrapButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.flpScrapButtons.ForeColor = System.Drawing.Color.White;
            this.flpScrapButtons.Location = new System.Drawing.Point(1258, 0);
            this.flpScrapButtons.Name = "flpScrapButtons";
            this.flpScrapButtons.Size = new System.Drawing.Size(126, 896);
            this.flpScrapButtons.TabIndex = 6;
            // 
            // flpNgButtons
            // 
            this.flpNgButtons.BackColor = System.Drawing.Color.Red;
            this.flpNgButtons.Dock = System.Windows.Forms.DockStyle.Left;
            this.flpNgButtons.ForeColor = System.Drawing.Color.White;
            this.flpNgButtons.Location = new System.Drawing.Point(0, 0);
            this.flpNgButtons.Name = "flpNgButtons";
            this.flpNgButtons.Size = new System.Drawing.Size(116, 896);
            this.flpNgButtons.TabIndex = 7;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // NewFailureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1384, 896);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panelQr);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.flpScrapButtons);
            this.Controls.Add(this.flpNgButtons);
            this.Name = "NewFailureForm";
            this.Text = "NewFailureForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewFailureForm_FormClosing);
            this.Load += new System.EventHandler(this.NewFailureForm_Load);
            this.panelQr.ResumeLayout(false);
            this.panelQr.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelQr;
        private System.Windows.Forms.Label labelDecodedQr;
        private System.Windows.Forms.Button btnTakePic;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FlowLayoutPanel flpScrapButtons;
        private System.Windows.Forms.FlowLayoutPanel flpNgButtons;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox textBox1;
    }
}