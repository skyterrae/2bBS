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
        float ActiveKnotValue, MouseX;


        public KnotHandler(int KnotAmount, int Degree)
        {
            KV = new float[KnotAmount + 1];
            degree = Degree;
            ForceUniform();
            ActiveKnotIndex = -1;
        }
        public int Degree
        {
            get { return degree; }
            set 
            { 
                // past alleen de degree aan als er uberhaubt genoeg 
                //controlepunten zijn om er nog een curve mee te kunnen maken 
                if(value < KV.Length)
                    degree = value; 
            }
        }

        private void DrawKnotLine(Graphics G, int indexA, int indexB)
        {
            //draw line on the form
            G.DrawLine(new Pen(Brushes.Lavender, 2), new PointF(KV[indexA],0), new PointF(KV[indexB],0));
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
                if (ActiveKnotIndex != 0 && ActiveKnotIndex != KV.Length-1)
                    X = StaticFunctions.Clamp(mouse.X, KV[ActiveKnotIndex - 1], KV[ActiveKnotIndex+1]);
                else
                    X = StaticFunctions.Clamp(mouse.X, 0, KV.Length-1);
                float Y = StaticFunctions.Clamp(mouse.Y, -1, 1);
                KV[ActiveKnotIndex] = X;
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
                if (ActiveKnotIndex == 0 || ActiveKnotIndex == KV.Length-1)
                    X = StaticFunctions.Clamp(mouse.X, 0, KV.Length - 1);
                else
                    X = StaticFunctions.Clamp(mouse.X, KV[ActiveKnotIndex - 1], KV[ActiveKnotIndex + 1]);
                KV[ActiveKnotIndex] = X;
                ActiveKnotIndex = -1;
                ActiveKnotValue = 0;
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
        public void ClampEnds()
        {
            //maakt KnotVector zo dat het begin en eindpunt 
            //in de Controlepunten geraakt worden door de Curve
            for (int i = 0; i < KV.Length; i++)
            {
                //clampt beginpunt
                if(0<=i && i<degree+1)
                    KV[i] = degree+1;
                //clampt eindpunt
                else if ( KV.Length - degree <= i&& i < KV.Length)
                    KV[i] = KV.Length - degree;
                //maakt rest van de punten
                else KV[i] = i+1;
            }
        }
        public float[] KnotVector
        {
            get { return KV; }
        }
    }
}
