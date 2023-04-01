using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAVReader
{
    public class ReadWav
    {
        int AvgBytesPerSec = 0;
        int SF = 0;
        int iLastFrequency = 0;
        int iLOR = 0;
        int LineOfResolution = 0;
        double ExectTime = 0;
        int ExectDataByteSample = 0;
        int TotalDataBytes = 0;
        double TimeVal = 0;
        int TotalTime = 0;
        int channel = 1;
        int iclick = 1;
        double divider_CH1 = 1;
        double divider_CH2 = 1;
        double sensitivity_CH1 = 0;
        double sensitivity_CH2 = 0;
        string label_Ch1 = null;
        string label_Ch2 = null;
        string Name_Ch1 = null;
        string Name_Ch2 = null;
        double[] xarray = new double[0];
        double[] yarray = new double[0];
        int SamplePerSec = 0;
        bool bShowOrbit = false;
        ArrayList arlYData = new ArrayList();
        double[] Fulldata_CH1 = null;
        double[] Fulldata_CH2 = null;
        double[] FullTime_CH1 = null;
        double[] FullTime_CH2 = null;

        public List<List<double>> allXData
        { get; set; }
        public List<List<double>> allYData
        { get; set; }
        //public List<double> completeWaveXData
        //{ get; set; }
        public List<double> completeWaveYData
        { get; set; }
        public double timeDifferenceX
        { get; set; }

        public void ReadFile(string filePath)
        {
            if (filePath == null)
                throw new ArgumentException("File Path is null");
            if (!File.Exists(filePath))
                throw new FileNotFoundException("File path is incorrect");
            try
            {
                allXData = new List<List<double>>();
                allYData = new List<List<double>>();
                completeWaveYData = new List<double>();
                CreateFinalWave(filePath);                
            }
            catch (Exception ex)
            { throw ex; }
        }
        private void CreateFinalWave(string spath)
        {
            ArrayList arlTachoData = new ArrayList();
            int[] LOR = { 100, 200, 400, 800, 1600, 3200, 6400, 12800, 25600, 51200, 102400, 204800 };
            int[] LOR1 = { 256, 512, 1024, 2048, 4096, 8192, 16384, 32738, 65536, 131072, 262144, 524288 };
            AvgBytesPerSec = 0;
            SF = 0;
            iLastFrequency = 0;
            iLOR = 0;
            LineOfResolution = 0;
            ExectTime = 0;
            ExectDataByteSample = 0;
            TotalDataBytes = 0;
            TimeVal = 0;
            TotalTime = 0;
            channel = 1;
            string RIFF = "RIFF";

            string OROS = "oros";
            string fmt = "fmt ";
            string data = "data";
            double[] xData = null;
            int iDataStart = 0;
            try
            {
                iclick = 1;
                using (FileStream wav = new FileStream(spath, FileMode.Open, FileAccess.Read))
                {
                    Byte[] Parameter = new byte[wav.Length];
                    wav.Read(Parameter, 0, Parameter.Length);
                    string SFH = null;
                    //Check Header ID
                    for (int i = 0; i < 4; i++)
                    {
                        int val = Convert.ToInt32(Parameter[i].ToString());
                        char c = (char)val;
                        SFH += c.ToString();
                    }
                    if (SFH == RIFF)
                    {
                        SFH = null;
                        for (int i = 12; i < 16; i++)
                        {
                            int val = Convert.ToInt32(Parameter[i].ToString());
                            char c = (char)val;
                            SFH += c.ToString();
                        }
                        if (SFH == fmt)
                        {
                            SFH = null;
                            for (int i = 23; i > 21; i--)
                            {
                                int val = Convert.ToInt32(Parameter[i].ToString());
                                string sval = DeciamlToHexadeciaml1(val);
                                SFH += SingleToDoubleDigitChar(sval);
                            }
                            channel = HexadecimaltoDecimal(SFH);
                            string[] SFbyteD = new string[4];
                            SFH = null;
                            int ctr = 0;
                            for (int i = 27; i > 23; i--)
                            {
                                int val = Convert.ToInt32(Parameter[i].ToString());
                                string sval = DeciamlToHexadeciaml1(val);
                                SFH += SingleToDoubleDigitChar(sval);
                            }
                            SF = HexadecimaltoDecimal(SFH);//sample per second
                            SFH = null;
                            for (int i = 31; i > 27; i--)
                            {
                                int val = Convert.ToInt32(Parameter[i].ToString());
                                string sval = DeciamlToHexadeciaml1(val);
                                SFH += SingleToDoubleDigitChar(sval);
                            }
                            AvgBytesPerSec = HexadecimaltoDecimal(SFH);//avg byte per second

                            SFH = null;

                            for (int i = 27; i > 23; i--)
                            {
                                string sval = Parameter[i].ToString();
                                SFH += SingleToDoubleDigitChar(sval);
                            }
                            iLastFrequency = Convert.ToInt32(SFH);
                            int iFinalFrequency = iLastFrequency;
                            //lblOrder.Text = iFinalFrequency.ToString();
                            iLOR = 0;
                            for (int i = 0; i < LOR.Length; i++)
                            {
                                double temp = (double)LOR[i] / (double)iLastFrequency;
                                if (temp >= 0.666667)
                                {
                                    iLOR = i;
                                    break;
                                }
                            }
                            LineOfResolution = LOR1[iLOR];
                            int RUCDvariable = (LOR[iLOR] / 100) * 2;
                            if (RUCDvariable < 5)
                            {
                                RUCDvariable = 5;
                            }
                            ExectTime = (double)LOR[iLOR] / (double)iLastFrequency;
                            ExectDataByteSample = LineOfResolution;// Convert.ToInt32((double)SF * ExectTime);
                            SFH = null;

                            for (int i = 36; i < 40; i++)
                            {
                                int val = Convert.ToInt32(Parameter[i].ToString());
                                char c = (char)val;
                                SFH += c.ToString();
                            }

                            // for rene
                            string tempDATA = null;
                            SFH = null;
                            int tempICTR = 36;
                            while (SFH != data)
                            {
                                for (int i = tempICTR; i < tempICTR + 4; i++)
                                {
                                    int val = Convert.ToInt32(Parameter[i].ToString());
                                    char c = (char)val;
                                    tempDATA += c.ToString();
                                }
                                tempICTR++;
                                SFH = tempDATA;
                                tempDATA = null;
                            }
                            //for rene end

                            bool xyz = false;
                            if (SFH == data)
                            {
                                iDataStart = tempICTR + 3;//iDataStart = 44; //For rene commented 
                                SFH = null;
                                for (int i = tempICTR + 6; i > (tempICTR + 2); i--)//for (int i = 43; i > 39; i--) //For rene commented  
                                {
                                    int val = Convert.ToInt32(Parameter[i].ToString());
                                    string sval = DeciamlToHexadeciaml1(val);
                                    SFH += SingleToDoubleDigitChar(sval);
                                }
                                TotalDataBytes = HexadecimaltoDecimal(SFH);
                                TimeVal = Convert.ToDouble(1 / Convert.ToDouble(SF));
                                timeDifferenceX = TimeVal;

                                xData = new double[ExectDataByteSample];

                                for (int i = 0; i < ExectDataByteSample; i++)
                                {
                                    xData[i] = i * TimeVal;
                                }
                                double[] xarray = xData;
                                int SamplePerSec = 0;
                                if (channel == 1)
                                {
                                    SamplePerSec = AvgBytesPerSec / SF;
                                }
                                else if (channel == 2)
                                {
                                    SamplePerSec = AvgBytesPerSec / SF;
                                    SamplePerSec = SamplePerSec / 2;
                                }
                                else
                                {
                                    SamplePerSec = AvgBytesPerSec / SF;
                                    SamplePerSec = SamplePerSec / channel;
                                }
                                TotalTime = TotalDataBytes / AvgBytesPerSec;
                                try
                                {
                                    if (wav.Length > (TotalDataBytes + 44))
                                    {
                                        try
                                        {
                                            SFH = null;
                                            for (int i = TotalDataBytes + 44 + 7; i > TotalDataBytes + 44 + 3; i--)
                                            {
                                                int val = Convert.ToInt32(Parameter[i].ToString());
                                                string sval = DeciamlToHexadeciaml1(val);
                                                SFH += SingleToDoubleDigitChar(sval);
                                            }
                                            int TotalOROSDataBytes = HexadecimaltoDecimal(SFH);

                                            Byte[] orosChunk = new byte[TotalOROSDataBytes];
                                            int orosctr = TotalDataBytes + 44 + 8;
                                            for (int i = 0; i < orosChunk.Length; i++)
                                            {
                                                orosChunk[i] = Parameter[orosctr];
                                                orosctr++;
                                            }

                                             sensitivity_CH1 = 0;
                                             sensitivity_CH2 = 0;
                                             label_Ch1 = null;
                                             label_Ch2 = null;
                                             Name_Ch1 = null;
                                             Name_Ch2 = null;
                                            sensitivity_CH1 = BitConverter.ToSingle(orosChunk, 8);
                                            divider_CH1 = (3.16 * 1.414 / 32768) / sensitivity_CH1;
                                            if (channel == 2)
                                            {
                                                sensitivity_CH2 = BitConverter.ToSingle(orosChunk, 100);
                                                double divider_CH2 = (3.16 * 1.414 / 32768) / sensitivity_CH2;
                                                //lblSensitivity.Text = "CH1 " + Math.Round(sensitivity_CH1, 3).ToString() + ", CH2 " + Math.Round(sensitivity_CH2, 3).ToString();
                                            }
                                            byte[] NameExtracter = new byte[32];
                                            for (int i = 39, j = 0; j < 32; i++, j++)
                                            {
                                                NameExtracter[j] = orosChunk[i];
                                            }
                                            label_Ch1 = Encoding.ASCII.GetString(NameExtracter);
                                            label_Ch1 = label_Ch1.Trim(new char[] { '\0', ' ' });
                                            string[] arrch_label = label_Ch1.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                                            if (arrch_label.Length > 1)
                                            {
                                                label_Ch1 = arrch_label[1].ToString();
                                            }
                                            if (channel == 2)
                                            {
                                                NameExtracter = new byte[32];
                                                for (int i = 131, j = 0; j < 32; i++, j++)
                                                {
                                                    NameExtracter[j] = orosChunk[i];
                                                }
                                                label_Ch2 = Encoding.ASCII.GetString(NameExtracter);
                                                label_Ch2 = label_Ch2.Trim(new char[] { '\0', ' ' });
                                                arrch_label = label_Ch2.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                                                if (arrch_label.Length > 1)
                                                {
                                                    label_Ch2 = arrch_label[1].ToString();
                                                }
                                            }
                                            NameExtracter = new byte[20];
                                            for (int i = 78, j = 0; j < 20; i++, j++)
                                            {
                                                NameExtracter[j] = orosChunk[i];
                                            }

                                            Name_Ch1 = Encoding.ASCII.GetString(NameExtracter);
                                            Name_Ch1 = Name_Ch1.Trim(new char[] { '\0', ' ' });
                                            if (channel == 2)
                                            {
                                                NameExtracter = new byte[20];
                                                for (int i = 170, j = 0; j < 20; i++, j++)
                                                {
                                                    NameExtracter[j] = orosChunk[i];
                                                }

                                                Name_Ch2 = Encoding.ASCII.GetString(NameExtracter);
                                                Name_Ch2 = Name_Ch2.Trim(new char[] { '\0', ' ' });
                                            }
                                            //end
                                        }
                                        catch (Exception exx)
                                        {
                                            xyz = true;
                                        }
                                    }
                                }
                                catch (Exception eex)
                                {
                                    xyz = true;
                                }
                            }

                            if (xyz)
                            {
                                tempDATA = null;
                                SFH = null;
                                tempICTR = 36;
                                while (SFH != OROS)
                                {
                                    if (tempICTR < TotalDataBytes - 100)
                                    {
                                        try
                                        {
                                            for (int i = tempICTR; i < tempICTR + 4; i++)
                                            {
                                                int val = Convert.ToInt32(Parameter[i].ToString());
                                                char c = (char)val;
                                                tempDATA += c.ToString();
                                            }
                                            tempICTR++;
                                            SFH = tempDATA;
                                            tempDATA = null;
                                        }

                                        catch (Exception ex)
                                        {
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                if (SFH == OROS)
                                {
                                    SFH = null;
                                    for (int i = tempICTR + 6; i > (tempICTR + 2); i--) //for (int i = 43; i > 39; i--)
                                    {
                                        int val = Convert.ToInt32(Parameter[i].ToString());
                                        string sval = DeciamlToHexadeciaml1(val);
                                        SFH += SingleToDoubleDigitChar(sval);
                                    }
                                    int TotalOROSDataBytes = HexadecimaltoDecimal(SFH);

                                    Byte[] orosChunk = new byte[TotalOROSDataBytes];
                                    int orosctr = tempICTR + 7;// TotalDataBytes + 36 + 8;
                                    for (int i = 0; i < orosChunk.Length; i++)
                                    {
                                        orosChunk[i] = Parameter[orosctr];
                                        orosctr++;
                                    }

                                    sensitivity_CH1 = 0;
                                    sensitivity_CH2 = 0;
                                    label_Ch1 = null;
                                    label_Ch2 = null;
                                    Name_Ch1 = null;
                                    Name_Ch2 = null;
                                    sensitivity_CH1 = BitConverter.ToSingle(orosChunk, 8);
                                    //lblSensitivity.Text = sensitivity_CH1.ToString();
                                    divider_CH1 = (3.16 * 1.414 / 32768) / sensitivity_CH1;
                                    if (channel == 2)
                                    {
                                        sensitivity_CH2 = BitConverter.ToSingle(orosChunk, 100);
                                        divider_CH2 = (3.16 * 1.414 / 32768) / sensitivity_CH2;
                                        //lblSensitivity.Text = "CH1 " + Math.Round(sensitivity_CH1, 3).ToString() + ", CH2 " + Math.Round(sensitivity_CH2, 3).ToString();
                                    }
                                    byte[] NameExtracter = new byte[32];
                                    for (int i = 39, j = 0; j < 32; i++, j++)
                                    {
                                        NameExtracter[j] = orosChunk[i];
                                    }
                                    label_Ch1 = Encoding.ASCII.GetString(NameExtracter);
                                    label_Ch1 = label_Ch1.Trim(new char[] { '\0', ' ' });
                                    string[] arrch_label = label_Ch1.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                                    if (arrch_label.Length > 1)
                                    {
                                        label_Ch1 = arrch_label[1].ToString();
                                    }
                                    //lblYunit.Text = label_Ch1;
                                    if (channel == 2)
                                    {
                                        NameExtracter = new byte[32];
                                        for (int i = 131, j = 0; j < 32; i++, j++)
                                        {
                                            NameExtracter[j] = orosChunk[i];
                                        }
                                        label_Ch2 = Encoding.ASCII.GetString(NameExtracter);
                                        label_Ch2 = label_Ch2.Trim(new char[] { '\0', ' ' });
                                        arrch_label = label_Ch2.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                                        if (arrch_label.Length > 1)
                                        {
                                            label_Ch2 = arrch_label[1].ToString();
                                        }
                                        //lblYunit.Text = "CH1 " + label_Ch1 + ", CH2 " + label_Ch2;
                                    }
                                    //lblXunit.Text = "sec";

                                    // Amit Jain    17 feb 2010
                                    //Generate the graph of channel 1.  Will not show the graph of tacho. Will act as single channel in the graphical mode.
                                    NameExtracter = new byte[20];
                                    for (int i = 78, j = 0; j < 20; i++, j++)
                                    {
                                        NameExtracter[j] = orosChunk[i];
                                    }

                                    Name_Ch1 = Encoding.ASCII.GetString(NameExtracter);
                                    Name_Ch1 = Name_Ch1.Trim(new char[] { '\0', ' ' });
                                    if (channel == 2)
                                    {
                                        NameExtracter = new byte[20];
                                        for (int i = 170, j = 0; j < 20; i++, j++)
                                        {
                                            NameExtracter[j] = orosChunk[i];
                                        }

                                        Name_Ch2 = Encoding.ASCII.GetString(NameExtracter);
                                        Name_Ch2 = Name_Ch2.Trim(new char[] { '\0', ' ' });
                                    }

                                    SFH = null;

                                    for (int i = orosctr; i < orosctr + 4; i++)
                                    {
                                        int val = Convert.ToInt32(Parameter[i].ToString());
                                        char c = (char)val;
                                        SFH += c.ToString();
                                    }
                                    if (SFH == data)
                                    {
                                        SFH = null;
                                        int datastart = orosctr + 4 - 1;
                                        for (int i = datastart + 4; i > datastart; i--)
                                        {
                                            int val = Convert.ToInt32(Parameter[i].ToString());
                                            string sval = DeciamlToHexadeciaml1(val);
                                            SFH += SingleToDoubleDigitChar(sval);
                                        }
                                        TotalDataBytes = HexadecimaltoDecimal(SFH);
                                        TimeVal = Convert.ToDouble(1 / Convert.ToDouble(SF));

                                        iDataStart = datastart + 4 + 1;
                                        xData = new double[ExectDataByteSample];

                                        for (int i = 0; i < ExectDataByteSample; i++)
                                        {
                                            xData[i] = i * TimeVal;
                                        }
                                        xarray = xData;
                                        SamplePerSec = 0;
                                        if (channel == 1)
                                        {
                                            SamplePerSec = AvgBytesPerSec / SF;
                                        }
                                        else if (channel == 2)
                                        {
                                            SamplePerSec = AvgBytesPerSec / SF;
                                            SamplePerSec = SamplePerSec / 2;
                                        }
                                        else
                                        {
                                            SamplePerSec = AvgBytesPerSec / SF;
                                            SamplePerSec = SamplePerSec / channel;
                                        }
                                        TotalTime = TotalDataBytes / AvgBytesPerSec;
                                    }
                                }
                            }
                            bShowOrbit = false;
                            if (TotalTime > 0)
                            {
                                short sample;
                                double[] narray = new double[0];
                                BinaryReader fr = new BinaryReader(wav);

                                double[] soundBytes = new double[xData.Length];
                                double[] soundBytes1 = new double[xData.Length];
                                arlYData = new ArrayList();
                                ctr = 0;
                                int abc = 0;
                                fr.BaseStream.Position = iDataStart;
                                if (channel == 1)
                                {
                                    //dataGridViewX1.Visible = false;
                                    //expandableSplitter5.Visible = false;
                                    if (divider_CH1 == 1)
                                        divider_CH1 = (3.16 * 1.414 / 32768);
                                    int xx = 0;
                                    double[] abcd = new double[TotalDataBytes / 2];
                                    Fulldata_CH1 = new double[abcd.Length];
                                    while (fr.BaseStream.Position - iDataStart < TotalDataBytes)//while (fr.BaseStream.Position != fr.BaseStream.Length)                            
                                    {

                                        sample = fr.ReadInt16();
                                        abcd[xx] = sample;

                                        soundBytes[ctr] = sample;
                                        double SampleVal = Convert.ToDouble(sample * divider_CH1); //SampleVal = SampleVal / 1000;
                                        Fulldata_CH1[xx] = SampleVal;
                                        if (SampleVal < 100)
                                        {
                                            SampleVal = Math.Round(SampleVal, 9);
                                            soundBytes[ctr] = (SampleVal);
                                        }
                                        else
                                        {
                                            SampleVal = Math.Round(SampleVal);
                                            soundBytes[ctr] = (SampleVal);
                                        }
                                        ctr++;
                                        xx++;
                                        if (ctr == xData.Length)
                                        {
                                            double[] tempYdata = null;
                                            //if (!bShowOrbit)
                                            //{
                                            //    double[] Fmag = ConvertToFFT(soundBytes, xData, iLastFrequency);
                                            //    arlYData.Add(Fmag);
                                            //    tempYdata = soundBytes;
                                            //}
                                            //else
                                            {
                                                allYData.Add(soundBytes.ToList());
                                                allXData.Add(xData.ToList());
                                                arlYData.Add(soundBytes);
                                                tempYdata = soundBytes;
                                            }
                                            //hshDrag.Add(trendValCtr, trendValCtr);
                                            ctr = 0;
                                            soundBytes = new double[xData.Length];
                                            
                                        }
                                    }
                                    completeWaveYData = Fulldata_CH1.ToList();
                                }
                                else
                                {
                                    
                                    int xx = 0;
                                    int yy = 0;
                                    double[] abcd = new double[TotalDataBytes / 4];
                                    Fulldata_CH1 = new double[abcd.Length];
                                    double[] abcd1 = new double[TotalDataBytes / 4];
                                    Fulldata_CH2 = new double[abcd1.Length];
                                    int timectr = 0;
                                    while (fr.BaseStream.Position - iDataStart < TotalDataBytes)//while (fr.BaseStream.Position != fr.BaseStream.Length)
                                    {

                                        sample = fr.ReadInt16();

                                        double SampleVal = 0;// Convert.ToDouble(sample * divider); //SampleVal = SampleVal / 1000;
                                        if (xx % 2 == 0)
                                        {
                                            SampleVal = Convert.ToDouble(sample * divider_CH1);
                                            if (SampleVal < 100)
                                            {
                                                SampleVal = Math.Round(SampleVal, 9);
                                                soundBytes[ctr] = (SampleVal);
                                            }
                                            else
                                            {
                                                SampleVal = Math.Round(SampleVal);
                                                soundBytes[ctr] = (SampleVal);
                                            }
                                            abcd[yy] = sample;
                                            Fulldata_CH1[yy] = SampleVal;
                                            xx++;

                                        }
                                        else
                                        {
                                            SampleVal = Convert.ToDouble(sample * divider_CH2);
                                            if (SampleVal < 100)
                                            {
                                                SampleVal = Math.Round(SampleVal, 9);
                                                soundBytes1[ctr] = (SampleVal);
                                            }
                                            else
                                            {
                                                SampleVal = Math.Round(SampleVal);
                                                soundBytes1[ctr] = (SampleVal);
                                            }
                                            abcd1[yy] = sample;
                                            Fulldata_CH2[yy] = SampleVal;
                                            ctr++;
                                            xx++;
                                            yy++;
                                        }
                                        if (ctr == xData.Length)
                                        {
                                            double[] tempYdata = null;
                                            //if (!bShowOrbit)
                                            //{
                                            //    double[] Fmag = ConvertToFFT(soundBytes, xData, iLastFrequency);
                                            //    arlYData.Add(Fmag);
                                            //    tempYdata = soundBytes;
                                            //}
                                            //else
                                            {
                                                allYData.Add(soundBytes.ToList());
                                                allXData.Add(xData.ToList());
                                                arlYData.Add(soundBytes);
                                                tempYdata = soundBytes;
                                            }
                                            //hshDrag.Add(trendValCtr, trendValCtr);
                                            ctr = 0;
                                            soundBytes = new double[xData.Length];


                                            
                                        }
                                    }
                                    completeWaveYData = Fulldata_CH1.ToList();
                                    //double and = findHighestValue(abcd);
                                }
                            }
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //ErrorLog_Class.ErrorLogEntry(ex);
            }
            
        }
        private string SingleToDoubleDigitChar(string single)
        {
            string sval = single;
            switch (sval)
            {
                case "0":
                    sval = "00";
                    break;
                case "1":
                    sval = "01";
                    break;
                case "2":
                    sval = "02";
                    break;
                case "3":
                    sval = "03";
                    break;
                case "4":
                    sval = "04";
                    break;
                case "5":
                    sval = "05";
                    break;
                case "6":
                    sval = "06";
                    break;
                case "7":
                    sval = "07";
                    break;
                case "8":
                    sval = "08";
                    break;
                case "9":
                    sval = "09";
                    break;
            }
            return sval;
        }
        private void CreatingWave1(string spath)
        {
            int[] LOR = { 100, 200, 400, 800, 1600, 3200, 6400, 12800, 25600, 51200, 102400, 204800 };
            int[] LOR1 = { 256, 512, 1024, 2048, 4096, 8192, 16384, 32738, 65536, 131072, 262144, 524288 };
            try
            {
                int iclick = 1;
                using (FileStream wav = new FileStream(spath, FileMode.Open, FileAccess.Read))
                {

                    Byte[] Parameter = new byte[44];

                    wav.Read(Parameter, 0, 44);
                    int channel = Convert.ToInt32(Parameter[22].ToString());
                    string[] SFbyteD = new string[4];
                    string SFH = null;
                    int ctr = 0;
                    for (int i = 27; i > 23; i--)
                    {
                        int val = Convert.ToInt32(Parameter[i].ToString());
                        string sval = DeciamlToHexadeciaml1(val);                        
                        SFH += SingleToDoubleDigitChar(sval); 
                    }
                    int SF = HexadecimaltoDecimal(SFH);




                    SFH = null;

                    for (int i = 31; i > 27; i--)
                    {
                        int val = Convert.ToInt32(Parameter[i].ToString());
                        string sval = DeciamlToHexadeciaml1(val);
                        
                        SFH += SingleToDoubleDigitChar(sval);
                    }
                    int AvgBytesPerSec = HexadecimaltoDecimal(SFH);


                    SFH = null;

                    for (int i = 27; i > 23; i--)
                    {

                        string sval = Parameter[i].ToString();
                        SFH += SingleToDoubleDigitChar(sval);
                    }
                    int iLastFrequency = Convert.ToInt32(SFH);
                    int iFinalFrequency = iLastFrequency;
                    int iLOR = 0;
                    for (int i = 0; i < LOR.Length; i++)
                    {
                        double temp = (double)LOR[i] / (double)iLastFrequency;
                        if (temp >= 0.666667)
                        {

                            iLOR = i;

                            break;
                        }
                    }
                    int LineOfResolution = LOR1[iLOR];

                    double ExectTime = (double)LOR[iLOR] / (double)iLastFrequency;

                    int ExectDataByteSample = Convert.ToInt32((double)SF * ExectTime);

                    SFH = null;

                    for (int i = 43; i > 39; i--)
                    {
                        int val = Convert.ToInt32(Parameter[i].ToString());
                        string sval = DeciamlToHexadeciaml1(val);
                        SFH += SingleToDoubleDigitChar(sval);
                    }
                    int TotalDataBytes = HexadecimaltoDecimal(SFH);


                    double TimeVal = Convert.ToDouble(1 / Convert.ToDouble(SF));
                    double[] xData = new double[ExectDataByteSample];

                    for (int i = 0; i < ExectDataByteSample; i++)
                    {
                        xData[i] = i * TimeVal;
                    }
                    var xarray = xData;
                    int SamplePerSec = 0;
                    if (channel == 1)
                    {
                        SamplePerSec = AvgBytesPerSec / SF;
                    }
                    else if (channel == 2)
                    {
                        SamplePerSec = AvgBytesPerSec / SF;
                        SamplePerSec = SamplePerSec / 2;
                    }
                    else
                    {
                        SamplePerSec = AvgBytesPerSec / SF;
                        SamplePerSec = SamplePerSec / channel;
                    }

                    int TotalTime = TotalDataBytes / AvgBytesPerSec;

                    var bShowOrbit = false;
                    if (TotalTime > 0)
                    {
                        short sample;
                        double[] narray = new double[0];
                        BinaryReader fr = new BinaryReader(wav);


                        double[] soundBytes = new double[xData.Length];
                        double[] soundBytes1 = new double[xData.Length];
                        var arlYData = new ArrayList();
                        ctr = 0;
                        int abc = 0;
                        double divider = (double)1 / (double)32767;
                        string[] splitedspath = spath.Split(new string[] { "\\", ".wav" }, StringSplitOptions.RemoveEmptyEntries);
                        string pathforCSV = null;
                        for (int i = 0; i < splitedspath.Length - 1; i++)
                        {
                            pathforCSV = pathforCSV + splitedspath[i].ToString() + "\\";
                        }
                        pathforCSV = pathforCSV + splitedspath[splitedspath.Length - 1] + "_CHN1.csv";

                        //if (File.Exists(pathforCSV))
                        //{
                        //    //ReadCSVfileForData(pathforCSV);
                        //    divider = divider * Convert.ToDouble(lblFixedRangeValue.Text.ToString());
                        //}
                        //else
                        //{
                        //    SetLabelDefault();
                        //}


                        if (channel == 1)
                        {
                            int xx = 0;
                            double[] abcd = new double[TotalDataBytes / 2];
                            while (fr.BaseStream.Position - 44 < TotalDataBytes)//while (fr.BaseStream.Position != fr.BaseStream.Length)
                            {

                                sample = fr.ReadInt16();
                                abcd[xx] = sample;
                                double SampleVal = Convert.ToDouble(sample * divider); //SampleVal = SampleVal / 1000;
                                if (SampleVal < 100)
                                {
                                    SampleVal = Math.Round(SampleVal, 3);
                                    soundBytes[ctr] = (SampleVal);
                                }
                                else
                                {
                                    SampleVal = Math.Round(SampleVal);
                                    soundBytes[ctr] = (SampleVal);
                                }
                                ctr++;
                                xx++;
                                if (ctr == xData.Length)
                                {
                                    double[] tempYdata = null;
                                    //if (!bShowOrbit)
                                    //{
                                    //    double[] Fmag = ConvertToFFT(soundBytes, xData, iLastFrequency);
                                    //    arlYData.Add(Fmag);
                                    //    tempYdata = soundBytes;
                                    //}
                                    //else
                                    {
                                        arlYData.Add(soundBytes);
                                        tempYdata = soundBytes;
                                    }
                                    //hshDrag.Add(trendValCtr, trendValCtr);
                                    ctr = 0;
                                    soundBytes = new double[xData.Length];
                                    
                                }
                            }
                            //double and = findHighestValue(abcd);
                        }
                        else
                        {
                            int xx = 0;
                            int yy = 0;
                            double[] abcd = new double[TotalDataBytes / 4];
                            double[] abcd1 = new double[TotalDataBytes / 4];
                            while (fr.BaseStream.Position - 44 < TotalDataBytes)//while (fr.BaseStream.Position != fr.BaseStream.Length)
                            {

                                sample = fr.ReadInt16();

                                double SampleVal = Convert.ToDouble(sample * divider); //SampleVal = SampleVal / 1000;
                                if (xx % 2 == 0)
                                {
                                    if (SampleVal < 100)
                                    {
                                        SampleVal = Math.Round(SampleVal, 3);
                                        soundBytes[ctr] = (SampleVal);
                                    }
                                    else
                                    {
                                        SampleVal = Math.Round(SampleVal);
                                        soundBytes[ctr] = (SampleVal);
                                    }
                                    abcd[yy] = sample;
                                    xx++;

                                }
                                else
                                {
                                    if (SampleVal < 100)
                                    {
                                        SampleVal = Math.Round(SampleVal, 3);
                                        soundBytes1[ctr] = (SampleVal);
                                    }
                                    else
                                    {
                                        SampleVal = Math.Round(SampleVal);
                                        soundBytes1[ctr] = (SampleVal);
                                    }
                                    abcd1[yy] = sample;
                                    ctr++;
                                    xx++;
                                    yy++;
                                }
                                if (ctr == xData.Length)
                                {
                                    double[] tempYdata = null;
                                    //if (!bShowOrbit)
                                    //{
                                    //    double[] Fmag = ConvertToFFT(soundBytes, xData, iLastFrequency);
                                    //    arlYData.Add(Fmag);
                                    //    tempYdata = soundBytes;
                                    //}
                                    //else
                                    {
                                        arlYData.Add(soundBytes);
                                        tempYdata = soundBytes;
                                    }
                                    //hshDrag.Add(trendValCtr, trendValCtr);
                                    ctr = 0;
                                    soundBytes = new double[xData.Length];

                                }
                            }
                            //double and = findHighestValue(abcd);
                        }
                    }
                    else
                    {
                        throw new IndexOutOfRangeException("Not Enough Sample to Draw");
                       
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        private int HexadecimaltoDecimal(string hexadecimal)
        {
            int result = 0;

            for (int i = 0; i < hexadecimal.Length; i++)
            {
                result += Convert.ToInt32(GetNumberFromNotation(hexadecimal[i]) * Math.Pow(16, Convert.ToInt32(hexadecimal.Length) - (i + 1)));
            }
            return Convert.ToInt32(result);
        }

        int GetNumberFromNotation(char c)
        {
            if (c == 'A')
                return 10;
            else if (c == 'B')
                return 11;
            else if (c == 'C')
                return 12;
            else if (c == 'D')
                return 13;
            else if (c == 'E')
                return 14;
            else if (c == 'F')
                return 15;

            return Convert.ToInt32(c.ToString());
        }
        string DeciamlToHexadeciaml1(int number)
        {
            string[] hexvalues = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };
            string result = null, final = null;
            int rem = 0, div = 0;
            try
            {
                while (true)
                {
                    rem = (number % 16);
                    result += hexvalues[rem].ToString();

                    if (number < 16)
                        break;
                    //result += ',';
                    number = (number / 16);

                }

                for (int i = (result.Length - 1); i >= 0; i--)
                {
                    final += result[i];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return final;
        }
    }
}
