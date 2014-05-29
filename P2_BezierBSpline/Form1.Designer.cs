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
            this.chLCurveChoise = new System.Windows.Forms.ComboBox();
            this.lblcurveChoise = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnPointIncrease = new System.Windows.Forms.Button();
            this.btnPointDecrease = new System.Windows.Forms.Button();
            this.knotControl1 = new P2_BezierBSpline.KnotControl();
            this.SuspendLayout();
            // 
            // chLCurveChoise
            // 
            this.chLCurveChoise.FormattingEnabled = true;
            this.chLCurveChoise.Items.AddRange(new object[] {
            "Bezier (Castillieau)",
            "Bezier (Math)",
            "BSpline (Math)"});
            this.chLCurveChoise.Location = new System.Drawing.Point(821, 37);
            this.chLCurveChoise.Name = "chLCurveChoise";
            this.chLCurveChoise.Size = new System.Drawing.Size(137, 21);
            this.chLCurveChoise.TabIndex = 1;
            this.chLCurveChoise.SelectedIndexChanged += new System.EventHandler(this.chLCurveChoise_SelectedIndexChanged);
            // 
            // lblcurveChoise
            // 
            this.lblcurveChoise.AutoSize = true;
            this.lblcurveChoise.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblcurveChoise.Location = new System.Drawing.Point(820, 14);
            this.lblcurveChoise.Name = "lblcurveChoise";
            this.lblcurveChoise.Size = new System.Drawing.Size(121, 20);
            this.lblcurveChoise.TabIndex = 2;
            this.lblcurveChoise.Text = "Curve of Choise";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(820, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "PointAmount: 6";
            // 
            // btnPointIncrease
            // 
            this.btnPointIncrease.Location = new System.Drawing.Point(821, 127);
            this.btnPointIncrease.Name = "btnPointIncrease";
            this.btnPointIncrease.Size = new System.Drawing.Size(59, 27);
            this.btnPointIncrease.TabIndex = 4;
            this.btnPointIncrease.Text = "Increase";
            this.btnPointIncrease.UseVisualStyleBackColor = true;
            this.btnPointIncrease.Click += new System.EventHandler(this.btnPointIncrease_Click);
            // 
            // btnPointDecrease
            // 
            this.btnPointDecrease.Location = new System.Drawing.Point(886, 127);
            this.btnPointDecrease.Name = "btnPointDecrease";
            this.btnPointDecrease.Size = new System.Drawing.Size(69, 27);
            this.btnPointDecrease.TabIndex = 5;
            this.btnPointDecrease.Text = "Decrease";
            this.btnPointDecrease.UseVisualStyleBackColor = true;
            this.btnPointDecrease.Click += new System.EventHandler(this.btnPointDecrease_Click);
            // 
            // knotControl1
            // 
            this.knotControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.knotControl1.Location = new System.Drawing.Point(12, 458);
            this.knotControl1.Name = "knotControl1";
            this.knotControl1.Size = new System.Drawing.Size(946, 160);
            this.knotControl1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(970, 630);
            this.Controls.Add(this.btnPointDecrease);
            this.Controls.Add(this.btnPointIncrease);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblcurveChoise);
            this.Controls.Add(this.chLCurveChoise);
            this.Controls.Add(this.knotControl1);
            this.Name = "MainForm";
            this.Text = "LiniarCurve";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private KnotControl knotControl1;
        private System.Windows.Forms.ComboBox chLCurveChoise;
        private System.Windows.Forms.Label lblcurveChoise;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnPointIncrease;
        private System.Windows.Forms.Button btnPointDecrease;
    }
}

