using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using Common;
using Common.DataBase;


namespace WCFService
{
    #region ForumServer
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
    class ForumServer : IMessage
    {

        #region members
        private static List<IMessageCallback> subscribers = new List<IMessageCallback>();
        private static Dictionary<Client, IMessageCallback> clients = new Dictionary<Client, IMessageCallback>();
        private static List<Client> clientList = new List<Client>();
        public ServiceHost host = null;

        private Users users = (Users)InitData.initForumData()[0];
        private Pages dataManager = (Pages)InitData.initForumData()[1];
        private static int guest_No = 1;

        private string oldIp = "net.tcp://132.72.246.120:8000";

        public IMessageCallback CurrentCallback
        {
            get
            {
                return OperationContext.Current.GetCallbackChannel<IMessageCallback>();
            }
        }

        #endregion

        #region connect
        public void Connect()
        {
            using (ServiceHost host = new ServiceHost(
                typeof(ForumServer),
                new Uri("net.tcp://localhost:8000")))
            {
                host.AddServiceEndpoint(typeof(IMessage),
                  new NetTcpBinding(),
                  "ISubscribe");

                try
                {
                    host.Open();
                    Console.WriteLine("Successfully opened port 8000.");
                    Console.ReadLine();
                    host.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        #endregion

        #region Login
        public void Login(string details)
        {
            bool ans = false;
            string systemMsg = null;
            Client client = null;
            string[] detailsArr = details.Split(' ');
            switch (detailsArr[0])
            {
                case "guest":
                    client = new Client { Name = "guest" + guest_No, Permission = 1, X = 0, Y = 0, Z = 0 };
                    guest_No++;
                    systemMsg = "Log in as a guest";
                    ans = true;
                    break;
                case "login":
                    Console.WriteLine("im here");
                    client = isMember(detailsArr[1], detailsArr[2]);
                    if (client == null)
                    {
                        systemMsg = "Login failed - wrong details (username or password).";
                        ans = false;
                    }
                    else
                    {
                        systemMsg = "Login succeded.";
                        ans = true;
                    }
                    break;
                case "register":
                    client = new Client { Name = detailsArr[3], FirstName = detailsArr[1],
                        LastName = detailsArr[2], PassWord = detailsArr[4],
                                          Email = detailsArr[5],
                                          Permission = 2,
                                          X = 0,
                                          Y = 0,
                                          Z = 0
                    };
                    systemMsg = "registered user.";
                    ans = true;
                    users.register(detailsArr[1], detailsArr[2], detailsArr[3], detailsArr[4], detailsArr[5]);
                    break;
                default:
                    Console.WriteLine("Illegal input!!!");
                    systemMsg = "illegal input.";
                    ans = false;
                    break;
            }

            if (!clients.ContainsValue(CurrentCallback))
            {
                if (client == null)
                {
                    Logger.log(string.Format("Error: User's connection failed."));
                }
                else
                {
                    clients.Add(client, CurrentCallback);
                    Console.WriteLine("Client {0} joined the system.", client.Name);
                    Logger.log(string.Format("User {0} joined the system with permission {1}.", client.Name, client.Permission));
                }
                CurrentCallback.receiveJoin(client, ans, systemMsg);
                Console.WriteLine("Got login from gui");
                Back(client);
            }
            return;
        }
        #endregion

        #region Logout
        public void Logout(Client client)
        {
            if (clients.ContainsValue(CurrentCallback))
            {
                foreach (Client c in clients.Keys)
                {
                    if (client.Name.Equals(c.Name))
                    {
                        IMessageCallback callBack = clients[c];
                        clients.Remove(c);
                        callBack.receiveLogout();

                        Console.WriteLine("User {0} left the system.", client.Name);
                        Logger.log(string.Format("User {0} left the system.", client.Name));
                        break;
                    }
                }
            }
        }
        #endregion

        #region Subscribe
        public bool Subscribe()
        {
            try
            {
                //Get the hashCode of the connecting app and store it as a connection
                //IMessageCallback callback = OperationContext.Current.GetCallbackChannel<IMessageCallback>();
                if (!subscribers.Contains(CurrentCallback))
                {
                    subscribers.Add(CurrentCallback);
                }
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        #endregion

        #region Unsubscribe
        public bool Unsubscribe()
        {
            try
            {
                //remove any connection that is leaving
                IMessageCallback callback = OperationContext.Current.GetCallbackChannel<IMessageCallback>();
                if (subscribers.Contains(callback))
                    subscribers.Remove(callback);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region AddMessage
        public void AddMessage(String message)
        {
            //Go through the list of connections and call their callback funciton
            subscribers.ForEach(delegate(IMessageCallback callback)
            {
                if (((ICommunicationObject)callback).State == CommunicationState.Opened)
                {
                    Console.WriteLine("Calling OnMessageAdded on callback ({0}).", callback.GetHashCode());
                    callback.OnMessageAdded(message, DateTime.Now);
                }
                else
                {
                    subscribers.Remove(callback);
                }
            });

        }
        #endregion

        #region sendMsg
        public void sendMsg(string msg, int id)
        {
            IMessageCallback me = OperationContext.Current.GetCallbackChannel<IMessageCallback>();
            me.OnMessageAdded("hahaha", DateTime.Now);
        }
        #endregion

        #region GetPost
        public void GetPost(Client client)
        {
            if (clients.ContainsValue(CurrentCallback))
            {
                foreach (Client c in clients.Keys)
                {
                    if (client.User_ID == c.User_ID)
                    {
                        int x = client.X;
                        int y = client.Y;
                        int z = client.Z;
                        //DataMessage = data.getPost(x, y, z);
                        //
                        IMessageCallback clientCallBack = clients[c];
                        clientCallBack.OnMessageAdded("you asked for post in loc: " + x + "," + y + "," + z, DateTime.Now);
                    }
                }
            }
        }
        #endregion

        /**
         * STARTS FUNCTIONALLITY HERE.
         */

        //TO TEST
        #region GetPage
        public void GetPage(Client client, int position, int size, int pageNumber)
        {
            if (clients.ContainsValue(CurrentCallback))
            {
                foreach (Client c in clients.Keys)
                {
                    if (client.Name.Equals(c.Name))
                    {
                        int[] tmpPos = upDatePositionForword(c, position);
                        c.X = tmpPos[0];
                        c.Y = tmpPos[1];
                        c.Z = tmpPos[2];

                        Position pos = new Position { x = tmpPos[0], y = tmpPos[1], z = tmpPos[2] };
                        Page page = dataManager.getPage(pos, size, pageNumber);
                        Logger.log(string.Format("User {0} asked for page {1}.", client.Name, dataManager.getPage(pos, size, pageNumber).header));

                        IMessageCallback callBack = clients[c];
                        callBack.receivePage(page);
                        Logger.log(string.Format("Server sent to {0} page {1}.", client.Name, dataManager.getPage(pos, size, pageNumber).header));
                        break;
                    }
                }
            }
        }
        #endregion

        //TO TEST
        #region Back
        public void Back(Client client)
        {
            int _size = 10;
            int _pageNumber = 0;

            if (clients.ContainsValue(CurrentCallback))
            {
                foreach (Client c in clients.Keys)
                {
                    if (client.Name.Equals(c.Name))
                    {
                        int[] res = upDatePositionBack(c);
                        c.X = res[0];
                        c.Y = res[1];
                        c.Z = res[2];

                        Position pos = new Position { x = res[0], y = res[1], z = res[2] };
                        Page page = dataManager.getPage(pos, _size, _pageNumber);

                        IMessageCallback callBack = clients[c];
                        callBack.receivePage(page);
                        break;
                    }
                }
            }
        }
        #endregion

        //TO CHECK - Edi found a bug
        #region Register
        public void Register(Client client)
        {
            if (clients.ContainsValue(CurrentCallback))
            {
                if (!users.isUser(client.Name))
                {
                    users.register(client.FirstName, client.LastName, client.Name, client.PassWord, client.Email);
                }
            }
        } 
        #endregion

        //TO TEST
        #region AddPost
        public void AddPost(Client client, string title, string body)
        {
            if (clients.ContainsValue(CurrentCallback))
            {
                foreach (Client c in clients.Keys)
                {
                    if (client.Name.Equals(c.Name))
                    {
                        if ((isValidPosition(c, "thread")) && (c.Permission > 1) &&
                            (isRegisteredUser(c.Name)))
                        {
                            //Console.WriteLine("in: {0},{1},{2}", c.X, c.Y, c.Z);
                            Position pos = new Position { x = c.X, y = c.Y, z = c.Z };
                            dataManager.addPost(pos, title, body, c.Name, DateTime.Now);

                            RefreshPage(c);
                            break;
                        }
                    }
                }
            }
        } 
        #endregion

        //TO TEST - send notify
        #region AddThread
        public void AddThread(Client client, string title, string body)
        {
            if (clients.ContainsValue(CurrentCallback))
            {
                foreach (Client c in clients.Keys)
                {
                    if (client.Name.Equals(c.Name))
                    {
                        if ((isValidPosition(c, "sub")) && (c.Permission > 1) &&
                            (isRegisteredUser(c.Name)))
                        {
                            Position pos = new Position { x = c.X, y = c.Y, z = c.Z };
                            dataManager.addThread(pos, title, body, c.Name, DateTime.Now);
                            //send all
                            this.notifyAll(pos, "New message added.");
                            RefreshPage(c);
                            break;
                        }
                    }
                }
            }
        } 
        #endregion

        //TO TEST
        #region EditPost
        public void EditPost(Client client, int post, string title, string body)
        {
            if (clients.ContainsValue(CurrentCallback))
            {
                foreach (Client c in clients.Keys)
                {
                    if (client.Name.Equals(c.Name))
                    {
                        Position pos = new Position { x = c.X, y = c.Y, z = post };
                        if ((isValidPosition(c, "thread")) && (isPostOwner(pos, c.Name)))
                        {
                            if (isPostOwner(pos, c.Name))
                            {
                                bool ans = dataManager.editPost(pos, title, body, c.Name, DateTime.Now);
                                RefreshPage(c);
                            }
                        }
                    }
                }
            }
        } 
        #endregion

        //TO TEST
        #region EditThread
        public void EditThread(Client client, int thread, string title, string body)
        {
            if (clients.ContainsValue(CurrentCallback))
            {
                foreach (Client c in clients.Keys)
                {
                    if (client.Name.Equals(c.Name))
                    {
                        Position pos = new Position { x = c.X, y = thread, z = 0 };
                        if ((isValidPosition(c, "sub")) && (isThreadOwner(pos, c.Name)))
                        {
                            dataManager.editThread(pos, title, body, c.Name, DateTime.Now);
                            string notifyMsg = "\nEdit Post by " + c.Name + ".\n" +
                                               "SubForum: " +
                                               dataManager.getSubForumTitle(new Position { x = c.X, y = 0, z = 0 }) +
                                               ".\nThread: " + dataManager.getThreadTitle(pos) + ".\n";
                            notifyAllPosted(pos, notifyMsg);
                            RefreshPage(c);
                            break;
                        }
                    }
                }
            }
        } 
        #endregion

        //TO TEST
        #region AddSubForum
        public void AddSubForum(Client client, string title, string moderator)
        {
            if (clients.ContainsValue(CurrentCallback))
            {
                foreach (Client c in clients.Keys)
                {
                    if (client.Name.Equals(c.Name))
                    {
                        if ((isValidPosition(c, "base")) && (hasAdminPermission(c.Name))
                            && (isRegisteredUser(moderator)))
                        {
                            dataManager.addSubForum(title, moderator, DateTime.Now);
                            RefreshPage(c);
                            break;
                        }
                    }
                }
            }
        } 
        #endregion

        //TO TEST
        #region RemoveSubForum
        public void RemoveSubForum(Client client, int position)
        {
            if (clients.ContainsValue(CurrentCallback))
            {
                foreach (Client c in clients.Keys)
                {
                    if (client.Name.Equals(c.Name))
                    {
                        if ((isValidPosition(c, "base")) && (hasAdminPermission(c.Name)))
                        {
                            Position pos = new Position { x = position, y = 0, z = 0 };
                            dataManager.removeSubForum(pos);
                            RefreshPage(c);
                            break;
                        }
                    }
                }
            }
        }
        #endregion

        //TO FINISH - send notify
        #region RemovePost
        public void RemovePost(Client client, int position)
        {
            if (clients.ContainsValue(CurrentCallback))
            {
                foreach (Client c in clients.Keys)
                {
                    if (client.Name == c.Name)
                    {
                        Position pos = new Position { x = c.X, y = c.Y, z = position };
                        if ((isValidPosition(c, "thread")) && (canRemovePost(pos, c.Name)))
                        {
                            dataManager.removePost(pos);
                            RefreshPage(c);
                            //TODO: send all
                            break;
                        }
                    }
                }
            }
        } 
        #endregion

        //TO TEST - notify
        #region RemoveThread
        public void RemoveThread(Client client, int position)
        {
            if (clients.ContainsValue(CurrentCallback))
            {
                foreach (Client c in clients.Keys)
                {
                    if (client.Name.Equals(c.Name))
                    {
                        Position pos = new Position { x = c.X, y = position, z = 0 };
                        if ((isValidPosition(c, "sub")) && (canRemoveThread(pos, c.Name)))
                        {   
                            dataManager.removeThread(pos);
                            RefreshPage(c);
                            notifyAllPosted(pos, "Removed Thread you posted on");
                            break;
                        }
                    }
                }
            }
        } 
        #endregion

        //To TEST
        #region AddModerator
        public void AddModerator(Client client, int pos, string userName)
        {
            if (clients.ContainsValue(CurrentCallback))
            {
                foreach (Client c in clients.Keys)
                {
                    if (client.Name.Equals(c.Name))
                    {
                        if ((hasAdminPermission(c.Name)) && (isRegisteredUser(userName)) && 
                            (dataManager.getUserMessageCount(pos, userName) > 4))
                        {
                            dataManager.addSubforumModerator(pos, userName);
                            users.setPermission(userName, 3);

                            Position tmpPos = new Position { x = pos, y = 0, z = 0 };

                            Console.WriteLine("System: added {0} as a moderator at {1}.", dataManager.getSubForumTitle(tmpPos));
                            Logger.log(string.Format("System: added {0} as a moderator at {1}.", dataManager.getSubForumTitle(tmpPos)));
                            break;
                        }
                    }
                }
            }
        } 
        #endregion

        //TO TEST
        #region RemoveModerator
        public void RemoveModerator(Client client, int position, string userName)
        {
            if (clients.ContainsValue(CurrentCallback))
            {
                foreach (Client c in clients.Keys)
                {
                    if (client.Name.Equals(c.Name))
                    {
                        if (hasAdminPermission(c.Name))
                        {
                            if ((dataManager.getModeratorCount(position) > 1) &&
                                (dataManager.isModerator(position, userName)))
                            {
                                DateTime lt = dataManager.getLastMessageTime(position, userName);
                                lt.AddHours(1);
                                int res = DateTime.Compare(DateTime.Now, lt);

                                if (res >= 0)
                                {
                                    dataManager.removeModerator(position, userName);

                                    Position tmpPos = new Position { x = position, y = 0, z = 0 };

                                    Console.WriteLine("System: remove the moderator {0} from {1}.", dataManager.getSubForumTitle(tmpPos));
                                    Logger.log(string.Format("System: remove the moderator {0} from {1}.", dataManager.getSubForumTitle(tmpPos)));
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        } 
        #endregion

        //TO FINISH
        #region ChangeModerator
        public void ChangeModerator(Client client, string toRemove, string toAdd, int position)
        {
            if (clients.ContainsValue(CurrentCallback))
            {
                foreach (Client c in clients.Keys)
                {
                    if (client.Name.Equals(c.Name))
                    {
                        if (hasAdminPermission(c.Name))
                        {
                            //out - is a moderator && in -  is a registered user
                            if ((dataManager.isModerator(position, toRemove)) && (users.getPermission(toAdd) > 1))
                            {
                                dataManager.addSubforumModerator(position, toAdd);
                                dataManager.removeModerator(position, toRemove);
                                break;
                            }
                        }
                    }
                }
            }
        } 
        #endregion

        //TO TEST
        #region SubForumMsgCount
        public void SubForumMsgCount(Client client, int position)
        {
            if (clients.ContainsValue(CurrentCallback))
            {
                foreach (Client c in clients.Keys)
                {
                    if (client.Name.Equals(c.Name))
                    {
                        if (hasAdminPermission(c.Name))
                        {
                            int res = dataManager.getMessageCount(position);
                            Position pos = new Position { x = position, y = 0, z = 0 };
                            IMessageCallback callBack = clients[c];
                            string sfTitle = dataManager.getSubForumTitle(pos);
                            string statMsg = "\nSubForum: " + sfTitle + "\nTotal Threads+Posts: " + res;
                            callBack.receiveStatistics(statMsg);
                            break;
                        }
                    }
                }
            }
        } 
        #endregion

        //TO TEST
        #region MemberMsgCount
        public void MemberMsgCount(Client client, string userName)
        {
            if (clients.ContainsValue(CurrentCallback))
            {
                foreach (Client c in clients.Keys)
                {
                    if (client.Name.Equals(c.Name))
                    {
                        if (hasAdminPermission(c.Name))
                        {
                            int res = dataManager.getUserMessageCount(userName);
                            IMessageCallback callBack = clients[c];
                            string statMsg = "User: " + userName + "\nTotal Publications(T\\P): " + res;
                            callBack.receiveStatistics(statMsg);
                            break;
                        }
                    }
                }
            }
        } 
        #endregion

        //TO TEST
        #region RefreshPage
        public void RefreshPage(Client client)
        {
            int size = 10;
            int pNumber = 0;
            foreach (Client c in clients.Keys)
            {
                if (client.Name == c.Name)
                {
                    Position pos = new Position { x = c.X, y = c.Y, z= c.Z };
                    Page page = dataManager.getPage(pos, size, pNumber);

                    IMessageCallback callBack = clients[c];
                    callBack.receivePage(page);
                }
            }
        }
        #endregion


        #region private methods

        #region isClientAlive
        private bool isClientAlive(Client client)
        {
            if (clients.ContainsValue(CurrentCallback))
            {
                foreach (Client c in clients.Keys)
                {
                    if (client.Name == c.Name)
                    {
                        Console.WriteLine("Client: {0} is alive.", client.Name);
                        return true;
                    }
                }
            }
            Console.WriteLine("Client: {0} is NOT alive.", client.Name);
            return false;
        }
        #endregion

        #region upDatePositionForword
        private int[] upDatePositionForword(Client client, int pos)
        {
            int x = client.X;
            int y = client.Y;
            int z = client.Z;
            int[] res = new int[3];
            res[0] = client.X;
            res[1] = client.Y;
            res[2] = client.Z;

            if (x == 0)
            {
                //1
                res[0] = pos;
            }
            else if (y == 0)
            {
                res[1] = pos;
            }
            else if (z == 0)
            {
                res[2] = pos;
            }
            else
            {
                Console.WriteLine("Illegal position!!!");
            }
            return res;
        }
        #endregion

        #region upDatePositionBack
        private int[] upDatePositionBack(Client client)
        {
            int x = client.X;
            int y = client.Y;
            int z = client.Z;
            int[] res = new int[3];
            res[0] = client.X;
            res[1] = client.Y;
            res[2] = client.Z;

            if (z > 0)
            {
                //client.Z--;
                res[2] = 0;
            }
            else if (y > 0)
            {
                //client.Y--;
                res[1] = 0;
            }
            else if (x > 0)
            {
                //client.X--;
                res[0] = 0;
            }
            else
            {
                Console.WriteLine("Cannot go back from Main Page, {0}", client.Name);
            }

            return res;
        }
        #endregion

        #region isValidPosition
        private bool isValidPosition(Client client, string pos)
        {
            int[] tmpPos = new int[3];
            tmpPos[0] = client.X;
            tmpPos[1] = client.Y;
            tmpPos[2] = client.Z;

            if (pos.Equals("base"))
            {
                return ((tmpPos[0] == 0) && (tmpPos[1] == 0) && (tmpPos[2] == 0));
            }
            else if (pos.Equals("sub"))
            {
                return ((tmpPos[0] != 0) && (tmpPos[1] == 0) && (tmpPos[2] == 0));
            }
            else if (pos.Equals("thread"))
            {
                return ((tmpPos[0] != 0) && (tmpPos[1] != 0) && (tmpPos[2] == 0));
            }
            else if (pos.Equals("post"))
            {
                return ((tmpPos[0] != 0) && (tmpPos[1] != 0) && (tmpPos[2] != 0));
            }
            else
            {
                Console.WriteLine("Illegal pos");
                return false;
            }
        }
        #endregion

        #region hasModeratorPermission
        private bool hasModeratorPermission(string mName)
        {
            int res = users.getPermission(mName);
            Console.WriteLine("name: {0}, per: {1}", mName, res);
            if (res > 2)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region hasAdminPermission
        private bool hasAdminPermission(string mName)
        {
            int res = users.getPermission(mName);
            Console.WriteLine("name: {0}, per: {1}", mName, res);
            if (res == 4)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region isMember
        private Client isMember(string userName, string passWord)
        {
            if (users.isMember(userName, passWord))
            {
                ClientDetail clientDetails = users.getUserDetails(userName);
                Client client = new Client
                {
                    Name = userName,
                    FirstName = clientDetails.name,
                    LastName = clientDetails.lastName,
                    PassWord = passWord,
                    Email = clientDetails.email,
                    Permission = clientDetails.permission,
                    X = 0,
                    Y = 0,
                    Z = 0
                };
                return client;
            }
            return null;
        }
        #endregion

        #region isPostOwner
        private bool isPostOwner(Position pos, string client)
        {
            return dataManager.getPostOwners(pos).Equals(client);
        }
        #endregion

        #region isThreadOwner
        private bool isThreadOwner(Position pos, string client)
        {
            return dataManager.getThreadOwners(pos).Equals(client);
        }
        #endregion

        #region isSubForumModerator
        private bool isSubForumModerator(Position pos, string client)
        {
            return dataManager.isSubModerator(pos, client);
        }
        #endregion

        #region canRemovePost
        private bool canRemovePost(Position pos, string client)
        {
            return ((isSubForumModerator(pos, client)) ||
                (hasAdminPermission(client)) ||
                (isPostOwner(pos, client)));
        }
        #endregion

        #region canRemoveThread
        private bool canRemoveThread(Position pos, string client)
        {
            return ((isSubForumModerator(pos, client)) ||
                (hasAdminPermission(client)) ||
                (isThreadOwner(pos, client)));
        }
        #endregion

        #region isRegisteredUser
        private bool isRegisteredUser(string userName)
        {
            return users.isUser(userName);
        }
        #endregion

        #region notifyAllPosted
        private void notifyAllPosted(Position pos, string msg)
        {
            List<string> toNotifyUsers = dataManager.getPostedUsers(pos);
            foreach (Client c in clients.Keys)
            {
                if (toNotifyUsers.Contains(c.Name))
                {
                    IMessageCallback callBack = clients[c];
                    callBack.receiveNotify(msg);
                }
            }
        } 
        #endregion

        #region notifyAll
        private void notifyAll(Position pos, string msg)
        {
            foreach (Client c in clients.Keys)
            {
                IMessageCallback callBack = clients[c];
                callBack.receiveNotify(msg);
            }
        }
        #endregion

        #endregion

    }
}
#endregion