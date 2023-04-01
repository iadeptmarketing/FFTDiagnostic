using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bearings;

namespace iAdpetFFTDiagnostic
{
    public partial class BearingControl : UserControl
    {
        public BearingControl()
        {
            InitializeComponent();
        }
        BearingLibraryClass _bearingLibraryClass = new BearingLibraryClass();

        private void cmbbxManufacturer_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbbxBearingNumber.Items.Clear();
            cmbbxBearingNumber.Items.AddRange(
                _bearingLibraryClass.GetBearings(cmbbxManufacturer.SelectedItem.ToString()));
            cmbbxBearingNumber.SelectedIndex = 0;
        }

        private void rbStandard_CheckedChanged(object sender, EventArgs e)
        {
            //if (rbStandard.Checked)
            //{
            //    StandardBearingEnabler(true);
            //    cmbbxManufacturer.SelectedIndex = 0;
            //}
            //else
            //{
            //    //cmbbxManufacturer.SelectedIndex = -1;
            //    StandardBearingEnabler(false);
            //}
            StandardBearingEnabler(rbStandard.Checked);
            if (string.IsNullOrEmpty(cmbbxManufacturer.SelectedText))
                cmbbxManufacturer.SelectedIndex = 0;
        }

        private void StandardBearingEnabler(bool enable)
        {
            label1.Enabled = enable;
            label2.Enabled = enable;
            cmbbxManufacturer.Enabled = enable;
            cmbbxBearingNumber.Enabled = enable;
        }

        private void BearingControl_Load(object sender, EventArgs e)
        {

        }

        private void cmbbxBearingNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            _BearingData = _bearingLibraryClass.GetBearingData(cmbbxBearingNumber.SelectedItem.ToString());
            tbBalls.Text = _bearingLibraryClass._Balls.ToString();
            tbBPFI.Text = _bearingLibraryClass._BPFI.ToString();
            tbBPFO.Text = _bearingLibraryClass._BPFO.ToString();
            tbBSF.Text = _bearingLibraryClass._BSF.ToString();
            tbFTF.Text = _bearingLibraryClass._FTF.ToString();
        }

        public Dictionary<string, double> _BearingData { get; private set; } = new Dictionary<string, double>();
    }
}
