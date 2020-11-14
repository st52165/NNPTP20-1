using BaseLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point = System.Windows.Point;

namespace Mathematics
{
    public class NewtonFractal
    {
        private const string defaultFractalImagePath = "../../../out.png";

        public Bitmap FractalImage { get; private set; }
        public Size FractalSize { get => DrawnDomain.Size; }
        public NewtonFractalDomain DrawnDomain { get; private set; }
        public NewtonIteration Iteration { get; private set; } = new NewtonIteration();
        public string FractalImagePath { get; private set; } = defaultFractalImagePath;
        public List<Color> Colors { get; set; } = GetDefaultColors();

        public NewtonFractal(NewtonFractalDomain drawnDomain, NewtonIteration newtonFractalIteration, List<Color> colors, string fractalImagePath = defaultFractalImagePath)
        {
            InitializeBasicNewtonFractal(drawnDomain, fractalImagePath);
            Iteration = newtonFractalIteration;
            Colors = colors;
        }
        public NewtonFractal(NewtonFractalDomain drawnDomain, string fractalImagePath = defaultFractalImagePath)
        {
            InitializeBasicNewtonFractal(drawnDomain, fractalImagePath);
        }
        public NewtonFractal(string[] parameters)
        {
            GetBasicParametersFromString(parameters);
        }

        public void GetBasicParametersFromString(string[] parameters)
        {
            Functions.CheckArrayMinimumLength(parameters, NewtonFractalDomain.MinimumParametersCount);

            if (DrawnDomain is null) DrawnDomain = new NewtonFractalDomain(parameters);
            else DrawnDomain.SetFractalDomainFromParametersArray(parameters);

            if (FractalImage is null) FractalImage = new Bitmap(FractalSize.Width, FractalSize.Height);

            if (parameters.Length > NewtonFractalDomain.MinimumParametersCount)
            {
                FractalImagePath = parameters[NewtonFractalDomain.MinimumParametersCount];
            }
        }

        public void Draw()
        {
            Point currentPixel;
            NewtonSolutionInfo iterationResult;
            Console.Write("\nDrawing...");
            for (int i = 0; i < FractalSize.Width; i++)
            {
                for (int j = 0; j < FractalSize.Height; j++)
                {
                    currentPixel = new Point(i, j);

                    iterationResult = Iteration.Execute(
                        DrawnDomain.GetInitialValueFromPixel(currentPixel));

                    ColorizePixelByRootNumber(currentPixel, iterationResult);
                }
            }
            Console.WriteLine("\nFinished!");
        }
        public void Save()
        {
            try
            {
                FractalImage.Save(FractalImagePath);
                Console.WriteLine("\nSaved successfully!");
            }
            catch (IOException e)
            {
                throw new IOException("An error occurred while saving the image:\n" + e.Message);
            }
        }

        private void ColorizePixelByRootNumber(Point calculatedPixel, NewtonSolutionInfo pixelInfo)
        {
            Color finalPixelColor = Colors[pixelInfo.RootNumber % Colors.Count];
            finalPixelColor = MultiplyColor(finalPixelColor, -pixelInfo.Iterations * 2);

            FractalImage.SetPixel((int)calculatedPixel.X, (int)calculatedPixel.Y, finalPixelColor);
        }

        private void InitializeBasicNewtonFractal(NewtonFractalDomain drawnDomain, string fractalImagePath)
        {
            DrawnDomain = drawnDomain;
            FractalImage = new Bitmap(FractalSize.Width, FractalSize.Width);
            FractalImagePath = fractalImagePath;
        }

        private static List<Color> GetDefaultColors()
            => new List<Color>()
            {
                Color.Red, Color.Green, Color.Blue,
                Color.Yellow, Color.Orange, Color.Fuchsia,
                Color.Gold, Color.Cyan, Color.Magenta
            };
        private static Color MultiplyColor(Color basic, int coefficient)
        {
            return Color.FromArgb
                (
                AddSaturateSubColorValue(basic.R),
                AddSaturateSubColorValue(basic.G),
                AddSaturateSubColorValue(basic.B)
                );

            int AddSaturateSubColorValue(int basicSubColorValue)
            {
                return Math.Min(Math.Max(0, basicSubColorValue + coefficient), 255);
            }
        }
    }
}
