using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace iAdpetFFTDiagnostic
{
    class DiagnosisClass
    {
        //double[] xx = null;
        //int[] yy = null;
        private int[] _ff = null;
        double[] _dXVals;
        double[] _dYVals;
        private bool _isPpf;
        private int _iRpm;
        private int _allPeak = 0;
        private double _iRpmMargin;
        private int _rbf; //Rotor Bar Frequency
        private double _highestPeakMargin;
        private int _polePass = 18;
        List<Peak> _peaks;
        private List<double> _peakOrder;
        private List<double> _peaksBr;
        private List<double> _peakOrderGmf;
        private List<double> _peakOrderPinion;
        private List<double> _rbfPeeks;
        private List<double> _lfPeeks;
        private List<double> _bpPeeks;
        private List<double> _vpPeeks;
        private List<double> _ppPeeks;
        private List<double> _cpfPeeks;

        private List<double> _bpfoPeeks;
        private List<double> _bpfiPeeks;
        private List<double> _ftfPeeks;
        private List<double> _bsfPeeks;
        public ArrayList FaultNameWithValue = new ArrayList();

        private IUserInputInterface _interpretOptions;
        public List<string> GetFaultDescription;

        public List<string> GetFaultsName;

        public DiagnosisClass(IUserInputInterface interpretOptions, double[] xData, double[] yData, int rpm)
        {
            try
            {


                if (interpretOptions == null)
                    throw new ApplicationException("Interpret Options can not be null.");
                if (xData == null || yData == null)
                    throw new ApplicationException("Graph Data can not be null.");
                if (rpm == 0 || rpm < 0)
                    throw new ApplicationException("RPM can not be null.");
                _dXVals = xData;
                _dYVals = yData;
                _iRpm = rpm;
                _interpretOptions = interpretOptions;
                //For testing
                _iRpmMargin = rpm * .05 > 100 ? 100 / (double)rpm : rpm * .05 / rpm;
                //_iRpmMargin = interpretOptions.IsRpmMargin ? interpretOptions.RpmMargin > 0 ? interpretOptions.RpmMargin / (double) rpm : 1:1;
                GetFaultsName = new List<string>();
                GetFaultDescription = new List<string>();
                CalculatePeaksAndOrder();
                CalculateSpecialPeaksAndOrders();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }


        public void CalculatePeaksAndOrder()
        {
            _peaks = new List<Peak>();
            _peakOrder = new List<double>();
            try
            {
                double fst;
                double scnd;
                double thrd;

                //Fetching the peeks
                try
                {
                    for (int i = 2; i < _dYVals.Length; i++)
                    {
                        fst = _dYVals[i - 2];
                        scnd = _dYVals[i - 1];
                        thrd = _dYVals[i];

                        if (fst < scnd && scnd > thrd)
                        {
                            _peaks.Add(new Peak() {PeakAmplitude = _dYVals[i - 1], PeakLocation = _dXVals[i - 1]});
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                //Sorting Peaks in ascending order based on amplitude
                _peaks.Sort();
                //Rearranging Peeks in descending order based on amplitude
                _peaks.Reverse();

                //Getting highest Peak x (in CPM/RPM) and y
                double highestPeakX = (_peaks[0].PeakLocation * 60);
                double highestPeakY = _peaks[0].PeakAmplitude;

                //Finalizing RPM
                if (_iRpm <= 1)
                {
                    if ((_iRpm - highestPeakX) < Convert.ToInt32(_iRpmMargin * 60))
                    {
                        _iRpm = (int)highestPeakX;                        
                    }
                }
                else if (Convert.ToInt32(_iRpmMargin * _iRpm) > 0)
                {
                    var rpmMarginLower = _iRpm - Convert.ToInt32(_iRpmMargin * _iRpm);
                    var rpmMarginHigher = _iRpm + Convert.ToInt32(_iRpmMargin * _iRpm);
                    if (Enumerable.Range(rpmMarginLower, rpmMarginHigher - rpmMarginLower).Contains((int)highestPeakX))
                    {
                        _iRpm = (int)highestPeakX;
                    }
                }
                if(_iRpm<=1)
                {
                    throw new ApplicationException("Unable to calculate RPM values. Kindly contact administrator");
                }
                _iRpmMargin = _iRpmMargin == 1 ? _iRpmMargin / _iRpm : _iRpmMargin;
                //Maintaining limited peaks subjected to 1 / 5th of the highest peak
                double rangeValue = highestPeakY / 5;
                _peaks.RemoveAll(x => x.PeakAmplitude < rangeValue);

                foreach (var peak in _peaks)
                {
                    _peakOrder.Add(Math.Round(peak.PeakLocation * 60 / _iRpm, 2));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CalculateSpecialPeaksAndOrders()
        {
            _isPpf = false;
            //Find 1X order/x value for further analysis
            int highestPeakX = (int) (_peaks[0].PeakLocation * 60);
            _highestPeakMargin = 1 / (double) highestPeakX;

            //Calculating Belt related Peaks and orders
            if (_interpretOptions.IsBeltDrive)
            {
                int drivenRpm = _iRpm * _interpretOptions.DrivingSheaveDiameter /
                                _interpretOptions.DrivenSheaveDiameter;
                double beltFreq = 3.1416 * drivenRpm * _interpretOptions.DrivenSheaveDiameter /
                                  _interpretOptions.BeltLength;
                _peaksBr = new List<double>();

                foreach (var peak in _peaks)
                    _peaksBr.Add(Math.Round(peak.PeakLocation * 60 / beltFreq, 2));

            }

            //calculating Gear Mesh Frequency
            if (_interpretOptions.IsGearBox)
            {
                int gmf = _iRpm * _interpretOptions.GearTooth;
                _peakOrderGmf = new List<double>();
                int frequencyPinion = _interpretOptions.PinionRpm * _interpretOptions.PinionTooth;
                _peakOrderPinion = new List<double>();
                foreach (var peak in _peaks)
                {
                    _peakOrderGmf.Add(Math.Round(peak.PeakLocation * 60 / gmf, 2));
                    _peakOrderPinion.Add(Math.Round(peak.PeakLocation * 60 / frequencyPinion, 2));
                }
            }

            //RotorBarFrequency
            //if (_interpretOptions.IsGearBox)
            {
                _rbf = _interpretOptions.RotorBarCount * _iRpm; // Number of RotorBars * RPM
                _rbfPeeks = new List<double>();
                foreach (var peak in _peaks)
                {
                    _rbfPeeks.Add(Math.Round(peak.PeakLocation * 60 / _rbf, 2));
                }
            }

            //LineFrequency
            _lfPeeks = new List<double>();
            foreach (var peak in _peaks)
            {
                _lfPeeks.Add(Math.Round(peak.PeakLocation * 60 / _interpretOptions.LineFrequency, 2));
            }

            //BladePassFrequency
            int bladePass = _interpretOptions.BladeCount * _iRpm; //BladeCount * RPM
            _bpPeeks = new List<double>();
            foreach (var peak in _peaks)
            {
                _bpPeeks.Add(Math.Round(peak.PeakLocation * 60 / bladePass, 2));
            }

            //VanePassFrequency
            int vanePass = _interpretOptions.VaneCount * _iRpm; //vanecount * RPM
            _vpPeeks = new List<double>();
            foreach (var peak in _peaks)
            {
                _vpPeeks.Add(Math.Round(peak.PeakLocation * 60 / vanePass, 2));
            }

            //PolePassFrequency
            int syncSpeed = _interpretOptions.LineFrequency * 2 / _interpretOptions.PoleCount;
            int slipFrequency = Math.Abs(syncSpeed - _iRpm);
            _polePass = slipFrequency * _interpretOptions.PoleCount;
            _ppPeeks = new List<double>();
            if (_polePass > 0)
            {
                _isPpf = true;
                
                foreach (var peak in _peaks)
                {
                    _ppPeeks.Add(Math.Round(peak.PeakLocation * 60 / _polePass, 2));
                }
            }

            //CoilPassFrequency
            int coilPass = _interpretOptions.CoilCount * _iRpm;
            _cpfPeeks = new List<double>();
            foreach (var peak in _peaks)
            {
                _cpfPeeks.Add(Math.Round(peak.PeakLocation * 60 / coilPass, 2));
            }

            if (_interpretOptions.IsBearing)
            {
                Dictionary<string, double> BearingFaultFrequencies = _interpretOptions.Control_Bearing._BearingData;
                double BPFO = BearingFaultFrequencies["BPFO"] * _iRpm;
                double BPFI = BearingFaultFrequencies["BPFI"] * _iRpm;
                double FTF = BearingFaultFrequencies["FTF"] * _iRpm;
                double BSF = BearingFaultFrequencies["BSF"] * _iRpm;
                if (BPFO > 0 && BPFI > 0 && BSF > 0 && FTF > 0)
                {
                    _bpfoPeeks = new List<double>();
                    _bpfiPeeks = new List<double>();
                    _ftfPeeks = new List<double>();
                    _bsfPeeks = new List<double>();
                    foreach (var peak in _peaks)
                    {
                        _bpfoPeeks.Add(Math.Round(peak.PeakLocation * 60 / BPFO, 2));
                        _bpfiPeeks.Add(Math.Round(peak.PeakLocation * 60 / BPFI, 2));
                        _bsfPeeks.Add(Math.Round(peak.PeakLocation * 60 / BSF, 2));
                        _ftfPeeks.Add(Math.Round(peak.PeakLocation * 60 / FTF, 2));
                    }
                }
            }
        }

        public void InterpretBeltFaults()
        {
            if (_peaksBr.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                _peaksBr.Exists(x => x <= 2 + _iRpmMargin && x >= 2 - _iRpmMargin) &&
                _peaksBr.Exists(x => x <= 3 + _iRpmMargin && x >= 3 - _iRpmMargin) &&
                _peaksBr.Exists(x => x <= 4 + _iRpmMargin && x >= 4 - _iRpmMargin) &&
                _peakOrder.Exists(x => x >= 1 - _iRpmMargin && x <= 1 + _iRpmMargin))
            {
                GetFaultsName.Add("Belt drives: Worn belts");
                GetFaultDescription.Add("Description :" + Environment.NewLine +
                                        "Belt rate with harmonics");
            }
        }

        public void InterpretGearFaults()
        {
            //if peak found on GMF and GMF+xVal and GMF-xVal and 1X and 2X then GearBox: Gear Mesh
            if (_peakOrder.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                _peakOrder.Exists(x => x <= 2 + _iRpmMargin && x >= 2 - _iRpmMargin) &&
                _peakOrderGmf.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                _peakOrderGmf.Exists(x =>
                    x <= 1 + _highestPeakMargin + _iRpmMargin && x >= 1 + _highestPeakMargin - _iRpmMargin) &&
                _peakOrderGmf.Exists(x =>
                    x <= 1 - _highestPeakMargin + _iRpmMargin && x >= 1 - _highestPeakMargin - _iRpmMargin) &&
                _peakOrderPinion.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin))
            {
                GetFaultsName.Add("GearBox: Gear Mesh problem");
                GetFaultDescription.Add("Description :" + Environment.NewLine +
                                        "Peak found on GMF and GMF side bands and 1X and 2X and Pinion 1X");
            }


            //if peak found on GMF and its sidebands with 1X-xVal for upto 3XGMF and 1X then GearBox: ToothWear
            if (_peakOrder.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                _peakOrderGmf.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                _peakOrderGmf.Exists(x =>
                    x <= 1 + _highestPeakMargin + _iRpmMargin && x >= 1 + _highestPeakMargin - _iRpmMargin) &&
                _peakOrderGmf.Exists(x =>
                    x <= 1 - _highestPeakMargin + _iRpmMargin && x >= 1 - _highestPeakMargin - _iRpmMargin) &&
                _peakOrderGmf.Exists(x =>
                    x <= 2 + _highestPeakMargin + _iRpmMargin && x >= 2 + _highestPeakMargin - _iRpmMargin) &&
                _peakOrderGmf.Exists(x =>
                    x <= 2 - _highestPeakMargin + _iRpmMargin && x >= 2 - _highestPeakMargin - _iRpmMargin) &&
                _peakOrderGmf.Exists(x =>
                    x <= 3 + _highestPeakMargin + _iRpmMargin && x >= 3 + _highestPeakMargin - _iRpmMargin) &&
                _peakOrderGmf.Exists(x =>
                    x <= 3 - _highestPeakMargin + _iRpmMargin && x >= 3 - _highestPeakMargin - _iRpmMargin) &&
                _peakOrderPinion.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin))
            {
                GetFaultsName.Add("GearBox: ToothWear problem / Eccentric gears problem / Gear backlash");
                GetFaultDescription.Add("Description :" + Environment.NewLine +
                                        "Peak found on GMF and its sidebands for upto 3XGMF and 1X and Pinion 1X");
            }



            //if GMF is highest with 1X-xVal SideBands and 1X and 2X available then Gearbox: Tooth Load
            if (_peakOrder.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                _peakOrder.Exists(x => x <= 2 + _iRpmMargin && x >= 2 - _iRpmMargin) &&
                _peakOrderGmf.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                _peakOrderGmf.Exists(x =>
                    x <= 1 + _highestPeakMargin + _iRpmMargin && x >= 1 + _highestPeakMargin - _iRpmMargin) &&
                _peakOrderGmf.Exists(x =>
                    x <= 1 - _highestPeakMargin + _iRpmMargin && x >= 1 - _highestPeakMargin - _iRpmMargin) &&
                _peakOrderGmf.Exists(x =>
                    x <= 2 + _highestPeakMargin + _iRpmMargin && x >= 2 + _highestPeakMargin - _iRpmMargin) &&
                _peakOrderGmf.Exists(x =>
                    x <= 2 - _highestPeakMargin + _iRpmMargin && x >= 2 - _highestPeakMargin - _iRpmMargin) &&
                _peakOrderGmf.Exists(x =>
                    x <= 3 + _highestPeakMargin + _iRpmMargin && x >= 3 + _highestPeakMargin - _iRpmMargin) &&
                _peakOrderGmf.Exists(x =>
                    x <= 3 - _highestPeakMargin + _iRpmMargin && x >= 3 - _highestPeakMargin - _iRpmMargin) &&
                _peakOrderPinion.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin))
            {
                if (_peakOrderGmf.FindIndex(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) >
                    _peakOrderGmf.FindIndex(x => x <= 2 + _iRpmMargin && x >= 2 - _iRpmMargin))
                {
                    GetFaultsName.Add("GearBox: ToothLoad problem");
                    GetFaultDescription.Add("Description :" + Environment.NewLine +
                                            "Peak found on GMF and its sidebands upto 3XGMF and 1X and 2X with Pinion 1X and 1xGMF is higher than 2xGMF");
                }
                else
                {
                    GetFaultsName.Add("GearBox: Misaligned gears");
                    GetFaultDescription.Add("Description :" + Environment.NewLine +
                                            "Peak found on GMF and its sidebands upto 3XGMF and 1X and 2X with Pinion 1X and 1xGMF is lower than 2xGMF");
                }
            }

            //Gearbox: Cracked or broken tooth
            if (_peakOrder.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                _peakOrderGmf.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                _peakOrderGmf.Exists(x =>
                    x <= 1 + _highestPeakMargin + _iRpmMargin && x >= 1 + _highestPeakMargin - _iRpmMargin) &&
                _peakOrderGmf.Exists(x =>
                    x <= 1 - _highestPeakMargin + _iRpmMargin && x >= 1 - _highestPeakMargin - _iRpmMargin))
            {
                GetFaultsName.Add("Gearbox: Cracked or broken tooth");
                GetFaultDescription.Add("Description :" + Environment.NewLine +
                                        "Peak found on GMF and GMF side bands and 1X ");
            }

            //Gearbox: Hunting tooth frequency
            if (_peakOrder.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                _peakOrder.Exists(x => x <= 2 + _iRpmMargin && x >= 2 - _iRpmMargin) &&
                _peakOrderPinion.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin))
            {
                GetFaultsName.Add("Gearbox: Hunting tooth frequency");
                GetFaultDescription.Add("Description :" + Environment.NewLine +
                                        "Peak found on 1X and 2X and Pinion 1X");
            }
        }

        public void InterpretBasicFaults()
        {
            //if only 1 peak and 1x peak is highest
            // unbalance/looseness or Eccentricity: Eccentricity general comments
            //if (_allPeak == 1)
            if (_peaks.Count >= 1)
            {
                if (_peakOrder[0] <= 1.0 + _iRpmMargin && _peakOrder[0] >= 1.0 - _iRpmMargin)
                {
                    GetFaultsName.Add("Unbalance / Losseness Structural");
                    GetFaultDescription.Add("Description :" + Environment.NewLine +
                                            "There is only one significant peak on first order");
                }

                if ((_peakOrder[0] <= 1.0 + _iRpmMargin && _peakOrder[0] >= 1.0 - _iRpmMargin) &&
                    (_interpretOptions.Direction == "Horizontal" || _interpretOptions.Direction == "Vertical"))
                {
                    GetFaultsName.Add("Eccentricity: Eccentricity general comments" + "\n" +
                                      "Induction motors: Rotor bow" + "\n" + "Belt drives: Belt resonance ");
                    GetFaultDescription.Add("Description :" + Environment.NewLine +
                                            "There is only one significant peak on first order with Radial Direction");
                }


                if ((_peakOrder[0] <= 1.0 + _iRpmMargin && _peakOrder[0] >= 1.0 - _iRpmMargin) &&
                    _interpretOptions.Direction == "Axial")
                {
                    GetFaultsName.Add("Belt drives: Sheave misalignment");
                    GetFaultDescription.Add("Description :" + Environment.NewLine +
                                            "There is only one significant peak on first order with Axial Direction");
                }
            }


            //if (_allPeak == 2)
            if (_peaks.Count >= 2)
            {
                //if 2 peaks and 1x is highest
                //Bent shaft
                if (_peakOrder[0] <= 1.0 + _iRpmMargin && _peakOrder[0] >= 1.0 - _iRpmMargin &&
                    _peakOrder[1] <= 2.0 + _iRpmMargin && _peakOrder[1] >= 2.0 - _iRpmMargin)
                {
                    GetFaultsName.Add("Bent Shaft / Unbalance " + "\n" + " Couplings: Coupling unbalance");
                    GetFaultDescription.Add("Description :" + Environment.NewLine +
                                            "There are two significant peaks and also first is on first order and second is on second order ");
                }

                //if 2 peaks and 0.42x-0.48x is highest
                //oil whirl instability
                if (_peakOrder[0] <= .48 + _iRpmMargin && _peakOrder[0] >= 0.42 - _iRpmMargin &&
                    _peakOrder[1] <= 1.0 + _iRpmMargin && _peakOrder[1] >= 1.0 - _iRpmMargin)
                {
                    GetFaultsName.Add("Sleeve Bearing - Oil Whirl Instability");
                    GetFaultDescription.Add("Description :" + Environment.NewLine +
                                            "There are two significant peaks and first is on .42 - .48 X RPM");
                }

                if (_peakOrder.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 2 + _iRpmMargin && x >= 2 - _iRpmMargin) &&
                    _interpretOptions.Direction == "Axial" &&
                    _peakOrder.FindIndex(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) <
                    _peakOrder.FindIndex(x => x <= 2 + _iRpmMargin && x >= 2 - _iRpmMargin))
                {
                    GetFaultsName.Add("Couplings: Non-parallel coupling faces");
                    GetFaultDescription.Add("Description :" + Environment.NewLine +
                                            "High 1X peak in the axial direction");
                }
            }

            //if (_allPeak == 3)
            if (_peaks.Count >= 3)
            {
                //if 3 peaks and 1x is highest, 2x is second highest
                //Angular misalignment
                if (_peakOrder[0] <= 1.0 + _iRpmMargin && _peakOrder[0] >= 1.0 - _iRpmMargin &&
                    _peakOrder[1] <= 2.0 + _iRpmMargin && _peakOrder[1] >= 2.0 - _iRpmMargin)
                {
                    GetFaultsName.Add("Angular Missalignment / Unbalance");
                    GetFaultDescription.Add("Description :" + Environment.NewLine +
                                            "There are three significant peaks and also first is on first order and second is on third order");
                }

                //if 3 peaks and 2x is highest, 1x is second highest
                //Parallel misalignment/cocked bearing
                if (_peakOrder[1] <= 1.0 + _iRpmMargin && _peakOrder[0] >= 1.0 - _iRpmMargin &&
                    _peakOrder[0] <= 2.0 + _iRpmMargin && _peakOrder[1] >= 2.0 - _iRpmMargin &&
                    _interpretOptions.Direction == "Axial")
                {
                    GetFaultsName.Add("Cocked bearing / Parallel Misallignment");
                    GetFaultDescription.Add("Description :" + Environment.NewLine +
                                            "There are three significant peaks where first is on second or first order and second is on first or second order and third is on third order  ");
                }
            }

            //if 4 peaks and 2x highest, 1x second highest, 3x third highest
            //Mechanical looseness
            //if (_allPeak == 4)
            if (_peaks.Count >= 4)
            {
                if (_peakOrder[1] <= 1.0 + _iRpmMargin && _peakOrder[0] >= 1.0 - _iRpmMargin &&
                    _peakOrder[0] <= 2.0 + _iRpmMargin && _peakOrder[1] >= 2.0 - _iRpmMargin &&
                    _peakOrder.Exists(x => x <= 0.5 + _iRpmMargin && x >= 0.5 - _iRpmMargin))
                {
                    GetFaultsName.Add("Mechanical looseness - Type B");
                    GetFaultDescription.Add("Description :" + Environment.NewLine +
                                            "There are Four significant peaks where 2x highest, 1x second highest and a peak is found at 1/2 X RPM");
                }
            }


            if (_peakOrder.Exists(x => x <= 3 + _iRpmMargin && x >= 3 - _iRpmMargin) &&
                _peakOrder.Exists(x => x <= 6 + _iRpmMargin && x >= 6 - _iRpmMargin) &&
                _peakOrder.Exists(x => x <= 9 + _iRpmMargin && x >= 9 - _iRpmMargin))
            {
                GetFaultsName.Add("Couplings: Misaligned 3-jaw coupling");
                GetFaultDescription.Add("Description :" + Environment.NewLine +
                                        "There are significant peaks at 3X, 6X and 9X");
            }


            if (_peakOrder.Exists(x => x <= 3 + _iRpmMargin && x >= 3 - _iRpmMargin) &&
                _interpretOptions.Direction == "Axial")
            {
                GetFaultsName.Add("Couplings: Locked gearflex coupling");
                GetFaultDescription.Add("Description :" + Environment.NewLine +
                                        "High axial vibration and the presence of the 3X peak");
            }

            if (_vpPeeks.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) ||
                _bpPeeks.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin))
            {
                GetFaultsName.Add("Hydraulic and aerodynamic: Flow turbulence");
                GetFaultDescription.Add("Description :" + Environment.NewLine +
                                        "Peak at the blade pass or vane pass frequency");
            }

            //Induction motors: Loose rotor
            if (_peakOrder.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                _peakOrder.Exists(x => x <= 2 + _iRpmMargin && x >= 2 - _iRpmMargin) &&
                _peakOrder.Exists(x => x <= 3 + _iRpmMargin && x >= 3 - _iRpmMargin) &&
                _peakOrder.Exists(x => x <= 4 + _iRpmMargin && x >= 4 - _iRpmMargin))
            {
                GetFaultsName.Add("Induction motors: Loose rotor");
                GetFaultDescription.Add("Description :" + Environment.NewLine +
                                        "There presence of running speed harmonics");
            }

            if (_peakOrder.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                _peakOrder.Exists(x => x <= 2 + _iRpmMargin && x >= 2 - _iRpmMargin) &&
                _peakOrder.Exists(x => x <= 3 + _iRpmMargin && x >= 3 - _iRpmMargin) &&
                _peakOrder.Exists(x => x <= 4 + _iRpmMargin && x >= 4 - _iRpmMargin) &&
                _peakOrder.Exists(x => x <= 5 + _iRpmMargin && x >= 5 - _iRpmMargin))
            {
                GetFaultsName.Add("Couplings: Coupling wear");
                GetFaultDescription.Add("Description :" + Environment.NewLine +
                                        "There presence of Harmonics of 1X");
            }

            //if >7 peaks and 3x highest, 1x second highest, 2x third, 4x fourth, 5x fifth, 6x sixth and 7x seventh
            //Wear/Clearence problem
            //if (_allPeak > 7)
            if (_peaks.Count >= 7)
            {
                if (_peakOrder.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 2 + _iRpmMargin && x >= 2 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 3 + _iRpmMargin && x >= 3 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 4 + _iRpmMargin && x >= 4 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 5 + _iRpmMargin && x >= 5 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 6 + _iRpmMargin && x >= 6 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 7 + _iRpmMargin && x >= 7 - _iRpmMargin))
                {
                    GetFaultsName.Add("Sleeve Bearing - Wear/Clearence problem");
                    GetFaultDescription.Add("Description :" + Environment.NewLine +
                                            "There presence of running speed harmonics");
                }
            }

            //if >10 peaks
            //if (_allPeak > 10)
            if (_peaks.Count >= 10)
            {
                if (_peakOrder.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 2 + _iRpmMargin && x >= 2 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 3 + _iRpmMargin && x >= 3 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 4 + _iRpmMargin && x >= 4 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 5 + _iRpmMargin && x >= 5 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 0.5 + _iRpmMargin && x >= 0.5 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 1.5 + _iRpmMargin && x >= 1.5 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 2.5 + _iRpmMargin && x >= 2.5 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 3.5 + _iRpmMargin && x >= 3.5 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 4.5 + _iRpmMargin && x >= 4.5 - _iRpmMargin))
                {
                    GetFaultsName.Add("Rotor Rub problem");
                    GetFaultDescription.Add("Description :" + Environment.NewLine +
                                            "There presence of running speed harmonics and sub harmonics as well");

                }

                if (_peakOrder.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 2 + _iRpmMargin && x >= 2 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 3 + _iRpmMargin && x >= 3 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 4 + _iRpmMargin && x >= 4 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 5 + _iRpmMargin && x >= 5 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 0.5 + _iRpmMargin && x >= 0.5 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 1.5 + _iRpmMargin && x >= 1.5 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 6 + _iRpmMargin && x >= 6 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 7 + _iRpmMargin && x >= 7 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 8 + _iRpmMargin && x >= 8 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 9 + _iRpmMargin && x >= 9 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 10 + _iRpmMargin && x >= 10 - _iRpmMargin))
                {
                    GetFaultsName.Add("Mechanical looseness - Type C");
                    GetFaultDescription.Add("Description :" + Environment.NewLine +
                                            "There is presence of running speed harmonics and sub harmonics as well");
                }

                if (_peakOrder[0] <= 1.0 + _iRpmMargin && _peakOrder[0] >= 1.0 - _iRpmMargin &&
                    _peakOrder[1] <= 2.0 + _iRpmMargin && _peakOrder[1] >= 2.0 - _iRpmMargin &&
                    _peakOrder.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 2 + _iRpmMargin && x >= 2 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 3 + _iRpmMargin && x >= 3 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 4 + _iRpmMargin && x >= 4 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 5 + _iRpmMargin && x >= 5 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 6 + _iRpmMargin && x >= 6 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 7 + _iRpmMargin && x >= 7 - _iRpmMargin))
                {
                    GetFaultsName.Add(" Looseness Rotating / Unbalance");
                    GetFaultDescription.Add("Description :" + Environment.NewLine +
                                            "There are multiple significant peaks and also peaks are on first , second and so on up to tenth order.  ");
                }

                if (_peakOrder.Exists(x => x <= 3 + _iRpmMargin && x >= 3 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 6 + _iRpmMargin && x >= 6 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 9 + _iRpmMargin && x >= 9 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 2 + _iRpmMargin && x >= 2 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 4 + _iRpmMargin && x >= 4 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 5 + _iRpmMargin && x >= 5 - _iRpmMargin))
                {
                    GetFaultsName.Add("Rolling element bearings: Inner race sliding on shaft");
                    GetFaultDescription.Add("Description :" + Environment.NewLine +
                                            "The vibration spectrum have an elevated 3X peak and the harmonics of the 3X frequency.");
                }

                if (_peakOrder[0] <= 4.0 + _iRpmMargin && _peakOrder[0] >= 4.0 - _iRpmMargin &&
                    _peakOrder.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 2 + _iRpmMargin && x >= 2 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 3 + _iRpmMargin && x >= 3 - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 5 + _iRpmMargin && x >= 5 - _iRpmMargin))
                {
                    GetFaultsName.Add("Rolling element bearings: Outer race loose in housing");
                    GetFaultDescription.Add("Description :" + Environment.NewLine +
                                            "Peaks at 4X running speed are elevated");

                }
            }
        }


        public void InterpretMotorFaults()
        {
            if (_rbfPeeks.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                _peakOrder.Exists(x =>
                    x <= _rbf - 2 * _interpretOptions.LineFrequency + _iRpmMargin &&
                    x >= _rbf - 2 * _interpretOptions.LineFrequency - _iRpmMargin) &&
                _peakOrder.Exists(x =>
                    x <= _rbf + 2 * _interpretOptions.LineFrequency + _iRpmMargin &&
                    x >= _rbf + 2 * _interpretOptions.LineFrequency - _iRpmMargin))
            {
                GetFaultsName.Add("Symptoms: Loose rotor bars or Type II rotor faults");
                GetFaultDescription.Add("Description :" + Environment.NewLine +
                                        "A peak at the rotor bar pass frequency with sidebands of twice line frequency");
            }

            if (_peakOrder.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                _peakOrder.Exists(x => x <= 2 + _iRpmMargin && x >= 2 - _iRpmMargin) &&
                _lfPeeks.Exists(x => x <= 2 + _iRpmMargin && x >= 2 - _iRpmMargin) &&
                _peakOrder.Exists(x =>
                    x <= 2 * _interpretOptions.LineFrequency - _interpretOptions.LineFrequency / 3 + _iRpmMargin && x >=
                    2 * _interpretOptions.LineFrequency - _interpretOptions.LineFrequency / 3 - _iRpmMargin) &&
                _peakOrder.Exists(x =>
                    x <= 2 * _interpretOptions.LineFrequency + _interpretOptions.LineFrequency / 3 + _iRpmMargin && x >=
                    2 * _interpretOptions.LineFrequency + _interpretOptions.LineFrequency / 3 - _iRpmMargin))
            {
                GetFaultsName.Add("Induction motors: Loose connections");
                GetFaultDescription.Add("Description :" + Environment.NewLine +
                                        "High 100/120 Hz with 33/40 Hz sidebands");
            }

            if (_peakOrder.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                _peakOrder.Exists(x => x <= 2 + _iRpmMargin && x >= 2 - _iRpmMargin) &&
                _lfPeeks.Exists(x => x <= 2 + _iRpmMargin && x >= 2 - _iRpmMargin))
            {
                GetFaultsName.Add("Induction motors: Stator eccentricity or soft foot");
                GetFaultDescription.Add("Description :" + Environment.NewLine +
                                        "Twice line frequency (100 or 120 Hz) radial");
            }


            if (_peakOrder.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                _lfPeeks.Exists(x => x <= 2 + _iRpmMargin && x >= 2 - _iRpmMargin))
            {
                GetFaultsName.Add("Induction motors: Loose stator windings");
                GetFaultDescription.Add("Description :" + Environment.NewLine +
                                        "High 100/120 Hz radial");
            }

            if (_isPpf)
            {
                if (_peakOrder.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                    _lfPeeks.Exists(x => x <= 2 + _iRpmMargin && x >= 2 - _iRpmMargin) &&
                    _peakOrder.Exists(x =>
                        x <= 1 - (_polePass / 60) + _iRpmMargin && x >= 1 - (_polePass / 60) - _iRpmMargin) &&
                    _peakOrder.Exists(x =>
                        x <= 1 + (_polePass / 60) + _iRpmMargin && x >= 1 + (_polePass / 60) - _iRpmMargin))
                {
                    GetFaultsName.Add("Induction motors: Shorted laminations");
                    GetFaultDescription.Add("Description :" + Environment.NewLine +
                                            "Large peak at 2xLF and presence of the pole-pass sidebands around 1xRPM");
                }

                if (_peakOrder.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                    _ppPeeks.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                    _lfPeeks.Exists(x => x <= 2 + _iRpmMargin && x >= 2 - _iRpmMargin) &&
                    _peakOrder.Exists(x =>
                        x <= 1 - (_polePass / 60) + _iRpmMargin && x >= 1 - (_polePass / 60) - _iRpmMargin) &&
                    _peakOrder.Exists(x =>
                        x <= 1 + (_polePass / 60) + _iRpmMargin && x >= 1 + (_polePass / 60) - _iRpmMargin) &&
                    _lfPeeks.Exists(x =>
                        x <= 2 - (_polePass / 60) + _iRpmMargin && x >= 2 - (_polePass / 60) - _iRpmMargin) &&
                    _lfPeeks.Exists(x =>
                        x <= 2 + (_polePass / 60) + _iRpmMargin && x >= 2 + (_polePass / 60) - _iRpmMargin))
                {
                    GetFaultsName.Add("Eccentricity: Eccentric motor rotor");
                    GetFaultDescription.Add("Description :" + Environment.NewLine +
                                            "Presence of the pole-pass sidebands around 1xTS and 2xLF");
                }

                if (_peakOrder.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                    _peakOrder.Exists(x =>
                        x <= 1 - (_polePass / 60) + _iRpmMargin && x >= 1 - (_polePass / 60) - _iRpmMargin) &&
                    _peakOrder.Exists(x =>
                        x <= 1 + (_polePass / 60) + _iRpmMargin && x >= 1 + (_polePass / 60) - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 2 + _iRpmMargin && x >= 2 - _iRpmMargin) &&
                    _peakOrder.Exists(x =>
                        x <= 2 - (_polePass / 60) + _iRpmMargin && x >= 2 - (_polePass / 60) - _iRpmMargin) &&
                    _peakOrder.Exists(x =>
                        x <= 2 + (_polePass / 60) + _iRpmMargin && x >= 2 + (_polePass / 60) - _iRpmMargin) &&
                    _peakOrder.Exists(x => x <= 3 + _iRpmMargin && x >= 3 - _iRpmMargin) &&
                    _peakOrder.Exists(x =>
                        x <= 3 - (_polePass / 60) + _iRpmMargin && x >= 3 - (_polePass / 60) - _iRpmMargin) &&
                    _peakOrder.Exists(x =>
                        x <= 3 + (_polePass / 60) + _iRpmMargin && x >= 3 + (_polePass / 60) - _iRpmMargin))
                {
                    GetFaultsName.Add("Induction motors: Type I rotor faults");
                    GetFaultDescription.Add("Description :" + Environment.NewLine +
                                            "Pole pass sidebands around 1X and harmonics");
                }
            }

            if (_peakOrder.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                _ppPeeks.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                _lfPeeks.Exists(x => x <= 2 + _iRpmMargin && x >= 2 - _iRpmMargin))
            {
                GetFaultsName.Add("Eccentricity: Eccentric motor stator");
                GetFaultDescription.Add("Description :" + Environment.NewLine +
                                        "Twice line frequency (100 or 120 Hz) radial");
            }

            if (_rbfPeeks.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                _rbfPeeks.Exists(x =>
                    x <= 1 - ((2 * _interpretOptions.LineFrequency) / (60 * _rbf)) + _iRpmMargin &&
                    x >= 1 - ((2 * _interpretOptions.LineFrequency) / (60 * _rbf)) - _iRpmMargin) &&
                _rbfPeeks.Exists(x =>
                    x <= 1 + ((2 * _interpretOptions.LineFrequency) / (60 * _rbf)) + _iRpmMargin &&
                    x >= 1 + ((2 * _interpretOptions.LineFrequency) / (60 * _rbf)) - _iRpmMargin))

            {
                GetFaultsName.Add("Induction motors: Cracked or broken rotor bars");
                GetFaultDescription.Add("Description :" + Environment.NewLine +
                                        "2xLF sidebands around RBF");
            }

            if (_cpfPeeks.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                _peakOrder.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin))
            {
                GetFaultsName.Add("Synchronous motors: Loose stator coils");
                GetFaultDescription.Add("Description :" + Environment.NewLine +
                                        "High coil passing frequency with possible 1X sidebands");
            }

            if ((_lfPeeks.Exists(x => x <= 3 + _iRpmMargin && x >= 3 - _iRpmMargin) ||
                 _lfPeeks.Exists(x => x <= 6 + _iRpmMargin && x >= 6 - _iRpmMargin)) &&
                _peakOrder.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin))
            {
                GetFaultsName.Add("DC motors: General fault comment");
                GetFaultDescription.Add("Description :" + Environment.NewLine +
                                        "3xLF if half-wave rectification or 6xLF if full-wave rectification");
            }

            if (_lfPeeks.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin))
            {
                GetFaultsName.Add("DC motors: Grounding fault");
                GetFaultDescription.Add("Description :" + Environment.NewLine +
                                        "Elevated peak at the line frequency");
            }

            if ((_lfPeeks.Exists(x => x <= 3 + _iRpmMargin && x >= 3 - _iRpmMargin) ||
                 _lfPeeks.Exists(x => x <= 6 + _iRpmMargin && x >= 6 - _iRpmMargin)) &&
                (_lfPeeks.Exists(x => x <= 6 + _iRpmMargin && x >= 6 - _iRpmMargin) ||
                 _lfPeeks.Exists(x => x <= 12 + _iRpmMargin && x >= 12 - _iRpmMargin)) &&
                _peakOrder.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin))
            {
                GetFaultsName.Add("DC motors: SCR Tuning faults");
                GetFaultDescription.Add("Description :" + Environment.NewLine +
                                        "1X sidebands around 1XSCR and 2XSCR firing frequencies");
            }

            if ((_lfPeeks.Exists(x => x <= 3 + _iRpmMargin && x >= 3 - _iRpmMargin) ||
                 _lfPeeks.Exists(x => x <= 6 + _iRpmMargin && x >= 6 - _iRpmMargin)) &&
                _peakOrder.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                _peakOrder.Exists(x => x <= 2 + _iRpmMargin && x >= 2 - _iRpmMargin))
            {
                GetFaultsName.Add("DC motors: DC motor hunting");
                GetFaultDescription.Add("Description :" + Environment.NewLine +
                                        "Sidebands around 1XSCR  firing frequencies with 1XRPM and 2XRPM");
            }

            if ((_lfPeeks.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) ||
                 _lfPeeks.Exists(x => x <= 2 + _iRpmMargin && x >= 2 - _iRpmMargin)) &&
                (_lfPeeks.Exists(x => x <= 2 + _iRpmMargin && x >= 2 - _iRpmMargin) ||
                 _lfPeeks.Exists(x => x <= 4 + _iRpmMargin && x >= 4 - _iRpmMargin)) &&
                (_lfPeeks.Exists(x => x <= 3 + _iRpmMargin && x >= 3 - _iRpmMargin) ||
                 _lfPeeks.Exists(x => x <= 6 + _iRpmMargin && x >= 6 - _iRpmMargin)) &&
                _peakOrder.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin))
            {
                GetFaultsName.Add("DC motors: Phase loss");
                GetFaultDescription.Add("Description :" + Environment.NewLine +
                                        "Peak at 1/3xSCR, 2/3xSCR and 1xSCR");
            }

            if (_lfPeeks.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                _lfPeeks.Exists(x => x <= 2 + _iRpmMargin && x >= 2 - _iRpmMargin) &&
                _lfPeeks.Exists(x => x <= 3 + _iRpmMargin && x >= 3 - _iRpmMargin) &&
                _lfPeeks.Exists(x => x <= 4 + _iRpmMargin && x >= 4 - _iRpmMargin) &&
                _lfPeeks.Exists(x => x <= 5 + _iRpmMargin && x >= 5 - _iRpmMargin) &&
                _lfPeeks.Exists(x => x <= 6 + _iRpmMargin && x >= 6 - _iRpmMargin) &&
                _peakOrder.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin))
            {
                GetFaultsName.Add("DC motors: Loose connectors, shorted control card and more");
                GetFaultDescription.Add("Description :" + Environment.NewLine +
                                        "Harmonics of line frequency");
            }
        }

        public void InterpretBearingFaults()
        {
            if (_bpfoPeeks.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                _bpfoPeeks.Exists(x => x <= 2 + _iRpmMargin && x >= 2 - _iRpmMargin) &&
                _bpfoPeeks.Exists(x => x <= 3 + _iRpmMargin && x >= 3 - _iRpmMargin))
            {
                GetFaultsName.Add("Bearing BPFO: Bearing Defect in outer race");
                GetFaultDescription.Add("Description :" + Environment.NewLine +
                                        "Harmonics of BPFO");
            }

            if (_bpfiPeeks.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                _bpfiPeeks.Exists(x => x <= 2 + _iRpmMargin && x >= 2 - _iRpmMargin) &&
                _bpfiPeeks.Exists(x => x <= 3 + _iRpmMargin && x >= 3 - _iRpmMargin))
            {
                GetFaultsName.Add("Bearing BPFI: Bearing Defect in inner race");
                GetFaultDescription.Add("Description :" + Environment.NewLine +
                                        "Harmonics of BPFI");
            }

            if (_bsfPeeks.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                _bsfPeeks.Exists(x => x <= 2 + _iRpmMargin && x >= 2 - _iRpmMargin) &&
                _bsfPeeks.Exists(x => x <= 3 + _iRpmMargin && x >= 3 - _iRpmMargin))
            {
                GetFaultsName.Add("Bearing BSF: Bearing Defect in balls");
                GetFaultDescription.Add("Description :" + Environment.NewLine +
                                        "Harmonics of BSF");
            }

            if (_ftfPeeks.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                _ftfPeeks.Exists(x => x <= 2 + _iRpmMargin && x >= 2 - _iRpmMargin) &&
                _ftfPeeks.Exists(x => x <= 3 + _iRpmMargin && x >= 3 - _iRpmMargin))
            {
                GetFaultsName.Add("Bearing FTF: Bearing Defect in cage");
                GetFaultDescription.Add("Description :" + Environment.NewLine +
                                        "Harmonics of FTF");
            }
        }

        public void InterpretFanFaults()
        {
            if (_peakOrder.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                _vpPeeks.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                _peakOrder.Exists(x =>
                    x <= _interpretOptions.BladeCount - 1 + _iRpmMargin &&
                    x >= _interpretOptions.BladeCount - 1 - _iRpmMargin) &&
                _peakOrder.Exists(x =>
                    x <= _interpretOptions.BladeCount + 1 + _iRpmMargin &&
                    x >= _interpretOptions.BladeCount + 1 - _iRpmMargin) &&
                _bpPeeks.Exists(x => x <= 1 + _iRpmMargin && x >= 1 - _iRpmMargin) &&
                _peakOrder.Exists(x =>
                    x <= _interpretOptions.VaneCount - 1 + _iRpmMargin &&
                    x >= _interpretOptions.VaneCount - 1 - _iRpmMargin) &&
                _peakOrder.Exists(x =>
                    x <= _interpretOptions.VaneCount + 1 + _iRpmMargin &&
                    x >= _interpretOptions.VaneCount + 1 - _iRpmMargin) &&
                _vpPeeks.Exists(x => x <= 2 + _iRpmMargin && x >= 2 - _iRpmMargin) &&
                _peakOrder.Exists(x =>
                    x <= _interpretOptions.BladeCount * 2 - 1 + _iRpmMargin &&
                    x >= _interpretOptions.BladeCount * 2 - 1 - _iRpmMargin) &&
                _peakOrder.Exists(x =>
                    x <= _interpretOptions.BladeCount * 2 + 1 + _iRpmMargin &&
                    x >= _interpretOptions.BladeCount * 2 + 1 - _iRpmMargin) &&
                _bpPeeks.Exists(x => x <= 2 + _iRpmMargin && x >= 2 - _iRpmMargin) &&
                _peakOrder.Exists(x =>
                    x <= _interpretOptions.VaneCount * 2 - 1 + _iRpmMargin &&
                    x >= _interpretOptions.VaneCount * 2 - 1 - _iRpmMargin) &&
                _peakOrder.Exists(x =>
                    x <= _interpretOptions.VaneCount * 2 + 1 + _iRpmMargin &&
                    x >= _interpretOptions.VaneCount * 2 + 1 - _iRpmMargin))
            {
                GetFaultsName.Add("Hydraulic and aerodynamic: Blade faults");
                GetFaultDescription.Add("Description :" + Environment.NewLine +
                                        "A peak at the blade pass or vane pass frequency.  Harmonics exist, and sidebands of the operating speed occur when the vibration is rising and falling with each rotation.");
            }
        }



        public string Description { get; set; }
    }
}

