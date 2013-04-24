using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace Common.DataBase
{
    [DataContract]
    public class Users : ForumUsers
    {
        [DataMember]
        private SortedDictionary<string, ClientDetail> users { get; set; }

        public Users()
        {
            users = new SortedDictionary<string, ClientDetail>();
        }

        public void register(string name, string lastName, string userName, string password, string email)
        {
            if (!users.ContainsKey(userName))
            {
                ClientDetail cd = new ClientDetail { name = name, lastName = lastName, email = email, password = password, permission = 2 };
                users.Add(userName, cd);
            }
        }

        public Boolean isUser(string userName)
        {
            return users.ContainsKey(userName);
        }

        public int getPermission(string username)
        {
            if (users.ContainsKey(username))
                return users[username].permission;
            else
                return -1;
        }
        
        public void setPermission(string username,int permission)
        {
            if (users.ContainsKey(username) && permission>=1 && permission<=4)
                users[username].permission=permission;
        }
        
        public Boolean isMember(string username, string password)
        {
            return users.ContainsKey(username) && users[username].password.Equals(password);
        }

        public ClientDetail getUserDetails(string name)
        {
            return users[name];
        }

    }
}
