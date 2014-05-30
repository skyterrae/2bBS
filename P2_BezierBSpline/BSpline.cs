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

        public int N  //#controlepunten -1
        {
            get{  return CPcounter - 1;  }
        }
        public override int Degree //degree / graad
        {
            get { return KC.Knots.Degree; }
        }
        public int M  //lengte knotvector: P+N+1
        {
            get { return CPcounter + KC.Knots.Degree; }
        }

        public BSpline(KnotControl kc) : base(30, 12)
        {
            //constructor; begint met 12 controlepunten
            KC = kc;
            KC.Visible = true;
            BSplinePoints = CalculateBSpline();
        }

        public override bool Update(PointF mouse)
        {
            if (base.Update(mouse) )
            {
                //herberekend de punten van de curve tijdens het verschuiven van een controlepunt
                Refresh();
                return true;
            }
            return false;
        }
        public override void Refresh()
        {
            //haalt Knotvector op en herberekend punten van de curve
            BSplinePoints = CalculateBSpline();
        }

        public override void Draw(Graphics G)
        {
            //tekend de controlepunten en contenthull
            base.Draw(G);

            //tekend de curve
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

        private PointF[] CalculateBSpline(int iterationsPerU = 15)
        {
            //berekend voor alle punten U op de curve hun positie
            PointF[] Ps = new PointF[(N-1-Degree)*25];
            float Uend = N;
            float U = Degree+1;
            int i = 0;
            while(U<=Uend && i<Ps.Length)
            {
                //berekend punt op positie U
                Ps[i] = PointAt(U);
                //berekend volgende U
                U = U + 0.04f;
                i++;
            }

            return Ps;
        }
        
        private PointF PointAt(float valueU)
        {
            //berekend het punt op de plek U op  de curve

            PointF P = PointF.Empty;
            float b = 0;
            for (int i = 0; i < CPcounter - KC.Knots.Degree +1; i++)
            {
                //berekend per controlepunt hoeveel het meeteld en telt hem bij het geheel op
                b = PointWeightB(valueU, i, KC.Knots.Degree);
                P = StaticFunctions.Add(P, StaticFunctions.Mult(b, ControlPoints[i]));
            }
            return P;
        }
        private float PointWeightB(float ValueU, int shiftI, int localDegreeK)
        {
            //berekend hoeveel een (controle)punt mee zal tellen op plek U op de curve

            float[] KnotVectorT = this.KC.Knots.KnotVector;

            //bij laagste degree wordt telt het controle punt 100% mee.
            if (localDegreeK == 1)
            {
                if (KnotVectorT[shiftI] <= ValueU && ValueU < KnotVectorT[shiftI + 1])
                    return 1.0f;
                else return 0;
            }
            //voor hogere degrees moeten we recursief het puntgewicht berekenen
            //wiskunde uit Graphicsboek p 379
            float A, B, C;
            A = (ValueU - KnotVectorT[shiftI]) / (KnotVectorT[shiftI + localDegreeK - 1] - KnotVectorT[shiftI]);
            B = (KnotVectorT[shiftI + localDegreeK] - ValueU) / (KnotVectorT[shiftI + localDegreeK] - KnotVectorT[shiftI + 1]);

            C =   A * PointWeightB(ValueU, shiftI    , localDegreeK - 1)
                + B * PointWeightB(ValueU, shiftI + 1, localDegreeK - 1);

            return C;
        }
    }
}
