using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Drawing;

namespace P2_BezierBSpline
{
    class MathBezier : Drawer
    {
        PointF[] bezierPoints;

        public MathBezier() : base(12, 6)
        {
            DoMathBezier();
        }

        public override bool Update(PointF mouse)
        {
            if (base.Update(mouse))
            {
                DoMathBezier();
                return true;
            }
            return false;
        }



        public override void Draw(Graphics G)
        {
            base.Draw(G);

            for (int i = 0; i < bezierPoints.Length - 1; i++)
            {
                DrawBezierLine(G, i, i + 1);
            }
        }
        public override void Refresh()
        {
            DoMathBezier();
        }
        private void DrawBezierLine(Graphics G, int indexA, int indexB)
        {
            //draw line on the form
            G.DrawLine(new Pen(Brushes.Red, 2), bezierPoints[indexA], bezierPoints[indexB]);
        }

        // The method that does the curve-calculating-work. 
        // It returns an array filled with the points of the curve.
        // It will create a curve with 100 points as a default.
        private void DoMathBezier(int iterations = 100)
        {
            bezierPoints = new PointF[iterations];

            for (int n = 1; n <= iterations ; n++)
            {
                float u = (float)n / (float)iterations;
                int graad = CPcounter-1;
                PointF pointF = PointF.Empty;

                // Hier gaan we alle punten af.
                // Eigenlijk de som van m = 0 tot graad, voor de volgende formule:
                // (graad over m) * (1-u)^(graad-m)*u^m*Pm
                // Pm is het m-de punt.
                // Gebruiken longs in berekening want graad over m gebruikt faculteit.
                for (int m = 0; m <= graad; m++)
                {
                    double answer = Math.Pow(1 - u, graad - m);
                    answer *= Math.Pow(u, m);
                    answer *= C(graad, m); //(graad over m)

                    PointF p = StaticFunctions.Mult((float)answer, ControlPoints[m]);
                    pointF = StaticFunctions.Add(pointF, p);
                }

                bezierPoints[n-1] = pointF;
            }
        }
        private double C(int graad, int m)
        {
            double gFac = DoFactorial(graad);
            double mFac = DoFactorial(m);
            double gmFac = DoFactorial(graad - m);

            double factorial = gFac / (mFac * gmFac);

            return factorial;
        }
        private double DoFactorial(int i)
        {
            double factorial = 1;

            for (int j = 2; j <= i; j++)
            {
                factorial *= j;
            }

            return factorial;
        }
    }
}
