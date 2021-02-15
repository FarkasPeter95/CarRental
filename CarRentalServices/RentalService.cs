using CarRentalSevices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Configuration;
using System.Windows.Controls;
using System.IO;

namespace CarRentalServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class RentalService : IRentalService
    {

        public RentalService()
        {
            ConnectToDB();
        }
            
            
        SqlConnection conn;
        SqlCommand comm;

        SqlConnectionStringBuilder connStingBuilder;

        
        void ConnectToDB() {
            connStingBuilder = new SqlConnectionStringBuilder();
            connStingBuilder.DataSource = "PETER-PC\\SQLEXPRESS";
            connStingBuilder.InitialCatalog = "CarRental";
            connStingBuilder.Encrypt = true;
            connStingBuilder.TrustServerCertificate = true;
            connStingBuilder.ConnectTimeout = 30;
            connStingBuilder.AsynchronousProcessing = true;
            connStingBuilder.MultipleActiveResultSets = true;
            connStingBuilder.IntegratedSecurity = true;

            conn = new SqlConnection(connStingBuilder.ToString());
            comm = conn.CreateCommand(); 

        }

        /*  public string GetData(int value)
          {
              return string.Format("You entered: {0}", value);
          }

          public CompositeType GetDataUsingDataContract(CompositeType composite)
          {
              if (composite == null)
              {
                  throw new ArgumentNullException("composite");
              }
              if (composite.BoolValue)
              {
                  composite.StringValue += "Suffix";
              }
              return composite;
          }*/


       /* public List<Car> FillDataGrid(string sql, string table)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable(table);
                da.Fill(dt);
                dg.ItemsSource = dt.DefaultView;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

        }*/
    

        public int AddClient(Client client)
        {
            try
            {
                comm.CommandText = "INSERT INTO Client VALUES (@IdCardNumber,@Name,@BirthDate,@ZipCode,@City,@Adress,@PhoneNumber)";

                comm.Parameters.AddWithValue("IdCardNumber", client.Idcardnumber);
                comm.Parameters.AddWithValue("Name", client.Name);
                comm.Parameters.AddWithValue("BirthDate", client.Birthdate);
                comm.Parameters.AddWithValue("Zipcode", client.Zipcode);
                comm.Parameters.AddWithValue("City", client.City);
                comm.Parameters.AddWithValue("Adress", client.Adress);
                comm.Parameters.AddWithValue("Phonenumber", client.Phonenumber);

                comm.CommandType = CommandType.Text;
                conn.Open();

                return comm.ExecuteNonQuery();

            }
           // System.Data.SqlTypes.SqlTypeException

            catch (Exception)
            {

               return 0;
            }
            finally {
                if (conn != null)
                {
                    conn.Close();
                }
            }

        }

        public int UpdateClient(Client client)
        {
            try
            {
                comm.CommandText = "UPDATE Client SET IdCardNumber=@IdCardNumber,Name=@Name,BirthDate=@BirthDate,ZipCode=@ZipCode,City=@City,Adress=@Adress,PhoneNumber=@PhoneNumber WHERE ClientID=@Id";
                comm.Parameters.AddWithValue("Id", client.Id);
                comm.Parameters.AddWithValue("IdCardNumber", client.Idcardnumber);
                comm.Parameters.AddWithValue("Name", client.Name);
                comm.Parameters.AddWithValue("BirthDate", client.Birthdate);
                comm.Parameters.AddWithValue("ZipCode", client.Zipcode);
                comm.Parameters.AddWithValue("City", client.City);
                comm.Parameters.AddWithValue("Adress", client.Adress);
                comm.Parameters.AddWithValue("PhoneNumber", client.Phonenumber);
                comm.CommandType = CommandType.Text;
                conn.Open();

                return comm.ExecuteNonQuery();

            }


            catch (System.Data.SqlClient.SqlException)
            {

                return 0;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }


        public int DeleteClient(Client client)
        {
            try
            {
                comm.CommandText = "DELETE Client WHERE ClientID=@Id";
                comm.Parameters.AddWithValue("Id", client.Id);
                comm.CommandType = CommandType.Text;
                conn.Open();

                return comm.ExecuteNonQuery();

            }


            catch (System.Data.SqlClient.SqlException)
            {

                return 0;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public List<Client> GetAllClients()
        {
            List<Client> clientList = new List<Client>();
            try
            {
                comm.CommandText = "SELECT * FROM Client";

                comm.CommandType = CommandType.Text;
                conn.Open();

                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    Client c = new Client();
                    c.Id = Convert.ToInt32(reader[0].ToString());
                    c.Idcardnumber = reader[1].ToString();
                    c.Name = reader[2].ToString();
                    c.Birthdate = Convert.ToDateTime(reader[3].ToString());
                    c.Zipcode = reader[4].ToString();
                    c.City = reader[5].ToString();
                    c.Adress = reader[6].ToString();
                    c.Phonenumber = reader[7].ToString();
                    clientList.Add(c);

                }
                return clientList;

            }
            catch (System.Data.SqlClient.SqlException)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public Client SearchClientById(int id)
        {
            Client c = new Client();
            try
            {
                comm.CommandText = "SELECT * FROM Client WHERE ClientID=@Id";
                comm.Parameters.AddWithValue("Id", id);
                comm.CommandType = CommandType.Text;
                conn.Open();

                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    c.Id = Convert.ToInt32(reader[0].ToString());
                    c.Idcardnumber = reader[1].ToString();
                    c.Name = reader[2].ToString();
                    c.Birthdate = Convert.ToDateTime(reader[3].ToString());
                    c.Zipcode = reader[4].ToString();
                    c.City = reader[5].ToString();
                    c.Adress = reader[6].ToString();
                    c.Phonenumber = reader[7].ToString();
                }
                return c;

            }
            catch (System.Data.SqlClient.SqlException)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public int AddCar(Car car)
        {
            try
            {
                comm.CommandText = "INSERT INTO Car VALUES (@LicensePlate,@Manufacturer,@Model,@Year,@KmClock,@Fuel,@Color,@Seats,@VIN,@RentalPricePerDay,@RentalPricePerHour,@Available,@Image)";

                comm.Parameters.AddWithValue("LicensePlate", car.Licenseplate);
                comm.Parameters.AddWithValue("Manufacturer", car.Manufacturer);
                comm.Parameters.AddWithValue("Model", car.Model);
                comm.Parameters.AddWithValue("Year", car.Year);
                comm.Parameters.AddWithValue("KmClock", car.Kmclock);
                comm.Parameters.AddWithValue("Fuel", car.Fuel);
                comm.Parameters.AddWithValue("Color", car.Color);
                comm.Parameters.AddWithValue("Seats", car.Seats);
                comm.Parameters.AddWithValue("VIN", car.Vin);
                comm.Parameters.AddWithValue("RentalPricePerDay", car.Rentalpriceperday);
                comm.Parameters.AddWithValue("RentalPricePerHour", car.Rentalpriceperhour);
                comm.Parameters.AddWithValue("Available", car.Available);
                comm.Parameters.AddWithValue("Image", car.Image);


                comm.CommandType = CommandType.Text;
                conn.Open();

                return comm.ExecuteNonQuery();

            }


            catch (System.Data.SqlClient.SqlException)
            {

                return 0;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

        }

        public int UpdateCar(Car car)
        {
            try
            {
                comm.CommandText = "UPDATE Car SET LicensePlate=@LicensePlate,Manufacturer=@Manufacturer,Model=@Model,Year=@Year,KmClock=@KmClock,Fuel=@Fuel," +
                    "Color=@Color,Seats=@Seats,VIN=@VIN,RentalPricePerDay=@RentalPricePerDay,RentalPricePerHour=@RentalPricePerHour,Available=@Available WHERE CarID=@Id";

                comm.Parameters.AddWithValue("Id", car.Id);
                comm.Parameters.AddWithValue("LicensePlate", car.Licenseplate);
                comm.Parameters.AddWithValue("Manufacturer", car.Manufacturer);
                comm.Parameters.AddWithValue("Model", car.Model);
                comm.Parameters.AddWithValue("Year", car.Year);
                comm.Parameters.AddWithValue("KmClock", car.Kmclock);
                comm.Parameters.AddWithValue("Fuel", car.Fuel);
                comm.Parameters.AddWithValue("Color", car.Color);
                comm.Parameters.AddWithValue("Seats", car.Seats);
                comm.Parameters.AddWithValue("VIN", car.Vin);
                comm.Parameters.AddWithValue("RentalPricePerDay", car.Rentalpriceperday);
                comm.Parameters.AddWithValue("RentalPricePerHour", car.Rentalpriceperhour);
                comm.Parameters.AddWithValue("Available", car.Available);

                comm.CommandType = CommandType.Text;
                conn.Open();

                return comm.ExecuteNonQuery();

            }


            catch (System.Data.SqlClient.SqlException)
            {

                throw;
                //return 0;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public int UpdateCarImage(Car car)
        {
            try
            {
                comm.CommandText = "UPDATE Car SET Image=@Image WHERE CarID=@Id";

                comm.Parameters.AddWithValue("Id", car.Id);
                comm.Parameters.AddWithValue("Image", car.Image);

                /* SqlParameter image = new SqlParameter("Image", SqlDbType.NVarChar);
                 if (car.Image != null)
                 {
                     image.Value = car.Image;
                     comm.Parameters.Add(image);
                 }
                 else
                 {
                     image.Value = DBNull.Value;
                     comm.Parameters.Add(image);
                 }*/


                comm.CommandType = CommandType.Text;
                conn.Open();

                return comm.ExecuteNonQuery();

            }


            catch (System.Data.SqlClient.SqlException)
            {

                throw;
                //return 0;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public int DeleteCar(Car car)
        {
            try
            {
                comm.CommandText = "DELETE Car WHERE CarID=@Id";
                comm.Parameters.AddWithValue("Id", car.Id);
                comm.CommandType = CommandType.Text;
                conn.Open();

                return comm.ExecuteNonQuery();

            }


            catch (System.Data.SqlClient.SqlException)
            {

                return 0;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public List<Car> GetAllCars()
        {
            List<Car> carList = new List<Car>();
            try
            {
                comm.CommandText = "SELECT * FROM Car";

                comm.CommandType = CommandType.Text;
                conn.Open();

                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {


                  /*  byte[] images = null;
                    FileStream stream = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(stream);
                    images = br.ReadBytes((int)stream.Length);*/

                    Car c = new Car();
                    c.Id = Convert.ToInt32(reader[0].ToString());
                    c.Licenseplate = reader[1].ToString();
                    c.Manufacturer = reader[2].ToString();
                    c.Model        = reader[3].ToString();
                    c.Year         = Convert.ToInt32(reader[4].ToString());
                    c.Kmclock      = Convert.ToInt32(reader[5].ToString());
                    c.Fuel         = reader[6].ToString();
                    c.Color        = reader[7].ToString();
                    c.Seats        = Convert.ToInt32(reader[8].ToString());
                    c.Vin          = reader[9].ToString();
                    c.Rentalpriceperday  = Convert.ToInt32(reader[10].ToString());
                    c.Rentalpriceperhour = Convert.ToInt32(reader[11].ToString());
                    c.Available = Convert.ToBoolean(reader[12].ToString());
                    c.Image = reader[13].ToString();
                    /*if (!reader.IsDBNull(13))
                    {
                        byte[] img = (byte[])(reader[13]);
                        if (img == null)
                            c.Image = null;
                        else
                        {
                            MemoryStream ms = new MemoryStream(img);
                            BinaryReader br = new BinaryReader(ms);
                            c.Image = br.ReadBytes((int)ms.Length);
                        }
                    }*/
                    // else c.Image = null;



                    // c.Image =  reader[13].ToString();

                    carList.Add(c);

                }
                return carList;

            }
            catch (System.Data.SqlClient.SqlException)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public List<Car> SearchCar(string column,string value)
        {
            List<Car> carList = new List<Car>();
            try
            {
                comm.CommandText = "SELECT * FROM Car WHERE "+ column + "='" + value + "'";
                //   comm.CommandText = "SELECT * FROM Car WHERE @Column=@Value";
                // comm.Parameters.AddWithValue("Column", column);
                // comm.Parameters.AddWithValue("Value", "'" + value + "'");
                comm.CommandType = CommandType.Text;
                conn.Open();

                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    Car c = new Car();
                    c.Id = Convert.ToInt32(reader[0].ToString());
                    c.Licenseplate = reader[1].ToString();
                    c.Manufacturer = reader[2].ToString();
                    c.Model = reader[3].ToString();
                    c.Year = Convert.ToInt32(reader[4].ToString());
                    c.Kmclock = Convert.ToInt32(reader[5].ToString());
                    c.Fuel = reader[6].ToString();
                    c.Color = reader[7].ToString();
                    c.Seats = Convert.ToInt32(reader[8].ToString());
                    c.Vin = reader[9].ToString();
                    c.Rentalpriceperday = Convert.ToInt32(reader[10].ToString());
                    c.Rentalpriceperhour = Convert.ToInt32(reader[11].ToString());
                    c.Available = Convert.ToBoolean(reader[12].ToString());
                    c.Image = reader[13].ToString();
                   /* if (!reader.IsDBNull(13))
                    {
                        byte[] img = (byte[])(reader[13]);
                        if (img == null)
                            c.Image = null;
                        else
                        {
                            MemoryStream ms = new MemoryStream(img);
                            BinaryReader br = new BinaryReader(ms);
                            c.Image = br.ReadBytes((int)ms.Length);
                        }
                    }*/
                    carList.Add(c);

                }
                return carList;

            }
            catch (System.Data.SqlClient.SqlException)
            {

                return null;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }


        public Car SearchCarById(int id)
        {
            Car c = new Car();
            try
            {
                comm.CommandText = "SELECT * FROM Car WHERE CarID=@Id";
                comm.Parameters.AddWithValue("Id", id);
                comm.CommandType = CommandType.Text;
                conn.Open();

                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {

                    c.Id = Convert.ToInt32(reader[0].ToString());
                    c.Licenseplate = reader[1].ToString();
                    c.Manufacturer = reader[2].ToString();
                    c.Model = reader[3].ToString();
                    c.Year = Convert.ToInt32(reader[4].ToString());
                    c.Kmclock = Convert.ToInt32(reader[5].ToString());
                    c.Fuel = reader[6].ToString();
                    c.Color = reader[7].ToString();
                    c.Seats = Convert.ToInt32(reader[8].ToString());
                    c.Vin = reader[9].ToString();
                    c.Rentalpriceperday = Convert.ToInt32(reader[10].ToString());
                    c.Rentalpriceperhour = Convert.ToInt32(reader[11].ToString());
                    c.Available = Convert.ToBoolean(reader[12].ToString());
                    c.Image = reader[13].ToString();
                }
                return c;

            }
            catch (System.Data.SqlClient.SqlException)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }


        public int NextCarId()
        {
            try
            {
                comm.CommandText = "SELECT IDENT_CURRENT('Car')+IDENT_INCR('Car')";
               // comm.CommandType = CommandType.Text;
                conn.Open();

                return Convert.ToInt32(comm.ExecuteScalar());
            }


            catch (System.Data.SqlClient.SqlException)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }


           
            
        }

        public RentalData GetReportData(int id)
        {
            try
            {
                comm.CommandText = "[RentalByID]";
                comm.Parameters.Add("@RentalID", SqlDbType.Int).Value = id;
                comm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter adp = new SqlDataAdapter(comm);
                DataTable dt = new DataTable();
                RentalData rentals = new RentalData();
                adp.Fill(rentals.RentalsTable);
                return rentals;
            }
            catch (Exception)
            {
                return null;
            }            
        }


        public int NextRentalId()
        {
            try
            {
                comm.CommandText = "SELECT IDENT_CURRENT('Rental')+IDENT_INCR('Rental')";
                // comm.CommandType = CommandType.Text;
                conn.Open();

                return Convert.ToInt32(comm.ExecuteScalar());
            }


            catch (System.Data.SqlClient.SqlException)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }




        }

        public int AddRental(Rental rental)
        {
            try
            {
                comm.CommandText = "INSERT INTO Rental VALUES (@ClientID,@CarID,@Datetime,@Status,@DateOut,@KmsOut,@KmsIn,@KmsDriven,@DateIn,@RentType,@Duration,@Cost,@Discount,@Total,@Advance,@Balance,@IssueBy)";

                comm.Parameters.AddWithValue("ClientID", rental.Clientid);
                comm.Parameters.AddWithValue("CarID", rental.Carid);
                comm.Parameters.AddWithValue("DateTime", rental.Datetime);
                comm.Parameters.AddWithValue("Status", rental.Status);
                comm.Parameters.AddWithValue("DateOut", rental.Dateout);
                comm.Parameters.AddWithValue("KmsOut", rental.Kmout);
                comm.Parameters.AddWithValue("KmsIn", DBNull.Value);
                comm.Parameters.AddWithValue("KmsDriven", DBNull.Value);
                comm.Parameters.AddWithValue("DateIn", DBNull.Value);
                comm.Parameters.AddWithValue("RentType", rental.Renttype);
                comm.Parameters.AddWithValue("Duration", rental.Duration);
                comm.Parameters.AddWithValue("Cost", rental.Cost);
                comm.Parameters.AddWithValue("Discount", rental.Discount);
                comm.Parameters.AddWithValue("Total", rental.Total);
                comm.Parameters.AddWithValue("Advance", rental.Advance);
                comm.Parameters.AddWithValue("Balance", rental.Balance);
                comm.Parameters.AddWithValue("IssueBy", rental.Issueby);

                comm.CommandType = CommandType.Text;
                conn.Open();

                return comm.ExecuteNonQuery();

            }


            catch (System.Data.SqlClient.SqlException)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

        }

        public static string SafeGetString( SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
            {
                return reader.GetString(colIndex);
            }
            else
            {
                return null;
            }
        }

        public static int SafeGetInt(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
            {
                return Convert.ToInt32(reader[colIndex].ToString());
            }
            else
            {
                return default(int);
            }
        }

        public static DateTime SafeGetDate(SqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
            {
                return reader.GetDateTime(colIndex);
            }
            else
            {
                return default(DateTime);
            }
        }

        List<Rental> IRentalService.GetAllRentals()
        {
            List<Rental> rentalList = new List<Rental>();
            try
            {
                comm.CommandText = "SELECT * FROM Rental";
  
                comm.CommandType = CommandType.Text;
                conn.Open();

                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {                   

                    Rental r = new Rental();

                    /*  r.Id = Convert.ToInt32(reader[0].ToString());
                      r.Clientid = Convert.ToInt32(reader[1].ToString());
                      r.Carid = Convert.ToInt32(reader[2].ToString());
                      r.Datetime = Convert.ToDateTime(reader[3].ToString());
                      r.Status  = Convert.ToBoolean(reader[4].ToString());
                      r.Dateout = Convert.ToDateTime(reader[5].ToString());
                      r.Kmout = Convert.ToInt32(reader[6].ToString());
                      r.Kmin = Convert.ToInt32(reader[7].ToString());
                      r.Kmsdriven  = Convert.ToInt32(reader[8].ToString());
                      r.Datein = Convert.ToDateTime(reader[9].ToString());
                      r.Renttype = SafeGetString(reader,10);
                      r.Duration = Convert.ToInt32(reader[11].ToString());
                      r.Cost = Convert.ToInt32(reader[12].ToString());
                      r.Discount = Convert.ToInt32(reader[13].ToString());
                      r.Total = Convert.ToInt32(reader[14].ToString());
                      r.Advance = Convert.ToInt32(reader[15].ToString());
                      r.Balance = Convert.ToInt32(reader[16].ToString());
                      r.Issueby = Convert.ToInt32(reader[17].ToString());*/

                    r.Id = SafeGetInt(reader, 0);
                    r.Clientid = SafeGetInt(reader, 1);
                    r.Carid = SafeGetInt(reader, 2);
                    r.Datetime = SafeGetDate(reader, 3);
                    r.Status = Convert.ToBoolean(reader[4].ToString());
                    r.Dateout = SafeGetDate(reader, 5);
                    r.Kmout = SafeGetInt(reader, 6);
                    r.Kmin = SafeGetInt(reader, 7);
                    r.Kmsdriven = SafeGetInt(reader, 8);
                    r.Datein = SafeGetDate(reader, 9);
                    r.Renttype = SafeGetString(reader, 10);
                    r.Duration = SafeGetInt(reader, 11);
                    r.Cost = SafeGetInt(reader, 12);
                    r.Discount = SafeGetInt(reader, 13);
                    r.Total = SafeGetInt(reader, 14);
                    r.Advance = SafeGetInt(reader, 15);
                    r.Balance = SafeGetInt(reader, 16);
                    r.Issueby = SafeGetInt(reader, 17);
                    rentalList.Add(r);

                }
                return rentalList;

            }
            catch (System.Data.SqlClient.SqlException)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public int Login(string userName, string password)
        {
            try
            {
                comm.CommandText = "SELECT COUNT(1) FROM [CarRental].[dbo].[User] WHERE UserName=@UserName AND PasswordHash=HASHBYTES('SHA2_512', @Password+CAST(Salt AS NVARCHAR(36)))";

                comm.Parameters.AddWithValue("@UserName", userName);
                comm.Parameters.AddWithValue("@Password", password);

                comm.CommandType = CommandType.Text;
                conn.Open();

                int count = Convert.ToInt32(comm.ExecuteScalar());
                return count;
               /* if (count == 1)
                * 
                {
                    comm.CommandText = "SELECT FullName FROM User WHERE UserName=@UserName2";
                    comm.Parameters.AddWithValue("@UserName2", userName);
                    comm.CommandType = CommandType.Text;
                    string fullName = Convert.ToString(comm.ExecuteScalar());
                }*/

               // return count;

            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                conn.Close();
            }       
        }

        public User GetUserByeUserName(string userName)
        {
            User u = new User();
            try
            {
                comm.CommandText = "SELECT * FROM [CarRental].[dbo].[User] WHERE UserName=@UserName";
                comm.Parameters.AddWithValue("UserName", userName);
                comm.CommandType = CommandType.Text;
                conn.Open();

                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                   u.Id = Convert.ToInt32(reader[0].ToString());
                   u.Username = reader[1].ToString();
                   u.Fullname = reader[2].ToString();
                   u.Phonenumber = reader[5].ToString();
                }
                return u;

            }
            catch (System.Data.SqlClient.SqlException)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public Rental GetRentalById(int id)
        {
            Rental r = new Rental();
            try
            {
                comm.CommandText = "SELECT * FROM Rental WHERE RentalID=@Id";

                comm.Parameters.AddWithValue("Id", id);

                comm.CommandType = CommandType.Text;
                conn.Open();

                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {

                   

                    /*  r.Id = Convert.ToInt32(reader[0].ToString());
                      r.Clientid = Convert.ToInt32(reader[1].ToString());
                      r.Carid = Convert.ToInt32(reader[2].ToString());
                      r.Datetime = Convert.ToDateTime(reader[3].ToString());
                      r.Status  = Convert.ToBoolean(reader[4].ToString());
                      r.Dateout = Convert.ToDateTime(reader[5].ToString());
                      r.Kmout = Convert.ToInt32(reader[6].ToString());
                      r.Kmin = Convert.ToInt32(reader[7].ToString());
                      r.Kmsdriven  = Convert.ToInt32(reader[8].ToString());
                      r.Datein = Convert.ToDateTime(reader[9].ToString());
                      r.Renttype = SafeGetString(reader,10);
                      r.Duration = Convert.ToInt32(reader[11].ToString());
                      r.Cost = Convert.ToInt32(reader[12].ToString());
                      r.Discount = Convert.ToInt32(reader[13].ToString());
                      r.Total = Convert.ToInt32(reader[14].ToString());
                      r.Advance = Convert.ToInt32(reader[15].ToString());
                      r.Balance = Convert.ToInt32(reader[16].ToString());
                      r.Issueby = Convert.ToInt32(reader[17].ToString());*/

                    r.Id = SafeGetInt(reader, 0);
                    r.Clientid = SafeGetInt(reader, 1);
                    r.Carid = SafeGetInt(reader, 2);
                    r.Datetime = SafeGetDate(reader, 3);
                    r.Status = Convert.ToBoolean(reader[4].ToString());
                    r.Dateout = SafeGetDate(reader, 5);
                    r.Kmout = SafeGetInt(reader, 6);
                    r.Kmin = SafeGetInt(reader, 7);
                    r.Kmsdriven = SafeGetInt(reader, 8);
                    r.Datein = SafeGetDate(reader, 9);
                    r.Renttype = SafeGetString(reader, 10);
                    r.Duration = SafeGetInt(reader, 11);
                    r.Cost = SafeGetInt(reader, 12);
                    r.Discount = SafeGetInt(reader, 13);
                    r.Total = SafeGetInt(reader, 14);
                    r.Advance = SafeGetInt(reader, 15);
                    r.Balance = SafeGetInt(reader, 16);
                    r.Issueby = SafeGetInt(reader, 17);

                }
                return r;

            }
            catch (System.Data.SqlClient.SqlException)
            {

                throw;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public int UpdateRental(Rental rental)
        {
            try
            {
                comm.CommandText = "UPDATE Rental SET KmsIn=@KmsIn,DateIn=@DateIn,RentType=@RentType,Duration=@Duration,Cost=@Cost,Discount=@Discount,Total=@Total,Balance=@Balance,Advance=@Advance,IssueBy=@IssueBy,DateTime=@DateTime,KmsDriven=@KmsDriven  WHERE RentalID=@Id";

                comm.Parameters.AddWithValue("Id", rental.Id);
                comm.Parameters.AddWithValue("KmsIn", rental.Kmin);
                comm.Parameters.AddWithValue("KmsDriven", rental.Kmsdriven);
                comm.Parameters.AddWithValue("DateIn", rental.Datein);
                comm.Parameters.AddWithValue("Duration", rental.Duration);
                comm.Parameters.AddWithValue("Cost", rental.Cost);
                comm.Parameters.AddWithValue("Discount", rental.Discount);
                comm.Parameters.AddWithValue("Total", rental.Total);
                comm.Parameters.AddWithValue("Balance", rental.Balance);
                comm.Parameters.AddWithValue("RentType", rental.Renttype);
                comm.Parameters.AddWithValue("Advance", rental.Advance);
                comm.Parameters.AddWithValue("IssueBy", rental.Issueby);
                comm.Parameters.AddWithValue("DateTime", rental.Datetime);


                comm.CommandType = CommandType.Text;
                conn.Open();

                return comm.ExecuteNonQuery();

            }


            catch (System.Data.SqlClient.SqlException)
            {

                throw;
                //return 0;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public int CloseRental(Rental rental)
        {
            throw new NotImplementedException();
        }

        public List<Client> SearchClient(string column, string value)
        {
            List<Client> clientList = new List<Client>();
            try
            {
                comm.CommandText = "SELECT * FROM Client WHERE " + column + "='" + value + "'";
              
                comm.CommandType = CommandType.Text;
                conn.Open();

                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    Client c = new Client();
                    c.Id = Convert.ToInt32(reader[0].ToString());
                    c.Idcardnumber = reader[1].ToString();
                    c.Name = reader[2].ToString();
                    c.Birthdate = Convert.ToDateTime(reader[3].ToString());
                    c.Zipcode = reader[4].ToString();
                    c.City = reader[5].ToString();
                    c.Adress = reader[6].ToString();
                    c.Phonenumber = reader[7].ToString();
                    clientList.Add(c);

                }
                return clientList;

            }
            catch (System.Data.SqlClient.SqlException)
            {

                return null;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }
    }
}
