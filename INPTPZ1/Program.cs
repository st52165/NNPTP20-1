using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using Mathematics;
using Point = System.Windows.Point;

namespace INPTPZ1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                NewtonFractal newtonFractal = new NewtonFractal(args);
                newtonFractal.Iteration.PrintPolynomial();
                newtonFractal.Draw();
                newtonFractal.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:\n" + e.Message);
            }
            finally
            {
                Console.Write("Press any key to exit the program.");
                Console.ReadKey();
            }
        }
    }
}
