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
            this.lblPointAmount = new System.Windows.Forms.Label();
            this.btnPointIncrease = new System.Windows.Forms.Button();
            this.btnPointDecrease = new System.Windows.Forms.Button();
            this.knotControl1 = new P2_BezierBSpline.KnotControl();
            this.SuspendLayout();
            // 
            // chLCurveChoise
            // 
            this.chLCurveChoise.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.chLCurveChoise.FormattingEnabled = true;
            this.chLCurveChoise.Items.AddRange(new object[] {
            "Bezier (Castillieau)",
            "Bezier (Math)",
            "BSpline"});
            this.chLCurveChoise.Location = new System.Drawing.Point(770, 37);
            this.chLCurveChoise.Name = "chLCurveChoise";
            this.chLCurveChoise.Size = new System.Drawing.Size(137, 24);
            this.chLCurveChoise.TabIndex = 1;
            this.chLCurveChoise.SelectedIndexChanged += new System.EventHandler(this.chLCurveChoise_SelectedIndexChanged);
            // 
            // lblcurveChoise
            // 
            this.lblcurveChoise.AutoSize = true;
            this.lblcurveChoise.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblcurveChoise.Location = new System.Drawing.Point(766, 14);
            this.lblcurveChoise.Name = "lblcurveChoise";
            this.lblcurveChoise.Size = new System.Drawing.Size(121, 20);
            this.lblcurveChoise.TabIndex = 2;
            this.lblcurveChoise.Text = "Curve of Choise";
            // 
            // lblPointAmount
            // 
            this.lblPointAmount.AutoSize = true;
            this.lblPointAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblPointAmount.Location = new System.Drawing.Point(766, 104);
            this.lblPointAmount.Name = "lblPointAmount";
            this.lblPointAmount.Size = new System.Drawing.Size(175, 20);
            this.lblPointAmount.TabIndex = 3;
            this.lblPointAmount.Text = "PointAmount/Degree: 6";
            // 
            // btnPointIncrease
            // 
            this.btnPointIncrease.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnPointIncrease.Location = new System.Drawing.Point(770, 127);
            this.btnPointIncrease.Name = "btnPointIncrease";
            this.btnPointIncrease.Size = new System.Drawing.Size(72, 27);
            this.btnPointIncrease.TabIndex = 4;
            this.btnPointIncrease.Text = "Increase";
            this.btnPointIncrease.UseVisualStyleBackColor = true;
            this.btnPointIncrease.Click += new System.EventHandler(this.btnPointIncrease_Click);
            // 
            // btnPointDecrease
            // 
            this.btnPointDecrease.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.btnPointDecrease.Location = new System.Drawing.Point(848, 127);
            this.btnPointDecrease.Name = "btnPointDecrease";
            this.btnPointDecrease.Size = new System.Drawing.Size(77, 27);
            this.btnPointDecrease.TabIndex = 5;
            this.btnPointDecrease.Text = "Decrease";
            this.btnPointDecrease.UseVisualStyleBackColor = true;
            this.btnPointDecrease.Click += new System.EventHandler(this.btnPointDecrease_Click);
            // 
            // knotControl1
            // 
            this.knotControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.knotControl1.Location = new System.Drawing.Point(0, 430);
            this.knotControl1.Name = "knotControl1";
            this.knotControl1.Size = new System.Drawing.Size(970, 200);
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
            this.Controls.Add(this.lblPointAmount);
            this.Controls.Add(this.lblcurveChoise);
            this.Controls.Add(this.chLCurveChoise);
            this.Controls.Add(this.knotControl1);
            this.Name = "MainForm";
            this.Text = "BezierBSpline";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private KnotControl knotControl1;
        private System.Windows.Forms.ComboBox chLCurveChoise;
        private System.Windows.Forms.Label lblcurveChoise;
        private System.Windows.Forms.Label lblPointAmount;
        private System.Windows.Forms.Button btnPointIncrease;
        private System.Windows.Forms.Button btnPointDecrease;
    }
}

