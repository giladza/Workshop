using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsFormsApplication1;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        private int Connection_Type = 0; //{0-NoConnection,1-Guset,2-User,3-Moderator,4-Admin}
        int pushed_now;
        RegFrm RegisterForm;
        Form3 LoginForm;
        Form5 EditForm;
        Form6 AddThreadForm;
        Form7 AddSubjectForm;
        Form4 addpostform;
        Form8 EditThreadForm;

        Gui_proxy proxy;

        Boolean ordenaryRun = true;
        Boolean EditOpen = false;
        string EditText;
        Boolean DeleteThread = false;
        Boolean DeleteSubject = false;
        Boolean DeletePost = false;
        Boolean EditThread = false;

        int counter = 0;

        //***** new info to be used******//
        int ScreenDepth = 0; //{o-subject,1-threads,2-posts}
        string connectionMessage;
        int currentPremission;

        //*****************
        //page place ment info
        Boolean Is_Subject_screen = false; //we are on the main screen
        Boolean Is_Thread_screeen = false; //we are on the thread screen
        Boolean Is_Post_Screen = false;

        //*****************


        string[] titlesThSu = new string[20];
        string[] bodyThSu = new string[20];

        string[] titlepost = new string[20];
        string[] bodypost = new string[20];

        public Form2()
        {
            //here i get the RCRProxy
            this.proxy = new Gui_proxy();//need to be updated with the new form
            this.proxy.updateForm(this);//need to update the newly created form.
      
            InitializeComponent();
        }

               //{0 guest,1-register,2-login}
        public void updateConnection(int connection)
      {
           if (connection == 1)
            {
                this.Connection_Type = 1;
            }else
                if (connection == 2)
                {
                    this.Connection_Type = 2;
                }else
                    if (connection == 3)
                    {
                        this.Connection_Type = 3;
                    }
        }


        //{0 guest,1-register,2-login}
        public bool CallForConnect(int connection)
        {
            bool ans;
            string type;


            if (connection == 0)
            {
                type = "guest";
                this.Connection_Type = 1;
                this.button2.Hide();
                this.proxy.updateConnectionType(type);
            }else
               if (connection == 1)
               {
                   this.Connection_Type = 2;
                   this.button2.Hide();
                   this.proxy.updateConnectionType(this.connectionMessage);
                   //here we will fill the and retrive the register info and sand it
               }else
                   if (connection == 2)
                   {
                       //here we will fill the and retrive the register info and sand it 
                   }

            return this.proxy.Connect();
        }


        public void UpdatePRemmision(int pre)
        {
            this.currentPremission = pre;

            if (pre == 1)
            {
            } 
            else if (pre == 2)
            {
            }
            else if (pre == 3)
            {
                this.Activate_Moderator_Mode();
            }
            else
            if (pre == 4)
                         {
                             this.Activate_Admin_Mode();
                         }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
       
        private void Activate_Guest_Mode()
        {
            this.label7.Text = "Connected as a Guest";
            this.button22.Show();
            this.button23.Show();
            this.button24.Hide();
            this.button25.Hide();
            this.button26.Hide();
            this.button29.Hide();
            this.button28.Hide();
            this.button30.Hide();
            this.button31.Hide();
            this.button32.Hide();
            this.button12.Hide();
            this.button27.Hide();
            this.button33.Hide();
        }

        private void Activate_Connected_Mode()
        {
            this.label7.Text = "Connected as a Registered User";
            this.button22.Hide();
            this.button23.Hide();
            this.button25.Hide();
            this.button26.Hide();
            this.button31.Hide();
            this.button32.Hide();
            this.button30.Show();
            this.button28.Show();
            this.button29.Show();
            this.button24.Show();
            this.button12.Show();
            this.button27.Hide();
            this.button33.Hide();
        }

        private void Activate_Moderator_Mode()
        {
            this.label7.Text = "Connected as a Moderator";
            this.button24.Show();
            this.button29.Show();
            this.button28.Show();
            this.button30.Show();
            this.button25.Hide();
            this.button26.Hide();
            this.button27.Hide();
            this.button33.Hide();
        }

        private void Activate_Admin_Mode()
        {
            this.label7.Text = "Connected as an Admin";
            this.button24.Show();
            this.button25.Show();
            this.button26.Show();
            this.button29.Show();
            this.button28.Show();
            this.button30.Show();
            this.button27.Show();
            this.button33.Show();
        }

        private void Show_Subject_Page()
        {//prepare the page for show
            this.label5.Text = "Forum Subjects";
            this.Showpanel();
            this.button31.Hide();
            this.button32.Hide();
            this.button12.Hide();
            this.Is_Subject_screen = true;
            this.Is_Thread_screeen = false;
        }

        private void Show_Thread_Page()
        {//prepare the page for show
            this.label5.Text = "Forum Threads";

            if (this.Connection_Type == 1)
            {
                this.button31.Hide();
                this.button32.Hide();
            }
            else
            {
                this.button31.Show();
                this.button32.Show();
                this.button12.Show();
            }
            this.Showpanel();
           
            this.Is_Subject_screen = false;
            this.Is_Thread_screeen = true;
        }

        private void Show_post_Page()
        {//prepare the page for show
            this.hidepanel();
            this.button31.Hide();
            this.button32.Hide();
            this.Is_Subject_screen = false;
            this.Is_Thread_screeen = false;

        }

        public void hidepanel()
        {
            panel1.Hide();
        }

        public void Showpanel()
        {
            panel1.Show();
        }


        public void PutPosts()
        {
            //this method will arange the posts in the right place/the main subject too
            //it will also rest all the needed data
        }

        public void Putthreads()
        {
            //this method will arange the posts in the right place/the main subject too
            //it will also rest all the needed data
        }



        public void Show_Those_threadsOrSubjects(int Number)
        {
            for (int i = 1; i < 10; i++)
            {
                if (i <= Number)
                    ShowThisThread(i);

                if (i > Number)
                    HideThisThread(i);
            }
        }

        public void Show_Those_Posts(int Number)
        {
            for (int i = 1; i < 7; i++)
            {
                if (i <= Number)
                    ShowThisPosts(i);

                if (i > Number)
                    HideThisPosts(i);
            }
        }


       // private void 
//************************************************************************//
//********************connection to servers ******************************//

        public void sendRequest_for_newPage(int num_of_page)
        {
            if (ScreenDepth == 0)
            {
                ScreenDepth =1;
                this.Show_Thread_Page();
                this.proxy.Out_call_get_page(num_of_page, ScreenDepth, 9, 0);
            }else
                if (ScreenDepth == 1)
                {
                    ScreenDepth = 2;
                    this.Show_post_Page();
                    this.proxy.Out_call_get_page(num_of_page, ScreenDepth, 6, 0);

                }else
                    if (ScreenDepth == 2)
                    {

                    }
        }

        public void sendRequest_for_back()
        {
            if (ScreenDepth == 0)
            {
                //do nothing-or later refresh the page
            }
            else
                if (ScreenDepth == 1)
                {
                    ScreenDepth = 0;
                    this.proxy.updateScreenDepth(0);
                    this.Show_Subject_Page();
                    this.proxy.Out_call_back();
                }else
                    if (ScreenDepth == 2)
                    {
                        ScreenDepth = 1;
                        //this.proxy.updatePageSize(9);
                        //this.proxy.updatePageNumber(0);
                        this.proxy.updateScreenDepth(1);
                        this.Show_Thread_Page();
                        this.proxy.Out_call_back();

                    }
           
        }


//*************************************************************************
//*************************************************************************

        private void Form2_Load(object sender, EventArgs e)
        {
                  Boolean ans;
           ans = CallForConnect(0);//first we connect as a guests
            if (ans == true)
            {
              
                Activate_Guest_Mode();
            }
            else
            {
                MessageBox.Show("WE have connection problem-<as a guest -Debugging>");
            }

            proxy.updateScreenDepth(0);
            //proxy.Out_call_back();
           // proxy.Call("back");
        }

       
   
        public void SubForumsPushForum(int place, string title,string body, string message)
        {
            this.titlesThSu[place] = title;
            this.bodyThSu[place] = body;
            EnterTextToSubjectOrThread(message, place);
        }

        public void PostsPushForum(int place, string title, string body,string message)
        {
            this.titlepost[place] = title;
            this.bodypost[place] = body;
            EnterTextToPost(message, place);
        }

            
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //panel1.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        //***************************Login**********************************************************//
        private void button23_Click(object sender, EventArgs e)
        {//login button
            LoginForm = new Form3(this);
            this.Hide();
            LoginForm.Show();
        }

        public void EnterLogin(string userName, string Password)
        {
            //RegisterResult(false);

            this.connectionMessage = "login " + userName + " " + Password;

            this.proxy.Out_call_LogOut();

            this.proxy = new Gui_proxy();//need to be updated with the new form
            this.proxy.updateForm(this);//need to update the newly created form.

            this.proxy.updateTryLogin();

            CallForConnect(1);
        }

        public void GotLoginConnectin(Boolean ans)
        {
            if (ans == true)
            {
                RegisterLogin(ans);
                this.Show();
                proxy.updateScreenDepth(0);
                this.ScreenDepth = 0;


                if (this.currentPremission == 4)
                {
                    this.Activate_Admin_Mode();
                }
                else
                    if (this.currentPremission == 3)
                    {
                        this.Activate_Moderator_Mode();
                    }
                    else
                    {
                        this.Activate_Connected_Mode();
                    }


                this.Show_Subject_Page();
               

                //run the forum as loged in

            }
            else
                if (ans == false)
                {
                    RegisterLogin(false);

                    this.proxy = new Gui_proxy();//need to be updated with the new form
                    this.proxy.updateForm(this);//need to update the newly created form.

                    //run the forum as a guest
                    proxy.updateScreenDepth(0);
                    this.ScreenDepth = 0;
                    CallForConnect(0);
                    this.Activate_Guest_Mode();
                    this.Show_Subject_Page();

                }

        }

        public void RegisterLogin(Boolean result)
        {
            LoginForm.UpdateRegisterResult(result);


        }


        //******************************************************************************************



      //***************************************** REGISTER CONNECTED ACTIONs ************************************
        private void button22_Click(object sender, EventArgs e)
        {//register button
            RegisterForm = new RegFrm();
            RegisterForm.UpdateForm(this);
            this.Hide();
            RegisterForm.Show();
        }

        //************************* Register deal***************************************************
        public void CheckRegister(string name, string userName, string Email, string Password)
        {
            this.connectionMessage = "register " + name + " john " + userName + " " + Password + " " + Email;
            
            this.proxy.Out_call_LogOut();
          

            this.proxy = new Gui_proxy();//need to be updated with the new form
            this.proxy.updateForm(this);//need to update the newly created form.

            this.proxy.uppdateTryRegister_login();
            CallForConnect(1);
            
        }

        public void GotRegisterConnectin(Boolean ans)
        {
            if (ans == true)
            {
                RegisterResult(ans);
                this.Show();
                proxy.updateScreenDepth(0);
                this.ScreenDepth = 0;
                this.Activate_Connected_Mode();
                this.Show_Subject_Page();
                //run the forum as loged in

            }else
                if (ans == false)
                {
                    RegisterResult(ans);

                    this.proxy = new Gui_proxy();//need to be updated with the new form
                    this.proxy.updateForm(this);//need to update the newly created form.
                    //run the forum as a guest
                    proxy.updateScreenDepth(0);
                    this.ScreenDepth = 0;
                    CallForConnect(0);
                    this.Activate_Guest_Mode();
                    this.Show_Subject_Page();

                }

        }
        public void RegisterResult(Boolean result)
        {
            RegisterForm.UpdateRegisterResult(result);
        }

        //**************************************LOGUT************************************

        private void button24_Click(object sender, EventArgs e)
        {//This is Disconnect button 

            this.proxy.Out_call_LogOut();


            this.proxy = new Gui_proxy();//need to be updated with the new for
            this.proxy.updateForm(this);//need to update the newly created form.
            proxy.updateScreenDepth(0);
            this.ScreenDepth = 0;

            CallForConnect(0);
            //here we send a disconnect message ^^^^^^^^^^^^^^^^^^^^^^^^^^^ IMPLEMENT NEEDED
            this.Activate_Guest_Mode();
            this.Show_Subject_Page();
        }


       //********************************************************************************************************
     

        private void label4_Click(object sender, EventArgs e)
        {


        }

        private void button28_Click(object sender, EventArgs e)
        {
         
                    MessageBox.Show("please choose the thread to delete");
                    DeletePost = true;
        
        }

        private void button29_Click(object sender, EventArgs e)
        {
            this.addpostform = new Form4(this);
            this.addpostform.Show();
        }

      



//*****************************************************************
        //**********************************************************
        public void ShowThisThread(int i)
        {
            if (i == 1)
            {
                this.button9.Show();
            }

            if (i == 2)
            {
                this.button10.Show();
            }

            if (i == 3)
            {
                this.button11.Show();
            }

            if (i == 4)
            {
                this.button14.Show();
            }

            if (i == 5)
            {
                this.button15.Show();
            }

            if (i == 6)
            {
                this.button16.Show();
            }
            if (i == 7)
            {
                this.button17.Show();
            }

            if (i == 8)
            {
                this.button18.Show();
            }

            if (i == 9)
            {
                this.button19.Show();
            }

        }

        public void HideThisThread(int i)
        {
            if (i == 1)
            {
                this.button9.Hide();
            }

            if (i == 2)
            {
                this.button10.Hide();
            }

            if (i == 3)
            {
                this.button11.Hide();
            }

            if (i == 4)
            {
                this.button14.Hide();
            }

            if (i == 5)
            {
                this.button15.Hide();
            }

            if (i == 6)
            {
                this.button16.Hide();
            }
            if (i == 7)
            {
                this.button17.Hide();
            }

            if (i == 8)
            {
                this.button18.Hide();
            }

            if (i == 9)
            {
                this.button19.Hide();
            }

        }


          public void ShowThisPosts(int i)
          {

              if (i == 1)
              {
                  this.button3.Show();
              }

              if (i == 2)
              {
                  this.button4.Show();
              }

              if (i == 3)
              {
                  this.button5.Show();
              }

              if (i == 4)
              {
                  this.button6.Show();
              }

              if (i == 5)
              {
                  this.button7.Show();
              }

              if (i == 6)
              {
                  this.button8.Show();
              }


          }

          public void HideThisPosts(int i)
          {
              if (i == 1)
              {
                  this.button3.Hide();
              }

              if (i == 2)
              {
                  this.button4.Hide();
              }

              if (i == 3)
              {
                  this.button5.Hide();
              }

              if (i == 4)
              {
                  this.button6.Hide();
              }

              if (i == 5)
              {
                  this.button7.Hide();
              }

              if (i == 6)
              {
                  this.button8.Hide();
              }
          }

          public void EnterTextToSubjectOrThread(string text, int ThreadNumber)
          {
              if (ThreadNumber == 1)
              {
                  this.button9.Text = text;
              }

              if (ThreadNumber == 2)
              {
                  this.button10.Text = text;
              }

              if (ThreadNumber == 3)
              {
                  this.button11.Text = text;
              }

              if (ThreadNumber == 4)
              {
                  this.button14.Text = text;
              }

              if (ThreadNumber == 5)
              {
                  this.button15.Text = text;
              }

              if (ThreadNumber == 6)
              {
                  this.button16.Text = text;
              }
              if (ThreadNumber == 7)
              {
                  this.button17.Text = text;
              }

              if (ThreadNumber == 8)
              {
                  this.button18.Text = text;
              }

              if (ThreadNumber == 9)
              {
                  this.button19.Text = text;
              }
          }
          


          public void EnterTextToPost(string text, int postNumber)
          {
              if (postNumber == 1)
              {
                  this.button1.Text = text;
              }

              if (postNumber == 2)
              {
                  this.button3.Text = text;
              }

              if (postNumber == 3)
              {
                  this.button4.Text = text;
              }

              if (postNumber == 4)
              {
                  this.button5.Text = text;
              }

              if (postNumber == 5)
              {
                  this.button6.Text = text;
              }

              if (postNumber == 6)
              {
                  this.button7.Text = text;
              }

              if (postNumber == 7)
              {
                  this.button8.Text = text;
              }

          }

          //***************************addpost**********************************************************//
          public void addpost(string title, string body)
          {
              this.proxy.Out_call_addPost(title, body);
          }


  


          //***************************Back**********************************************************//
          //***************************thread_post_ Back**********************************************************//
          private void button13_Click(object sender, EventArgs e)
          {//this is thread_subj back button
              sendRequest_for_back();
          }
          //******************************************************************************************
          //***************************post_post_ Back**********************************************************//
          private void button2_Click(object sender, EventArgs e)
          {//post is post back button
              sendRequest_for_back();
          }

          private void button2_Click_1(object sender, EventArgs e)
          {
              sendRequest_for_back();
          }
          //******************************************************************************************


          //***************************Edit Post**********************************************************//
          private void button30_Click(object sender, EventArgs e)
          {
         
              MessageBox.Show("please choose a post to edit");
              this.EditOpen = true;

          }

          private void Start_Edit()
          {
              this.Hide();
              this.EditForm.Show();
          }

          public void Send_Edit_to_server(int place,string title,string body)
          {
              this.proxy.Out_call_editPost(place,title,body);
              
              //this.proxy.
              //EnterTextToPost(text, this.pushed_now);
              //here invoke the update or edit message ^^^^^^^^^^^^^^^^^^^^^^^^^^^ IMPLEMENT NEEDED
          }

          public void Send_Edit_thread_to_server(int place, string title, string body)
          {
              this.proxy.Out_call_editThread(place, title, body);

              //this.proxy.
              //EnterTextToPost(text, this.pushed_now);
              //here invoke the update or edit message ^^^^^^^^^^^^^^^^^^^^^^^^^^^ IMPLEMENT NEEDED
          }


          //******************************************************************************************

          //***************************Delete thread**********************************************************//

          private void button32_Click(object sender, EventArgs e)
          {//remove thread
              if (this.Is_Thread_screeen)
              {
                  MessageBox.Show("please choose the thread to delete");
                  DeleteThread = true;
              }
              else
              {
                  MessageBox.Show("Some Mistake happaned");
              }
              
            
          }


          public void Send_server_Del_thread(int place)
          {
              this.proxy.Out_call_removeThread(place);
          }

          
          //**********************************************************************************************


          //***************************Delete subject**********************************************************//
          private void button26_Click(object sender, EventArgs e)
          {
              if (this.Is_Subject_screen)
              {
                  MessageBox.Show("please choose the Subject to delete");
                  DeleteSubject = true;
              }
              else
              {
                  MessageBox.Show("Some Mistake happaned");
              }

          }

          public void Send_server_Del_subject(int place)
          {
              //here invoke the update or edit message ^^^^^^^^^^^^^^INVOKE MESSAGE^^^^^^ IMPLEMENT NEEDED
          }
        
          //**********************************************************************************************


          //***************************Add thread**********************************************************//

          private void button31_Click(object sender, EventArgs e)
          {
              this.AddThreadForm = new Form6(this);
              this.AddThreadForm.Show();
          }

          public void Send_New_thread(string body,string title)
          {
              this.proxy.Out_call_addThread(title, body);
          }

          //***************************Edit thread**********************************************************//
          private void button12_Click_1(object sender, EventArgs e)
          {//this is edit thread
              this.EditThreadForm = new Form8(this);
              MessageBox.Show("Please Choose The Thread To Edit");
              this.EditThread = true;
          }
          //**********************************************************************************************

          //***************************Add Subject**********************************************************//

          private void button25_Click(object sender, EventArgs e)
          {
              this.AddSubjectForm = new Form7(this);
              this.AddSubjectForm.Show();
          }

          public void Send_New_subject(string thread)
          {
              //here invoke the update or edit message ^^^^^^^^^^^^^^^^^^^^^^^^^^^ IMPLEMENT NEEDED
          }

          //**********************************************************************************************

          //***************************Threads Post Pushed**********************************************************//
          private void button9_Click(object sender, EventArgs e)
          { //first button
              this.pushed_now = 1;
              this.EditText = button9.Text;

              if ((this.ordenaryRun == true) && (this.DeleteThread != true) && (this.DeleteSubject != true) && (this.EditThread != true))
              {
                  //MessageBox.Show("first Pushed now1");
                  sendRequest_for_newPage(1);
              }
              if (this.DeleteThread == true)
              {
                  Send_server_Del_thread(pushed_now);
                  DeleteThread = false;
              }
              if (this.DeleteSubject == true)
              {
                  Send_server_Del_subject(pushed_now);
                  DeleteSubject = false;
              }

              if (this.EditThread == true)
              {
                  this.EditThreadForm.Show();
                  this.EditThreadForm.UpdateTextToEdit(this.pushed_now, this.titlesThSu[pushed_now], this.bodyThSu[pushed_now]); ;
                  EditThread = false;
              }
              
              //here invoke the update or edit message ^^^^^^^^^^^^^^^^^^^^^^^^^^^ IMPLEMENT NEEDED
          }

          private void button10_Click(object sender, EventArgs e)
          {//second button
              this.pushed_now = 2;
              this.EditText = button10.Text;

              if ((this.ordenaryRun == true) && (this.DeleteThread != true) && (this.DeleteSubject != true) && (this.EditThread != true))
              {
                 // MessageBox.Show("second Pushed now2");
                  sendRequest_for_newPage(2);
              }
              if (this.DeleteThread == true)
              {
                  Send_server_Del_thread(pushed_now);
                  DeleteThread = false;
              }
              if (this.DeleteSubject == true)
              {
                  this.EditThreadForm.Show();
                  Send_server_Del_subject(pushed_now);
                  DeleteSubject = false;
              }

              if (this.EditThread == true)
              {
                  this.EditThreadForm.UpdateTextToEdit(this.pushed_now, this.titlesThSu[pushed_now], this.bodyThSu[pushed_now]); ;
                  EditThread = false;
              }
              //here invoke the update or edit message ^^^^^^^^^^^^^^^^^^^^^^^^^^^ IMPLEMENT NEEDED
          }

          private void button11_Click(object sender, EventArgs e)
          {
              this.pushed_now = 3;
              this.EditText = button11.Text;
              if ((this.ordenaryRun == true) && (this.DeleteThread != true) && (this.DeleteSubject != true) && (this.EditThread != true))
              {
                  //MessageBox.Show("third Pushed now");
                  sendRequest_for_newPage(3);
              }
              if (this.DeleteThread == true)
              {
                  Send_server_Del_thread(pushed_now);
                  DeleteThread = false;
              }
              if (this.DeleteSubject == true)
              {
                  Send_server_Del_subject(pushed_now);
                  DeleteSubject = false;
              }
              if (this.EditThread == true)
              {
                  this.EditThreadForm.Show();
                  this.EditThreadForm.UpdateTextToEdit(this.pushed_now, this.titlesThSu[pushed_now], this.bodyThSu[pushed_now]); ;
                  EditThread = false;
              }
              //here invoke the update or edit message ^^^^^^^^^^^^^^^^^^^^^^^^^^^ IMPLEMENT NEEDED
          }

          private void button14_Click(object sender, EventArgs e)
          {
              this.pushed_now = 4;
              this.EditText = button14.Text;
              if ((this.ordenaryRun == true) && (this.DeleteThread != true) && (this.DeleteSubject != true) && (this.EditThread != true))
              {
                 // MessageBox.Show("fourth Pushed now");
                  sendRequest_for_newPage(4);
              }
              if (this.DeleteThread == true)
              {
                  Send_server_Del_thread(pushed_now);
                  DeleteThread = false;
              }
              if (this.DeleteSubject == true)
              {
                  Send_server_Del_subject(pushed_now);
                  DeleteSubject = false;
              }
              if (this.EditThread == true)
              {
                  this.EditThreadForm.Show();
                  this.EditThreadForm.UpdateTextToEdit(this.pushed_now, this.titlesThSu[pushed_now], this.bodyThSu[pushed_now]); ;
                  EditThread = false;
              }
              //here invoke the update or edit message ^^^^^^^^^^^^^^^^^^^^^^^^^^^ IMPLEMENT NEEDED
          }

          private void button15_Click(object sender, EventArgs e)
          {
              this.pushed_now = 5;
              this.EditText = button15.Text;
              if ((this.ordenaryRun == true) && (this.DeleteThread != true) && (this.DeleteSubject != true) && (this.EditThread != true))
              {
                  //MessageBox.Show("5 Pushed now");
                  sendRequest_for_newPage(5);
              }
              if (this.DeleteThread == true)
              {
                  Send_server_Del_thread(pushed_now);
                  DeleteThread = false;
              }
              if (this.DeleteSubject == true)
              {
                  Send_server_Del_subject(pushed_now);
                  DeleteSubject = false;
              }
              if (this.EditThread == true)
              {
                  this.EditThreadForm.Show();
                  this.EditThreadForm.UpdateTextToEdit(this.pushed_now, this.titlesThSu[pushed_now], this.bodyThSu[pushed_now]); ;
                  EditThread = false;
              }
              //here invoke the update or edit message ^^^^^^^^^^^^^^^^^^^^^^^^^^^ IMPLEMENT NEEDED
          }

          private void button16_Click(object sender, EventArgs e)
          {
              this.pushed_now = 6;
              this.EditText = button16.Text;
              if ((this.ordenaryRun == true) && (this.DeleteThread != true) && (this.DeleteSubject != true) && (this.EditThread != true))
              {
                  //MessageBox.Show("6 Pushed now");
                  sendRequest_for_newPage(6);
              }
              if (this.DeleteThread == true)
              {
                  Send_server_Del_thread(pushed_now);
                  DeleteThread = false;
              }
              if (this.DeleteSubject == true)
              {
                  Send_server_Del_subject(pushed_now);
                  DeleteSubject = false;
              }
              if (this.EditThread == true)
              {
                  this.EditThreadForm.Show();
                  this.EditThreadForm.UpdateTextToEdit(this.pushed_now, this.titlesThSu[pushed_now], this.bodyThSu[pushed_now]); ;
                  EditThread = false;
              }
              //here invoke the update or edit message ^^^^^^^^^^^^^^^^^^^^^^^^^^^ IMPLEMENT NEEDED
          }

          private void button17_Click(object sender, EventArgs e)
          {
              this.pushed_now = 7;
              this.EditText = button17.Text;
              if ((this.ordenaryRun == true) && (this.DeleteThread != true) && (this.DeleteSubject != true) && (this.EditThread != true))
              {
                  //MessageBox.Show("7 Pushed now");
                  sendRequest_for_newPage(7);
              }
              if (this.DeleteThread == true)
              {
                  Send_server_Del_thread(pushed_now);
                  DeleteThread = false;
              }
              if (this.DeleteSubject == true)
              {
                  Send_server_Del_subject(pushed_now);
                  DeleteSubject = false;
              }
              if (this.EditThread == true)
              {
                  this.EditThreadForm.Show();
                  this.EditThreadForm.UpdateTextToEdit(this.pushed_now, this.titlesThSu[pushed_now], this.bodyThSu[pushed_now]); ;
                  EditThread = false;
              }
              //here invoke the update or edit message ^^^^^^^^^^^^^^^^^^^^^^^^^^^ IMPLEMENT NEEDED
          }

          private void button18_Click(object sender, EventArgs e)
          {
              this.pushed_now = 8;
              this.EditText = button18.Text;
              if ((this.ordenaryRun == true) && (this.DeleteThread != true) && (this.DeleteSubject != true) && (this.EditThread != true))
              {
                  //MessageBox.Show("8 Pushed now");
                  sendRequest_for_newPage(8);
              }
              if (this.DeleteThread == true)
              {
                  Send_server_Del_thread(pushed_now);
                  DeleteThread = false;
              }
              if (this.DeleteSubject == true)
              {
                  Send_server_Del_subject(pushed_now);
                  DeleteSubject = false;
              }
              if (this.EditThread == true)
              {
                  this.EditThreadForm.Show();
                  this.EditThreadForm.UpdateTextToEdit(this.pushed_now, this.titlesThSu[pushed_now], this.bodyThSu[pushed_now]); ;
                  EditThread = false;
              }
              //here invoke the update or edit message ^^^^^^^^^^^^^^^^^^^^^^^^^^^ IMPLEMENT NEEDED
          }

          private void button19_Click(object sender, EventArgs e)
          {
              this.pushed_now = 9;
              this.EditText = button19.Text;
              if ((this.ordenaryRun == true) && (this.DeleteThread != true) && (this.DeleteSubject != true)&&(this.EditThread != true))
              {
                  //MessageBox.Show("9 Pushed now");
                  sendRequest_for_newPage(9);
              }
              if (this.DeleteThread == true)
              {
                  Send_server_Del_thread(pushed_now);
                  DeleteThread = false;
              }
              if (this.DeleteSubject == true)
              {
                  Send_server_Del_subject(pushed_now);
                  DeleteSubject = false;
              }
              if (this.EditThread == true)
              {
                  this.EditThreadForm.Show();
                  this.EditThreadForm.UpdateTextToEdit(this.pushed_now, this.titlesThSu[pushed_now], this.bodyThSu[pushed_now]); ;
                  EditThread = false;
              }
              //here invoke the update or edit message ^^^^^^^^^^^^^^^^^^^^^^^^^^^ IMPLEMENT NEEDED
          }


          private void button1_Click(object sender, EventArgs e)
          {
              this.pushed_now = 1;
              this.EditText = button1.Text;
              if (EditOpen == true)
              {
                  this.EditForm = new Form5(this);
                  this.EditForm.UpdateTextToEdit(this.pushed_now,this.titlepost[pushed_now],this.bodypost[pushed_now]);
                  
                  this.EditForm.Show();
                  this.EditOpen = false;

              }

              if (this.DeletePost == true)
              {
                  this.proxy.Out_call_removePost(1);
                  DeletePost = false;
              }
              //here invoke the update or edit message ^^^^^^^^^^^^^^^^^^^^^^^^^^^ IMPLEMENT NEEDED

          }

          private void button3_Click(object sender, EventArgs e)
          {
              this.pushed_now = 2;
              this.EditText = button3.Text;
              if (EditOpen == true)
              {
                  this.EditForm = new Form5(this);
                  this.EditForm.UpdateTextToEdit(this.pushed_now, this.titlepost[pushed_now], this.bodypost[pushed_now]);
                
                  this.EditForm.Show();
                  this.EditOpen = false;

              }


              if (this.DeletePost == true)
              {
                  this.proxy.Out_call_removePost(2);
                  DeletePost = false;
              }
              //here invoke the update or edit message ^^^^^^^^^^^^^^^^^^^^^^^^^^^ IMPLEMENT NEEDED
          }

          private void button4_Click(object sender, EventArgs e)
          {
              this.pushed_now = 3;
              this.EditText = button4.Text;
              if (EditOpen == true)
              {
                  this.EditForm = new Form5(this);
                  this.EditForm.UpdateTextToEdit(this.pushed_now, this.titlepost[pushed_now], this.bodypost[pushed_now]);
                
                  this.EditForm.Show();
                  this.EditOpen = false;

              }

              if (this.DeletePost == true)
              {
                  this.proxy.Out_call_removePost(3);
                  DeletePost = false;
              }
              //here invoke the update or edit message ^^^^^^^^^^^^^^^^^^^^^^^^^^^ IMPLEMENT NEEDED

          }

          private void button5_Click(object sender, EventArgs e)
          {
              this.pushed_now = 4;
              this.EditText = button5.Text;
              if (EditOpen == true)
              {
                  this.EditForm = new Form5(this);
                  this.EditForm.UpdateTextToEdit(this.pushed_now, this.titlepost[pushed_now], this.bodypost[pushed_now]);
                  
                  this.EditForm.Show();
                  this.EditOpen = false;

              }


              if (this.DeletePost == true)
              {
                  this.proxy.Out_call_removePost(4);
                  DeletePost = false;
              }
              //here invoke the update or edit message ^^^^^^^^^^^^^^^^^^^^^^^^^^^ IMPLEMENT NEEDED
          }

          private void button6_Click(object sender, EventArgs e)
          {

              this.pushed_now = 5;
              this.EditText = button6.Text;
              if (EditOpen == true)
              {
                  this.EditForm = new Form5(this);
                  this.EditForm.UpdateTextToEdit(this.pushed_now, this.titlepost[pushed_now], this.bodypost[pushed_now]);
               
                  this.EditForm.Show();
                  this.EditOpen = false;

              }


              if (this.DeletePost == true)
              {
                  this.proxy.Out_call_removePost(5);
                  DeletePost = false;
              }
              //here invoke the update or edit message ^^^^^^^^^^^^^^^^^^^^^^^^^^^ IMPLEMENT NEEDED
          }

          private void button7_Click(object sender, EventArgs e)
          {
              this.pushed_now = 6;
              this.EditText = button7.Text;
              if (EditOpen == true)
              {
                  this.EditForm = new Form5(this);
                  this.EditForm.UpdateTextToEdit(this.pushed_now, this.titlepost[pushed_now], this.bodypost[pushed_now]);
                
                  this.EditForm.Show();
                  this.EditOpen = false;

              }


              if (this.DeletePost == true)
              {
                  this.proxy.Out_call_removePost(6);
                  DeletePost = false;
              }
              //here invoke the update or edit message ^^^^^^^^^^^^^^^^^^^^^^^^^^^ IMPLEMENT NEEDED
          }

          private void button8_Click(object sender, EventArgs e)
          {
              this.pushed_now =7;
              this.EditText = button8.Text;
              if (EditOpen == true)
              {
                  this.EditForm = new Form5(this);
                  this.EditForm.UpdateTextToEdit(this.pushed_now, this.titlepost[pushed_now], this.bodypost[pushed_now]);
           
                  this.EditForm.Show();
                  this.EditOpen = false;

              }


              if (this.DeletePost == true)
              {
                  this.proxy.Out_call_removePost(7);
                  DeletePost = false;
              }

          
              //here invoke the update or edit message ^^^^^^^^^^^^^^^^^^^^^^^^^^^ IMPLEMENT NEEDED
          }

          private void button27_Click(object sender, EventArgs e)
          {

          }

          private void button12_Click(object sender, EventArgs e)
          {

          }

          private void button2_Click_2(object sender, EventArgs e)
          {

          }

          private void label1_Click_1(object sender, EventArgs e)
          {

          }

          public void UpdateTextBox(string Texts)
          {
              this.textBox1.Text = Texts;
          }
          private void textBox1_TextChanged(object sender, EventArgs e)
          {
              
          }

          private void button27_Click_1(object sender, EventArgs e)
          {
              MessageBox.Show("gilad this button not implemented the moderator will enter throw the back door muhhahha!");
          }

         

       

        


      
    




    }
}
