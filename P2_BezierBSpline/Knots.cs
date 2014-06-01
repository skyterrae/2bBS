using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace P2_BezierBSpline
{
    public class KnotHandler
    {
        protected float[] KV;
        int degree, ActiveKnotIndex;
        public int iterationsPerU;
        float ActiveKnotValue, MouseX;
        private Dictionary<Point, float> B;

        public KnotHandler(int KnotAmount, int Degree)
        {
            KV = new float[KnotAmount + Degree-1];
            iterationsPerU = 25;
            degree = Degree;
            ActiveKnotIndex = -1;
            ForceUniform();
            Refresh();
        }
        public int Degree
        {
            get { return degree; }
        }
        private void Refresh()
        {
            B = new Dictionary<Point, float>();
            CalculatePointweights();
        }
        private void DrawKnotLine(Graphics G, int indexA, int indexB)
        {
            //draw line on the form
            G.DrawLine(new Pen(Brushes.Lavender, 2), new PointF(KV[indexA], 0), new PointF(KV[indexB], 0));
        }
        private void setMouse(PointF mouse)
        {
            MouseX = mouse.X;
        }

        public bool Begin(PointF mouse)
        {
            setMouse(mouse);

            //selects a point, if one is clicked on
            if (ActiveKnotIndex == -1)
            {
                for (int i = 0; i < KV.Length; i++)
                {
                    //vind het geselcteerde punt
                    if (Math.Abs(MouseX - KV[i]) < 0.05f)
                    {
                        //activeerd de punt
                        ActiveKnotValue = KV[i];
                        ActiveKnotIndex = i;
                        break;
                    }
                }
                Refresh();
                return true;
            }
            return false;
        }
        public bool Update(PointF mouse)
        {
            setMouse(mouse);
            //updates the selected point
            if (ActiveKnotIndex != -1 && Math.Abs(MouseX - KV[ActiveKnotIndex]) < 0.05f)
            {
                //herberekend locatie van het bewegende punt
                float X;
                if (ActiveKnotIndex != 0 && ActiveKnotIndex != KV.Length - 1)
                    X = StaticFunctions.Clamp(mouse.X, KV[ActiveKnotIndex - 1], KV[ActiveKnotIndex + 1]);
                else
                    X = StaticFunctions.Clamp(mouse.X, 0, KV.Length - 1);
                KV[ActiveKnotIndex] = X;
                Refresh();
                return true;
            }
            return false;
        }
        public bool End(PointF mouse)
        {
            setMouse(mouse);
            //if a point is selected, deselect it
            if (ActiveKnotIndex != -1)
            {
                //laatste herbereking van geselecteerd punt
                float X;
                if (ActiveKnotIndex == 0 || ActiveKnotIndex == KV.Length - 1)
                    X = StaticFunctions.Clamp(mouse.X, 0, KV.Length - 1);
                else
                    X = StaticFunctions.Clamp(mouse.X, KV[ActiveKnotIndex - 1], KV[ActiveKnotIndex + 1]);

                KV[ActiveKnotIndex] = X;
                ActiveKnotIndex = -1;
                ActiveKnotValue = 0;
                Refresh();
                return true;
            }
            return false;
        }

        public void ForceUniform()
        {
            //forceert uniformiteit van de Knotvector
            for (int i = 0; i < KV.Length; i++)
            {
                KV[i] = i+1;
            }
        }
        private void CalculatePointweights()
        {
            //precalculates the pointweights per shift and parameter value
            for (int i = 1; i < KV.Length - degree; i++)
            {
                int u = (int)(KV[degree] * iterationsPerU);
                float b;
                while (u < (KV[KV.Length-degree])*iterationsPerU)
                {
                    b = PointWeightB(u,i, degree);
                    if(Math.Abs(b)>0) //als gewicht is 0, dan niet in de dictionary
                        B.Add(new Point(i, u), b);
                    u++;
                }
            }
        }
        public float PointWeightB(int ValueU, int shiftI)
        {
            //gets the value out of the dictionary
            Point pw = new Point(shiftI, ValueU);
            if (B.ContainsKey(pw))
                return B[pw];
            else return 0; //zit niet in de dictionary als het gewicht toch 0 is
        }

        private float PointWeightB(int ValueU, int shiftI, int localDegreeK)
        {
            //berekend hoeveel een (controle)punt mee zal tellen op plek U op de curve

            //bij laagste degree wordt telt het controle punt 100% mee.
            float localU = (float)ValueU / (float)iterationsPerU;
            if (localDegreeK == 1)
            {
                if (KV[shiftI] <= localU && localU < KV[shiftI + 1])
                    return 1.0f;
                else return 0;
            }
            //voor hogere degrees moeten we recursief het puntgewicht berekenen
            //wiskunde uit Graphicsboek p 379
            float A, B, C;

            A = (localU - KV[shiftI]) / (KV[shiftI + localDegreeK - 1] - KV[shiftI]);
            B = (KV[shiftI + localDegreeK] - localU) / (KV[shiftI + localDegreeK] - KV[shiftI + 1]);

            C = A * PointWeightB(ValueU, shiftI, localDegreeK - 1)
                + B * PointWeightB(ValueU, shiftI + 1, localDegreeK - 1);

            return C;
        }
        public void DrawBlendFunctions(Graphics G, float TransX, float TransY, float ScaleX, float ScaleY)
        {
            //tekend de gewichten van punten op in het knotVector-Yas
            foreach (KeyValuePair<Point, float> p in B)
                G.DrawEllipse(new Pen(Brushes.Green), TransX+ ScaleX*((float)p.Key.Y/25.0f), p.Value*ScaleY+TransY,1,1);
        }
        public float[] KnotVector
        {
            get { return KV; }
        }
    }
}

