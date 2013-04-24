using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using Common;
using Common.DataBase;

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
                    rp.sendCommand(tmp);
                    //rp.getPage(Int32.Parse(tmp), 10, 0);
                    //Page p = rp.getPage(3);
                    //Console.WriteLine(p.eli);
                    tmp = Console.ReadLine();
                }
            }
            if (((ICommunicationObject)rp).State == CommunicationState.Opened)
            {
                rp.Close();
            }
        }
    }
}
