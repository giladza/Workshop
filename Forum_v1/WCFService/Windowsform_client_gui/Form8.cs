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
    public partial class Form8 : Form
    {

        Form2 mainform;

        string title;
        string body;
        int place;

        public Form8(Form2 form)
        {
            this.mainform = form;
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.mainform.Show();
            this.Close();
        }


        public void UpdateTextToEdit(int place, string title, string body)
        {
            this.title = title;
            this.body = body;
            this.textBox1.Text = this.title;
            this.textBox2.Text = this.body;
            this.place = place;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.textBox1.ResetText();
            this.textBox2.ResetText();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            this.body = this.textBox2.Text;
            this.title = this.textBox1.Text;

            this.mainform.Send_Edit_thread_to_server(this.place, this.title, this.body);
            //invoke here message update^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
            this.Hide();
            this.mainform.Show();
            this.Close();

        }
    }
}
