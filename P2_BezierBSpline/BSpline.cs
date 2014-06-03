using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace P2_BezierBSpline
{
    class BSpline : CurveBase
    {
        //classe die het tekenen en uitrekenen van een BSpline Curve handled
        
        PointF[] BSplinePoints;
        KnotControl KC;

        public BSpline(KnotControl kc) : base(30, 12)
        {
            //constructor; begint met 12 controlepunten, max 30 controlepunten
            KC = kc;
            KC.Visible = true;
            CalculateBSpline();
        }

        #region CalculateFunctions
        private void CalculateBSpline()
        {
            //berekend voor alle punten U op de curve hun positie
            BSplinePoints = new PointF[M * KC.Knots.iterationsPerU];
            int U = 0;
            while (U < BSplinePoints.Length)
            {
                //berekend punt op positie U
                BSplinePoints[U] = PointAt(U);
                //berekend volgende U
                U++;
            }
        }      
        private PointF PointAt(int valueU)
        {
            //berekend het punt op de plek U op  de curve

            PointF P = PointF.Empty;
            float b = 0;
            for (int i = 0; i <= M && i < ControlPoints.Length; i++)
            {
                //berekend per controlepunt hoeveel het meeteld en telt hem bij het geheel op
                b = KC.Knots.getBfromDictionary(valueU, i);
                P = PointMathHelper.Add(P, PointMathHelper.Mult(b, ControlPoints[i]));
            }
            return P;
        }
        #endregion

        #region Override Update Functions
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
            CalculateBSpline();
        }
        #endregion

        #region DrawFunctions
        public override void Draw(Graphics G)
        {
            //tekend de controlepunten en contenthull
            base.Draw(G);

            //tekend de curve
            for (int i = (Degree+1) * KC.Knots.iterationsPerU; BSplinePoints != null && i < BSplinePoints.Length - Degree * KC.Knots.iterationsPerU-2; i++)
            {
                DrawBSplineLine(G, i, i + 1);
            }           
        }

        private void DrawBSplineLine(Graphics G, int indexA, int indexB)
        {
            //draw line on the form
            G.DrawLine(new Pen(Brushes.Blue, 2), BSplinePoints[indexA], BSplinePoints[indexB]);
        }
        #endregion

        #region Properties
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
        #endregion
    }
}
