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
            knots = new KnotHandler(12,3);

            //zorgen voor juiste schaalomzetting bij het tekenen van de knotgerelateerde dingen
            ScaleX = 2 * (this.Width - 60) / ((knots.KnotVector.Length) * 2 +1); 
            ScaleY = -75.0f; 
            TransX = 30; 
            TransY = 110;
        }
        public void setKnotAmount(int cpAmount)
        {
            //maakt de knots opnieuw aan als het aantal controlepunten van de Curve verandeerd
            knots = new KnotHandler(cpAmount, knots.Degree);
            ScaleX = 2 * (this.Width - 60) / ((knots.KnotVector.Length) * 2 + 1); 
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            //changes context hull if a point is selected
            if (Knots.Update(new PointF((((float)e.X - TransX) / ScaleX), (((float)e.Y - TransY) / ScaleY))))
            {
                Refresh(); //refreshed alleen de control omdat de verplaatsing nog niet af it
            }

        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            //deselects a point (if one is selected)

            if (Knots.End(new PointF((((float)e.X- TransX) / ScaleX ), (((float)e.Y- TransY) / ScaleY ))))
                this.Parent.Refresh();  //refreshes ook de Mainform zodat de Curve getekend wordt aan de hand van de nieuwe Knotvector
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

            //draws the raster
            for (int x = 0; x <= (knots.KnotVector.Length) *2; x++)           
                DrawVertiRaster(x, e.Graphics);

            for (float y = -1; y <= 1; y = y+ 0.2f)
                DrawHoriRaster(y, e.Graphics);

            //zwaardere lijnen bij de gehele getallen op de y-as (+benaming)
            e.Graphics.DrawString("+1".ToString(), MainForm.DefaultFont, Brushes.Blue, new Point(13, (int)TransY - 75));
            e.Graphics.DrawLine(new Pen(Brushes.Blue, 1), new Point((int)TransX, (int)TransY - 75), new Point(this.Width - (int)TransX, (int)TransY - 75));
            e.Graphics.DrawString("0".ToString(), MainForm.DefaultFont, Brushes.Blue, new Point(18, (int)TransY-8));
            e.Graphics.DrawLine(new Pen(Brushes.Blue, 1), new Point((int)TransX, (int)TransY), new Point(this.Width - (int)TransX, (int)TransY));
            e.Graphics.DrawString("-1".ToString(), MainForm.DefaultFont, Brushes.Blue, new Point(13, (int)TransY + 59));
            e.Graphics.DrawLine(new Pen(Brushes.Blue, 1), new Point((int)TransX, (int)TransY + 75), new Point(this.Width - (int)TransX, (int)TransY + 75));

            //tekend locaties van de knots
            for (int i = 0; i < knots.KnotVector.Length; i++)
                DrawVertiKnotLocation(i, e.Graphics);

            knots.DrawBlendFunctions(e.Graphics, TransX, TransY, ScaleX, ScaleY);
        }
        private void DrawHoriRaster(float y, Graphics G)
        {
            //teken horizontale lijn
            G.DrawLine(new Pen(Brushes.LightBlue, 1), new Point((int)TransX, (int)(TransY + y * ScaleY)), new Point(this.Width - (int)TransX, (int)(TransY + y * ScaleY)));
        }
        private void DrawVertiKnotLocation(int i, Graphics G)
        {
            //tekend een lijn waar knot i zit op het assenstelsel
            int coorX = (int)(TransX + knots.KnotVector[i] * ScaleX );
            int coorY1 = (int)(TransY + 0.2f * ScaleY);
            int coorY2 = (int)(TransY + (-0.2f) * ScaleY);
            G.DrawLine(new Pen(Brushes.Red, 2), new Point(coorX, coorY1+1), new Point(coorX, coorY2+1));
            G.DrawString("k"+(i+1).ToString(), MainForm.DefaultFont, Brushes.Red, new Point(coorX -12, coorY1-13));
        }
        private void DrawVertiRaster(int x, Graphics G)
        {
            //teken verticale rarsterlijn
            Pen P= new Pen(Brushes.LightBlue, 1);
            int coorX = (int)(TransX + x * ScaleX/2.0f);
            if (x % 2 == 0 && x>0 && x<=knots.KnotVector.Length*2)
            {
                //geeft lijn een x-coor-benaming
                G.DrawString((x / 2).ToString(), MainForm.DefaultFont, Brushes.Blue, new Point(coorX + 1, (int)TransY));
            }

            //lijnen per 0.5
            G.DrawLine(P, new Point(coorX, (int)TransY - 75), new Point(coorX, (int)TransY + 75));

            //lijnen aan zijnkant van raster
            G.DrawLine(new Pen(Brushes.Blue, 1), new Point((int)TransX, (int)TransY - 75), new Point((int)TransX, (int)TransY + 75));
            G.DrawLine(new Pen(Brushes.Blue, 1), new Point(this.Width - (int)TransX, (int)TransY - 75), new Point(this.Width - (int)TransX, (int)TransY + 75));
        }

        private void btnDegreeIncrease_Click(object sender, EventArgs e)
        {
            //verhoogt het aantal knots (en de graad)
            if (knots.Degree < 12 && knots.KnotVector.Length > 2 * knots.Degree + 2)
            {
                knots = new KnotHandler(knots.KnotVector.Length - knots.Degree + 1, knots.Degree + 1);
                ScaleX = 2 * (this.Width - 60) / ((knots.KnotVector.Length) * 2 + 1); 
            }
            lblDegree.Text = "Degree: " + knots.Degree;
            this.Parent.Refresh();
        }

        private void lblDegreeDecrease_Click(object sender, EventArgs e)
        {
            //verlaagt het aantal knots (en de graad)
            if (knots.Degree > 2)
            {
                knots = new KnotHandler(knots.KnotVector.Length - knots.Degree + 1, knots.Degree - 1);
                ScaleX = 2 * (this.Width - 60) / ((knots.KnotVector.Length) * 2 + 1); 
            }
            lblDegree.Text = "Degree: " + knots.Degree;
            this.Parent.Refresh();
        }

        private void btnForceUniform_Click(object sender, EventArgs e)
        {
            //zorgt ervoor dat de Knotvector uniform wordt zodat de Curve dat ook wordt.
            knots.ForceUniform();
            this.Parent.Refresh();
        }
    }
}
