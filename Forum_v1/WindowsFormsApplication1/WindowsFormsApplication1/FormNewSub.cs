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
    public partial class FormNewSub : Form
    {
        FormMain parent;
        public FormNewSub(FormMain pt)
        {
            InitializeComponent();
            parent = pt;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(maskedTextBox1.Text) || String.IsNullOrEmpty(textBox1.Text))
                new FormError("One of the fields is empty!").Show();
            else
            {
                if (!parent.forumService.newSub(maskedTextBox1.Text, textBox1.Text))
                    new FormError("Invalid input text").Show();
                else
                    this.Dispose();
            }
        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

    }
}
