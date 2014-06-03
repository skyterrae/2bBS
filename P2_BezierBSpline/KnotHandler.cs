using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace P2_BezierBSpline
{
    public class KnotHandler
    {
        //Klasse die het maken en aanpassen van de KnotVector aanpast.
        //Houdt ook de gewichten van punten bij die worden berekend met 
        //de blendfunctions in CalculatePointWeight()

        //( had deze klasse kunnen laten inheriten van Drawer (net als de andere Curve-achtige klassen) 
        //  maar dat werdt verwarrend omdat Knots anders moeten zijn dan ControlPoints)

        protected float[] knotVector;
        int degree, ActiveKnotIndex;
        public int iterationsPerU;
        float ActiveKnotValue, MouseX;
        private Dictionary<Point, float> B;

        public KnotHandler(int KnotAmount, int Degree)
        {
            //contructor
            knotVector = new float[KnotAmount + Degree-1];
            iterationsPerU = 25;
            degree = Degree;
            ActiveKnotIndex = -1;
            ForceUniform();
            Refresh();
        }
        #region Calculate BlendFunctions
        private void Refresh()
        {
            //herberekend PointWeights in Dictionary B
            B = new Dictionary<Point, float>();
            CalculatePointweights();
        }
        public void ForceUniform()
        {
            //forceert uniformiteit van de Knotvector
            for (int i = 0; i < knotVector.Length; i++)
            {
                knotVector[i] = i+1;
            }
            Refresh();
        }
        private void CalculatePointweights()
        {
            //precalculates the pointweights per shift and parameter value
            for (int i = 0; i < knotVector.Length; i++)  //for every shift
            {
                int u = 0;
                float b;
                while (u < (knotVector[knotVector.Length-1])*iterationsPerU)  //for every value of u
                {
                    b = PointWeightB(u,i, degree);
                    if(Math.Abs(b)>0) 
                        //als gewicht is 0, dan niet in de dictionary
                        B.Add(new Point(i, u), b);
                    u++;
                }
            }
        }
        public float getBfromDictionary(int ValueU, int shiftI)
        {
            //gets the weight b of point on u out of the dictionary
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
                if (KnotVectorT(shiftI) <= localU && localU < KnotVectorT(shiftI + 1))
                    return 1.0f;
                else return 0;
            }
            //voor hogere degrees moeten we recursief het puntgewicht berekenen
            //wiskunde uit Graphicsboek p 379
            float A, B, C;

            A = (localU - KnotVectorT(shiftI)) / (KnotVectorT(shiftI + localDegreeK - 1) - KnotVectorT(shiftI));
            B = (KnotVectorT(shiftI + localDegreeK) - localU) / (KnotVectorT(shiftI + localDegreeK) - KnotVectorT(shiftI + 1));

            C = A * PointWeightB(ValueU, shiftI, localDegreeK - 1)
                + B * PointWeightB(ValueU, shiftI + 1, localDegreeK - 1);

            return C;
        }
        private float KnotVectorT(int index)
        {
            //geeft een waarde van de KV, maar pakt de laatste uit die array als de index over de arraygrootte heengaat
            if (index >= knotVector.Length)
                return knotVector[knotVector.Length - 1];
            else return knotVector[index];
        }
        #endregion

        #region DrawFunctions
        public void DrawBlendFunctions(Graphics G, float TransX, float TransY, float ScaleX, float ScaleY)
        {
            //tekend de gewichten van punten op in het knotVector-Yas
            foreach (KeyValuePair<Point, float> p in B)
                G.DrawEllipse(new Pen(Brushes.Green), TransX+ ScaleX*((float)p.Key.Y/25.0f), p.Value*ScaleY+TransY,1,1);
        }
        private void DrawKnotLine(Graphics G, int indexA, int indexB)
        {
            //draw line on the form
            G.DrawLine(new Pen(Brushes.Lavender, 2), new PointF(knotVector[indexA], 0), new PointF(knotVector[indexB], 0));
        }
        #endregion

        #region KnotValue UpdateFunctions
        public bool Begin(PointF mouse)
        {
            MouseX = mouse.X;

            //selects a point, if one is clicked on
            if (ActiveKnotIndex == -1)
            {
                for (int i = 0; i < knotVector.Length; i++)
                {
                    //vind het geselcteerde punt
                    if (Math.Abs(MouseX - knotVector[i]) < 0.05f)
                    {
                        //activeerd de punt
                        ActiveKnotValue = knotVector[i];
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
            MouseX = mouse.X;

            //updates the selected point
            if (ActiveKnotIndex != -1)
            {
                //herberekend locatie van het bewegende punt
                float X;
                if (ActiveKnotIndex != 0 && ActiveKnotIndex != knotVector.Length - 1)
                    X = PointMathHelper.Clamp(mouse.X, knotVector[ActiveKnotIndex - 1], knotVector[ActiveKnotIndex + 1]);
                else
                    X = PointMathHelper.Clamp(mouse.X, 0, knotVector.Length - 1);
                knotVector[ActiveKnotIndex] = X;
                Refresh();
                return true;
            }
            return false;
        }
        public bool End(PointF mouse)
        {
            MouseX = mouse.X;

            //if a point is selected, deselect it
            if (ActiveKnotIndex != -1)
            {
                //laatste herbereking van geselecteerd punt
                float X;
                if (ActiveKnotIndex == 0 || ActiveKnotIndex == knotVector.Length - 1)
                    X = PointMathHelper.Clamp(mouse.X, 0, knotVector.Length - 1);
                else
                    X = PointMathHelper.Clamp(mouse.X, knotVector[ActiveKnotIndex - 1], knotVector[ActiveKnotIndex + 1]);

                knotVector[ActiveKnotIndex] = X;
                ActiveKnotIndex = -1;
                ActiveKnotValue = 0;
                Refresh();
                return true;
            }
            return false;
        }
        #endregion

        #region Properties
        public int Degree
        {
            get { return degree; }
        }
        public float[] KnotVector
        {
            get { return knotVector; }
        }
        #endregion

    }
}

