namespace WindowsFormsApplication1
{
    partial class FormMain
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SubForumPanel = new System.Windows.Forms.Panel();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.ThreadPanel = new System.Windows.Forms.Panel();
            this.button8 = new System.Windows.Forms.Button();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.MainForumPanel = new System.Windows.Forms.Panel();
            this.button7 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.SubForumPanel.SuspendLayout();
            this.ThreadPanel.SuspendLayout();
            this.MainForumPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(476, 77);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 76);
            this.button1.TabIndex = 0;
            this.button1.Text = "Login";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(476, 159);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(89, 76);
            this.button2.TabIndex = 1;
            this.button2.Text = "Log Out";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(476, 324);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(89, 76);
            this.button3.TabIndex = 2;
            this.button3.Text = "Back to Forum";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // SubForumPanel
            // 
            this.SubForumPanel.BackColor = System.Drawing.SystemColors.Window;
            this.SubForumPanel.Controls.Add(this.button6);
            this.SubForumPanel.Controls.Add(this.button5);
            this.SubForumPanel.Controls.Add(this.listBox2);
            this.SubForumPanel.Location = new System.Drawing.Point(8, 6);
            this.SubForumPanel.Name = "SubForumPanel";
            this.SubForumPanel.Size = new System.Drawing.Size(455, 388);
            this.SubForumPanel.TabIndex = 3;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(376, 147);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 76);
            this.button6.TabIndex = 4;
            this.button6.Text = "Create new Thread";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(376, 65);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 76);
            this.button5.TabIndex = 3;
            this.button5.Text = "Enter Thread";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // listBox2
            // 
            this.listBox2.BackColor = System.Drawing.SystemColors.MenuBar;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(26, 19);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(344, 329);
            this.listBox2.TabIndex = 2;
            // 
            // ThreadPanel
            // 
            this.ThreadPanel.BackColor = System.Drawing.SystemColors.Window;
            this.ThreadPanel.Controls.Add(this.button8);
            this.ThreadPanel.Controls.Add(this.listBox3);
            this.ThreadPanel.Location = new System.Drawing.Point(4, 9);
            this.ThreadPanel.Name = "ThreadPanel";
            this.ThreadPanel.Size = new System.Drawing.Size(455, 388);
            this.ThreadPanel.TabIndex = 5;
            this.ThreadPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.ThreadPanel_Paint);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(376, 65);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 76);
            this.button8.TabIndex = 5;
            this.button8.Text = "Post Reply";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // listBox3
            // 
            this.listBox3.BackColor = System.Drawing.SystemColors.MenuBar;
            this.listBox3.FormattingEnabled = true;
            this.listBox3.Location = new System.Drawing.Point(26, 19);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(344, 329);
            this.listBox3.TabIndex = 5;
            // 
            // MainForumPanel
            // 
            this.MainForumPanel.BackColor = System.Drawing.SystemColors.Window;
            this.MainForumPanel.Controls.Add(this.button7);
            this.MainForumPanel.Controls.Add(this.button4);
            this.MainForumPanel.Controls.Add(this.listBox1);
            this.MainForumPanel.Location = new System.Drawing.Point(12, 3);
            this.MainForumPanel.Name = "MainForumPanel";
            this.MainForumPanel.Size = new System.Drawing.Size(455, 388);
            this.MainForumPanel.TabIndex = 4;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(376, 147);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 76);
            this.button7.TabIndex = 2;
            this.button7.Text = "Create new Sub-Forum";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(376, 65);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 76);
            this.button4.TabIndex = 1;
            this.button4.Text = "Enter Sub-Forum";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(26, 19);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(344, 329);
            this.listBox1.TabIndex = 0;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(476, 241);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(89, 76);
            this.button9.TabIndex = 6;
            this.button9.Text = "Register";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(476, 12);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(89, 59);
            this.button10.TabIndex = 7;
            this.button10.Text = "User List";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 412);
            this.Controls.Add(this.SubForumPanel);
            this.Controls.Add(this.ThreadPanel);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.MainForumPanel);
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.SubForumPanel.ResumeLayout(false);
            this.ThreadPanel.ResumeLayout(false);
            this.MainForumPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel SubForumPanel;
        private System.Windows.Forms.Panel MainForumPanel;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Panel ThreadPanel;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.ListBox listBox3;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
    }
}