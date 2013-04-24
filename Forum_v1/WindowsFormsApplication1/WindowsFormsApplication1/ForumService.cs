using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.ServiceModel;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public class ForumService : IMessageCallback, IDisposable
    {
        IMessage pipeProxy = null;
        Client me = null;
        bool isLoggedIn = false;

        public ForumService()
        {
            initProxy();
        }

        public void initProxy()
        {
            DuplexChannelFactory<IMessage> pipeFactory =
                  new DuplexChannelFactory<IMessage>(
                      new InstanceContext(this),
                      new NetTcpBinding(),
                      new EndpointAddress("net.tcp://localhost:8000/ISubscribe"));
            try
            {
                pipeProxy = pipeFactory.CreateChannel();
            }
            catch (Exception e)
            {
            }
        }

        public void logout()
        {
        }

        
        public string[] getUserList()
        {
            String[] ans = { "rut", "boot", "moot" };
            return ans;
        }

        public string[] getSubForumList()
        {
            String[] ans = { "rut1111", "boot1111", "moot1111" };
            return ans;
        }
        public string[] getThreadList(int sub)
        {
            String[] ans = { "rut222", "boot2222", "moot2222" };
            return ans;
        }

        public string[] getPostList(int sub)
        {
            String[] ans = { "rut3333", "boot3333", "moot3333" };
            return ans;
        }

        public Boolean promote(string p)
        {
            return true;
        }

        public Boolean register(string p, string p_2, string p_3)
        {
            return true;
        }

        public Boolean login(string p, string p_2)
        {
            //new FormError("bla").Show();
            this.isLoggedIn = false;
            string loginMsg = "login " + p + " " + p_2;
            this.pipeProxy.Login(loginMsg);

            //new FormError("bla").Show();
            //while (!isLoggedIn) { }

            return true;
        }

        internal Boolean postReply(string p)
        {
            return true;
        }

        internal Boolean newThread(string p, string p_2)
        {
            return true;
        }

        internal Boolean newSub(string p, string p_2)
        {
            return true;
        }


        // Callback

        public void OnMessageAdded(string message, DateTime timestamp)
        {
            throw new NotImplementedException();
        }

        public void receivePage(Common.DataBase.Page page)
        {
            throw new NotImplementedException();
        }

        public void receiveJoin(Client client, bool ans, string systemMsg)
        {
            MessageBox.Show("got join");
            if (!ans)
            {
                isLoggedIn = false;
            }
            else
            {
                isLoggedIn = true;
                this.me = client;
            }
            //new FormError("logged in").Show();
        }

        public void receiveLogout()
        {
            throw new NotImplementedException();
        }

        public void receiveNotify(string notifyMsg)
        {
            throw new NotImplementedException();
        }

        public void receiveStatistics(string statMsg)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
