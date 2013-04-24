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
    public partial class RegFrm : Form
    {
        private Boolean Is_Connecting = false; //this boolean Var determines wheather we are
        // waiting for answer from the server
        //if we didn"t get any answer we yet we block the go back
        private Boolean GotAnswer = false; // true if we got any answer for the server.
        private Boolean Answer = false; // if we got answer we need to check if it is True or false
        private int moveRate = 1;
        private Form2 RunForm;
        private int counter = 0;

     
       // private int counter = 1;

        public RegFrm()
        {
            InitializeComponent();
        }

        private void RegFrm_Load(object sender, EventArgs e)
        {
            label7.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string FstN, UserN, Email, Password1, Password2;
            Boolean flag1 = true;

            FstN = textBox1.Text;
            UserN = textBox2.Text;
            Email = textBox3.Text;
            Password1 = textBox4.Text;
            Password2 = textBox5.Text;

            if (FstN == "" || UserN == "" || Email == "" || Password1 == "" || Password2 == "")
            {
                MessageBox.Show("Missing Information Please Re-enter");
                flag1 =false;
            }

            if (Password2 != Password1 && flag1==true)
            {
                flag1 = false;
                MessageBox.Show("You have entered different passwords");
                textBox4.ResetText();
                textBox5.ResetText();
            }

            if (flag1 == true)
            {
                //this.Hide();
                timer1.Start();
                label7.Show();
                RunForm.CheckRegister(FstN, UserN, Email, Password1);
               // Form2 n = new Form2();
              
               
                //MessageBox.Show("nice");
                //Form2 d = new Form2();
               // this.Show();
            }
           //send this information to the server
           //Open Wait button
           //wait for aproval.....

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.ResetText();
            textBox2.ResetText();
            textBox3.ResetText();
            textBox4.ResetText();
            textBox5.ResetText();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // this is a Back Button functionality....will be done later
            this.RunForm.Show();
            this.RunForm = null;
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(+moveRate);


            if (GotAnswer == true)
            {
                if (Answer == true)
                {
                    timer1.Stop();
                    this.RunForm.Show();
                    this.RunForm = null;
                    this.Hide();
                    MessageBox.Show("Connected Successfuly");
                    this.Close();
                }
                else
                {   
                    GotAnswer = false;
                    Answer = false;
                    moveRate = 1;
                    this.textBox1.ResetText();
                    this.textBox2.ResetText();
                    this.textBox3.ResetText();
                    label7.Hide();
                    timer1.Stop();
                    MessageBox.Show("Login Failed Please Try again");
                    progressBar1.Value = 0;
                    this.Close();
                    
                    
                }


            }



            if (progressBar1.Value == 99)
            {
                label7.Hide();
                //here it will check if we got the informetion we need for example akowledge
                MessageBox.Show("we enter to new page");
            
            }
        }

        //here we get instance of the main object
        public void UpdateForm(Form2 runForm)
        {
            this.RunForm = runForm;
        }

        public void UpdateRegisterResult(Boolean answer)
        {
            GotAnswer = true;
            Answer = answer;
        }
    }
}
