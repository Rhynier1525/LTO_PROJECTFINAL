namespace LTO_Project
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel4 = new Panel();
            panel5 = new Panel();
            loginbtn = new Button();
            label1 = new Label();
            passwordtxtbox = new TextBox();
            label3 = new Label();
            label2 = new Label();
            usernametxtbox = new TextBox();
            panel1 = new Panel();
            SuspendLayout();
            // 
            // panel4
            // 
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(686, 133);
            panel4.TabIndex = 3;
            // 
            // panel5
            // 
            panel5.Dock = DockStyle.Bottom;
            panel5.Location = new Point(0, 448);
            panel5.Name = "panel5";
            panel5.Size = new Size(686, 141);
            panel5.TabIndex = 4;
            // 
            // loginbtn
            // 
            loginbtn.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            loginbtn.Font = new Font("Century Gothic", 13.8F, FontStyle.Bold);
            loginbtn.Location = new Point(220, 371);
            loginbtn.Name = "loginbtn";
            loginbtn.Size = new Size(246, 59);
            loginbtn.TabIndex = 13;
            loginbtn.Text = "LOGIN";
            loginbtn.UseVisualStyleBackColor = true;
            loginbtn.Click += loginbtn_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(270, 151);
            label1.Name = "label1";
            label1.Size = new Size(153, 27);
            label1.TabIndex = 10;
            label1.Text = "LOGIN FORM";
            // 
            // passwordtxtbox
            // 
            passwordtxtbox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            passwordtxtbox.Location = new Point(220, 307);
            passwordtxtbox.Multiline = true;
            passwordtxtbox.Name = "passwordtxtbox";
            passwordtxtbox.PlaceholderText = "Enter Password";
            passwordtxtbox.Size = new Size(246, 34);
            passwordtxtbox.TabIndex = 9;
            passwordtxtbox.TextChanged += passwordtxtbox_TextChanged;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(220, 276);
            label3.Name = "label3";
            label3.Size = new Size(75, 18);
            label3.TabIndex = 12;
            label3.Text = "Password";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(220, 204);
            label2.Name = "label2";
            label2.Size = new Size(82, 18);
            label2.TabIndex = 11;
            label2.Text = "Username";
            // 
            // usernametxtbox
            // 
            usernametxtbox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            usernametxtbox.Location = new Point(220, 234);
            usernametxtbox.Multiline = true;
            usernametxtbox.Name = "usernametxtbox";
            usernametxtbox.PlaceholderText = "Enter Username";
            usernametxtbox.Size = new Size(246, 34);
            usernametxtbox.TabIndex = 8;
            usernametxtbox.TextChanged += usernametxtbox_TextChanged;
            // 
            // panel1
            // 
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(686, 589);
            panel1.TabIndex = 14;
            panel1.Paint += panel1_Paint;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(686, 589);
            Controls.Add(loginbtn);
            Controls.Add(label1);
            Controls.Add(passwordtxtbox);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(usernametxtbox);
            Controls.Add(panel5);
            Controls.Add(panel4);
            Controls.Add(panel1);
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LOGIN FORM";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel panel4;
        private Panel panel5;
        private Button loginbtn;
        private Label label1;
        private TextBox passwordtxtbox;
        private Label label3;
        private Label label2;
        private TextBox usernametxtbox;
        private Panel panel1;
    }
}
