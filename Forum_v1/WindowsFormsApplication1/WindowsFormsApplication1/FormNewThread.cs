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
    public partial class FormNewThread : Form
    {
        FormMain parent;
        public FormNewThread(FormMain pt)
        {
            InitializeComponent();
            parent = pt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(maskedTextBox1.Text))
                new FormError("One of the fields is empty!").Show();
            else
            {
                if (!parent.forumService.newThread(textBox1.Text, maskedTextBox1.Text))
                    new FormError("Invalid input text").Show();
                else
                    this.Dispose();
            }
        }
    }
}
