using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using WCFService;

namespace WCFService
{
    static class Program
    {
        
        static void Main()
        {
            RCRServer server = new RCRServer();
            server.Connect();
        }
    }
}
