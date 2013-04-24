using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.ServiceModel;
using System.Windows.Forms;
using WindowsFormsApplication1;
using Common.DataBase;
using System.Threading;

namespace WindowsFormsApplication1
{
    class Gui_proxy : IMessageCallback, IDisposable
    {
        Boolean logOnRequest = false;
        Boolean loginOnRequest = false;
        Boolean LogedOn = false;

        string messtitle;
        string messbody;

        int Page_To_get_num;
        int Page_size;
        int Page_number;
        int postion;
        int place;
        int Premission;
        #region members
        IMessage pipeProxy = null;
        Client me = null;

        //my variables
        Form2 mainForm;
        int formcount = 0;

        //variables
        private int Screen_depth = 0; //{0-open subforums page,1-insidesubforum,2-inside_some_post}

        bool gotLoginRespond = false;
        public bool isLoggedIn { get; set; }
        #endregion
        //blabla
        string ConnectionDetail = "guest";

        #region Connect
        public bool Connect()

        {

            //MessageBox.Show("entered connect");
            this.isLoggedIn = false;
            //this.gotLoginRespond = false;
            //note the "DuplexChannelFactory".  This is necessary for Callbacks.
            // A regular "ChannelFactory" won't work with callbacks.
            DuplexChannelFactory<IMessage> pipeFactory =
                  new DuplexChannelFactory<IMessage>(
                      new InstanceContext(this),
                      new NetTcpBinding(),
                      new EndpointAddress("net.tcp://localhost:8000/ISubscribe"));


            try
            {
                //Open the channel to the server
                pipeProxy = pipeFactory.CreateChannel();
                //Now tell the server who is connecting

                while (!isLoggedIn)
                {
                   
                    gotLoginRespond = false;
                    //Console.WriteLine("How do you want to enther");
                    //Console.WriteLine("For guest enter: <guest>");
                    //Console.WriteLine("For member enter: <login username password>");
                    //Console.WriteLine("To register (auto join after) enter: <register firstName lastName userName passWord email>");
                    pipeProxy.Login(ConnectionDetail); //first connects as a guest
                   
                    MessageBox.Show("Client activated!");
                    while (!gotLoginRespond) { }

                    
                    if (isLoggedIn)
                    {
                        break;
                    }
                }
                
                return true;
            }
            catch (Exception e)
            {
              
                //Console.WriteLine(e.Message);
                return false;
            }
        }

        public void Close()
        {
            pipeProxy.Unsubscribe();
        }
        #endregion

        public void Dispose()
        {
            throw new NotImplementedException();
        }


        public void OnMessageAdded(string message, DateTime timestamp)
        {
            throw new NotImplementedException();
        }

        public void receivePage(Page page)
        {
           
            int lengthx;
            string titleAndPsot;          
            if (Screen_depth == 0)
            {
                lengthx = page.length();

                
                foreach (int msgIndex in page.messages.Keys)
                {
                    Common.DataBase.Message msg = page.messages[msgIndex];
                   //MessageBox.Show(msgIndex.ToString());
                   // MessageBox.Show(msg.Title);
                   // MessageBox.Show(msg.Body);
                    titleAndPsot = "Title: " + msg.Title +  "\n";
                    this.mainForm.SubForumsPushForum(msgIndex, msg.Title, msg.Body , titleAndPsot);

                   mainForm.Show_Those_threadsOrSubjects(lengthx);
                }

            } if (Screen_depth == 1)
               {
               
                lengthx = page.length();


                foreach (int msgIndex in page.messages.Keys)
                {
                    Common.DataBase.Message msg = page.messages[msgIndex];

                    titleAndPsot = "Title: " + msg.Title + " ->  Post: " + msg.Body + "\n";
                    this.mainForm.SubForumsPushForum(msgIndex, msg.Title, msg.Body ,titleAndPsot);

                    mainForm.Show_Those_threadsOrSubjects(lengthx);

                }
               } if (Screen_depth == 2)
                 {


                     lengthx = page.length();


                     foreach (int msgIndex in page.messages.Keys)
                     {
                         Common.DataBase.Message msg = page.messages[msgIndex];

                         titleAndPsot = "Title: " + msg.Title + " ->  Post: " + msg.Body + "\n";
                         this.mainForm.PostsPushForum(msgIndex,msg.Title,msg.Body ,titleAndPsot);

                         mainForm.Show_Those_Posts(lengthx - 1);
                     }
                 }





            //throw new NotImplementedException();
        }

        #region receiveJoin
        public void receiveJoin(Client client, bool ans, string systemMsg)
        {
            if (!ans)
            {

                if (this.logOnRequest == true)
                {
                    this.LogedOn = false;
                    this.mainForm.GotRegisterConnectin(false);
                }else
                    if (this.loginOnRequest == true)
                    {
                        this.LogedOn = false;
                        this.mainForm.GotLoginConnectin(false);
                    }

                isLoggedIn = false;

              
            }
            else
            {
                isLoggedIn = true;
                this.me = client;
                this.Premission = client.Permission;
                if (this.logOnRequest == true)
                {
                    this.LogedOn = true;
                    this.mainForm.GotRegisterConnectin(true);
                }
                else
                    if (this.loginOnRequest == true)
                    {
                        this.mainForm.UpdatePRemmision(this.Premission);
                        this.LogedOn = true;
                        this.mainForm.GotLoginConnectin(true);
                    }
            }

            //Console.WriteLine(systemMsg);
            this.gotLoginRespond = true;
        }
        #endregion


