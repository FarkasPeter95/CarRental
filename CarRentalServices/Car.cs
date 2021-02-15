using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalServices
{

    [DataContract]
    public class Car
    {
        private int id;
        private string licenseplate;
        private string manufacturer;
        private string model;
        private int year;
        private int kmclock;
        private string vin;
        private string fuel;
        private string color;
        private int rentalpriceperhour;
        private int rentalpriceperday;
        private int seats;
        private bool available;
        private string image; 


        [DataMember]
        public int Id { get => id; set => id = value; }
        [DataMember]
        public string Licenseplate { get => licenseplate; set => licenseplate = value; }
        [DataMember] 
        public string Manufacturer { get => manufacturer; set => manufacturer = value; }
        [DataMember] 
        public string Model { get => model; set => model = value; }
        [DataMember] 
        public int Year { get => year; set => year = value; }
        [DataMember] 
        public int Kmclock { get => kmclock; set => kmclock = value; }
        [DataMember]
        public string Vin { get => vin; set => vin = value; }
        [DataMember] 
        public string Fuel { get => fuel; set => fuel = value; }
        [DataMember] 
        public string Color { get => color; set => color = value; }
        [DataMember] 
        public int Rentalpriceperhour { get => rentalpriceperhour; set => rentalpriceperhour = value; }
        [DataMember] 
        public int Rentalpriceperday { get => rentalpriceperday; set => rentalpriceperday = value; }
        [DataMember] 
        public int Seats { get => seats; set => seats = value; }
        [DataMember]
        public bool Available { get => available; set => available = value; }
        [DataMember]
        public string Image { get => image; set => image = value; }
    }
}


