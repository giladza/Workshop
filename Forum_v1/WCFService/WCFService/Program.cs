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
            ForumServer server = new ForumServer();
            server.Connect();
        }
    }
}
