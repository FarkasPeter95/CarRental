using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace CarRentalServices
{
    [DataContract]
    public class Client : IDataErrorInfo
    {

        private int id;
        private string idcardnumber;
        private string name;
        private DateTime birthdate;
        private string zipcode;
        private string city;
        private string adress;
        private string phonenumber;


        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                string result = null;

                switch (columnName)
                {

                    case "Name":
                        if (string.IsNullOrEmpty(Name))
                        {
                            result = "A nevet kötelező megadni!";
                        }
                        break;
                    case "Birthdate":
                        if (GetDifferenceInYears(Birthdate, DateTime.Today) < 18)
                        {
                            result = "Nem töltötte be a 18-at!";
                        }
                        break;
                    case "Idcardnumber":
                        if (string.IsNullOrEmpty(Idcardnumber))
                        {
                            result = "Kötelező megadni!";
                        }
                        else if (!Regex.IsMatch(Idcardnumber, @"^[A-Z]{2}[0-9]{6}$") && !Regex.IsMatch(Idcardnumber, @"^[0-9]{6}[A-Z]{2}$"))
                        {
                            result = "Nem megfelelő formátum";
                        }
                        break;
                    case "Zipcode":
                        if (string.IsNullOrEmpty(Zipcode))
                        {
                            result = "Kötelező megadni!";
                        }
                        else if (!Regex.IsMatch(Zipcode, @"^[1-9][0-9][0-9][0-9]$"))
                        {
                            result = "Nem megfelelő formátum";
                        }
                        break;

                }      
                return result;
            }
        }

        int GetDifferenceInYears(DateTime startDate, DateTime endDate)
        {
            return (endDate.Year - startDate.Year - 1) +
                (((endDate.Month > startDate.Month) ||
                ((endDate.Month == startDate.Month) && (endDate.Day >= startDate.Day))) ? 1 : 0);
        }

        [DataMember]
        public int Id { get => id; set => id = value; }
        [DataMember]
        public string Idcardnumber { get => idcardnumber; set => idcardnumber = value; }
        [DataMember]
        public string Name { get => name; set => name = value; }
        [DataMember] 
        public DateTime Birthdate { get => birthdate; set => birthdate = value; }
        [DataMember] 
        public string Zipcode { get => zipcode; set => zipcode = value; }
        [DataMember] 
        public string City { get => city; set => city = value; }
        [DataMember] 
        public string Adress { get => adress; set => adress = value; }
        [DataMember] 
        public string Phonenumber { get => phonenumber; set => phonenumber = value; }

        public string Error { get { return null; } }
    }
}
