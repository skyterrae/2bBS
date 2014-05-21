namespace P2_BezierBSpline
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.knotControl1 = new P2_BezierBSpline.KnotControl();
            this.SuspendLayout();
            // 
            // knotControl1
            // 
            this.knotControl1.Location = new System.Drawing.Point(12, 367);
            this.knotControl1.Name = "knotControl1";
            this.knotControl1.Size = new System.Drawing.Size(777, 160);
            this.knotControl1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(802, 539);
            this.Controls.Add(this.knotControl1);
            this.Name = "MainForm";
            this.Text = "LiniarCurve";
            this.ResumeLayout(false);

        }

        #endregion

        private KnotControl knotControl1;
    }
}

