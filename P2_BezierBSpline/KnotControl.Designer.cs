namespace P2_BezierBSpline
{
    partial class KnotControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnForceUniform = new System.Windows.Forms.Button();
            this.lblDegree = new System.Windows.Forms.Label();
            this.btnDegreeIncrease = new System.Windows.Forms.Button();
            this.lblDegreeDecrease = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnForceUniform
            // 
            this.btnForceUniform.Location = new System.Drawing.Point(689, 3);
            this.btnForceUniform.Name = "btnForceUniform";
            this.btnForceUniform.Size = new System.Drawing.Size(101, 23);
            this.btnForceUniform.TabIndex = 1;
            this.btnForceUniform.Text = "Force Uniform";
            this.btnForceUniform.UseVisualStyleBackColor = true;
            this.btnForceUniform.Click += new System.EventHandler(this.btnForceUniform_Click);
            // 
            // lblDegree
            // 
            this.lblDegree.AutoSize = true;
            this.lblDegree.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblDegree.Location = new System.Drawing.Point(36, 3);
            this.lblDegree.Name = "lblDegree";
            this.lblDegree.Size = new System.Drawing.Size(79, 20);
            this.lblDegree.TabIndex = 2;
            this.lblDegree.Text = "Degree: 3";
            // 
            // btnDegreeIncrease
            // 
            this.btnDegreeIncrease.Location = new System.Drawing.Point(121, 3);
            this.btnDegreeIncrease.Name = "btnDegreeIncrease";
            this.btnDegreeIncrease.Size = new System.Drawing.Size(75, 23);
            this.btnDegreeIncrease.TabIndex = 3;
            this.btnDegreeIncrease.Text = "Increase";
            this.btnDegreeIncrease.UseVisualStyleBackColor = true;
            this.btnDegreeIncrease.Click += new System.EventHandler(this.btnDegreeIncrease_Click);
            // 
            // lblDegreeDecrease
            // 
            this.lblDegreeDecrease.Location = new System.Drawing.Point(202, 3);
            this.lblDegreeDecrease.Name = "lblDegreeDecrease";
            this.lblDegreeDecrease.Size = new System.Drawing.Size(75, 23);
            this.lblDegreeDecrease.TabIndex = 4;
            this.lblDegreeDecrease.Text = "Decrease";
            this.lblDegreeDecrease.UseVisualStyleBackColor = true;
            this.lblDegreeDecrease.Click += new System.EventHandler(this.lblDegreeDecrease_Click);
            // 
            // KnotControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblDegreeDecrease);
            this.Controls.Add(this.btnDegreeIncrease);
            this.Controls.Add(this.lblDegree);
            this.Controls.Add(this.btnForceUniform);
            this.Name = "KnotControl";
            this.Size = new System.Drawing.Size(793, 233);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnForceUniform;
        private System.Windows.Forms.Label lblDegree;
        private System.Windows.Forms.Button btnDegreeIncrease;
        private System.Windows.Forms.Button lblDegreeDecrease;
    }
}
