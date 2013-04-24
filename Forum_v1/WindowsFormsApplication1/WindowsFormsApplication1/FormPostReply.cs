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
    public partial class FormPostReply : Form
    {
        FormMain parent;
        public FormPostReply(FormMain pt)
        {
            InitializeComponent();
            parent = pt;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)//post
        {
            if (String.IsNullOrEmpty(textBox1.Text))
                new FormError("the reply field is empty!").Show();
            else
            {
                if (!parent.forumService.postReply(textBox1.Text))
                    new FormError("Invalid input text").Show();
                else
                    this.Dispose();
            }
        }
    }
}
