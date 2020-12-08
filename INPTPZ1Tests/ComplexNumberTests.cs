using Microsoft.VisualStudio.TestTools.UnitTesting;
using INPTPZ1.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace INPTPZ1.Mathematics.Tests
{
    [TestClass()]
    public class ComplexNumberTests
    {

        [TestMethod()]
        public void AddTest()
        {
            Complex summand = new Complex(10, 20);
            Complex addend = new Complex(1, 2);

            Complex total = summand + addend;

            Complex expectedResult = new Complex(11, 22);

            Assert.AreEqual(expectedResult, total);
        }
    }
}


