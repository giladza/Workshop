using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace Common
{
    [DataContract]
    public class Client
    {
        private string userName;
        private int ID;
        private DateTime time;
        private string lastName;
        private string firstName;
        private string email;
        private string password;
        private int permission;
        private int X_Loc;
        private int Y_Loc;
        private int Z_Loc;

        [DataMember]
        public string Name
        {
            get { return userName; }
            set { userName = value; }
        }
        
        [DataMember]
        public int User_ID
        {
            get { return ID; }
            set { ID = value; }
        }

        [DataMember]
        public DateTime Time
        {
            get { return time; }
            set { time = value; }
        }

        [DataMember]
        public int X
        {
            get { return X_Loc; }
            set { X_Loc = value; }
        }

        [DataMember]
        public int Y
        {
            get { return Y_Loc; }
            set { Y_Loc = value; }
        }

        [DataMember]
        public int Z
        {
            get { return Z_Loc; }
            set { Z_Loc = value; }
        }

        [DataMember]
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        [DataMember]
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        [DataMember]
        public string PassWord
        {
            get { return password; }
            set { password = value; }
        }

        [DataMember]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        [DataMember]
        public int Permission
        {
            get { return permission; }
            set { permission = value; }
        }

    }
}
