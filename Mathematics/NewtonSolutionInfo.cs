using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mathematics
{
    public class NewtonSolutionInfo
    {
        public int Iterations { get; private set; }
        public int RootNumber { get; private set; }

        public NewtonSolutionInfo(int iterations, int rootNumber)
        {
            Iterations = iterations;
            RootNumber = rootNumber;
        }
    }
}
