using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;

namespace WCFService
{
    //interface declarations just like the client but the callback 
    //decleration is a little different
    [ServiceContract]
    interface IMessageCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnMessageAdded(string message, DateTime timestamp);
    }

    //This is a little different than the client 
    // in that we need to state the SessionMode as required or it will default to "notAllowed"
    [ServiceContract(CallbackContract = typeof(IMessageCallback),SessionMode = SessionMode.Required)]
    public interface IMessage
    {
        [OperationContract]
        void AddMessage(string message);
        [OperationContract]
        bool Subscribe();
        [OperationContract]
        bool Unsubscribe();
    }

     [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    class RCRServer : IMessage
    {
        private static  List<IMessageCallback> subscribers = new List<IMessageCallback>();
        public ServiceHost host = null;
    

        public void Connect()
        {
            //I'm doing this next part progromatically instead of in app.cfg 
            // because I think it makes it easier to understand (and xml is stupid)
            using (ServiceHost host = new ServiceHost(
                typeof(RCRServer),
                new Uri("net.tcp://localhost:8000")))
            {
                //notice the NetTcpBinding?  This allows programs instead of web stuff
                // to communicate with each other
                host.AddServiceEndpoint(typeof(IMessage),
                  new NetTcpBinding(),
                  "ISubscribe");
                
                
                try
                {
                    host.Open();
                    Console.WriteLine("Successfully opened port 8000.");
                    Console.ReadLine();
                    host.Close();
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
             
            }
        }

        

        public bool Subscribe()
        {
            try
            {
                //Get the hashCode of the connecting app and store it as a connection
                IMessageCallback callback = OperationContext.Current.GetCallbackChannel<IMessageCallback>();
                if (!subscribers.Contains(callback))
                    subscribers.Add(callback);
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool Unsubscribe()
        {
            try
            {
                //remove any connection that is leaving
                IMessageCallback callback = OperationContext.Current.GetCallbackChannel<IMessageCallback>();
                if (subscribers.Contains(callback))
                    subscribers.Remove(callback);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void AddMessage(String message)
        {
            //Go through the list of connections and call their callback funciton
            subscribers.ForEach(delegate(IMessageCallback callback)
            {
                if (((ICommunicationObject)callback).State == CommunicationState.Opened)
                {
                    Console.WriteLine("Calling OnMessageAdded on callback ({0}).", callback.GetHashCode());
                    callback.OnMessageAdded(message, DateTime.Now);
                }
                else
                {
                    subscribers.Remove(callback);
                }
            });

        }
    }
}
