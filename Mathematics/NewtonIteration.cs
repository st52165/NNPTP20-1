using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Mathematics
{
    public class NewtonIteration
    {
        public Polynomial Function { get; private set; }
        public Polynomial DerivedFunction
        {
            get => Function.GetFirstDerivative();
        }
        public Complex InitialValue { get; private set; }
        public List<Complex> Roots { get; private set; }

        public int MaxIterations { get; } = 30;
        public double AbsoluteValueIterationLimit { get; } = 0.5;
        public double Tolerance { get; } = 0.01;

        public NewtonIteration()
        {
            Function = GetPrimaryFunction();
            Roots = new List<Complex>();
        }
        public NewtonIteration(Polynomial function, List<Complex> roots, int maxIterations,
            double absoluteValueIterationLimit, double tolerance)
        {
            Function = function;
            Roots = roots;
            MaxIterations = maxIterations;
            AbsoluteValueIterationLimit = absoluteValueIterationLimit;
            Tolerance = tolerance;
        }

        public void PrintPolynomial()
        {
            Console.WriteLine(Function);
            Console.WriteLine(DerivedFunction);
        }

        public NewtonSolutionInfo Execute(Complex initialValue)
        {
            InitialValue = initialValue;
            return new NewtonSolutionInfo(FindSolution(), FindSolutionRootNumber());
        }

        private int FindSolution()
        {
            int iterations = 0;
            for (int i = 0; i < MaxIterations; i++, iterations++)
            {
                Complex quotient = Function.Evaluate(InitialValue) / DerivedFunction.Evaluate(InitialValue);
                InitialValue -= quotient;
                if (IsToleratedAbsoluteValue(quotient, AbsoluteValueIterationLimit,
                    (inputNumber, tolerance) => inputNumber >= tolerance))
                {
                    i--;
                }
            }
            return iterations;
        }
        private int FindSolutionRootNumber()
        {
            int rootNumber = 0;
            bool foundRoot = false;
            for (int i = 0; i < Roots.Count; i++)
            {
                if (IsToleratedAbsoluteValue(InitialValue - Roots[i], Tolerance,
                    (inputNumber, tolerance) => inputNumber <= tolerance))
                {
                    foundRoot = true;
                    rootNumber = i;
                }
            }
            if (!foundRoot)
            {
                Roots.Add(InitialValue);
                rootNumber = Roots.Count;
            }
            return rootNumber;
        }

        private static bool IsToleratedAbsoluteValue(Complex inputNumber, double tolerance, Func<double, double, bool> condition)
        {
            return condition(Complex.Abs(Complex.Pow(inputNumber, 2)), tolerance);
        }
        private static Polynomial GetPrimaryFunction()
        {
            return new Polynomial()
            {
                Coefficients = new List<Complex>()
                {
                    new Complex(1, 0),
                    Complex.Zero,
                    Complex.Zero,
                    new Complex(1, 0)
                }
            };
        }
    }
}
