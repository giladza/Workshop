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
    public partial class Form3 : Form
    {

        private Boolean Is_Connecting = false; //this boolean Var determines wheather we are
                                                // waiting for answer from the server
                                                //if we didn"t get any answer we yet we block the go back
        private Boolean GotAnswer = false; // true if we got any answer for the server.
        private Boolean Answer = false; // if we got answer we need to check if it is True or false
        private int moveRate = 1;
        private Form2 MainForm;
        string user;
        string password;

        public Form3(Form2 form)
        {
            MainForm = form;
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.label7.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //this is the submit button. we need to gether the info and send it to the Sever.

            //we check if no one of the fields is empty
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Please fill all the fields");
            }
              else
              {
                // here we send the info to the server 
                // here we start the timer
                  MainForm.EnterLogin(textBox1.Text, textBox2.Text);
                  this.Hide();
                  this.Is_Connecting = true;
                  user = textBox1.Text;
                  password = textBox2.Text;
                  timer1.Start();
                  this.label7.Show();
              }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.textBox1.ResetText();
            this.textBox2.ResetText();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Is_Connecting == false)
            {
                this.timer1.Stop();
                this.Hide();
                this.MainForm.Show();
                this.Close();
            }
            else
              {
                  MessageBox.Show("Please Wait for answer from the sever");
              }
   
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(+moveRate);

            if (GotAnswer == true)
            {
                if (Answer == true)
                {
                    this.timer1.Stop();
                    this.Hide();
                    MessageBox.Show("Connected Successfuly");
                    this.MainForm.Show();
                    this.Close();
                }
                else
                {
                    timer1.Stop();
                    MessageBox.Show("Login Failed Please Try again");
                    GotAnswer = false;
                    Answer = false;
                    moveRate = 1;
                    this.textBox1.ResetText();
                    this.textBox2.ResetText();
                    label7.Hide();
                    
                    this.Is_Connecting = false;
                    progressBar1.Value = 0;
                    this.Close();
                }


            }


            if (progressBar1.Value == 99)
            {
                label7.Hide();


            }

        }

        public void UpdateRegisterResult(Boolean answer)
        {
            GotAnswer = true;
            Answer = answer;
        }
    }
}
