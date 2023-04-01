namespace iAdpetFFTDiagnostic
{
    interface IUserInputInterface
    {
         bool IsGearBox { get; }

         bool IsFan { get; }

         bool IsMotor { get; }

        

        int GearTooth { get; }

        int PinionTooth { get; }

        int PinionRpm { get; }


        string Direction { get; }
         bool IsRpmMargin { get; }

        int RpmMargin { get; }

        int BladeCount { get; }


        bool IsBeltDrive { get; }

        int BeltLength { get; }

        int DrivenSheaveDiameter { get; }

        int DrivingSheaveDiameter { get; }

        int LineFrequency { get; }

        int RotorBarCount { get; }

        int VaneCount { get; }
        int PoleCount { get; }
        int CoilCount { get; }

        bool IsBearing { get; }

        BearingControl Control_Bearing { get; }
    }
}
