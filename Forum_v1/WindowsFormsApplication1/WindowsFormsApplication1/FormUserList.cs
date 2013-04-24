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
    public partial class FormUserList : Form
    {

        List<string> items = new List<string>();
        String[] users;
        FormMain parent;

        public FormUserList(FormMain pt)
        {
            InitializeComponent();

            parent = pt;
            users = parent.forumService.getUserList();
            listBox1.DataSource = users;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int r = listBox1.SelectedIndex;
            parent.forumService.promote(users[r]);
        }
    }
}
