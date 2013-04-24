using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;


namespace Common.DataBase
{
    [DataContract]
    public class ClientDetail
    {
        [DataMember]
        public string name{get; set;}
        [DataMember]
        public string lastName {get; set;}
        [DataMember]
        public string password{get;set;}
        [DataMember]
        public string email{get; set;}
        [DataMember]
        public int permission{get; set;} 
    }
}