        #region Getters
   
        #endregion

        #region Setters
        public void updateForm(Form2 newForm)
        {
            formcount++;
            if (formcount == 1) //updated only once
                this.mainForm = newForm;
        }

        public void updateConnectionType(string connectiondetail)
        {
            this.ConnectionDetail = connectiondetail;
        }

        public void updateScreenDepth(int ddepth)
        {
            Screen_depth = ddepth;
        }

        public void updatePageSize(int size)
        {
            this.Page_size = size;
        }
        public void updatePageNumber(int number)
        {
            this.Page_number = number;
        }
        public void uppdateTryRegister_login()
        {
            this.logOnRequest = true;
        }

        public void updateTryLogin()
        {
            this.loginOnRequest = true;
        }



        #endregion

        public void Out_call_back()
        {
            Call("Back");
        }

        public void Out_call_get_page(int number, int Depth,int num_message,int num_page)
        {
            this.Page_To_get_num = number;
            this.Page_size = num_message;
            this.Page_number = num_page;
            this.Screen_depth = Depth;

            Call("getPage");
        }

        public void Out_call_LogOut()
        {
            Call("LogOut");
        }

        public void Out_call_addPost(string messagetitle,string messagebody)
        {
            this.messtitle = messagetitle;
            this.messbody = messagebody;
            Call("AddPost");
        }

        public void Out_call_removePost(int positionx)
        {
            this.place = positionx;
            Call("RemovePost");
        }

        public void Out_call_addThread(string messagetitle, string messagebody)
        {
            this.messtitle = messagetitle;
            this.messbody = messagebody;
            Call("AddThread");
        }

        public void Out_call_Refresh()
        {
            Call("Refresh");
        }

        public void Out_call_removeThread(int positionx)
        {
            this.place = positionx;
            Call("RemoveThread");
        }

        public void Out_call_editPost(int place, string titlex, string bodyx)
        {
            this.place = place;
            this.messtitle = titlex;
            this.messbody = bodyx;
            Call("EditPost");
        }

        public void Out_call_editThread(int place, string titlex, string bodyx)
        {
            this.place = place;
            this.messtitle = titlex;
            this.messbody = bodyx;
            Call("EditThread");
        }

        private void Call(string command)
        {
            switch (command)
            {
                case "getPage":
                    this.get_page_server();
                    break;
                case "Back":
                    this.back_server();
                    break;
                case "LogOut":
                    this.logout_server();
                    break;
                case "AddPost":
                    this.addpost_server();
                    break;
                case "AddThread":
                    this.addThread_server();
                    break;
                case "RemoveThread":
                    removeThread_server(this.place);
                    break;
                case "Refresh":
                    Refresh_server_page();
                    break;
                case "RemovePost":
                    removePost_server(this.place);
                    break;
                case "EditPost":
                    editPost_server(this.place,this.messtitle,this.messbody);
                    break;
                case "EditThread":
                    editThread_server(this.place, this.messtitle, this.messbody);
                    break;
                default:
                    break;

               
            }
            
        }

        #region editThread_server
        public void editThread_server(int pos, string title, string body)
        {
            //MessageBox.Show("position is " + pos);
            pipeProxy.EditThread(me, pos, title, body);
        }
        #endregion


        #region editPost_server
        public void editPost_server(int pos, string title, string body)
        {
            pipeProxy.EditPost(me, pos, title, body);
            this.Out_call_Refresh();
        }
        #endregion


        #region removeThread_server
        public void removeThread_server(int pos)
        {
            //MessageBox.Show("position is " + pos);
            pipeProxy.RemoveThread(me, pos);
            this.Out_call_Refresh();
        }
        #endregion

        #region addThread_server
        public void addThread_server()
        {
            pipeProxy.AddThread(me, this.messtitle,this.messbody);
            this.Out_call_Refresh();
        }
        #endregion



        #region addpost_server
        public void addpost_server()
        {
            try
            {
                pipeProxy.AddPost(me, this.messtitle, this.messbody);
            }
            catch (Exception e)
            {
              
            }
        }
        #endregion

        #region back
        public void back_server()
        {
            pipeProxy.Back(me);  
        }
        #endregion

        #region Refresh_sever
        public void Refresh_server_page()
        {
            pipeProxy.RefreshPage(me);
        }
        #endregion


        #region getPage
        public void get_page_server()
        {
            pipeProxy.GetPage(me, Page_To_get_num, Page_size, Page_number);
        }
        #endregion

        #region logout
        public void logout_server()
        {
            pipeProxy.Logout(me);
        }
        #endregion

        #region removePost_server
        public void removePost_server(int position)
        {
            pipeProxy.RemovePost(me, position);
            this.Out_call_Refresh();
        }
        #endregion

        public void receiveLogout()
        {
            ((ICommunicationObject)pipeProxy).Close();
        }


        public void receiveNotify(string notifyMsg)
        {
            MessageBox.Show("got notify");
            this.mainForm.UpdateTextBox(notifyMsg);
            // throw new NotImplementedException();
        }

       // public void receiveNotify(Client client, string notifyMsg)
       // {
        //    this.messbody = notifyMsg;
           // throw new NotImplementedException();
       // }

        public void receiveStatistics(string statMsg)
        {
            this.messbody = statMsg;
            //throw new NotImplementedException();
        }
    }
}
