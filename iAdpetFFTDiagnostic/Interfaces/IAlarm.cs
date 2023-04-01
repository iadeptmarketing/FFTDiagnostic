using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iAdpetFFTDiagnostic.Interfaces
{
    internal interface IAlarm
    {
        double Acceleration_Axial { get; set; }
        double Acceleration_Horizontal { get; set; }
        double Acceleration_Vertical { get; set; }


        double Velocity_Axial { get; set; }
        double Velocity_Horizontal { get; set; }
        double Velocity_Vertical { get; set; }

        double Displacement_Axial { get; set; }
        double Displacement_Horizontal { get; set; }
        double Displacement_Vertical { get; set; }
    }
}
