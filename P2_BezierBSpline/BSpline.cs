using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace P2_BezierBSpline
{
    class BSpline : Drawer
    {
        PointF[] BSplinePoints;
        KnotControl KC;
        float[] KnotVectorT;     

        public BSpline(KnotControl kc) : base(25, 15)
        {
            KC = kc;
            KC.Visible = true;
            KnotVectorT = KC.Knots.KnotVector(CPcounter);
            BSplinePoints = CalculateBSpline();
        }

        public override bool Update(Point mouse)
        {
            if (base.Update(mouse) || KC.Knots.Update(mouse))
            {
                KnotVectorT = KC.Knots.KnotVector(CPcounter);
                BSplinePoints = CalculateBSpline();
                return true;
            }
            return false;
        }

        public override void Draw(Graphics G)
        {
            base.Draw(G);

            for (int i = 0; i < BSplinePoints.Length - 1; i++)
            {
                DrawBSplineLine(G, i, i + 1);
            }
        }

        private void DrawBSplineLine(Graphics G, int indexA, int indexB)
        {
            //draw line on the form
            G.DrawLine(new Pen(Brushes.Blue, 2), BSplinePoints[indexA], BSplinePoints[indexB]);
        }

        private PointF[] CalculateBSpline(int iterations = 100)
        {
            PointF[] Ps = new PointF[iterations];

            float dist = (float)(CPcounter -3) / (float)iterations;
            float U = KC.Knots.Degree;
            for (int i = 0; i<iterations; i++ )
            {
                Ps[i] = PointAt(U);
                U = U+ dist;
            }

            return Ps;
        }
        private PointF Add(PointF A, PointF B)
        {
            // + operator tussen twee punten
            return new PointF(A.X + B.X, A.Y + B.Y);
        }
        private PointF Mult(float c, PointF P)
        {
            // * operator tussen coeficient en punt
            return new PointF(c*P.X, c*P.Y);
        }
        private PointF PointAt(float valueU)
        {
            PointF P = PointF.Empty;
            float b = 0;
            for (int i = 0; i < CPcounter; i++)
            {
                b = PointWeightB(valueU, i, KC.Knots.Degree + 1);
                P = Add(P, Mult(b, ControlPoints[i]));
            }
            return P;
        }
        private float PointWeightB(float ValueU, int shiftI, int localDegreeK)
        {
            //laagste degree kijkt alleen naar punt
            if (localDegreeK == 1)
            {
                if (KnotVectorT[shiftI] <= ValueU && ValueU < KnotVectorT[shiftI + 1])
                    return 1.0f;
                else return 0;
            }
            //voor hogere degrees moeten we recursief de puntgewicht berekenen
            //wiskunde uit Graphicsboek p 379
            float A, B, C; 
            A = (ValueU - KnotVectorT[shiftI]) / (KnotVectorT[shiftI + localDegreeK - 1] - KnotVectorT[shiftI]);
            B = (KnotVectorT[shiftI+localDegreeK] - ValueU ) / (KnotVectorT[shiftI+localDegreeK] - KnotVectorT[shiftI+1]);

            C =   A * PointWeightB(ValueU, shiftI    , localDegreeK - 1)
                + B * PointWeightB(ValueU, shiftI + 1, localDegreeK - 1);

            return C;
        }
    }
}
