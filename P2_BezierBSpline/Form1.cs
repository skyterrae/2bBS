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
            InitializeComponent();
            chLCurveChoise.SelectedIndex = 2;
            Curve = new BSpline(knotControl1);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //draws the contenthull (and possibly the curve itsself)
            Curve.Draw(e.Graphics);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            //changes context hull if a point is selected
            if (Curve.Update(new Point(e.X, e.Y)))
                Refresh();
            
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            //deselects a point (if one is selected)
            if (Curve.End(new Point(e.X, e.Y)))
                Refresh();
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnClick(e);
            //selects a point, if one has been clicked on
            if (Curve.Begin(new Point(e.X, e.Y)))
                Refresh();
            
        }

        private void chLCurveChoise_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (chLCurveChoise.SelectedIndex)
            {
                case 0: Curve = new Bezier(); knotControl1.Visible = false; break;
                case 1: Curve = new Bezier(); knotControl1.Visible = false; break;
                case 2: Curve = new BSpline(knotControl1); knotControl1.Visible = true; break;
            }
            Refresh();
        }

        private void btnPointIncrease_Click(object sender, EventArgs e)
        {
            Curve.AddPoint();
            Curve.Refresh();
            Refresh();
        }

        private void btnPointDecrease_Click(object sender, EventArgs e)
        {
            Curve.DeletePoint();
            Curve.Refresh();
            Refresh();
        } 
    }
}
