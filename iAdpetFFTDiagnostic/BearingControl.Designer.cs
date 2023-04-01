namespace iAdpetFFTDiagnostic
{
    partial class BearingControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rbStandard = new System.Windows.Forms.RadioButton();
            this.rbCustomized = new System.Windows.Forms.RadioButton();
            this.cmbbxManufacturer = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbbxBearingNumber = new System.Windows.Forms.ComboBox();
            this.tbBPFI = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbBPFO = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbFTF = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbBSF = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbBalls = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // rbStandard
            // 
            this.rbStandard.AutoSize = true;
            this.rbStandard.Checked = true;
            this.rbStandard.Location = new System.Drawing.Point(4, 4);
            this.rbStandard.Name = "rbStandard";
            this.rbStandard.Size = new System.Drawing.Size(68, 17);
            this.rbStandard.TabIndex = 0;
            this.rbStandard.TabStop = true;
            this.rbStandard.Text = "Standard";
            this.rbStandard.UseVisualStyleBackColor = true;
            this.rbStandard.CheckedChanged += new System.EventHandler(this.rbStandard_CheckedChanged);
            // 
            // rbCustomized
            // 
            this.rbCustomized.AutoSize = true;
            this.rbCustomized.Location = new System.Drawing.Point(249, 4);
            this.rbCustomized.Name = "rbCustomized";
            this.rbCustomized.Size = new System.Drawing.Size(79, 17);
            this.rbCustomized.TabIndex = 1;
            this.rbCustomized.Text = "Customized";
            this.rbCustomized.UseVisualStyleBackColor = true;
            this.rbCustomized.Visible = false;
            // 
            // cmbbxManufacturer
            // 
            this.cmbbxManufacturer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbxManufacturer.FormattingEnabled = true;
            this.cmbbxManufacturer.Items.AddRange(new object[] {
            "ALL",
            "BAR",
            "BCA",
            "BOW",
            "COO",
            "DGE",
            "FAF",
            "FAG",
            "INA",
            "KAY",
            "KBC",
            "KOY",
            "LBT",
            "MCG",
            "MES",
            "MHP",
            "MRC",
            "NDH",
            "NIC",
            "NSK",
            "NTN",
            "REX",
            "RHP",
            "ROL",
            "ROT",
            "SEA",
            "SKF",
            "SNR",
            "STR",
            "TMK",
            "TOR"});
            this.cmbbxManufacturer.Location = new System.Drawing.Point(112, 31);
            this.cmbbxManufacturer.Name = "cmbbxManufacturer";
            this.cmbbxManufacturer.Size = new System.Drawing.Size(100, 21);
            this.cmbbxManufacturer.TabIndex = 2;
            this.cmbbxManufacturer.SelectedIndexChanged += new System.EventHandler(this.cmbbxManufacturer_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Select Manufacturer";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Select Bearing";
            // 
            // cmbbxBearingNumber
            // 
            this.cmbbxBearingNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbxBearingNumber.FormattingEnabled = true;
            this.cmbbxBearingNumber.Location = new System.Drawing.Point(112, 58);
            this.cmbbxBearingNumber.Name = "cmbbxBearingNumber";
            this.cmbbxBearingNumber.Size = new System.Drawing.Size(100, 21);
            this.cmbbxBearingNumber.TabIndex = 4;
            this.cmbbxBearingNumber.SelectedIndexChanged += new System.EventHandler(this.cmbbxBearingNumber_SelectedIndexChanged);
            // 
            // tbBPFI
            // 
            this.tbBPFI.Location = new System.Drawing.Point(61, 106);
            this.tbBPFI.Name = "tbBPFI";
            this.tbBPFI.Size = new System.Drawing.Size(80, 20);
            this.tbBPFI.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "BPFI";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(177, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "BPFO";
            // 
            // tbBPFO
            // 
            this.tbBPFO.Location = new System.Drawing.Point(213, 106);
            this.tbBPFO.Name = "tbBPFO";
            this.tbBPFO.Size = new System.Drawing.Size(80, 20);
            this.tbBPFO.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(96, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "FTF";
            // 
            // tbFTF
            // 
            this.tbFTF.Location = new System.Drawing.Point(132, 147);
            this.tbFTF.Name = "tbFTF";
            this.tbFTF.Size = new System.Drawing.Size(80, 20);
            this.tbFTF.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(333, 109);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "BSF";
            // 
            // tbBSF
            // 
            this.tbBSF.Location = new System.Drawing.Point(369, 106);
            this.tbBSF.Name = "tbBSF";
            this.tbBSF.Size = new System.Drawing.Size(80, 20);
            this.tbBSF.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(265, 150);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Balls";
            // 
            // tbBalls
            // 
            this.tbBalls.Location = new System.Drawing.Point(301, 147);
            this.tbBalls.Name = "tbBalls";
            this.tbBalls.Size = new System.Drawing.Size(80, 20);
            this.tbBalls.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(255, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "BPFI";
            this.label8.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(291, 32);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(80, 20);
            this.textBox1.TabIndex = 16;
            this.textBox1.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(255, 62);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(30, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "BPFI";
            this.label9.Visible = false;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(291, 59);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(80, 20);
            this.textBox2.TabIndex = 18;
            this.textBox2.Visible = false;
            // 
            // BearingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbBalls);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbFTF);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbBSF);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbBPFO);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbBPFI);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbbxBearingNumber);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbbxManufacturer);
            this.Controls.Add(this.rbCustomized);
            this.Controls.Add(this.rbStandard);
            this.Name = "BearingControl";
            this.Size = new System.Drawing.Size(507, 248);
            this.Load += new System.EventHandler(this.BearingControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbStandard;
        private System.Windows.Forms.RadioButton rbCustomized;
        private System.Windows.Forms.ComboBox cmbbxManufacturer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbbxBearingNumber;
        private System.Windows.Forms.TextBox tbBPFI;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbBPFO;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbFTF;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbBSF;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbBalls;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox2;
    }
}
