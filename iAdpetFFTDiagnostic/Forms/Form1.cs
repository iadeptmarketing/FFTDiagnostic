using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace iAdpetFFTDiagnostic
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private InterpretOptions _interpretOptions;

        private void btnAdvanced_Click(object sender, EventArgs e)
        {
            try
            {
                GetBasicGraphData();
                _interpretOptions = new InterpretOptions();
                _interpretOptions.ShowDialog();
                DiagnosisClass diagnosisClass = new DiagnosisClass(_interpretOptions, _xDatalist.ToArray(),
                    _yDatalist.ToArray(), _iRpm);

                diagnosisClass.InterpretBasicFaults();
                if (_interpretOptions.IsMotor)
                    diagnosisClass.InterpretMotorFaults();
                if (_interpretOptions.IsBeltDrive)
                    diagnosisClass.InterpretBeltFaults();
                if (_interpretOptions.IsGearBox)
                    diagnosisClass.InterpretGearFaults();
                if (_interpretOptions.IsFan)
                    diagnosisClass.InterpretFanFaults();
                if (_interpretOptions.IsBearing)
                    diagnosisClass.InterpretBearingFaults();

                StringBuilder sbFaults = new StringBuilder();
                //foreach (var fault in diagnosisClass.GetFaultsName)
                //{
                //    sbFaults.AppendLine(fault);
                //}
                if (diagnosisClass.GetFaultsName.Count > 0)
                {
                    for (int i = 0; i < diagnosisClass.GetFaultsName.Count; i++)
                    {
                        sbFaults.AppendLine(diagnosisClass.GetFaultsName[i] + "\n" + "- Reason " +
                                            diagnosisClass.GetFaultDescription[i] + "\n");
                    }

                    //MessageBox.Show(sbFaults.ToString());
                    Result resultBox = new Result();
                    resultBox.ResultBox.Text = sbFaults.ToString();
                    resultBox.Show();
                }
                else
                {
                    MessageBox.Show(
                        "With given set of data no fault is diagnosed. Please contact administrator for further information");
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);

            }
        }

        private void GetBasicGraphData()
        {
            try
            {
                string[] strxData = txtXData.Text.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                _xDatalist = new List<double>();
                string[] stryData = txtYData.Text.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                _yDatalist = new List<double>();
                for (int i = 0; i < strxData.Length; i++)
                {
                    _xDatalist.Add(Convert.ToDouble(strxData[i]));
                    _yDatalist.Add(Convert.ToDouble(stryData[i]));
                }

                _iRpm = Convert.ToInt32(txtRPM.Text);
                _iPulse = Convert.ToInt32(txtPulse.Text);
                _finalFreq = _iRpm / (double)(_iPulse * 60);
                if (_iRpm <= 60)
                {
                    MessageBox.Show("Either RPM Value Incorrect or contact administrator");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int _iRpm;
        private int _iPulse;
        private double _finalFreq;
        List<double> _xDatalist = new List<double>();
        List<double> _yDatalist = new List<double>();

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                GetBasicGraphData();
                FFT_Interpretation.Interprator interprator = new FFT_Interpretation.Interprator();
                string interpretation =
                    interprator.Interpret_FFT(_xDatalist.ToArray(), _yDatalist.ToArray(), _finalFreq,
                        txtXLabel.ToString());

                MessageBox.Show("With the Values of the RPM entered we found" + "\n\n" + interpretation,
                    "Interpretation of Graph");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);

            }
        }
       
    }
}
