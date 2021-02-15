using CarRentalServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Windows.Controls;

namespace CarRentalSevices
{ 

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IRentalService
    {   
        [OperationContract]
        int AddClient(Client client);

        [OperationContract]
        int UpdateClient(Client client);

        [OperationContract]
        int DeleteClient(Client client);

        [OperationContract]
        List<Client> GetAllClients();

        [OperationContract]
        Client SearchClientById(int id);

        [OperationContract]
        List<Client> SearchClient(string column, string value);




        [OperationContract]
        int AddCar(Car car);

        [OperationContract]
        int UpdateCar(Car car);

        [OperationContract]
        int DeleteCar(Car car);

        [OperationContract]
        int UpdateCarImage(Car car);
      

        [OperationContract]
        List<Car> GetAllCars();

        [OperationContract]
        List<Car> SearchCar(string column, string value);

        [OperationContract]
        Car SearchCarById(int id);

        [OperationContract]
        int NextCarId();




        [OperationContract]
        List<Rental> GetAllRentals();

        [OperationContract]
        int AddRental(Rental rental);

        [OperationContract]
        Rental GetRentalById(int id);

        [OperationContract]
        int NextRentalId();


        [OperationContract]
        int UpdateRental(Rental rental);


        [OperationContract]
        int CloseRental(Rental rental);



        [OperationContract]
        int Login(string userName, string password);

        [OperationContract]
        User GetUserByeUserName(string userName);




        [OperationContract]
        RentalData GetReportData(int id);                    


    }

    [DataContract]
    public class RentalData
    {
        public RentalData()
        {
            this.RentalsTable = new DataTable("RentalsData");
        }

        [DataMember]
        public DataTable RentalsTable { get; set; }
    }
}
