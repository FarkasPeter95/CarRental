using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServices
{
    [DataContract]
    public class Rental
    {
        private int id;
        private int carid;
        private int clientid;
        private DateTime datetime;
        private bool status;
        private DateTime dateout;
        private DateTime? datein;
        private int kmout;
        private int kmin;
        private int kmsdriven;
        private string renttype;
        private int duration;
        private int total;
        private int cost;
        private int discount;
        private int balance;
        private int advance;
        private int issueby;

        [DataMember]
        public int Id { get => id; set => id = value; }
        [DataMember]
        public int Carid { get => carid; set => carid = value; }
        [DataMember]
        public int Clientid { get => clientid; set => clientid = value; }
        [DataMember]
        public DateTime Datetime { get => datetime; set => datetime = value; }
        [DataMember] 
        public bool Status { get => status; set => status = value; }
        [DataMember] 
        public DateTime Dateout { get => dateout; set => dateout = value; }
        

        [DataMember]
        public DateTime? Datein
        {
            get
            {
                if (datein == default(DateTime))
                    return null;
                else
                    return datein;
            }
            set => datein = value;
        }        

        [DataMember] 
        public int Kmout { get => kmout; set => kmout = value; }
        [DataMember] 
        public int Kmin { get => kmin; set => kmin = value; }
        [DataMember] 
        public int Kmsdriven { get => kmsdriven; set => kmsdriven = value; }
        [DataMember] 
        public string Renttype { get => renttype; set => renttype = value; }
        [DataMember] 
        public int Duration { get => duration; set => duration = value; }
        [DataMember] 
        public int Total { get => total; set => total = value; }
        [DataMember] 
        public int Cost { get => cost; set => cost = value; }
        [DataMember] 
        public int Discount { get => discount; set => discount = value; }
        [DataMember] 
        public int Balance { get => balance; set => balance = value; }
        [DataMember] 
        public int Advance { get => advance; set => advance = value; }
        [DataMember] 
        public int Issueby { get => issueby; set => issueby = value; }
    }
}
