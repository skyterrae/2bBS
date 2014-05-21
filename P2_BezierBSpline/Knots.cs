using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace P2_BezierBSpline
{
    public class KnotHandler : Drawer
    {
        public KnotHandler() : base(35, 5)
        {
            setDefault();          
        }
        public PointF getKnot(int i)
        {
            return ControlPoints[i];
        }

        private void setDefault()
        {
            //sets the knots and knotvector to a default
            ForceUniform();
            ControlPoints[0].Y = 0.1f;
            ControlPoints[1].Y = 0.2f;
            ControlPoints[2].Y = 0.4f;
            ControlPoints[3].Y = 0.2f;
            ControlPoints[4].Y = 0.1f;
        }
        private void DrawKnotLine(Graphics G, int indexA, int indexB)
        {
            //draw line on the form
            G.DrawLine(new Pen(Brushes.Lavender, 2), ControlPoints[indexA], ControlPoints[indexB]);
        }

        public int Degree
        {
            get { return CPcounter; }
        }

        private void ForceUniform()
        {
            for (int i = 0; i < ControlPoints.Length; i++)
            {
                ControlPoints[i].X = (float)i+1;
            }
        }

        public float[] KnotVector(int N)
        {
            int KPlusN = N + CPcounter+1;
            // haalt de KnotVector uit de plaatsing van de Knots(/controlpoints)
            float[] KV = new float[KPlusN];
            for (int i = 0; i < KPlusN; i++)
                KV[i] = (float)ControlPoints[i].X;

            return KV;
        }
    }
}
