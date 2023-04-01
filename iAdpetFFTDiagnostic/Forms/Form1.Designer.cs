namespace iAdpetFFTDiagnostic
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtXData = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtYData = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRPM = new System.Windows.Forms.Label();
            this.txtRPM = new System.Windows.Forms.TextBox();
            this.btnAdvanced = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPulse = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtXLabel = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtXData
            // 
            this.txtXData.Location = new System.Drawing.Point(16, 106);
            this.txtXData.Margin = new System.Windows.Forms.Padding(4);
            this.txtXData.Multiline = true;
            this.txtXData.Name = "txtXData";
            this.txtXData.Size = new System.Drawing.Size(76, 432);
            this.txtXData.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 82);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "X-Data";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(101, 82);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Y-Data";
            // 
            // txtYData
            // 
            this.txtYData.Location = new System.Drawing.Point(101, 106);
            this.txtYData.Margin = new System.Windows.Forms.Padding(4);
            this.txtYData.Multiline = true;
            this.txtYData.Name = "txtYData";
            this.txtYData.Size = new System.Drawing.Size(76, 432);
            this.txtYData.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 16);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(404, 29);
            this.label3.TabIndex = 4;
            this.label3.Text = "Enter relevant data for diagnostic ";
            // 
            // lblRPM
            // 
            this.lblRPM.AutoSize = true;
            this.lblRPM.Location = new System.Drawing.Point(187, 82);
            this.lblRPM.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRPM.Name = "lblRPM";
            this.lblRPM.Size = new System.Drawing.Size(38, 17);
            this.lblRPM.TabIndex = 6;
            this.lblRPM.Text = "RPM";
            // 
            // txtRPM
            // 
            this.txtRPM.Location = new System.Drawing.Point(187, 106);
            this.txtRPM.Margin = new System.Windows.Forms.Padding(4);
            this.txtRPM.Name = "txtRPM";
            this.txtRPM.Size = new System.Drawing.Size(76, 22);
            this.txtRPM.TabIndex = 5;
            // 
            // btnAdvanced
            // 
            this.btnAdvanced.Location = new System.Drawing.Point(305, 162);
            this.btnAdvanced.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdvanced.Name = "btnAdvanced";
            this.btnAdvanced.Size = new System.Drawing.Size(107, 49);
            this.btnAdvanced.TabIndex = 7;
            this.btnAdvanced.Text = "Advanced Diagnosis";
            this.btnAdvanced.UseVisualStyleBackColor = true;
            this.btnAdvanced.Click += new System.EventHandler(this.btnAdvanced_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(305, 106);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(107, 49);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "Basic Diagnosis";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(187, 142);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Pulse";
            // 
            // txtPulse
            // 
            this.txtPulse.Location = new System.Drawing.Point(187, 165);
            this.txtPulse.Margin = new System.Windows.Forms.Padding(4);
            this.txtPulse.Name = "txtPulse";
            this.txtPulse.Size = new System.Drawing.Size(76, 22);
            this.txtPulse.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(187, 213);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "X-Label";
            // 
            // txtXLabel
            // 
            this.txtXLabel.Location = new System.Drawing.Point(187, 236);
            this.txtXLabel.Margin = new System.Windows.Forms.Padding(4);
            this.txtXLabel.Name = "txtXLabel";
            this.txtXLabel.Size = new System.Drawing.Size(76, 22);
            this.txtXLabel.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 554);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtXLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPulse);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnAdvanced);
            this.Controls.Add(this.lblRPM);
            this.Controls.Add(this.txtRPM);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtYData);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtXData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manual Data Entry for diagnosis";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtXData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtYData;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblRPM;
        private System.Windows.Forms.TextBox txtRPM;
        private System.Windows.Forms.Button btnAdvanced;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPulse;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtXLabel;
    }
}

