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
    public partial class FormLogin : Form
    {
        FormMain parent;
        internal FormLogin(FormMain pt)
        {
            InitializeComponent();
            parent = pt;
        }

        private void button2_Click(object sender, EventArgs e)//close
        {
            this.Dispose();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            //new FormError("bla").Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("bla");
            //new FormError("bla").Show();
            if (String.IsNullOrEmpty(maskedTextBox1.Text) || String.IsNullOrEmpty(maskedTextBox2.Text))
                new FormError("One of the fields is empty!----").Show();
            else
            {
                if (!parent.forumService.login(maskedTextBox1.Text, maskedTextBox2.Text))
                    new FormError("Invalid input text").Show();
                else
                    this.Dispose();
            }

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
