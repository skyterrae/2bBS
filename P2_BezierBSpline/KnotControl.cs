using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace P2_BezierBSpline
{
    public partial class KnotControl : UserControl
    {
        KnotHandler knots;
        float ScaleX, ScaleY, TransX, TransY;
        public KnotControl()
        {
            InitializeComponent();
            knots = new KnotHandler();

            ScaleX = 2* (this.Width - 60) / ((knots.Degree + 1) * 2); 
            ScaleY = -75.0f; 
            TransX = 30; 
            TransY = 80;
        }


        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            //changes context hull if a point is selected
            if (Knots.Update(new PointF((((float)e.X- TransX) / ScaleX ), (((float)e.Y- TransY) / ScaleY ))))
                Refresh();

        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            //deselects a point (if one is selected)

            if (Knots.End(new PointF((((float)e.X- TransX) / ScaleX ), (((float)e.Y- TransY) / ScaleY ))))
                Refresh();
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnClick(e);
            //selects a point, if one has been clicked on

            if (Knots.Begin(new PointF((((float)e.X- TransX) / ScaleX ), (((float)e.Y- TransY) / ScaleY ))))
                Refresh();
        } 
        public KnotHandler Knots
        {
            get { return knots; }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //draws her raster
            for (int x = 0; x <= (knots.Degree + 3) *2; x++)
            {
                DrawVertiRaster(x, e.Graphics);
            }
            for (float y = -1; y <= 1; y = y+ 0.2f)
            {
                DrawHoriRaster(y, e.Graphics);
            }
            //zwaardere lijnen bij de gehele getallen op de y-as (+benaming)
            e.Graphics.DrawString("+1".ToString(), MainForm.DefaultFont, Brushes.Blue, new Point(13, 5));
            e.Graphics.DrawLine(new Pen(Brushes.Blue, 1), new Point(30, 5), new Point(this.Width - 30, 5));
            e.Graphics.DrawString("0".ToString(), MainForm.DefaultFont, Brushes.Blue, new Point(18, 72));
            e.Graphics.DrawLine(new Pen(Brushes.Blue, 1), new Point(30, 80), new Point(this.Width - 30, 80));
            e.Graphics.DrawString("-1".ToString(), MainForm.DefaultFont, Brushes.Blue, new Point(13, 132));
            e.Graphics.DrawLine(new Pen(Brushes.Blue, 1), new Point(30, 155), new Point(this.Width - 30, 155));

            //draws the controlpoints
            Point Knot;
            Point OldKnot = new Point(30, 80) ;    
            for (int x = 0; x < knots.Degree; x++)
            {
                Knot = new Point((int)(TransX + knots.getKnot(x).X *ScaleX), (int)(TransY + ScaleY*knots.getKnot(x).Y));
                e.Graphics.DrawLine(new Pen(Brushes.Red, 2), OldKnot, Knot);
                e.Graphics.FillEllipse(Brushes.Black, Knot.X - 4, Knot.Y - 4, 8, 8);
                OldKnot = Knot;
            }
            Knot = new Point(this.Width - 30, 80);
            e.Graphics.DrawLine(new Pen(Brushes.Red, 2), OldKnot, Knot);
            
        }
        private void DrawHoriRaster(float y, Graphics G)
        {
            //teken horizontale lijn
            G.DrawLine(new Pen(Brushes.LightBlue, 1), new Point(30, (int)(TransY + y * ScaleY)), new Point(this.Width - 30, (int)(TransY + y * ScaleY)));
        }
        private void DrawVertiRaster(int x, Graphics G)
        {
            //teken verticale rarsterlijn
            Pen P;
            int coorX = (int)(TransX + x * ScaleX/2.0f);
            if (x % 2 == 0 && x>0 && x<=knots.Degree*2)
            {
                //dikke pen bij hele getallen
                P = new Pen(Brushes.Blue, 2);
                //geeft lijn een x-coor-benaming
                G.DrawString((x / 2).ToString(), MainForm.DefaultFont, Brushes.Blue, new Point(coorX+1, 80));
            }
            else //dunne pen
                P = new Pen(Brushes.LightBlue, 1);

            //bij halve getallen dunnen lijnen en geen nummer
            G.DrawLine(P, new Point(coorX, 5), new Point(coorX, 155));

            //lijnen aan zijnkant van raster
            G.DrawLine(new Pen(Brushes.Blue, 1), new Point(30, 5), new Point(30, 155));
            G.DrawLine(new Pen(Brushes.Blue, 1), new Point(this.Width - 30, 5), new Point(this.Width - 30, 155));
        }
    }
}
