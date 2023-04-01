namespace iAdpetFFTDiagnostic
{
    partial class InterpretOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InterpretOptions));
            this.chkGear = new System.Windows.Forms.CheckBox();
            this.txtGearTeeth = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRPMMargin = new System.Windows.Forms.TextBox();
            this.chkRPMMargin = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPinionTeeth = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPinionRPM = new System.Windows.Forms.TextBox();
            this.cmbDirection = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDrivingDiameter = new System.Windows.Forms.TextBox();
            this.chkBelt = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDrivenDiameter = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBeltLength = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtBladeCount = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbLF = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbPoleCount = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtVaneCount = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtRotorBarCount = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtCoilCount = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkMotor = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtRPM = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.chkFan = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.chkBearing = new System.Windows.Forms.CheckBox();
            this.bearingControl1 = new iAdpetFFTDiagnostic.BearingControl();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkGear
            // 
            this.chkGear.AutoSize = true;
            this.chkGear.Checked = true;
            this.chkGear.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGear.Location = new System.Drawing.Point(8, 0);
            this.chkGear.Margin = new System.Windows.Forms.Padding(4);
            this.chkGear.Name = "chkGear";
            this.chkGear.Size = new System.Drawing.Size(85, 21);
            this.chkGear.TabIndex = 0;
            this.chkGear.Text = "GearBox";
            this.chkGear.UseVisualStyleBackColor = true;
            this.chkGear.CheckedChanged += new System.EventHandler(this.chkGear_CheckedChanged);
            // 
            // txtGearTeeth
            // 
            this.txtGearTeeth.Location = new System.Drawing.Point(8, 49);
            this.txtGearTeeth.Margin = new System.Windows.Forms.Padding(4);
            this.txtGearTeeth.Name = "txtGearTeeth";
            this.txtGearTeeth.Size = new System.Drawing.Size(201, 22);
            this.txtGearTeeth.TabIndex = 1;
            this.txtGearTeeth.Text = "32";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(209, 17);
            this.label1.TabIndex = 100;
            this.label1.Text = "Enter number of Teeths in Gear";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(1105, 511);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(100, 28);
            this.btnOk.TabIndex = 19;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 82);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Enter Margin (in RPM)";
            // 
            // txtRPMMargin
            // 
            this.txtRPMMargin.Location = new System.Drawing.Point(8, 106);
            this.txtRPMMargin.Margin = new System.Windows.Forms.Padding(4);
            this.txtRPMMargin.Name = "txtRPMMargin";
            this.txtRPMMargin.Size = new System.Drawing.Size(201, 22);
            this.txtRPMMargin.TabIndex = 15;
            this.txtRPMMargin.Text = "50";
            // 
            // chkRPMMargin
            // 
            this.chkRPMMargin.AutoSize = true;
            this.chkRPMMargin.Checked = true;
            this.chkRPMMargin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRPMMargin.Location = new System.Drawing.Point(8, 0);
            this.chkRPMMargin.Margin = new System.Windows.Forms.Padding(4);
            this.chkRPMMargin.Name = "chkRPMMargin";
            this.chkRPMMargin.Size = new System.Drawing.Size(64, 21);
            this.chkRPMMargin.TabIndex = 14;
            this.chkRPMMargin.Text = "RPM ";
            this.chkRPMMargin.UseVisualStyleBackColor = true;
            this.chkRPMMargin.CheckedChanged += new System.EventHandler(this.chkRPMMargin_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 82);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(216, 17);
            this.label3.TabIndex = 100;
            this.label3.Text = "Enter number of Teeths in Pinion";
            // 
            // txtPinionTeeth
            // 
            this.txtPinionTeeth.Location = new System.Drawing.Point(8, 107);
            this.txtPinionTeeth.Margin = new System.Windows.Forms.Padding(4);
            this.txtPinionTeeth.Name = "txtPinionTeeth";
            this.txtPinionTeeth.Size = new System.Drawing.Size(201, 22);
            this.txtPinionTeeth.TabIndex = 2;
            this.txtPinionTeeth.Text = "16";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 140);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 17);
            this.label4.TabIndex = 100;
            this.label4.Text = "Enter RPM of Pinion";
            // 
            // txtPinionRPM
            // 
            this.txtPinionRPM.Location = new System.Drawing.Point(8, 165);
            this.txtPinionRPM.Margin = new System.Windows.Forms.Padding(4);
            this.txtPinionRPM.Name = "txtPinionRPM";
            this.txtPinionRPM.Size = new System.Drawing.Size(201, 22);
            this.txtPinionRPM.TabIndex = 3;
            this.txtPinionRPM.Text = "6000";
            // 
            // cmbDirection
            // 
            this.cmbDirection.FormattingEnabled = true;
            this.cmbDirection.Items.AddRange(new object[] {
            "Axial",
            "Horizontal",
            "Vertical"});
            this.cmbDirection.Location = new System.Drawing.Point(989, 418);
            this.cmbDirection.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDirection.Name = "cmbDirection";
            this.cmbDirection.Size = new System.Drawing.Size(201, 24);
            this.cmbDirection.TabIndex = 17;
            this.cmbDirection.Text = "Axial";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(989, 391);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "Select Direction";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 25);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(203, 17);
            this.label6.TabIndex = 100;
            this.label6.Text = "Enter Driving Sheave Diameter";
            // 
            // txtDrivingDiameter
            // 
            this.txtDrivingDiameter.Location = new System.Drawing.Point(8, 49);
            this.txtDrivingDiameter.Margin = new System.Windows.Forms.Padding(4);
            this.txtDrivingDiameter.Name = "txtDrivingDiameter";
            this.txtDrivingDiameter.Size = new System.Drawing.Size(201, 22);
            this.txtDrivingDiameter.TabIndex = 5;
            this.txtDrivingDiameter.Text = "25";
            // 
            // chkBelt
            // 
            this.chkBelt.AutoSize = true;
            this.chkBelt.Checked = true;
            this.chkBelt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBelt.Location = new System.Drawing.Point(8, 0);
            this.chkBelt.Margin = new System.Windows.Forms.Padding(4);
            this.chkBelt.Name = "chkBelt";
            this.chkBelt.Size = new System.Drawing.Size(91, 21);
            this.chkBelt.TabIndex = 4;
            this.chkBelt.Text = "Belt Drive";
            this.chkBelt.UseVisualStyleBackColor = true;
            this.chkBelt.CheckedChanged += new System.EventHandler(this.chkBelt_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 82);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(200, 17);
            this.label7.TabIndex = 100;
            this.label7.Text = "Enter Driven Sheave Diameter";
            // 
            // txtDrivenDiameter
            // 
            this.txtDrivenDiameter.Location = new System.Drawing.Point(8, 107);
            this.txtDrivenDiameter.Margin = new System.Windows.Forms.Padding(4);
            this.txtDrivenDiameter.Name = "txtDrivenDiameter";
            this.txtDrivenDiameter.Size = new System.Drawing.Size(201, 22);
            this.txtDrivenDiameter.TabIndex = 6;
            this.txtDrivenDiameter.Text = "15";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 140);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(118, 17);
            this.label8.TabIndex = 100;
            this.label8.Text = "Enter Belt Length";
            // 
            // txtBeltLength
            // 
            this.txtBeltLength.Location = new System.Drawing.Point(8, 165);
            this.txtBeltLength.Margin = new System.Windows.Forms.Padding(4);
            this.txtBeltLength.Name = "txtBeltLength";
            this.txtBeltLength.Size = new System.Drawing.Size(201, 22);
            this.txtBeltLength.TabIndex = 7;
            this.txtBeltLength.Text = "75";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 25);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(159, 17);
            this.label9.TabIndex = 21;
            this.label9.Text = "Enter Number of Blades";
            // 
            // txtBladeCount
            // 
            this.txtBladeCount.Location = new System.Drawing.Point(8, 49);
            this.txtBladeCount.Margin = new System.Windows.Forms.Padding(4);
            this.txtBladeCount.Name = "txtBladeCount";
            this.txtBladeCount.Size = new System.Drawing.Size(201, 22);
            this.txtBladeCount.TabIndex = 13;
            this.txtBladeCount.Text = "8";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(989, 265);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(149, 17);
            this.label10.TabIndex = 23;
            this.label10.Text = "Select Line Frequency";
            // 
            // cmbLF
            // 
            this.cmbLF.FormattingEnabled = true;
            this.cmbLF.Items.AddRange(new object[] {
            "50 Hz",
            "60 Hz"});
            this.cmbLF.Location = new System.Drawing.Point(989, 292);
            this.cmbLF.Margin = new System.Windows.Forms.Padding(4);
            this.cmbLF.Name = "cmbLF";
            this.cmbLF.Size = new System.Drawing.Size(201, 24);
            this.cmbLF.TabIndex = 16;
            this.cmbLF.Text = "50 Hz";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 82);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(151, 17);
            this.label11.TabIndex = 25;
            this.label11.Text = "Enter Number of Poles";
            // 
            // cmbPoleCount
            // 
            this.cmbPoleCount.FormattingEnabled = true;
            this.cmbPoleCount.Items.AddRange(new object[] {
            "2",
            "4",
            "6",
            "8"});
            this.cmbPoleCount.Location = new System.Drawing.Point(8, 107);
            this.cmbPoleCount.Margin = new System.Windows.Forms.Padding(4);
            this.cmbPoleCount.Name = "cmbPoleCount";
            this.cmbPoleCount.Size = new System.Drawing.Size(201, 24);
            this.cmbPoleCount.TabIndex = 10;
            this.cmbPoleCount.Text = "2";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 25);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(156, 17);
            this.label12.TabIndex = 28;
            this.label12.Text = "Enter Number of Vanes";
            // 
            // txtVaneCount
            // 
            this.txtVaneCount.Location = new System.Drawing.Point(8, 49);
            this.txtVaneCount.Margin = new System.Windows.Forms.Padding(4);
            this.txtVaneCount.Name = "txtVaneCount";
            this.txtVaneCount.Size = new System.Drawing.Size(201, 22);
            this.txtVaneCount.TabIndex = 9;
            this.txtVaneCount.Text = "8";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 140);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(184, 17);
            this.label13.TabIndex = 30;
            this.label13.Text = "Enter Number of Rotor Bars";
            // 
            // txtRotorBarCount
            // 
            this.txtRotorBarCount.Location = new System.Drawing.Point(8, 165);
            this.txtRotorBarCount.Margin = new System.Windows.Forms.Padding(4);
            this.txtRotorBarCount.Name = "txtRotorBarCount";
            this.txtRotorBarCount.Size = new System.Drawing.Size(201, 22);
            this.txtRotorBarCount.TabIndex = 11;
            this.txtRotorBarCount.Text = "10";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(989, 329);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(146, 17);
            this.label14.TabIndex = 32;
            this.label14.Text = "Enter Number of Coils";
            // 
            // txtCoilCount
            // 
            this.txtCoilCount.Location = new System.Drawing.Point(989, 356);
            this.txtCoilCount.Margin = new System.Windows.Forms.Padding(4);
            this.txtCoilCount.Name = "txtCoilCount";
            this.txtCoilCount.Size = new System.Drawing.Size(201, 22);
            this.txtCoilCount.TabIndex = 18;
            this.txtCoilCount.Text = "10";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkGear);
            this.groupBox1.Controls.Add(this.txtGearTeeth);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtPinionTeeth);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtPinionRPM);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(16, 15);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(225, 208);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkBelt);
            this.groupBox2.Controls.Add(this.txtDrivingDiameter);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtDrivenDiameter);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtBeltLength);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(257, 15);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(225, 208);
            this.groupBox2.TabIndex = 34;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkMotor);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.txtVaneCount);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.cmbPoleCount);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.txtRotorBarCount);
            this.groupBox3.Location = new System.Drawing.Point(499, 15);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(225, 208);
            this.groupBox3.TabIndex = 35;
            this.groupBox3.TabStop = false;
            // 
            // chkMotor
            // 
            this.chkMotor.AutoSize = true;
            this.chkMotor.Checked = true;
            this.chkMotor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMotor.Location = new System.Drawing.Point(12, 0);
            this.chkMotor.Margin = new System.Windows.Forms.Padding(4);
            this.chkMotor.Name = "chkMotor";
            this.chkMotor.Size = new System.Drawing.Size(66, 21);
            this.chkMotor.TabIndex = 8;
            this.chkMotor.Text = "Motor";
            this.chkMotor.UseVisualStyleBackColor = true;
            this.chkMotor.CheckedChanged += new System.EventHandler(this.chkMotor_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.txtRPM);
            this.groupBox4.Controls.Add(this.chkRPMMargin);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.txtRPMMargin);
            this.groupBox4.Location = new System.Drawing.Point(981, 15);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(225, 208);
            this.groupBox4.TabIndex = 37;
            this.groupBox4.TabStop = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 25);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(76, 17);
            this.label15.TabIndex = 16;
            this.label15.Text = "Enter RPM";
            // 
            // txtRPM
            // 
            this.txtRPM.Location = new System.Drawing.Point(8, 49);
            this.txtRPM.Margin = new System.Windows.Forms.Padding(4);
            this.txtRPM.Name = "txtRPM";
            this.txtRPM.Size = new System.Drawing.Size(201, 22);
            this.txtRPM.TabIndex = 17;
            this.txtRPM.Text = "1500";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.chkFan);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.txtBladeCount);
            this.groupBox5.Location = new System.Drawing.Point(740, 15);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox5.Size = new System.Drawing.Size(225, 208);
            this.groupBox5.TabIndex = 36;
            this.groupBox5.TabStop = false;
            // 
            // chkFan
            // 
            this.chkFan.AutoSize = true;
            this.chkFan.Checked = true;
            this.chkFan.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFan.Location = new System.Drawing.Point(12, 0);
            this.chkFan.Margin = new System.Windows.Forms.Padding(4);
            this.chkFan.Name = "chkFan";
            this.chkFan.Size = new System.Drawing.Size(54, 21);
            this.chkFan.TabIndex = 12;
            this.chkFan.Text = "Fan";
            this.chkFan.UseVisualStyleBackColor = true;
            this.chkFan.CheckedChanged += new System.EventHandler(this.chkFan_CheckedChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.chkBearing);
            this.groupBox6.Controls.Add(this.bearingControl1);
            this.groupBox6.Location = new System.Drawing.Point(16, 241);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox6.Size = new System.Drawing.Size(708, 298);
            this.groupBox6.TabIndex = 39;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "groupBox6";
            // 
            // chkBearing
            // 
            this.chkBearing.AutoSize = true;
            this.chkBearing.Location = new System.Drawing.Point(8, 0);
            this.chkBearing.Margin = new System.Windows.Forms.Padding(4);
            this.chkBearing.Name = "chkBearing";
            this.chkBearing.Size = new System.Drawing.Size(79, 21);
            this.chkBearing.TabIndex = 39;
            this.chkBearing.Text = "Bearing";
            this.chkBearing.UseVisualStyleBackColor = true;
            this.chkBearing.CheckedChanged += new System.EventHandler(this.chkBearing_CheckedChanged);
            // 
            // bearingControl1
            // 
            this.bearingControl1.Enabled = false;
            this.bearingControl1.Location = new System.Drawing.Point(4, 23);
            this.bearingControl1.Margin = new System.Windows.Forms.Padding(5);
            this.bearingControl1.Name = "bearingControl1";
            this.bearingControl1.Size = new System.Drawing.Size(696, 267);
            this.bearingControl1.TabIndex = 38;
            // 
            // InterpretOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1221, 554);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtCoilCount);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cmbLF);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbDirection);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InterpretOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select filters for diagnosis";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InterpretOptions_FormClosing);
            this.Load += new System.EventHandler(this.InterpretOptions_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkGear;
        private System.Windows.Forms.TextBox txtGearTeeth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRPMMargin;
        private System.Windows.Forms.CheckBox chkRPMMargin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPinionTeeth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPinionRPM;
        private System.Windows.Forms.ComboBox cmbDirection;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDrivingDiameter;
        private System.Windows.Forms.CheckBox chkBelt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDrivenDiameter;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtBeltLength;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtBladeCount;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbLF;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbPoleCount;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtVaneCount;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtRotorBarCount;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtCoilCount;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkMotor;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox chkFan;
        private BearingControl bearingControl1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox chkBearing;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtRPM;
    }
}