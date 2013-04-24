using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace Common.DataBase
{
    [DataContract]
    public class Message
    {
        [DataMember]
        private string title;
        [DataMember]
        private string body;
        [DataMember]
        private string id;
        [DataMember]
        private DateTime creationTime;
        [DataMember]
        private DateTime editTime;

        public Message(string title,string body,string id)
        {
            this.title = title;
            this.body = body;
            this.id = id;
        }

        public string Title
        {
            get { return title; }
            set { this.title = value; }
        }

        public string Body
        {
            get { return body; }
            set { this.body = value; }
        }

        public string ID
        {
            get { return id; }
            set { this.id = value; }
        }
        public DateTime CreationTime
        {
            get { return creationTime; }
            set { this.creationTime = value; }
        }

        public DateTime EditedTime
        {
            get { return editTime; }
            set { this.editTime = value; }
        }
    }
}
