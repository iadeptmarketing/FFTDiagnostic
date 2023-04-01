using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UFFReader;
using WAVReader;

namespace iAdpetFFTDiagnostic
{
    public partial class FileSelectionForm : Form
    {
        
        public FileSelectionForm()
        {
            InitializeComponent();
        }
        public string SelectedFileType
        { get; set; }


        /// <summary>
        /// Browse button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Multiselect = false;
            if (SelectedFileType == "CSV")
                openFileDialog.Filter = "CSV files (*.csv)|*.csv";
            else if (SelectedFileType.Contains("UFF"))
                openFileDialog.Filter = "UFF files(*.uff)|*.uff|UNV files(*.unv)|*.unv";
            else if (SelectedFileType.Contains("WAV"))
                openFileDialog.Filter = "WAV files(*.wav)|*.wav";

            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog.FileName;
            }
        }
        private InterpretOptions _interpretOptions;

        private int _iRpm = 1;
        
        private void InterpretFFT(List<List<double>> _xDatalist, List<List<double>> _yDatalist)
        {
            _iRpm = 1;
            _interpretOptions = new InterpretOptions();
            _interpretOptions.ShowDialog();
            if (_interpretOptions.IsRpmMargin && _interpretOptions.RPM > 1)
            {
                _iRpm = _interpretOptions.RPM;
            }
            IDictionary<string, string> faults = new Dictionary<string, string>();
            for (int i=0;i<_yDatalist.Count;i++)
            {
                DiagnosisClass diagnosisClass = new DiagnosisClass(_interpretOptions, _xDatalist[i].ToArray(),
                _yDatalist[i].ToArray(), _iRpm);

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

                if (diagnosisClass.GetFaultsName.Count > 0)
                {
                    for (int j = 0; j < diagnosisClass.GetFaultsName.Count; j++)
                    {
                        try
                        {
                            faults.Add(diagnosisClass.GetFaultsName[i], diagnosisClass.GetFaultDescription[i]);
                        }
                        catch
                        { }
                        //sbFaults.AppendLine(diagnosisClass.GetFaultsName[i] + "\n" + "- Reason " +
                        //                    diagnosisClass.GetFaultDescription[i] + "\n");
                    }

                    //MessageBox.Show(sbFaults.ToString());
                   
                }
                
            }
            if (faults.Count > 0)
            {
                StringBuilder sbFaults = new StringBuilder();
                foreach(var v in faults)
                {
                    sbFaults.AppendLine(v.Key + "\n" + "- Reason " + v.Value + "\n");
                }
                Result resultBox = new Result();
                resultBox.ResultBox.Text = sbFaults.ToString();
                resultBox.ShowDialog();
            }
            else
            {
                MessageBox.Show(
                    "With given set of data no fault is diagnosed. Please contact administrator for further information");
            }
        }

        
        private void InterpretFFT(List<double> _xDatalist, List<double> _yDatalist)
        {
            _iRpm = 1;
            _interpretOptions = new InterpretOptions();
            _interpretOptions.ShowDialog();
            if(_interpretOptions.IsRpmMargin && _interpretOptions.RPM>1)
            {
                _iRpm = _interpretOptions.RPM;
            }
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
                resultBox.ShowDialog();
            }
            else
            {
                MessageBox.Show(
                    "With given set of data no fault is diagnosed. Please contact administrator for further information");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Select a file to be analysed");
                return;
            }
            if (!File.Exists(textBox1.Text))
            {
                MessageBox.Show("Selected file does not exist");
                return;
            }

            if (SelectedFileType == "CSV")
            {
                MessageBox.Show("CSV file types are not included in demo. Kindly contact administrator");
            }
            if (SelectedFileType.Contains("Time"))
            {
                if (SelectedFileType.Contains("UFF"))
                {
                    MessageBox.Show("Analysis for UFF files with TimeWave data are not included in demo. Kindly contact administrator");
                    this.Close();
                }
                ReadWav readWav = new ReadWav();
                try
                {
                    readWav.ReadFile(textBox1.Text);
                    List<List<double>> XFFT = new List<List<double>>();
                    List<List<double>> YFFT = new List<List<double>>();
                    for (int i = 0; i < readWav.allXData.Count; i++)
                    {
                        //Convert Time to FFT
                        TimeToFFT.FFTConversion conversion = new TimeToFFT.FFTConversion();
                        List<double> convertedFFT = conversion.ConvertTimeDataToFFT(readWav.allYData[i].ToArray()).ToList();
                        //calculating hz
                        var xData = readWav.allXData[i].ToArray();
                        double lastTimevalue = (double)(xData[xData.Length - 1]);
                        lastTimevalue = Math.Round(lastTimevalue, 2);
                        double HzRate = (double)(1 / lastTimevalue);
                        List<double> Hz = new List<double>();
                        for (int j = 0; j < convertedFFT.Count(); j++)
                        {
                            Hz.Add(HzRate * j);                        
                        }
                        XFFT.Add(Hz);
                        YFFT.Add(convertedFFT);
                        //MessageBox.Show("Interpretation Process for splitted wave count " + (i+1).ToString());
                        
                    }
                    InterpretFFT(XFFT, YFFT);
                }
                catch(Exception ex)
                {

                }
            }
            if (SelectedFileType.Contains("UFF"))
            {
                ReadUFF readUFF = new ReadUFF();
                if (SelectedFileType == "UFF(FFT Data)")
                {
                    readUFF.IsYdataSquared = true;
                }
                try
                {
                    readUFF.ReadFile(textBox1.Text);

                    for (int i = 0; i < readUFF.allXData.Count; i++)
                    {
                        MessageBox.Show("Interpretation Process for Channel No." + readUFF.allChannel[i] + " taken on " + readUFF.allDateTime[i]);
                        InterpretFFT(readUFF.allXData[i], readUFF.allYData[i]);
                    }
                }
                catch (Exception ex)
                {

                }
            }
            this.Close();
        }


        
    }
}
