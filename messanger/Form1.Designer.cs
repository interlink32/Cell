namespace messanger
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
            this.lbl_me = new System.Windows.Forms.Label();
            this.txt_id = new System.Windows.Forms.TextBox();
            this.txt_partner = new System.Windows.Forms.TextBox();
            this.lbl_partner = new System.Windows.Forms.Label();
            this.txt_chat = new System.Windows.Forms.RichTextBox();
            this.txt_send = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbl_me
            // 
            this.lbl_me.AutoSize = true;
            this.lbl_me.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_me.Location = new System.Drawing.Point(0, 0);
            this.lbl_me.Name = "lbl_me";
            this.lbl_me.Size = new System.Drawing.Size(31, 13);
            this.lbl_me.TabIndex = 1;
            this.lbl_me.Text = "my id";
            // 
            // txt_id
            // 
            this.txt_id.Dock = System.Windows.Forms.DockStyle.Top;
            this.txt_id.Location = new System.Drawing.Point(0, 13);
            this.txt_id.Name = "txt_id";
            this.txt_id.Size = new System.Drawing.Size(284, 20);
            this.txt_id.TabIndex = 2;
            // 
            // txt_partner
            // 
            this.txt_partner.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txt_partner.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txt_partner.Dock = System.Windows.Forms.DockStyle.Top;
            this.txt_partner.Location = new System.Drawing.Point(0, 46);
            this.txt_partner.Name = "txt_partner";
            this.txt_partner.Size = new System.Drawing.Size(284, 20);
            this.txt_partner.TabIndex = 4;
            // 
            // lbl_partner
            // 
            this.lbl_partner.AutoSize = true;
            this.lbl_partner.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_partner.Location = new System.Drawing.Point(0, 33);
            this.lbl_partner.Name = "lbl_partner";
            this.lbl_partner.Size = new System.Drawing.Size(51, 13);
            this.lbl_partner.TabIndex = 3;
            this.lbl_partner.Text = "partner id";
            // 
            // txt_chat
            // 
            this.txt_chat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_chat.Dock = System.Windows.Forms.DockStyle.Top;
            this.txt_chat.Location = new System.Drawing.Point(0, 66);
            this.txt_chat.Name = "txt_chat";
            this.txt_chat.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.txt_chat.Size = new System.Drawing.Size(284, 252);
            this.txt_chat.TabIndex = 5;
            this.txt_chat.Text = "";
            // 
            // txt_send
            // 
            this.txt_send.Dock = System.Windows.Forms.DockStyle.Top;
            this.txt_send.Location = new System.Drawing.Point(0, 318);
            this.txt_send.Name = "txt_send";
            this.txt_send.Size = new System.Drawing.Size(284, 20);
            this.txt_send.TabIndex = 6;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox1.Location = new System.Drawing.Point(0, 338);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(284, 20);
            this.textBox1.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 339);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.txt_send);
            this.Controls.Add(this.txt_chat);
            this.Controls.Add(this.txt_partner);
            this.Controls.Add(this.lbl_partner);
            this.Controls.Add(this.txt_id);
            this.Controls.Add(this.lbl_me);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "Messanger";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_me;
        private System.Windows.Forms.TextBox txt_id;
        private System.Windows.Forms.TextBox txt_partner;
        private System.Windows.Forms.Label lbl_partner;
        private System.Windows.Forms.RichTextBox txt_chat;
        private System.Windows.Forms.TextBox txt_send;
        private System.Windows.Forms.TextBox textBox1;
    }
}

