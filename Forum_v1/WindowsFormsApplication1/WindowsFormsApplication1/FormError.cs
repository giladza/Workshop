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
    public partial class FormError : Form
    {
        public FormError(String text)
        {
            InitializeComponent();
            textBox1.Text += text;
        }

        private void FormError_Load(object sender, EventArgs e)
        {

        }
    }
}
