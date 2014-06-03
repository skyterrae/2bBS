using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace P2_BezierBSpline
{
    public class CurveBase
    {
        //basisclass die de onderliggende dingen regelt voor de verschillende curve-soorten
        //wordt in deze vorm niet gebruikt (alleen de kinderen van deze klasse worden gebruikt)

        protected PointF[] ControlPoints;
        protected PointF ActivePoint, Mouse;
        protected int CPcounter, ActivePointIndex;

        public CurveBase(int MaxPoints, int CurrPoints)
        {
            //constructor
            ControlPoints = new PointF[MaxPoints];

            CPcounter = CurrPoints;
            //geeft beginplek voor alle punten (ook de onzichtbare)
            for (int i = 0; i < ControlPoints.Length; i++)
            {
                ControlPoints[i] = new Point(50 + i * 30, 350 - (i % 2) * 150);
            }
            //er is geen punt geselecteerd
            ActivePointIndex = -1;
            ActivePoint = Point.Empty;
        }    
        
        #region DrawFunctions
        public virtual void Draw(Graphics G)
        {
            //tekend de punten en lijnen (content hull)
            for (int i = 0; i < CPcounter - 1; i++)
            {
                DrawLine(G, i, i+1);
                DrawPoint(G, i);
            }
            DrawPoint(G, CPcounter - 1);
        }
        protected void DrawPoint(Graphics G, int index)
        {
            //draws point on the form
            G.FillEllipse(Brushes.Black, ControlPoints[index].X - 4, ControlPoints[index].Y - 4, 8, 8);
            G.DrawString("p" + (index + 1).ToString(), MainForm.DefaultFont, Brushes.Blue, (float)ControlPoints[index].X - 14, (float)ControlPoints[index].Y - 16);
        }
        protected void DrawLine(Graphics G, int indexA, int indexB)
        {
         //draw line on the form
         G.DrawLine(new Pen(Brushes.PaleTurquoise, 2), ControlPoints[indexA], ControlPoints[indexB]);
        }
        #endregion

        #region ControlPoint Amount UpdateFunctions
        public virtual void Refresh()
        { }
        public virtual void AddPoint()
        {
            //voegt een punt toe, als het max aantal puten nog niet bereikt is.
            if (CPcounter < ControlPoints.Length)
            {
                CPcounter++;
            }
        }
        public virtual void DeletePoint()
        {
            //haalt laatste punt weg.
            if (CPcounter > 3)
                CPcounter--;
        }
        #endregion

        #region ControlPoint Location UpdateFunctions
        public virtual void setMouse(PointF M)
        {
            Mouse = M;
        }
        public virtual bool Begin(PointF mouse)
        {
            setMouse(mouse);
            //selects a point, if one is clicked on
            if (ActivePointIndex == -1)
            {
                for (int i = 0; i < CPcounter; i++)
                {
                    if (Math.Abs(Mouse.X - ControlPoints[i].X) < 20 && Math.Abs(Mouse.Y - ControlPoints[i].Y) < 20)
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
        public virtual bool Update(PointF mouse)
        {
            setMouse(mouse);
            //updates the selected point
            if (ActivePointIndex != -1 &&
                Math.Abs(Mouse.X - ControlPoints[ActivePointIndex].X) > 2 && Math.Abs(Mouse.Y - ControlPoints[ActivePointIndex].Y) > 2)
            {
                ControlPoints[ActivePointIndex] = Mouse;
                return true;
            }
            return false;
        }
        public virtual bool End(PointF mouse)
        {
            setMouse(mouse);
            //if a point is selected, deselect it
            if (ActivePointIndex != -1)
            {
                ControlPoints[ActivePointIndex] = mouse;
                ActivePointIndex = -1;
                ActivePoint = Point.Empty;
                return true;
            }
            return false;
        }
        #endregion
         
        #region Properties
        public virtual int PointAmount
        {
            get { return CPcounter; }
        }
        public virtual int Degree
        {
            get { return CPcounter - 1; }
        }
        #endregion
    }


}
