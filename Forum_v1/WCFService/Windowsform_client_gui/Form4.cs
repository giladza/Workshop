using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form4 : Form
    {
        string messagebody;
        string messageTitle;
        Form2 mainform;

        public Form4(Form2 form)
        {
            this.mainform = form;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.messagebody = this.textBox1.Text;
            this.messageTitle = this.textBox2.Text;
            this.mainform.addpost(this.messageTitle, this.messagebody);
            this.mainform = null;
            this.Close();
            //inovoke this message in some object
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            //this.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.textBox2.ResetText();
            this.textBox1.ResetText();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close() ;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
