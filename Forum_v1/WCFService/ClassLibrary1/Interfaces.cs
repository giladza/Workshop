using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Common.DataBase;

namespace Common
{

    #region IMessageCallback
    //CallBack Interface
    [ServiceContract]
    public interface IMessageCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnMessageAdded(string message, DateTime timestamp);
        [OperationContract(IsOneWay = true)]
        void receivePage(Page page);
        [OperationContract(IsOneWay = true)]
        void receiveJoin(Client client, bool ans, string msg);
        [OperationContract(IsOneWay = true)]
        void receiveLogout();
        [OperationContract(IsOneWay = true)]
        void receiveNotify(string notifyMsg);
        [OperationContract(IsOneWay = true)]
        void receiveStatistics(string statMsg);
    }
    #endregion

    #region IMessage
    //Interface
    [ServiceContract(CallbackContract = typeof(IMessageCallback), SessionMode = SessionMode.Required)]
    public interface IMessage
    {
        [OperationContract]
        void AddMessage(string message);
        [OperationContract]
        bool Subscribe();
        [OperationContract]
        bool Unsubscribe();
        [OperationContract]
        void sendMsg(string msg, int cb);


        //Edi, Start here!!!
        [OperationContract(IsOneWay = true)]
        void Login(string details);
        [OperationContract(IsOneWay = true, IsTerminating = true)]
        void Logout(Client client);
        [OperationContract(IsOneWay = true)]
        void GetPost(Client client);
        [OperationContract(IsOneWay = true)]
        void GetPage(Client client, int position, int size, int pageNumber);
        [OperationContract(IsOneWay = true)]
        void Back(Client client);
        [OperationContract(IsOneWay = true)]
        void Register(Client client);
        [OperationContract(IsOneWay = true)]
        void AddPost(Client client, string title, string body);
        [OperationContract(IsOneWay = true)]
        void AddThread(Client client, string title, string body);
        [OperationContract(IsOneWay = true)]
        void EditPost(Client client, int pos, string title, string body);
        [OperationContract(IsOneWay = true)]
        void EditThread(Client client, int pos, string title, string body);
        [OperationContract(IsOneWay = true)]
        void AddSubForum(Client client, string title, string mName);
        [OperationContract(IsOneWay = true)]
        void RemoveSubForum(Client client, int position);
        [OperationContract(IsOneWay = true)]
        void RemovePost(Client client, int position);
        [OperationContract(IsOneWay = true)]
        void RemoveThread(Client client, int position);
        [OperationContract(IsOneWay = true)]
        void AddModerator(Client client, int pos, string userName);
        [OperationContract(IsOneWay = true)]
        void RemoveModerator(Client client, int position, string userName);
        [OperationContract(IsOneWay = true)]
        void ChangeModerator(Client client, string toRemove, string toAdd, int position);
        [OperationContract(IsOneWay = true)]
        void SubForumMsgCount(Client client, int position);
        [OperationContract(IsOneWay = true)]
        void MemberMsgCount(Client client, string userName);

        [OperationContract(IsOneWay = true)]
        void RefreshPage(Client client);

    }
    #endregion

}
