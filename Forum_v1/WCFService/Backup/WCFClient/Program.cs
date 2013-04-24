using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
namespace WCFClient
{
    

    class Program
    {
        static void Main(string[] args)
        {
            RCRProxy rp = new RCRProxy();
            if (rp.Connect() == true)
            {
                string tmp = Console.ReadLine();
                while (tmp != "EXIT")
                {
                    rp.SendMessage(tmp);
                    tmp = Console.ReadLine();
                }
            }
            if(((ICommunicationObject)rp).State == CommunicationState.Opened)
                rp.Close();
            
        }
    }
}
