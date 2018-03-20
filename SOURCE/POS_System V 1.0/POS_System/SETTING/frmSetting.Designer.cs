namespace POS_System
{
    partial class frmSetting
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.picBrowser = new System.Windows.Forms.PictureBox();
            this.picWord = new System.Windows.Forms.PictureBox();
            this.picSticky = new System.Windows.Forms.PictureBox();
            this.picBox_Company = new System.Windows.Forms.PictureBox();
            this.picCal = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBrowser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSticky)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Company)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCal)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.ForestGreen;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(650, 45);
            this.label1.TabIndex = 0;
            this.label1.Text = "Settings";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(62, 186);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Calculator";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(293, 186);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Company";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(525, 186);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Notepad";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(207, 374);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Word";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(420, 374);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "Browser";
            // 
            // picBrowser
            // 
            this.picBrowser.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBrowser.Image = global::POS_System.Properties.Resources.Chrome_96px;
            this.picBrowser.Location = new System.Drawing.Point(404, 273);
            this.picBrowser.Name = "picBrowser";
            this.picBrowser.Size = new System.Drawing.Size(95, 98);
            this.picBrowser.TabIndex = 5;
            this.picBrowser.TabStop = false;
            this.picBrowser.Click += new System.EventHandler(this.picBrowser_Click);
            // 
            // picWord
            // 
            this.picWord.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picWord.Image = global::POS_System.Properties.Resources.Microsoft_Word_100px;
            this.picWord.Location = new System.Drawing.Point(180, 273);
            this.picWord.Name = "picWord";
            this.picWord.Size = new System.Drawing.Size(95, 98);
            this.picWord.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picWord.TabIndex = 4;
            this.picWord.TabStop = false;
            this.picWord.Click += new System.EventHandler(this.picWord_Click);
            // 
            // picSticky
            // 
            this.picSticky.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picSticky.Image = global::POS_System.Properties.Resources.Note_96px;
            this.picSticky.Location = new System.Drawing.Point(512, 85);
            this.picSticky.Name = "picSticky";
            this.picSticky.Size = new System.Drawing.Size(95, 98);
            this.picSticky.TabIndex = 3;
            this.picSticky.TabStop = false;
            this.picSticky.Click += new System.EventHandler(this.picSticky_Click);
            // 
            // picBox_Company
            // 
            this.picBox_Company.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picBox_Company.Image = global::POS_System.Properties.Resources.Organization_96px;
            this.picBox_Company.Location = new System.Drawing.Point(284, 85);
            this.picBox_Company.Name = "picBox_Company";
            this.picBox_Company.Size = new System.Drawing.Size(95, 98);
            this.picBox_Company.TabIndex = 2;
            this.picBox_Company.TabStop = false;
            this.picBox_Company.Click += new System.EventHandler(this.picBox_Company_Click);
            // 
            // picCal
            // 
            this.picCal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picCal.Image = global::POS_System.Properties.Resources.Math_96px;
            this.picCal.Location = new System.Drawing.Point(55, 85);
            this.picCal.Name = "picCal";
            this.picCal.Size = new System.Drawing.Size(95, 98);
            this.picCal.TabIndex = 1;
            this.picCal.TabStop = false;
            this.picCal.Click += new System.EventHandler(this.picCal_Click);
            // 
            // frmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(650, 445);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.picBrowser);
            this.Controls.Add(this.picWord);
            this.Controls.Add(this.picSticky);
            this.Controls.Add(this.picBox_Company);
            this.Controls.Add(this.picCal);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(666, 484);
            this.MinimumSize = new System.Drawing.Size(666, 484);
            this.Name = "frmSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SETTING - Genesip Solutions";
            ((System.ComponentModel.ISupportInitialize)(this.picBrowser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSticky)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBox_Company)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picCal;
        private System.Windows.Forms.PictureBox picBox_Company;
        private System.Windows.Forms.PictureBox picSticky;
        private System.Windows.Forms.PictureBox picWord;
        private System.Windows.Forms.PictureBox picBrowser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}