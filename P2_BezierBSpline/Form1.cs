using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace P2_BezierBSpline
{
    public partial class MainForm : Form
    {
        public static Drawer Curve;
        
        public MainForm()
        {
            //contructor
            InitializeComponent();

            //zet de curve-keuzelijst op beginkeuze 
            //en initialiseerd de Curve als een curve van die soort
            chLCurveChoise.SelectedIndex = 2;
            Curve = new BSpline(knotControl1);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //tekend de contenthull met controlepunten
            //en de curve zelf
            Curve.Draw(e.Graphics);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            //als een punt geselecteerd is en de muis beweegt
            //dan herberekend de curve zich
            if (Curve.Update(new Point(e.X, e.Y)))
                Refresh();
            
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            //laatste curveupdate als de geselecteerde punt wordt losgelaten
            if (Curve.End(new Point(e.X, e.Y)))
                Refresh();
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnClick(e);
            //selecteerd een controlepunt en hertekend de curve.
            if (Curve.Begin(new Point(e.X, e.Y)))
                Refresh();
            
        }
        public override void Refresh()
        {
            base.Refresh();
            Curve.Refresh();
        }
        private void chLCurveChoise_SelectedIndexChanged(object sender, EventArgs e)
        {
            //veranderd de curve van soort als de keuzelijst-selectie veranderd.
            switch (chLCurveChoise.SelectedIndex)
            {
                case 0: Curve = new Bezier(); knotControl1.Visible = false; break;
                case 1: Curve = new Bezier(); knotControl1.Visible = false; break;
                case 2: Curve = new BSpline(knotControl1); knotControl1.Visible = true; break;
            }
            lblPointAmount.Text = "PointAmount: " + Curve.PointAmount;
            Refresh();
        }

        private void btnPointIncrease_Click(object sender, EventArgs e)
        {
            //voegt een controlepunt toe aan de curve
            Curve.AddPoint();
            lblPointAmount.Text = "PointAmount: " + Curve.PointAmount;
            if (Curve is BSpline)
            {
                knotControl1.setKnotAmount(Curve.PointAmount+1);
            }
            Refresh();
        }

        private void btnPointDecrease_Click(object sender, EventArgs e)
        {
            //haalt een controlepunt weg uit de curve.
            Curve.DeletePoint();
            lblPointAmount.Text = "PointAmount: " + Curve.PointAmount;

            if (Curve is BSpline)
            {
                knotControl1.setKnotAmount(Curve.PointAmount+1);
            }
            Refresh();
        } 
    }
}
