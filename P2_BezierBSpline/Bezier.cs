using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Drawing;

namespace P2_BezierBSpline
{
    class Bezier : Drawer
    {
        // Okay, dit is plan:
        // Met De Casteljau krijg je 1 punt van de Bezier-curve, niet de hele curve.
        // Om toch een hele curve te krijgen herhaaldelijk De Casteljau uitvoeren.
        // Voorlopig proberen 100 keer, dus een curve bestaande uit 100 punten.
        // Dus 100 iteraties van De Casteljau over alle punten, met u = 0.01, u = 0.02, ..., u - 0.99, u = 1.00.

        PointF[] bezierPoints;

        public Bezier()
        {
            DoCasteljau(25);
        }

        public override bool Update(Point mouse)
        {
            bool basebool = base.Update(mouse);

            if (basebool)
            {
                DoCasteljau(25);
            }
            return basebool;
        }

        public override void Draw(Graphics G)
        {
            base.Draw(G);


            // If the next part is uncommented it 'should' work.
            // But it doesn't.... :)
            // For now, it creates a red cross, and I don't know why...
            // De Casteljau works partly atm.
            // The y-coördinates seem to work, the x-coördinates don't at all, they range from 50-640
            // While the original 4 points of the curve range from 50-110.

            for (int i = 0; i < bezierPoints.Length - 1; i++)
            {
                DrawBezierLine(G, i, i + 1);
                DrawBezierPoint(G, i);
            }
            DrawBezierPoint(G, bezierPoints.Length - 1);
        }

        private void DrawBezierLine(Graphics G, int indexA, int indexB)
        {
            //draw line on the form
            G.DrawLine(new Pen(Brushes.Lavender, 2), bezierPoints[indexA], bezierPoints[indexB]);
        }

        private void DrawBezierPoint(Graphics G, int index)
        {
            //draws point on the form
            G.FillEllipse(Brushes.Purple, bezierPoints[index].X - 3, bezierPoints[index].Y - 3, 6, 6);
            //G.DrawString("p" + (index).ToString(), MainForm.DefaultFont, Brushes.Blue, (float)CPs[index].X - 14, (float)CPs[index].Y - 16);
        }

        // The method that does the work. It returns an array filled with the points of the curve.
        // It will create a curve with 100 points as a default.
        private PointF[] DoCasteljau(int iterations = 100)
        { 
            bezierPoints = new PointF[iterations];

            for (int n = 1; n < iterations + 1; n++)
            {
                float temp = n;
                float u = temp / iterations;
                List<PointF> oldPoints = new List<PointF>();
                List<PointF> restPoints = new List<PointF>();

                for (int i = 0; i < CPs.Length; i++)
                {
                    oldPoints.Add(CPs[i]);
                }

                while (oldPoints.Count > 1)
                {
                    for (int i = 0; i < oldPoints.Count - 1; i++)
                    {
                        PointF current = oldPoints[i];
                        PointF next = oldPoints[i + 1];

                        bool newXBigger = next.X > current.X;
                        bool newYBigger = next.Y > current.Y;

                        float newX, newY;
                        if (newXBigger)
                            newX = current.X + next.X * u;
                        else newX = current.X - next.X * u;
                        if (newYBigger)
                            newY = current.Y + next.Y * u;
                        else newY = current.Y - next.Y * u;


                        PointF bezier = new PointF(newX, newY);
                        restPoints.Add(bezier);
                    }

                    oldPoints.Clear();
                    oldPoints = restPoints.ToList<PointF>();
                    restPoints.Clear();
                }

                bezierPoints[n - 1] = oldPoints[0];

            }

            return bezierPoints;
        }
    }
}
