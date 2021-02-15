using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServices
{
    [DataContract]
    public class User
    {

        private int id;
        private string username;
        private string fullname;     
        private string phonenumber;

        [DataMember]
        public int Id { get => id; set => id = value; }
        [DataMember]
        public string Username { get => username; set => username = value; }
        [DataMember]
        public string Fullname { get => fullname; set => fullname = value; }     
        [DataMember]
        public string Phonenumber { get => phonenumber; set => phonenumber = value; }
    }
}
