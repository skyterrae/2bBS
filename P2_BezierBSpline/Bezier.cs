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
        PointF[] bezierPoints; // Array van de punten van de beziercurve. Wordt gevuld in de DoCasteljau-methode.

        public Bezier() : base(12, 6)
        {
            DoCasteljau();
        }

        public override bool Update(PointF mouse)
        {
            // Update de curve tijdens het verplaatsen van controlepunten
            if (base.Update(mouse))
            {
                DoCasteljau();
                return true;
            }
            return false;
        }
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
        public override void Refresh()
        {
            DoCasteljau();
        }
        private void DrawBezierLine(Graphics G, int indexA, int indexB)
        {
            // Draw line on the form
            G.DrawLine(new Pen(Brushes.Red, 2), bezierPoints[indexA], bezierPoints[indexB]);
        }

        // Hier wordt de berekening van de curve gedaan. Standaard voor u = 0.01 tot 1.00.
        // In iedere iteratie van u wordt over alle punten geïtereerd, om telkens het u-de deel van de lijn tussen die punten te pakken.
        // Dit wordt herhaalt tot er nog 1 punt over is. Dit levert een lijst op met de 100 punten van de beziercurve.
        private void DoCasteljau(int iterations = 100)
        { 
            bezierPoints = new PointF[iterations];

            // De iteratie over u.
            for (int n = 1; n < iterations + 1; n++)
            {
                float temp = n;
                float u = temp / iterations;
                List<PointF> oldPoints = new List<PointF>();
                List<PointF> restPoints = new List<PointF>();

                for (int i = 0; i < CPcounter; i++)
                {
                    oldPoints.Add(ControlPoints[i]);
                }

                // De iteratie over de punten.
                while (oldPoints.Count > 1)
                {
                    for (int i = 0; i < oldPoints.Count - 1; i++)
                    {
                        PointF current = oldPoints[i];
                        PointF next = oldPoints[i + 1];

                        float newX, newY;

                        // Nieuwe punt wordt berekent door verschil tussen de 2 punten keer u te doen.
                        newX = (next.X - current.X) * u;
                        newY = (next.Y - current.Y) * u;

                        PointF bezier = new PointF(current.X + newX, current.Y + newY);
                        restPoints.Add(bezier);
                    }
                    // Als alle nieuwe punten zijn berekent wordt de lijst met oude punten gewist en vervangen door de nieuw berekende punten.
                    oldPoints.Clear();
                    oldPoints = restPoints.ToList<PointF>();
                    restPoints.Clear();
                }
                bezierPoints[n - 1] = oldPoints[0];
            }
        }
    }
}
