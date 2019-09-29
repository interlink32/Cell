namespace user
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_user = new System.Windows.Forms.TextBox();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lst = new System.Windows.Forms.ListBox();
            this.btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(10);
            this.label1.Size = new System.Drawing.Size(76, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "user name";
            // 
            // txt_user
            // 
            this.txt_user.Dock = System.Windows.Forms.DockStyle.Top;
            this.txt_user.Location = new System.Drawing.Point(0, 33);
            this.txt_user.Name = "txt_user";
            this.txt_user.Size = new System.Drawing.Size(284, 20);
            this.txt_user.TabIndex = 1;
            // 
            // txt_password
            // 
            this.txt_password.Dock = System.Windows.Forms.DockStyle.Top;
            this.txt_password.Location = new System.Drawing.Point(0, 86);
            this.txt_password.Name = "txt_password";
            this.txt_password.Size = new System.Drawing.Size(284, 20);
            this.txt_password.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 53);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(10);
            this.label2.Size = new System.Drawing.Size(72, 33);
            this.label2.TabIndex = 2;
            this.label2.Text = "password";
            // 
            // lst
            // 
            this.lst.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lst.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lst.FormattingEnabled = true;
            this.lst.Location = new System.Drawing.Point(0, 187);
            this.lst.Name = "lst";
            this.lst.Size = new System.Drawing.Size(284, 171);
            this.lst.TabIndex = 4;
            // 
            // btn
            // 
            this.btn.Location = new System.Drawing.Point(63, 130);
            this.btn.Name = "btn";
            this.btn.Size = new System.Drawing.Size(158, 35);
            this.btn.TabIndex = 7;
            this.btn.UseVisualStyleBackColor = true;
            this.btn.Click += new System.EventHandler(this.Btn_login_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 358);
            this.Controls.Add(this.btn);
            this.Controls.Add(this.lst);
            this.Controls.Add(this.txt_password);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_user);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "User";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_user;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lst;
        private System.Windows.Forms.Button btn;
    }
}

