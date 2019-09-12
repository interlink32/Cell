namespace user_service
{
    partial class form
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
            this.lbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.lbl.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl.Location = new System.Drawing.Point(0, 0);
            this.lbl.Name = "label1";
            this.lbl.Size = new System.Drawing.Size(126, 275);
            this.lbl.TabIndex = 0;
            this.lbl.Text = "label1";
            // 
            // form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 275);
            this.Controls.Add(this.lbl);
            this.Name = "form";
            this.Text = "user_service";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl;
    }
}

