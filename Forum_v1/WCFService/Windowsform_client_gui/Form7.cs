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
    public partial class Form7 : Form
    {
        private Form2 MainForm;
        public Form7(Form2 mainForm)
        {
            MainForm = mainForm;
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {//back
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {//submit
            string msg = textBox1.Text;
            this.MainForm.Send_New_subject(msg);
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {//clear
            this.textBox1.ResetText();
        }
    }
}
