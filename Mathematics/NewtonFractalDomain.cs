using BaseLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Size = System.Drawing.Size;

namespace Mathematics
{
    public class NewtonFractalDomain
    {
        private const double minimumCalculatedCoordinateValue = 0.0001;

        public const int MinimumParametersCount = 6;

        public Size Size { get; private set; }
        public Point MinimumPoint { get; private set; }
        public Point MaximumPoint { get; private set; }

        public Vector Step
        {
            get => new Vector(
                GetAxisStep(MinimumPoint.X, MaximumPoint.X, Size.Width),
                GetAxisStep(MinimumPoint.Y, MaximumPoint.Y, Size.Height));
        }

        public NewtonFractalDomain(Size size, Point minimumPoint, Point maximumPoint)
        {
            Size = size;
            MinimumPoint = minimumPoint;
            MaximumPoint = maximumPoint;
        }
        public NewtonFractalDomain(Size size, double xMinimumPoint, double yMinimumPoint,
            double xMaximumPoint, double yMaximumPoint)
        {
            Size = size;
            MinimumPoint = new Point(xMinimumPoint, yMinimumPoint);
            MaximumPoint = new Point(xMaximumPoint, yMaximumPoint);
        }
        public NewtonFractalDomain(string[] parameters)
        {
            SetFractalDomainFromParametersArray(parameters);
        }

        public void SetFractalDomainFromParametersArray(string[] parameters)
        {
            Functions.CheckArrayMinimumLength(parameters, MinimumParametersCount);

            int indexOf = 0;
            string[] parameterNames = GetParameterNames();

            Size = InitializeNewCoordinates((width, height) => new Size((int)width, (int)height));
            MinimumPoint = InitializeNewCoordinates(InitializePoint);
            MaximumPoint = InitializeNewCoordinates(InitializePoint);

            T InitializeNewCoordinates<T>(Func<double, double, T> initializeProperty)
            {
                ParseParametersItemAndAssign(out double coordinateA);
                ParseParametersItemAndAssign(out double coordinateB);
                return initializeProperty(coordinateA, coordinateB);
            }
            void ParseParametersItemAndAssign<T>(out T result) where T : IConvertible
            {
                result = NumbersParser.Parse<T>(parameters[indexOf], parameterNames[indexOf++]);
            }
            Point InitializePoint(double x, double y) => new Point(x, y);
        }

        public Complex GetInitialValueFromPixel(Point calculatedPixel)
        {
            Vector multipliedStep = Step;
            multipliedStep.X *= calculatedPixel.X;
            multipliedStep.Y *= calculatedPixel.Y;

            Point initialPoint = MinimumPoint + multipliedStep;

            return new Complex(CheckCalculatedCoordinate(initialPoint.X),
                CheckCalculatedCoordinate(initialPoint.Y));
        }

        private static double GetAxisStep(double minimumCoordinate, double maximumCoordinate, double distance)
            => (maximumCoordinate - minimumCoordinate) / distance;
        private static double CheckCalculatedCoordinate(double coordinate) =>
            coordinate == 0 ? minimumCalculatedCoordinateValue : coordinate;

        private static string[] GetParameterNames()
        {
            const string pointFormat = "{0} point {1} coordinate";

            const string minPointName = "Minimum";
            const string maxPointName = "Maximum";
            const string xName = "X";
            const string yName = "Y";
            const string fractalLabel = "Fractal ";

            return new string[] { fractalLabel + "width", fractalLabel + "height",
                GetParameterName(minPointName, xName), GetParameterName(minPointName, yName),
                GetParameterName(maxPointName, xName), GetParameterName(maxPointName, yName)};

            string GetParameterName(string pointName, string axisName)
                => string.Format(pointFormat, pointName, axisName);
        }
    }
}
