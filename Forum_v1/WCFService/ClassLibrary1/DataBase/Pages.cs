using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace Common.DataBase
{
    [DataContract]
    public class Pages : ForumPages
    {
        #region Fields
        [DataMember]
        private Page subForums;
        [DataMember]
        List<int> SFMessageCount;
        [DataMember]
        List<SortedDictionary<string, int>> userMsg;
        [DataMember]
        private SortedDictionary<int, List<string>> subForumsModerator;
        [DataMember]
        private SortedDictionary<int, Page> threads;
        [DataMember]
        private SortedDictionary<int, SortedDictionary<int, Page>> posts;
        [DataMember]
        private SortedDictionary<int, SortedDictionary<string, DateTime>> lastmsgTime;
        #endregion

        #region Constructor
        public Pages()
        {
            subForums = new Page { header = "Main Forum" };
            threads = new SortedDictionary<int, Page>();
            subForumsModerator = new SortedDictionary<int, List<string>>();
            posts = new SortedDictionary<int, SortedDictionary<int, Page>>();
            SFMessageCount = new List<int>();
            userMsg = new List<SortedDictionary<string, int>>();
            lastmsgTime = new SortedDictionary<int, SortedDictionary<string, DateTime>>();

        }
        #endregion

        #region Get Page
        public Page getPage(Position p, int size, int pageNumber)
        {
            string tmpHeader = null;

            if (p.x == 0)
            {
                tmpHeader = subForums.header;
                return subForums.createPage(size, pageNumber, tmpHeader);
            }
            else if (p.y == 0)
            {
                tmpHeader = "SubForum: " + subForums.elementAt(p.x).Title;
                return threads[p.x].createPage(size, pageNumber, tmpHeader);
            }
            else
            {
                tmpHeader = "Thread: " + threads[p.x].elementAt(p.y).Title;
                return posts[p.x][p.y].createPage(size, pageNumber, tmpHeader);
            }
        }
        #endregion

        #region Add Post
        public void addPost(Position p, string title, string body, string clientID, DateTime time)
        {
            posts[p.x][p.y].addMessage(new Message(title, body, clientID));
            SFMessageCount.Insert(p.x - 1, SFMessageCount.ElementAt(p.x - 1) + 1);
            try
            {
                userMsg.ElementAt(p.x - 1)[clientID] = userMsg.ElementAt(p.x - 1)[clientID] + 1;
            }
            catch (KeyNotFoundException e)
            {
                userMsg.ElementAt(p.x - 1).Add(clientID, 1);
            }
            try
            {
                lastmsgTime[p.x][clientID] = time;
            }
            catch (KeyNotFoundException e)
            {
                lastmsgTime[p.x].Add(clientID, time);
            }
        }
        #endregion

        #region Add Thread
        public void addThread(Position p, string title, string body, string clientID, DateTime time)
        {
            threads[p.x].addMessage(new Message(title, body, clientID));
            posts[p.x].Add(threads[p.x].length(), new Page { header = "Thread" });
            posts[p.x][threads[p.x].length()].addMessage(new Message(title, body, clientID));
            SFMessageCount.Insert(p.x - 1, SFMessageCount.ElementAt(p.x - 1) + 1);
            try
            {
                userMsg.ElementAt(p.x - 1)[clientID] = userMsg.ElementAt(p.x - 1)[clientID] + 1;
            }
            catch (KeyNotFoundException e)
            {
                userMsg.ElementAt(p.x - 1).Add(clientID, 1);
            }
            try
            {
                lastmsgTime[p.x][clientID] = time;
            }
            catch (KeyNotFoundException e)
            {
                lastmsgTime[p.x].Add(clientID, time);
            }
        }
        #endregion

        #region Edit Post
        public Boolean editPost(Position p, string title, string body, string clientID, DateTime time)
        {
            if (posts[p.x][p.y].elementAt(p.z).ID.Equals(clientID))
            {
                posts[p.x][p.y].elementAt(p.z).Title = title;
                posts[p.x][p.y].elementAt(p.z).Body = body;
                return true;
            }
            return false;
        }
        #endregion

        #region Edit Thread
        public Boolean editThread(Position p, string title, string body, string clientID, DateTime time)
        {
            if (threads.ElementAt(p.x).Value.elementAt(p.y).ID.Equals(clientID))
            {
                threads[p.x].elementAt(p.y).Title = title;
                threads[p.x].elementAt(p.y).Body = body;
                posts[p.x][p.y].elementAt(1).Title = title;
                posts[p.x][p.y].elementAt(1).Body = body;
                return true;
            }
            return false;
        }
        #endregion

        #region Remove Post
        public void removePost(Position p)
        {
            posts[p.x][p.y].elementAt(p.z).Title = "Deleted";
            posts[p.x][p.y].elementAt(p.z).Body = "";
            userMsg.ElementAt(p.x - 1)[posts[p.x][p.y].elementAt(p.z).ID]--;
        }
        #endregion

        #region Remove Thread
        public void removeThread(Position p)
        {
            threads[p.x].elementAt(p.y).Title = "Deleted";
            threads[p.x].elementAt(p.y).Body = "";
            for (int i = 0; i < posts[p.x][p.y].length(); i++)
            {
                userMsg.ElementAt(p.x - 1)[posts[p.x][p.y].elementAt(i + 1).ID]--;
                posts[p.x][p.y].elementAt(i + 1).Title = "Deleted";
                posts[p.x][p.y].elementAt(i + 1).Body = "";
            }
        }
        #endregion

        #region Add Sub Forum
        public void addSubForum(string title, string moderator, DateTime time)
        {
            subForumsModerator.Add(subForums.length() + 1, new List<string>());
            addSubforumModerator(subForums.length() + 1, moderator);
            subForums.addMessage(new Message(title, null, null));
            threads.Add(subForums.length(), new Page { header = "SubForum" });
            posts.Add(subForums.length(), new SortedDictionary<int, Page>());
            SFMessageCount.Add(0);
            userMsg.Add(new SortedDictionary<string, int>());
            lastmsgTime.Add(subForums.length(), new SortedDictionary<string, DateTime>());
        }
        #endregion

        #region Remove Sub Forum
        public void removeSubForum(Position p)
        {
            foreach (string v in userMsg.ElementAt(p.x - 1).Keys)
                userMsg.ElementAt(p.x - 1)[v] = 0;
            SFMessageCount.Insert(p.x - 1, 0);
            subForums.elementAt(p.x).Title = "Deleted";
            subForums.elementAt(p.x).Body = "";
            subForumsModerator.Remove(p.x);
            for (int i = 0; i < threads[p.x].length(); i++)
            {
                removeThread(new Position { x = p.x, y = i + 1, z = 0 });
            }
        }
        #endregion

        #region Is A moderator
        public bool isModerator(int position, string moderator)
        {
            return subForumsModerator[position].Contains(moderator);

        }
        #endregion

        #region isSubModerator
        public bool isSubModerator(Position position, string moderator)
        {
            return subForumsModerator[position.x].Contains(moderator);
        }
        #endregion

        #region Add Moderator
        public void addSubforumModerator(int position, string moderator)
        {
            subForumsModerator[position].Add(moderator);

        }
        #endregion

        #region getPostOwners
        public string getPostOwners(Position pos)
        {
            return posts[pos.x][pos.y].elementAt(pos.z).ID;
        }
        #endregion

        #region getThreadOwners
        public string getThreadOwners(Position pos)
        {
            return threads[pos.x].elementAt(pos.y).ID;
        }
        #endregion

        #region Get Message Count
        public int getMessageCount(int subPos)
        {
            return SFMessageCount.ElementAt(subPos - 1);
        }
        #endregion

        #region Get User Message Count
        public int getUserMessageCount(int subPos, string userName)
        {
            try
            {
                return userMsg.ElementAt(subPos)[userName];
            }
            catch (KeyNotFoundException e)
            {
                return 0;
            }
        }
        #endregion

        #region Get User Message Count
        public int getUserMessageCount(string userName)
        {
            int res = 0;
            for (int i = 0; i < subForums.length(); i++)
                res += getUserMessageCount(i, userName);

            return res;
        }
        #endregion

        #region Get Moderator Count
        public int getModeratorCount(int subPos)
        {
            return subForumsModerator[subPos].Count;
        }
        #endregion

        #region Get Message Creation Time
        public DateTime getMessageCreationTime(Position p)
        {
            if (p.z == 0)
            {
                return threads[p.x].elementAt(p.y).CreationTime;
            }
            else
                return posts[p.x][p.y].elementAt(p.z).CreationTime;
        }
        #endregion

        #region Get Message Edited Time
        public DateTime getMessageeditedTime(Position p)
        {
            if (p.z == 0)
            {
                return threads[p.x].elementAt(p.y).EditedTime;
            }
            else
                return posts[p.x][p.y].elementAt(p.z).EditedTime;
        }
        #endregion

        #region Get User Last Message In Sub Forum Time
        public DateTime getLastMessageTime(int subPos, string userName)
        {
            return lastmsgTime[subPos][userName];
        }
        #endregion

        #region Get All User Which Posted To Thread
        public List<string> getPostedUsers(Position p)
        {
            List<string> users = new List<string>();
            for (int i = 0; i < posts[p.x][p.y].length(); i++)
                users.Add(posts[p.x][p.y].elementAt(i + 1).ID);
            return users;
        }
        #endregion

        #region Remove Moderator
        public void removeModerator(int position, string moderator)
        {
            subForumsModerator[position].Remove(moderator);
        }
        #endregion

        #region Get Thread Title
        public string getThreadTitle(Position p)
        {
            return threads.ElementAt(p.x).Value.elementAt(p.y).Title;
        }
        #endregion

        #region Get Post Title
        public string getPostTitle(Position p)
        {
            return posts[p.x][p.y].elementAt(p.z).Title;
        }
        #endregion

        #region Get Sub Forum Title
        public string getSubForumTitle(Position p)
        {
            return subForums.elementAt(p.x).Title;
        }
        #endregion
    }
}
