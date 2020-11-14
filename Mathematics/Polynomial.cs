using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Mathematics
{
    public class Polynomial
    {
        private const string formatPolynomial = "{0}{1}{2}";
        public List<Complex> Coefficients { get; set; }

        public Polynomial()
        {
            Coefficients = new List<Complex>();
        }

        public Complex Evaluate(Complex point)
        {
            Complex currentCoefficient, currentMemberValuePoint, result = Complex.Zero;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                currentCoefficient = Coefficients[i];
                currentMemberValuePoint = Complex.Pow(point, i);

                result += currentCoefficient * currentMemberValuePoint;
            }
            return result;
        }

        public override string ToString()
        {
            string result = string.Empty;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                result += string.Format(formatPolynomial,
                    i > 0 ? " + " : string.Empty, Coefficients[i], new string('x', i));
            }
            return result;
        }

        public Polynomial GetFirstDerivative()
        {
            Polynomial derived = new Polynomial();
            for (int i = 1; i < Coefficients.Count; i++)
            {
                derived.Coefficients.Add(Coefficients[i] * new Complex(i, 0));
            }
            return derived;
        }
    }
}
