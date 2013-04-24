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
    public partial class FormRegister : Form
    {
        ForumService forumService;

        public FormRegister(ForumService fs)
        {
            InitializeComponent();
            forumService = fs;
        }

        private void button1_Click(object sender, EventArgs e)//register
        {
            if (String.IsNullOrEmpty(maskedTextBox1.Text) || String.IsNullOrEmpty(maskedTextBox2.Text) || String.IsNullOrEmpty(maskedTextBox3.Text))
                new FormError("One of the fields is empty!").Show();
            else
            {
                if (!forumService.register(maskedTextBox1.Text, maskedTextBox2.Text, maskedTextBox3.Text))
                    new FormError("Invalid input text").Show();
                else
                {
                    forumService.login(maskedTextBox1.Text, maskedTextBox2.Text);
                    this.Dispose();
                }
            }
        }

        private void FormRegister_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
