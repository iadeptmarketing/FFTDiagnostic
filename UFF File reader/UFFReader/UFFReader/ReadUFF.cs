using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TimeToFFT;

namespace UFFReader
{
    public class ReadUFF
    {
        public void ReadFile(string filePath)
        {
            if(filePath == null)
                throw new ArgumentException("File Path is null");
            if (!File.Exists(filePath))
                throw new FileNotFoundException("File path is incorrect");
            try
            {
                List<string> lstFileData = ReadCompleteFile(filePath);
                GetFFTData(lstFileData);
            }
            catch(Exception ex)
            { }
        }

        public bool IsYdataSquared
        { get; set; }
        public List<List<double>> allXData
        { get; set; }
        public List<List<double>> allYData
        { get; set; }
        public List<string> allDateTime
        { get; set; }
        public List<string> allChannel
        { get; set; }

        public string _Datatype
        {
            get
            {
                return DataType;
            }
        }

        private void GetFFTData(List<string> lstFileData)
        {
            allXData = new List<List<double>>();
            allYData = new List<List<double>>();
            allChannel = new List<string>();
            allDateTime = new List<string>();

            foreach (string vs in lstFileData)
            {
                List<string> DataLinebyLine = vs.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                if (ExtractXYData(DataLinebyLine,IsYdataSquared))
                {
                    allXData.Add(xData);
                    allYData.Add(yData);
                    allChannel.Add(channel);
                    allDateTime.Add(datetime);
                }
            }
        }

        string datetime = null;
        string channel = null;
        List<double> xData;
        List<double> yData;

        //List<List<decimal>> allXData = new List<List<decimal>>();
        //List<List<decimal>> allYData = new List<List<decimal>>();
        //List<string> allDateTime = new List<string>();
        //List<string> allChannel = new List<string>();
        string DataType = null;
        private bool ExtractXYData(List<string> dataLinebyLine, bool IsSquared)
        {
            yData = new List<double>();
            xData = new List<double>();
            double Hzdiff = 0;
            double StartingHz = 0;
            DataType = null;
            if (dataLinebyLine.Count < 20)
                return false;
            for (int i = 0; i < dataLinebyLine.Count; i++)
            {
                if (i == 3)
                    datetime = dataLinebyLine[i].ToString();
                if (i == 5)
                    channel = dataLinebyLine[i].ToString();
                if (i == 7)
                {
                    List<string> yDataList = dataLinebyLine[i].ToString().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    Hzdiff = (double)Math.Round(decimal.Parse(yDataList[4].ToString(), System.Globalization.NumberStyles.Any), 4);
                    StartingHz = (double)Math.Round(decimal.Parse(yDataList[3].ToString(), System.Globalization.NumberStyles.Any), 4);
                }
                if (i == 8)
                {
                    List<string> yDataList = dataLinebyLine[i].ToString().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    if (!string.IsNullOrEmpty(yDataList[4]))
                    {
                        DataType = yDataList[4];
                    }
                }
                if (i > 11)
                {
                    List<string> yDataList = dataLinebyLine[i].ToString().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    foreach (string s in yDataList)
                    {
                        decimal zero = 0;
                        if (IsSquared)
                            yData.Add(Math.Sqrt((double)Math.Round(decimal.Parse(s, System.Globalization.NumberStyles.Any), 4)));
                        else
                            yData.Add((double)Math.Round(decimal.Parse(s, System.Globalization.NumberStyles.Any), 4));
                    }
                }
            }
            
            for (int i = 0; i < yData.Count; i++)
            {
                xData.Add(StartingHz +( i * Hzdiff));
            }
            if(yData.Any(z=> z<0))
            {
                ConvertToFFT.TWFtoFFT tWFtoFFT = new ConvertToFFT.TWFtoFFT();
                ArrayList NewValues = tWFtoFFT.ConvertTWFtoFFT(xData.ToArray(), yData.ToArray(), "sec", "mm");
                if (NewValues != null)
                {
                    xData = ((double[])NewValues[0]).ToList();
                    yData = ((double[])NewValues[1]).ToList();
                    string CurrentXLabel = (string)NewValues[2];
                    string CurrentYLabel = (string)NewValues[3];
                     
                }                   
            }
            return true;

        }

        private List<string> ReadCompleteFile(string filePath)
        {
            List<string> lstFileData = new List<string>();
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                string fileData = streamReader.ReadToEnd();
                lstFileData = fileData.Split(new string[] { "-1\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            return lstFileData;
            
        }
    }
}
