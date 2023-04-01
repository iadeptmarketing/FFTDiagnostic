using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace iAdpetFFTDiagnostic
{
    public partial class InterpretOptions : Form , IUserInputInterface
    {
        public InterpretOptions()
        {
            InitializeComponent();

            txtGearTeeth.KeyPress += textBox_KeyPress;
            txtBeltLength.KeyPress += textBox_KeyPress;
            txtBladeCount.KeyPress += textBox_KeyPress;
            txtDrivenDiameter.KeyPress += textBox_KeyPress;
            txtDrivingDiameter.KeyPress += textBox_KeyPress;
            txtPinionRPM.KeyPress += textBox_KeyPress;
            txtPinionTeeth.KeyPress += textBox_KeyPress;
            txtRPMMargin.KeyPress += textBox_KeyPress;
            
        }
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            //// only allow one decimal point
            //if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            //{
            //    e.Handled = true;
            //}
        }
       
        public bool IsGearBox => chkGear.Checked;

        public bool IsMotor => chkMotor.Checked;

        public bool IsFan => chkFan.Checked;

        public int GearTooth => int.Parse(txtGearTeeth.Text);

        public int PinionTooth => int.Parse(txtPinionTeeth.Text);

        public int PinionRpm => int.Parse(txtPinionRPM.Text);

        public bool IsBearing => chkBearing.Checked;

        public string Direction
        {
            get
            {
                if (cmbDirection.SelectedIndex > -1)
                {
                    return cmbDirection.Items[cmbDirection.SelectedIndex].ToString();
                }
                else
                {
                    return "Axial";
                }
            }
        }
        public bool IsRpmMargin => chkRPMMargin.Checked;

        public int RpmMargin => int.Parse(txtRPMMargin.Text);

        public int RPM => int.Parse(txtRPM.Text);

        public int BladeCount => int.Parse(txtBladeCount.Text);
        

        public bool IsBeltDrive => chkBelt.Checked;

        public int BeltLength => int.Parse(txtBeltLength.Text);

        public int DrivenSheaveDiameter => int.Parse(txtDrivenDiameter.Text);

        public int DrivingSheaveDiameter => int.Parse(txtDrivingDiameter.Text);

        public int LineFrequency
        {
            get
            {
                if (cmbLF.SelectedIndex==1)
                {
                    return 3600;
                }

                return 3000;
            }
        }

        public int RotorBarCount => int.Parse(txtRotorBarCount.Text);
        public int VaneCount => int.Parse(txtVaneCount.Text);
        public int PoleCount
        {
            get
            {
                if (cmbPoleCount.SelectedIndex > -1)
                {
                    return int.Parse(cmbPoleCount.Items[cmbPoleCount.SelectedIndex].ToString());
                }
                else
                {
                    return 2;
                }
            }
        }

        public int CoilCount => int.Parse(txtCoilCount.Text);

        public BearingControl Control_Bearing => bearingControl1;

        bool ValidateInteger(string text)
        {
            int result = 0;
            return int.TryParse(text, out result);
        }
        private void InterpretOptions_Load(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void InterpretOptions_FormClosing(object sender, FormClosingEventArgs e)
        {
            int maxRpm = (LineFrequency * 2) / PoleCount;
            if (RPM > maxRpm)
                MessageBox.Show(string.Format("As per selected data, maximum RPM of motor should be {0}. In case RPM provided is more than this, results may vary.", maxRpm));
        }

        private void chkGear_CheckedChanged(object sender, EventArgs e)
        {
            txtGearTeeth.Enabled = IsGearBox;
            txtPinionRPM.Enabled = IsGearBox;
            txtPinionTeeth.Enabled = IsGearBox;
        }

        private void chkBelt_CheckedChanged(object sender, EventArgs e)
        {
            txtDrivenDiameter.Enabled = IsBeltDrive;
            txtDrivingDiameter.Enabled = IsBeltDrive;
            txtBeltLength.Enabled = IsBeltDrive;
        }

        private void chkMotor_CheckedChanged(object sender, EventArgs e)
        {
            txtVaneCount.Enabled = IsMotor;
            cmbPoleCount.Enabled = IsMotor;
            txtRotorBarCount.Enabled = IsMotor;
        }

        private void chkRPMMargin_CheckedChanged(object sender, EventArgs e)
        {
            txtRPMMargin.Enabled = IsRpmMargin;
            txtRPM.Enabled = IsRpmMargin;
        }

        private void chkFan_CheckedChanged(object sender, EventArgs e)
        {
            txtBladeCount.Enabled = IsFan;
        }

        private void chkBearing_CheckedChanged(object sender, EventArgs e)
        {
            bearingControl1.Enabled = IsBearing;
        }
    }
}
