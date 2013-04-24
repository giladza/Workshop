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
    public partial class Form6 : Form
    {

        string body;
        string title;

        private Form2 mainForm;
        public Form6(Form2 mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        { //Back Button
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {//submit button

            if (textBox1.Text != "" || textBox2.Text != "")
            {
                body = textBox1.Text;
                title = textBox2.Text;

                this.mainForm.Send_New_thread(body,title);
                this.Close();
            }
            else
            {
                MessageBox.Show("Please Enter thread");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {//clear button

            this.textBox1.ResetText();
            this.textBox2.ResetText();
        }

        
    }
}
