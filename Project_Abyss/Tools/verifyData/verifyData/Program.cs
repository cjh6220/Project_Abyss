using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace verifyData
{
    class Program
    {
        static void Main(string[] args)
        {
            VerifyData vd = new VerifyData(args[0]);
            vd.Verify();
        }
    }
}
