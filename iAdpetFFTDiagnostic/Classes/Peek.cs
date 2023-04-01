using System;

namespace iAdpetFFTDiagnostic
{
    public class Peak : IEquatable<Peak>, IComparable<Peak>
    {
        public double PeakLocation { get; set; }
        public double PeakAmplitude { get; set; }

        
        public int CompareTo(Peak comparePeak)
        {
            if (comparePeak == null)
                return 1;
            return PeakAmplitude.CompareTo(comparePeak.PeakAmplitude);
        }

        public bool Equals(Peak comparePeak)
        {
            if (comparePeak == null)
                return false;
            return PeakAmplitude.Equals(comparePeak.PeakAmplitude);
        }
    }
}
