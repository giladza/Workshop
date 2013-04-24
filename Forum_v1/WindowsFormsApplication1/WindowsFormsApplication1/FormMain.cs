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
    public partial class FormMain : Form
    {
        public ForumService forumService;
        List<string> items = new List<string>();
        public FormMain()
        {
            InitializeComponent();
            MainForumPanel.Show();
            SubForumPanel.Hide();
            ThreadPanel.Hide();
            forumService = new ForumService();

            listBox1.DataSource = forumService.getSubForumList();
        }

        private void button1_Click(object sender, EventArgs e)//login
        {
            //MessageBox.Show("bla");
            new FormLogin(this).Show();
        }

        private void button2_Click(object sender, EventArgs e)//logout
        {
            forumService.logout();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)//return to main forum
        {
            SubForumPanel.Hide();
            ThreadPanel.Hide();
            listBox1.DataSource = null;
            listBox1.DataSource = forumService.getSubForumList();
            MainForumPanel.Show();
        }

        private void button4_Click(object sender, EventArgs e)//enter sub forum
        {
            int r = listBox1.SelectedIndex;
            listBox2.DataSource = null;
            listBox2.DataSource = forumService.getThreadList(r);
            MainForumPanel.Hide();
            SubForumPanel.Show();
        }


        private void button5_Click(object sender, EventArgs e)//enter thread
        {
            int r = listBox2.SelectedIndex;
            listBox3.DataSource = null;
            listBox3.DataSource = forumService.getPostList(r);
            SubForumPanel.Hide();
            ThreadPanel.Show();
        }

        private void button8_Click(object sender, EventArgs e)//post a reply to a thread
        {
            new FormPostReply(this).Show();
        }

        private void button6_Click(object sender, EventArgs e)//create new thread
        {
            new FormNewThread(this).Show();
        }

        private void button7_Click(object sender, EventArgs e)//create new sub forum
        {
            new FormNewSub(this).Show();
        }

        


        private void button10_Click(object sender, EventArgs e)//user list
        {
            new FormUserList(this).Show();
        }

        private void ThreadPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
