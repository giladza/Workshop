using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using Common;
using Common.DataBase;




namespace WCFClient
{

    class RCRProxy : IMessageCallback, IDisposable
    {

        #region members
        IMessage pipeProxy = null;
        Client me = null;
        bool gotLoginRespond = false;
        public bool isLoggedIn { get; set; }
        #endregion

        #region Connect
        public bool Connect()
        {
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
                    Console.Clear();
                    gotLoginRespond = false;

                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Welcome To -- Ricin -- Forum.");
                    Console.WriteLine(new string('\u2550', 29));

                    //string tmp = "\n\nHow do you want to enther\n";
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\n\nHow do you want to enther\n");

                    Console.ResetColor();
                    //Console.WriteLine(tmp);
                    //Console.WriteLine(new string('\u0332', tmp.Length));

                    Console.WriteLine("1. For guest enter: <guest>");
                    Console.WriteLine("2. For member enter: <login username password>");
                    Console.WriteLine("3. To register (auto join after)\n\tenter: <register firstName lastName userName passWord email>\n");
                    Console.Write(">> ");

                    string details = Console.ReadLine();
                    pipeProxy.Login(details);

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
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public void Close()
        {
            pipeProxy.Unsubscribe();
        }
        #endregion

        #region SendMessage
        public string SendMessage(string message)
        {
            try
            {
                pipeProxy.AddMessage(message);
                return "sent >>>>  " + message;
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }
        #endregion

        #region OnMessageAdded
        public void OnMessageAdded(string message, DateTime timestamp)
        {
            Console.WriteLine(message + ": " + timestamp.ToString("hh:mm:ss"));
        }
        #endregion

        #region Dispose
        public void Dispose()
        {
            pipeProxy.Unsubscribe();
        }
        #endregion

        #region sendMyMsg
        public void sendMyMsg(string msg)
        {
            pipeProxy.sendMsg(msg, 5);
        }
        #endregion

        #region getPost
        public void getPost()
        {
            //Client client = new Client { Name = "BOB", User_ID = 1, Time = DateTime.Now };
            pipeProxy.GetPost(me);
        }
        #endregion

        #region sendCommand - main func
        public void sendCommand(string cmd)
        {
            try
            {
                string[] tmpArr = cmd.Split(' ');
                int pos;
                //int pos = Int32.Parse(tmpArr[1]);
                switch (tmpArr[0])
                {
                    case "choose":
                        pos = Int32.Parse(tmpArr[1]);
                        this.getPage(pos, 10, 0);
                        break;
                    case "back":
                        this.back();
                        break;
                    case "addPost":
                        this.addPost(tmpArr[1], tmpArr[2]);
                        break;
                    case "addThread":
                        this.addThread(tmpArr[1], tmpArr[2]);
                        break;
                    case "addSubForum":
                        this.addSubForum(tmpArr[1], tmpArr[2]);
                        break;
                    case "removeSubForum":
                        pos = Int32.Parse(tmpArr[1]);
                        this.removeSubforum(pos);
                        break;
                    case "removeThread":
                        pos = Int32.Parse(tmpArr[1]);
                        this.removeThread(pos);
                        break;
                    case "removePost":
                        pos = Int32.Parse(tmpArr[1]);
                        this.removePost(pos);
                        break;
                    case "editPost":
                        pos = Int32.Parse(tmpArr[1]);
                        this.editPost(pos, tmpArr[2], tmpArr[3]);
                        break;
                    case "editThread":
                        pos = Int32.Parse(tmpArr[1]);
                        this.editThread(pos, tmpArr[2], tmpArr[3]);
                        break;
                    case "addModerator":
                        pos = Int32.Parse(tmpArr[1]);
                        this.addModerator(pos, tmpArr[2]);
                        break;
                    case "removeModerator":
                        pos = Int32.Parse(tmpArr[1]);
                        this.removeModerator(pos, tmpArr[2]);
                        break;
                    case "changeModerator":
                        pos = Int32.Parse(tmpArr[1]);
                        this.changeModerator(pos, tmpArr[2], tmpArr[3]);
                        break;
                    case "userMessageCount":
                        this.getMemberStatistics(tmpArr[1]);
                        break;
                    case "messageCount":
                        pos = Int32.Parse(tmpArr[1]);
                        this.getSubForumStatistics(pos);
                        break;
                    case "logout":
                        this.logout();
                        break;
                    case "help":
                        this.showHelp();
                        break;
                    default:
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Invalid input.");
            }
        } 
        #endregion

        #region back
        public void back()
        {
            //Console.WriteLine("old pos: [{0},{1},{2}]", me.X, me.Y, me.Z);
            pipeProxy.Back(me);
            //Console.WriteLine("new pos: [{0},{1},{2}]", me.X, me.Y, me.Z);
        } 
        #endregion

        #region getPage
        public void getPage(int pos, int size, int pageNumber)
        {
            pipeProxy.GetPage(me, pos, size, pageNumber);
        } 
        #endregion

        #region addPost
        public void addPost(string title, string body)
        {
            pipeProxy.AddPost(me, title, body);
        } 
        #endregion

        #region addThread
        public void addThread(string title, string body)
        {
            pipeProxy.AddThread(me, title, body);
        } 
        #endregion

        #region addSubForum
        public void addSubForum(string title, string mName)
        {
            pipeProxy.AddSubForum(me, title, mName);
        } 
        #endregion

        #region editThread
        public void editThread(int pos, string title, string body)
        {
            pipeProxy.EditThread(me, pos, title, body);
        } 
        #endregion

        #region editPost
        public void editPost(int pos, string title, string body)
        {
            pipeProxy.EditPost(me, pos, title, body);
        } 
        #endregion

        #region removePost
        public void removePost(int position)
        {
            pipeProxy.RemovePost(me, position);
        } 
        #endregion

        #region removeThread
        public void removeThread(int position)
        {
            pipeProxy.RemoveThread(me, position);
        } 
        #endregion

        #region removeSubforum
        public void removeSubforum(int position)
        {
            pipeProxy.RemoveSubForum(me, position);
        } 
        #endregion

        #region logout
        public void logout()
        {
            pipeProxy.Logout(me);
        } 
        #endregion

        #region getMemberStatistics
        public void getMemberStatistics(string userName)
        {
            pipeProxy.MemberMsgCount(me, userName);
        }
        #endregion

        #region getSubForumStatistics
        public void getSubForumStatistics(int pos)
        {
            pipeProxy.SubForumMsgCount(me, pos);
        }
        #endregion

        #region removeModerator
        public void removeModerator(int pos, string uName)
        {
            pipeProxy.RemoveModerator(me, pos, uName);
        }
        #endregion

        #region changeModerator
        public void changeModerator(int pos, string toRemove, string toAdd)
        {
            pipeProxy.ChangeModerator(me, toRemove, toAdd, pos);
        }
        #endregion

        #region addModerator
        public void addModerator(int pos, string uName)
        {
            pipeProxy.AddModerator(me, pos, uName);
        }
        #endregion

        #region showHelp
        public void showHelp()
        {
            string uLine = new string('\u2550', 10);
            string header = "Help.\n";
            string body = "\n\n1. choose <Page_NO>\n" +
                          "2. back\n" +
                          "3. addPost <Title> <Body>\n" +
                          "4. editPost <Post_No> <New Title> <New Body>\n" +
                          "5. removePost <Post_No>\n" +
                          "6. addThread <Title> <Body>\n" +
                          "7. editThread <Thread_No> <New Title> <New Body>\n" +
                          "8. removeThread <Thread_No>\n" +
                          "9. addSubForum <Title> <Moderator>\n" +
                          "10. removeSubForum <Sub_No>\n" +
                          "11. addModerator <Sub_No> <Moderator>\n" +
                          "12. removeModerator <Sub_No> <Moderator>\n" +
                          "13. changeModerator <Sub_No> <Old> <New>\n" +
                          "14. userMessageCount <UserName>\n" +
                          "15. messageCount <Sub_No>\n" +
                          "16. logout\n\n" +
                          "press -back- to return.\n\n>> ";

            string helpMsg = header + uLine + body;
            Console.Clear();
            Console.Write(helpMsg);
        }
        #endregion


        #region receivePage - callback
        public void receivePage(Page page)
        {
            Console.Clear();
            
            Console.Write(page.ToString());

            //Console.WriteLine("test - header: {0}", page.header);
        } 
        #endregion

        #region receiveJoin - callback
        public void receiveJoin(Client client, bool ans, string systemMsg)
        {
            if (!ans)
            {
                isLoggedIn = false;   
            }
            else
            {
                isLoggedIn = true;
                this.me = client;
            }

            Console.WriteLine(systemMsg);
            this.gotLoginRespond = true;
        }
        #endregion - callback

        #region receiveLogout - callback
        public void receiveLogout()
        {
            ((ICommunicationObject)pipeProxy).Close();
            Console.WriteLine("Connection closed.");
        } 
        #endregion

        #region receiveNotify - callback
        public void receiveNotify(string notifyMsg)
        {
            Console.WriteLine("\nSystem Message: {0}\n", notifyMsg);
        } 
        #endregion

        #region receiveStatistics - callback
        public void receiveStatistics(string statMsg)
        {
            Console.WriteLine("\nSystem Statistics: {0}\n", statMsg);
        } 
        #endregion
    }
}
