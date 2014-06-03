using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace P2_BezierBSpline
{
    public class Drawer
    {
        protected PointF[] ControlPoints;
        protected PointF ActivePoint, Mouse;
        protected int CPcounter, ActivePointIndex;


        public virtual int PointAmount
        {
            get { return CPcounter; }
        }
        public virtual int Degree
        {
            get { return CPcounter-1; }
        }

        public Drawer(int MaxPoints, int CurrPoints)
        {
            //constructor
            ControlPoints = new PointF[MaxPoints];

            CPcounter = CurrPoints;
            for (int i = 0; i < ControlPoints.Length; i++)
            {
                ControlPoints[i] = new Point(50 + i * 20, 100 - (i % 2) * 50);
            }
            ActivePointIndex = -1;
            ActivePoint = Point.Empty;
        }

        public virtual void AddPoint()  //voegt een controlepunt toe aan het eind van de curve
        {
            //voegt een punt toe, als het max aantal puten nog niet bereikt is.
            if (CPcounter < ControlPoints.Length)
            {
                CPcounter++;
            }
        }
        public virtual void Refresh()
        { }

        public void AddPoint(int afterthisone)  //nog niet getest
        {
            //voegt een punt toe, als het max aantal puten nog niet bereikt is.
            if (CPcounter < ControlPoints.Length)
            {
                CPcounter++;
                for (int i = CPcounter - 1; i > afterthisone; i--)
                {
                    ControlPoints[i] = ControlPoints[i - 1];
                }
                ControlPoints[afterthisone + 1] = new PointF((ControlPoints[afterthisone].X + ControlPoints[afterthisone + 1].X) / 2,
                    (ControlPoints[afterthisone].Y + ControlPoints[afterthisone + 1].Y) / 2);
            }
        }

        public virtual void DeletePoint()   
        {
            //haalt laatste punt weg.
            if(CPcounter >3)
                CPcounter--;
        }

        public void DeletePoint(int thisone)   //nog niet getest
        {
            //haalt aangegeven punt weg.
            if (CPcounter < ControlPoints.Length)
            {
                //schuift levende punten op in de array
                for (int i = thisone; i < CPcounter; i++)
                {
                    ControlPoints[i] = ControlPoints[i + 1];
                }
                CPcounter--;
            }
        }

        protected void DrawLine(Graphics G, int indexA, int indexB)
        {
            //draw line on the form
            G.DrawLine(new Pen(Brushes.PaleTurquoise, 2), ControlPoints[indexA], ControlPoints[indexB]);
        }

        protected void DrawPoint(Graphics G, int index)
        {
            //draws point on the form
            G.FillEllipse(Brushes.Black, ControlPoints[index].X - 4, ControlPoints[index].Y - 4, 8, 8);
            G.DrawString("p" + (index+1).ToString(), MainForm.DefaultFont, Brushes.Blue, (float)ControlPoints[index].X - 14, (float)ControlPoints[index].Y - 16);
        }

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
    }
}
