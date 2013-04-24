using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.DataBase
{
    interface ForumPages
    {
        Page getPage(Position p, int size, int pageNumber);

        void addPost(Position p, string title, string body, string clientID, DateTime time);

        void addThread(Position p, string title, string body, string clientID, DateTime time);

        Boolean editPost(Position p, string title, string body, string clientID, DateTime time);

        Boolean editThread(Position p, string title, string body, string clientID, DateTime time);

        void removePost(Position p);

        void removeThread(Position p);

        void addSubForum(string title, string moderator, DateTime time);

        void removeSubForum(Position p);

        bool isModerator(int position, string moderator);

        bool isSubModerator(Position position, string moderator);

        void addSubforumModerator(int position, string moderator);

        void removeModerator(int position, string moderator);

        string getPostOwners(Position pos);

        string getThreadOwners(Position pos);

        int getMessageCount(int subPos);

        int getUserMessageCount(int subPos, string userName);

        int getUserMessageCount(string userName);

        int getModeratorCount(int subPos);

        DateTime getMessageCreationTime(Position p);

        DateTime getMessageeditedTime(Position p);

        DateTime getLastMessageTime(int subPos, string userName);

        List<string> getPostedUsers(Position p);

        string getThreadTitle(Position p);

        string getPostTitle(Position p);

        string getSubForumTitle(Position p);

    }
}
