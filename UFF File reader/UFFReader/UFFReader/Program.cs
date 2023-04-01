using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFFReader
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadUFF readUFF = new ReadUFF();
            readUFF.ReadFile(@"C:\test.uff");
        }
    }
}
