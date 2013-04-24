using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.DataBase
{
    interface ForumUsers
    {
        void register(string name, string lastName, string userName, string password, string email);

        Boolean isUser(string userName);

        int getPermission(string username);

        void setPermission(string username, int permission);

        Boolean isMember(string username, string password);

        ClientDetail getUserDetails(string name);
    }
}
