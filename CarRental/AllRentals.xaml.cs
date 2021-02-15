using CarRental.ServiceReference1;
using CarRentalServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CarRental
{
    /// <summary>
    /// Interaction logic for AllRentals.xaml
    /// </summary>
    public partial class AllRentals : Window
    {
        
        public AllRentals()
        {
            InitializeComponent();
            RentalServiceClient service = new RentalServiceClient();
            dgRentals.ItemsSource = service.GetAllRentals();

        }

        private void dgRentals_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            try
            {
                if (sender != null)
                {

                    DataGrid grid = sender as DataGrid;
                    if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                    {
                        //This is the code which helps to show the data when the row is double clicked.                        
                        // DataGridRow dgr = grid.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem) as DataGridRow;
                        //  DataRowView dr = (DataRowView)dgr.Item;

                        //  DataRowView dataRow = (DataRowView)dgRentals.SelectedItem;
                        // int index = dgRentals.CurrentCell.Column.DisplayIndex;
                        //  window.tbDuration.Text = dataRow.Row.ItemArray[14].ToString();

                        RentalEdit window = new RentalEdit();
                        RentalServiceClient service = new RentalServiceClient();
                        var selectedItem = dgRentals.SelectedItem.ToString();
                        Type t = dgRentals.SelectedItem.GetType();
                        System.Reflection.PropertyInfo[] props = t.GetProperties();

                        string rentalid = props[0].GetValue(dgRentals.SelectedItem).ToString();
                        Rental rental = service.GetRentalById(Convert.ToInt32(rentalid));
                        
                      //  string clientid = props[2].GetValue(dgRentals.SelectedItem).ToString();
                      //  string carid = props[1].GetValue(dgRentals.SelectedItem).ToString();
                        Client client = service.SearchClientById(Convert.ToInt32(rental.Clientid));
                        Car car = service.SearchCarById(Convert.ToInt32(rental.Carid));
                        window.cobClients.Text = client.Idcardnumber;
                        window.cobClients.IsEnabled = false;
                        window.cobCars.Text = car.Licenseplate;
                        window.cobCars.IsEnabled = false;

                        window.dtpDateOut.Value = rental.Dateout;
                        window.dtpDateIn.Value = rental.Datein;
                        window.tbTotal.Text = rental.Total.ToString();
                        window.tbKmIn.Text = rental.Kmin.ToString();
                        window.tbKmOut.Text = rental.Kmout.ToString();
                        window.tbDuration.Text = rental.Duration.ToString();
                        window.tbDiscount.Text = rental.Discount.ToString();
                        window.tbBalance.Text = rental.Balance.ToString();
                        window.tbCost.Text = rental.Cost.ToString();
                        window.tbPaid.Text = rental.Advance.ToString();
                        window.rbPerDay.IsEnabled = true;
                        window.rbPerHour.IsEnabled = true;
                        string type = rental.Renttype.ToString().Trim();
                        if ( type == "Hour")
                        {
                            window.rbPerHour.IsChecked = true;
                        }
                        else
                        {
                            window.rbPerDay.IsChecked = true;
                        }
                      //  window.tbKmOut.Text = rental.Kmout.ToString();
                        window.dtpDateIn.IsEnabled = true;
                        window.tbKmIn.IsEnabled = true;
                        window.tbKmDriven.IsEnabled = true;
                        window.btnAdd.IsEnabled = false;
                        window.btnSave.IsEnabled = true;
                        window.btnClose.IsEnabled = true;
                        window.btnPrint.IsEnabled = true;

                        window.lblRentralId.Content = "Kölcsönzési azonosító: R-" + rental.Id.ToString();
                        if (rental.Status == false)
                        {
                            window.lblStatus.Content = "Sztátusz: Függőben";

                        }
                        else
                        {
                            window.lblStatus.Content = "Sztátusz: Lezárt ";
                            window.btnSave.IsEnabled = false;
                            window.btnClose.IsEnabled = false;
                        }
                        window.lblLastUpdated.Content = "Utolsó módosítás: " + rental.Datetime ;

                        RentalEdit.rentalId = rental.Id;

                  /*      ing Path = Carid}
                    "/>
                        nding Path = Clientid}
                "/>
                        inding Path = Dateout}"/>
                          = "{Binding Path=Datein}" /
                          nding Path = Kmout}"/>
                        nding Path = Kmin}"/>
                        "{Binding Path=Kmsdriven}
                        {Binding Path = Duration}"/
                        ding Path = Renttype}"/>
                        Binding Path = Cost}"/>
                        "{Binding Path=Discount}"
                        {Binding Path = Total}"/>
                        Binding Path = Balance}"/>
                        nding Path = Issueby}"/>
                        ding Path = Datetime}"/>
                        Binding Path = Status}"/>
                        */


                     //   window.dtpDateOut.Value = (DateTime) props[3].GetValue(dgRentals.SelectedItem);
                      //  window.dtpDateIn.Value = null;
                      //  window.tbDuration.Text = props[16].GetValue(dgRentals.SelectedItem).ToString();



                        // window.tbDuration.Text = dr[14].ToString();                       
                        window.Show();
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }



        }
    }
}
