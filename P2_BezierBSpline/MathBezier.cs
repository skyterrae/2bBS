using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Drawing;

namespace P2_BezierBSpline
{
    class MathBezier : CurveBase
    {
        //classe die zorgt vor het uitrekenen en tekenen van een Wiskundig berekende BezierCurve

        PointF[] bezierPoints; // Array van de punten van de beziercurve. Wordt gevuld in de DoMathBezier-methode.

        public MathBezier() : base(12, 6)
        {
            DoMathBezier();
        }    

        #region Calculating Functions

        // De methode die de array oplevert met 100 punten van de beziercurve.
        // Er wordt geïtereerd over u = 0.01 tot 1.00.
        // Per iteratie van u wordt de som berekent van de volgende formule (uit slides CurveIII):
        // (graad over m) * (1-u)^(graad-m)*u^m*Pm
        // Waarbij de som itereert van 0 tot de graad van de curve.
        private void DoMathBezier(int iterations = 100)
        {
            bezierPoints = new PointF[iterations];

            for (int n = 1; n <= iterations ; n++)
            {
                float u = (float)n / (float)iterations;
                int graad = CPcounter-1;
                PointF pointF = PointF.Empty;
                for (int m = 0; m <= graad; m++)
                {
                    double answer = Math.Pow(1 - u, graad - m);
                    answer *= Math.Pow(u, m);
                    answer *= C(graad, m); //(graad over m)

                    PointF p = PointMathHelper.Mult((float)answer, ControlPoints[m]);
                    pointF = PointMathHelper.Add(pointF, p);
                }

                // Vervolgens wordt het nieuwgevonden punt toegevoegd aan de array van bezierpunten.
                bezierPoints[n-1] = pointF;
            }
        }

        // Aparte methode om de graad over c te bereken, volgens deze formule:
        // (x over y) = x! / (y!*(x-y)!).
        // Wordt met doubles gewerkt vanwege de grootte van de getallen.
        private double C(int graad, int m)
        {
            //berekend (graad over m) 
            double gFac = DoFactorial(graad);
            double mFac = DoFactorial(m);
            double gmFac = DoFactorial(graad - m);

            double factorial = gFac / (mFac * gmFac);

            return factorial;
        }

        // Aparte methode om faculteit te berekenen, aangezien die niet standaard in C#-library te vinden is.
        private double DoFactorial(int i)
        {
            //berekend Faculteit(i)
            double factorial = 1;

            for (int j = 2; j <= i; j++)
            {
                factorial *= j;
            }

            return factorial;
        }
        #endregion

        #region Overrides van UpdateFunctions
        public override void Refresh()
        {
            DoMathBezier();
        }

        public override bool Update(PointF mouse)
        {
            // Update de curve tijdens het verplaatsen van controlepunten
            if (base.Update(mouse))
            {
                DoMathBezier();
                return true;
            }
            return false;
        }
        #endregion

        #region DrawFunctions
        public override void Draw(Graphics G)
        {
            // Tekent eerst de basispunten en lijnen.
            base.Draw(G);

            // En vervolgens wordt de beziercurve getekent m.b.v. de punten uit de bezierPoints array.
            for (int i = 0; i < bezierPoints.Length - 1; i++)
            {
                DrawBezierLine(G, i, i + 1);
            }
        }
        private void DrawBezierLine(Graphics G, int indexA, int indexB)
        {
            // Draw line on the form
            G.DrawLine(new Pen(Brushes.Red, 2), bezierPoints[indexA], bezierPoints[indexB]);
        }
        #endregion
    }
}
