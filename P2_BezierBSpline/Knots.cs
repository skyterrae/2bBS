using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace P2_BezierBSpline
{
    public class KnotHandler : Drawer
    {
        public KnotHandler() : base(60, 3)
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
            ControlPoints[0].Y = 0.8f;
            ControlPoints[1].Y = 0.1f;
            ControlPoints[2].Y = 0.1f;
            //ControlPoints[3].Y = 0.2f;
            //ControlPoints[4].Y = 0.1f;
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
        public override bool Begin(PointF mouse)
        {
            setMouse(mouse);
            //selects a point, if one is clicked on
            if (ActivePointIndex == -1)
            {
                for (int i = 0; i < CPcounter; i++)
                {
                    if (Math.Abs(Mouse.X - ControlPoints[i].X) < 0.05f && Math.Abs(Mouse.Y - ControlPoints[i].Y) < 0.05f)
                    {
                        ActivePoint = ControlPoints[i];
                        ActivePointIndex = i;
                        break;
                    }
                }
                return true;
            }
            return false;
        }
        public override bool Update(PointF mouse)
        {
            setMouse(mouse);
            //updates the selected point
            if (ActivePointIndex != -1 
                && Math.Abs(Mouse.X - ControlPoints[ActivePointIndex].X) < 0.05f
                && Math.Abs(Mouse.Y - ControlPoints[ActivePointIndex].Y) < 0.05f
                )
            {
                float X;
                if (ActivePointIndex != 0 && ActivePointIndex != CPcounter)
                    X = StaticFunctions.Clamp(mouse.X, ControlPoints[ActivePointIndex - 1].X, ControlPoints[ActivePointIndex+1].X);
                else
                    X = StaticFunctions.Clamp(mouse.X, 0, CPcounter);
                float Y = StaticFunctions.Clamp(mouse.Y, -1, 1);
                ControlPoints[ActivePointIndex] = new PointF(X, Y);
                return true;
            }
            return false;
        }
        public override bool End(PointF mouse)
        {
            setMouse(mouse);
            //if a point is selected, deselect it
            if (ActivePointIndex != -1)
            {
                float X;
                if (ActivePointIndex == 0 || ActivePointIndex == CPcounter)
                    X = StaticFunctions.Clamp(mouse.X, 0, CPcounter);
                else
                    X = StaticFunctions.Clamp(mouse.X, ControlPoints[ActivePointIndex - 1].X, ControlPoints[ActivePointIndex + 1].X);
                
                float Y = StaticFunctions.Clamp(mouse.Y, -1.0f, 1.0f);
                ControlPoints[ActivePointIndex] = new PointF(X, Y);
                ActivePointIndex = -1;
                ActivePoint = Point.Empty;
                return true;
            }
            return false;
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
