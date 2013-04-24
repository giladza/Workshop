using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace Common.DataBase
{
    [DataContract]
    public class Page
    {
        [DataMember]
        public SortedDictionary<int, Message> messages { get; set;}
        [DataMember]
        public string header { get; set; }

        public Page()
        {
            this.messages = new SortedDictionary<int, Message>();
        }

        private Page(SortedDictionary<int, Message> posts)
        {
            this.messages = posts;
        }

        public Message elementAt(int position)
        {
            Console.WriteLine("msg: {0},", messages == null);
            return messages[position];
        }

        public int length()
        {
            return messages.Count;
        }

        public Page createPage(int size, int pageNumber, string parentPage)
        {
            SortedDictionary<int, Message> p = new SortedDictionary<int, Message>();
            for (int i = 0; i < size && i < messages.Count; i++)
            {
                //Console.WriteLine("val: {0}", size * pageNumber + i + 1);
                p.Add(i + 1, messages[size * pageNumber + i + 1]);
            }
            Page res = new Page(p);
            res.header = parentPage;
            return res;
        }

        internal void addMessage(Message message)
        {
            messages.Add(length() + 1, message);
        }

        internal void removeMessage(int p)
        {
            messages.Remove(p);
        }
        
        public override string ToString()
        {
            string uLine = new string('\u0332', header.Length);

            string res = header + "\n" + uLine + "\n\n";
            foreach (int msg in messages.Keys)
            {
                Message tmpMsg = messages[msg];
                res += "(" + msg + ")" + "\tTitle: " + tmpMsg.Title + "\n" +
                       "\tBody: " + tmpMsg.Body + "\n\n";
            }
            res += "\n>> ";
            return res;
        }


        #region Gilad's additions

       

        #endregion

    }
}
