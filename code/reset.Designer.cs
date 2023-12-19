namespace store_management
{
    partial class reset
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
            this.lbluser = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnreset1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtuser1 = new System.Windows.Forms.TextBox();
            this.txtnewp = new System.Windows.Forms.TextBox();
            this.txtcomp = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbluser
            // 
            this.lbluser.AutoSize = true;
            this.lbluser.BackColor = System.Drawing.Color.White;
            this.lbluser.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbluser.ForeColor = System.Drawing.Color.Black;
            this.lbluser.Location = new System.Drawing.Point(12, 133);
            this.lbluser.Name = "lbluser";
            this.lbluser.Size = new System.Drawing.Size(111, 22);
            this.lbluser.TabIndex = 1;
            this.lbluser.Text = "User Name :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(12, 195);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 22);
            this.label1.TabIndex = 2;
            this.label1.Text = "New Password :";
            // 
            // btnreset1
            // 
            this.btnreset1.BackColor = System.Drawing.Color.Black;
            this.btnreset1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnreset1.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnreset1.ForeColor = System.Drawing.Color.White;
            this.btnreset1.Location = new System.Drawing.Point(153, 325);
            this.btnreset1.Name = "btnreset1";
            this.btnreset1.Size = new System.Drawing.Size(105, 29);
            this.btnreset1.TabIndex = 7;
            this.btnreset1.Text = "Reset";
            this.btnreset1.UseVisualStyleBackColor = false;
            this.btnreset1.Click += new System.EventHandler(this.btnreset1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century", 21.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkOrange;
            this.label2.Location = new System.Drawing.Point(91, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(241, 34);
            this.label2.TabIndex = 8;
            this.label2.Text = "Reset Password";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(12, 255);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(173, 22);
            this.label3.TabIndex = 10;
            this.label3.Text = "Confirm Password :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(422, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 24);
            this.label4.TabIndex = 12;
            this.label4.Text = "X";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // txtuser1
            // 
            this.txtuser1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtuser1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtuser1.Location = new System.Drawing.Point(233, 134);
            this.txtuser1.Multiline = true;
            this.txtuser1.Name = "txtuser1";
            this.txtuser1.Size = new System.Drawing.Size(205, 22);
            this.txtuser1.TabIndex = 13;
            // 
            // txtnewp
            // 
            this.txtnewp.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtnewp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnewp.Location = new System.Drawing.Point(233, 193);
            this.txtnewp.Name = "txtnewp";
            this.txtnewp.Size = new System.Drawing.Size(205, 26);
            this.txtnewp.TabIndex = 14;
            this.txtnewp.UseSystemPasswordChar = true;
            this.txtnewp.TextChanged += new System.EventHandler(this.txtnewp_TextChanged);
            // 
            // txtcomp
            // 
            this.txtcomp.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtcomp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcomp.Location = new System.Drawing.Point(233, 253);
            this.txtcomp.Name = "txtcomp";
            this.txtcomp.Size = new System.Drawing.Size(205, 26);
            this.txtcomp.TabIndex = 15;
            this.txtcomp.UseSystemPasswordChar = true;
            // 
            // reset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(459, 366);
            this.Controls.Add(this.txtcomp);
            this.Controls.Add(this.txtnewp);
            this.Controls.Add(this.txtuser1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnreset1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbluser);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "reset";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "reset";
            this.Load += new System.EventHandler(this.reset_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbluser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnreset1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtuser1;
        private System.Windows.Forms.TextBox txtnewp;
        private System.Windows.Forms.TextBox txtcomp;
    }
}