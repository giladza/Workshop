using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.DataBase
{
    public class InitData
    {
        #region initial values
        public static List<Object> initForumData()
        {
            Pages forum = new Pages();
            Users users = new Users();
            List<Object> res = new List<Object>();

            //admin
            users.register("Gilad", "Sabari", "G", "123", "bla@bla.com");
            users.setPermission("G", 4);

            //moderator
            users.register("Eli", "Beli", "Elicho", "456", "bla1@bla.com");
            users.setPermission("Elicho", 3);

            //regular
            users.register("Edward", "R", "Edi", "789", "bla2@bla.com");
            users.setPermission("Edi", 2);

            forum.addSubForum("Games", "Elicho",DateTime.Now);
            forum.addSubForum("Movies", "Elicho", DateTime.Now);

            forum.addThread(new Position { x = 1, y = 0, z = 0 }, "Fifa", "Sport game", "Edi", DateTime.Now);
            forum.addThread(new Position { x = 1, y = 0, z = 0 }, "IgI", "Computer game", "Edi", DateTime.Now);

            forum.addThread(new Position { x = 2, y = 0, z = 0 }, "Titanic", "Boring movie", "Edi", DateTime.Now);
            forum.addThread(new Position { x = 2, y = 0, z = 0 }, "Matrix", "Owsom movie!!!", "Edi", DateTime.Now);

            forum.addPost(new Position { x = 1, y = 1, z = 0 }, "Love it", "Very good game...", "Edi", DateTime.Now);
            forum.addPost(new Position { x = 1, y = 1, z = 0 }, "Hate it", "Very bad game...", "Edi", DateTime.Now);

            forum.addPost(new Position { x = 1, y = 2, z = 0 }, "nice game", "Very good game...", "Edi", DateTime.Now);
            forum.addPost(new Position { x = 1, y = 2, z = 0 }, "fichsa game", "Very bad game...", "Edi", DateTime.Now);

            forum.addPost(new Position { x = 2, y = 1, z = 0 }, "Love it", "Very good movie...", "Edi", DateTime.Now);
            forum.addPost(new Position { x = 2, y = 2, z = 0 }, "Hate it", "Very bad movie...", "Edi", DateTime.Now);


            res.Add(users);
            res.Add(forum);

            return res;
        }
        #endregion
    }
}
