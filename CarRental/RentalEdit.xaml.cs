using CarRental.ServiceReference1;
using CarRentalServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CarRental
{
    /// <summary>
    /// Interaction logic for RentalEdit.xaml
    /// </summary>
    public partial class RentalEdit : Window 
    { 

        Client[] clientList;
        Car[] carList;
      //  public static bool isnewrental;
        public static int rentalId = 0;
    public RentalEdit()
        {
            InitializeComponent();
            rbPerDay.IsChecked = true;
            RentalServiceClient service = new RentalServiceClient();
            clientList = service.GetAllClients();
            carList = service.GetAllCars();
            rentalId = service.NextRentalId();
            service.Close();

            dtpDateOut.IsEnabled = false;
            tbBalance.IsEnabled = false;
            tbDiscount.IsEnabled = false;
            tbCost.IsEnabled = false;
            //  tbKmIn.IsEnabled = false;
            tbKmOut.IsEnabled = false;
            tbKmDriven.IsEnabled = false;
            tbBalance.IsEnabled = false;
            tbPaid.IsEnabled = false;
            tbDuration.IsEnabled = false;
            tbTotal.IsEnabled = false;
            rbPerDay.IsEnabled = false;
            rbPerHour.IsEnabled = false;
           // rbPerDay.IsChecked = true;


            //  dtpDateIn.IsEnabled = false;
            //  tbKmIn.IsEnabled = false;
            // tbKmDriven.IsEnabled = false;

            //  tbCost.IsEnabled = false;
            /*  if (isCarSelected && isClientSelected)
              {
                  tbCost.IsEnabled = true;

              }*/

            cobClients.ItemsSource = clientList;
            cobClients.DisplayMemberPath = "Idcardnumber";

            cobCars.ItemsSource = carList;
            cobCars.DisplayMemberPath = "Licenseplate";

            dtpDateOut.Value = DateTime.Now;

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Rental r = new Rental();
            RentalServiceClient service = new RentalServiceClient();

            try
            {

                r.Clientid = clientList[cobClients.SelectedIndex].Id;
                r.Carid = carList[cobCars.SelectedIndex].Id;
                r.Datetime = DateTime.Now;
                r.Dateout = (DateTime) dtpDateOut.Value;
               // r.Datein = (DateTime) dtpDateIn.Value;
                r.Kmout = carList[cobCars.SelectedIndex].Kmclock;
               // r.Kmin = Convert.ToInt32(tbKmIn.Text);
               // r.Kmsdriven = r.Kmin - r.Kmout;
                r.Duration = Convert.ToInt32(tbDuration.Text);
                if (rbPerDay.IsChecked == true)
                {
                    r.Cost = Convert.ToInt32(r.Duration * carList[cobCars.SelectedIndex].Rentalpriceperday);
                    r.Renttype = "Day";
                }
                else
                {
                    r.Cost = Convert.ToInt32(r.Duration * carList[cobCars.SelectedIndex].Rentalpriceperhour);
                    r.Renttype = "Hour";
                }
                if (tbDiscount.Text != "")
                {
                    r.Discount = Convert.ToInt32(tbDiscount.Text);

                }
                else
                {
                    r.Discount = 0;
                }
                r.Total = r.Cost - r.Discount;
                if (tbPaid.Text != "")
                {
                    r.Advance = Convert.ToInt32(tbPaid.Text);

                }
                else
                {
                    r.Advance = 0;
                }
                r.Balance = r.Total - r.Advance;
                r.Issueby = Login.userLoggedIn.Id;
                r.Status = false;



             
                if (service.AddRental(r) == 1)
                {
                    carList[cobCars.SelectedIndex].Available = false;
                    service.UpdateCar(carList[cobCars.SelectedIndex]);
                    System.Windows.MessageBox.Show("Kölcsönzés hozzáadva", "Sikeres felvétel", MessageBoxButton.OK, MessageBoxImage.Information);
                    btnPrint.IsEnabled = true;

                }
                else if (service.AddRental(r) == 0)
                {
                    System.Windows.MessageBox.Show("Hozzáadás nem lehetséges, nem megfelelő adatok!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
            catch (System.InvalidOperationException)
            {

                System.Windows.MessageBox.Show("Hozzáadás nem lehetséges, nem megfelelő adatok!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            /* if (cobCars.SelectedItem != null)
             {
                 tbCost.Text = carList[cobCars.SelectedIndex].Id.ToString();
             }*/
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void tbKmClock_TextChanged(object sender, TextChangedEventArgs e)
        {
            tbKmOut.Text = tbKmClock.Text;
        }

        private void rbPerHour_Checked(object sender, RoutedEventArgs e)
        {
            lbRentType.Content = "Óra";
            if (tbDuration.Text != "" && isCarSelected)
            {                             
                    int cost = Convert.ToInt32(tbDuration.Text) * carList[cobCars.SelectedIndex].Rentalpriceperhour;
                    tbCost.Text = cost.ToString();                
            }
        }

        private void rbPerDay_Checked(object sender, RoutedEventArgs e)
        {
            lbRentType.Content = "Nap";
            if (tbDuration.Text != "" && isCarSelected)
            { 
                    int cost = Convert.ToInt32(tbDuration.Text) * carList[cobCars.SelectedIndex].Rentalpriceperday;
                    tbCost.Text = cost.ToString();
        
            }
        }

        private void tbKmIn_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbKmIn.Text != "")
            {
                int kmsdriven = Convert.ToInt32(tbKmIn.Text) - Convert.ToInt32(tbKmOut.Text);
                if (kmsdriven > 0)
                {
                    tbKmDriven.Text = kmsdriven.ToString();
                }
                else
                {
                    tbKmDriven.Text = "0";
                }
            }
        }

        private void tbDuration_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbDuration.Text != "" && isCarSelected)
            {
                if (rbPerDay.IsChecked == true)
                {
                    int cost = Convert.ToInt32(tbDuration.Text) * carList[cobCars.SelectedIndex].Rentalpriceperday;
                    tbCost.Text = cost.ToString();
                }
                else
                {
                    int cost = Convert.ToInt32(tbDuration.Text) * carList[cobCars.SelectedIndex].Rentalpriceperhour;
                    tbCost.Text = cost.ToString();
                }
            }
            
        }

        bool isCarSelected = false;
        private void cobCars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (cobCars.SelectedIndex != -1)
            {
                isCarSelected = true;
            }
            else
            {
                isCarSelected = false;
            }
            isSelectionOk();
            tbDuration.Text = "0";
            tbBalance.Text = "0";
            tbDiscount.Text = "0";
            tbCost.Text = "0";
            tbPaid.Text = "0";
            tbTotal.Text = "0";


        }

        bool isClientSelected = false;
        private void cobClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cobClients.SelectedIndex != -1)
            {
                isClientSelected = true;
            }
            else
            {
                isClientSelected = false;
            }
            isSelectionOk();
           
        }


        private void isSelectionOk()
        {
            if (isCarSelected && isClientSelected)
            {
              //  dtpDateIn.IsEnabled = true;
                dtpDateOut.IsEnabled = true;
                tbBalance.IsEnabled = true;
                tbDiscount.IsEnabled = true;
                tbCost.IsEnabled = true;
              //  tbKmIn.IsEnabled = true;
                tbKmOut.IsEnabled = true;
                tbBalance.IsEnabled = true;
                tbPaid.IsEnabled = true;
                tbDuration.IsEnabled = true;
                tbTotal.IsEnabled = true;
                rbPerDay.IsEnabled = true;
                rbPerHour.IsEnabled = true;

            }
            else
            {
              //  dtpDateIn.IsEnabled = false;
                dtpDateOut.IsEnabled = false;
                tbBalance.IsEnabled = false;
                tbDiscount.IsEnabled = false;
                tbCost.IsEnabled = false;
              //  tbKmIn.IsEnabled = false;
              //  tbKmOut.IsEnabled = false;
                tbBalance.IsEnabled = false;
                tbPaid.IsEnabled = false;
                tbDuration.IsEnabled = false;
                tbTotal.IsEnabled = false;
                rbPerDay.IsEnabled = false;
                rbPerHour.IsEnabled = false;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            RentalReportViewer rw = new RentalReportViewer();
            rw.Show();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            RentalServiceClient service = new RentalServiceClient();
            Rental r = service.GetRentalById(rentalId);


            r.Datein = (DateTime) dtpDateIn.Value;
            r.Kmin = Convert.ToInt32(tbKmIn.Text);
            r.Kmsdriven = r.Kmin - r.Kmout;
            r.Duration = Convert.ToInt32(tbDuration.Text);
            if (rbPerDay.IsChecked == true)
            {
                r.Cost = Convert.ToInt32(r.Duration * carList[cobCars.SelectedIndex].Rentalpriceperday);
                r.Renttype = "Day";
            }
            else
            {
                r.Cost = Convert.ToInt32(r.Duration * carList[cobCars.SelectedIndex].Rentalpriceperhour);
                r.Renttype = "Hour";
            }
            if (tbDiscount.Text != "")
            {
                r.Discount = Convert.ToInt32(tbDiscount.Text);

            }
            else
            {
                r.Discount = 0;
            }
            r.Total = r.Cost - r.Discount;
            if (tbPaid.Text != "")
            {
                r.Advance = Convert.ToInt32(tbPaid.Text);

            }
            else
            {
                r.Advance = 0;
            }
            r.Balance = r.Total - r.Advance;
            r.Issueby = Login.userLoggedIn.Id;
            r.Datetime = DateTime.Now;
            if (r.Kmin < carList[cobCars.SelectedIndex].Kmclock)
            {
                System.Windows.MessageBox.Show("A Km óra állása nem lehet kevesebb!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            if (r.Dateout >= (DateTime)dtpDateIn.Value)
            {
                System.Windows.MessageBox.Show("A visszahozatal dátuma nem lehett előbb mint a kikölcsönzés!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                carList[cobCars.SelectedIndex].Kmclock = r.Kmin;
                carList[cobCars.SelectedIndex].Available = true;
                service.UpdateCar(carList[cobCars.SelectedIndex]);
                if (service.UpdateRental(r) == 1)
                {
                    System.Windows.MessageBox.Show("Kölcsönzés adatai módosítva!", "Sikeres mentés", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (service.UpdateRental(r) == 0)
                {
                    System.Windows.MessageBox.Show("Módosítás, nem megfelelő adatok!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }


        private void TextChanged(object sender, RoutedEventArgs e)
        {
            int discount;
            int paid;
            int cost = Convert.ToInt32(tbCost.Text);
            if (isCarSelected && isClientSelected)
            {
                if (tbDiscount.Text != "")
                {
                    discount = Convert.ToInt32(tbDiscount.Text);                    
                }
                else
                {                  
                    discount = 0;                                    
                }
                if (tbPaid.Text != ""){
                    paid = Convert.ToInt32(tbPaid.Text);
                }
                else
                {
                    paid = 0;
                }
                int total = cost - discount;
                tbTotal.Text = total.ToString();
                int balance = total - paid;
                tbBalance.Text = balance.ToString();
            }


          //  paid = Convert.ToInt32(tbPaid.Text);
          //  cost = Convert.ToInt32(tbCost.Text);

        }

        private void dtpDateIn_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            DateTime start = (DateTime)dtpDateOut.Value;
            DateTime finish = (DateTime)dtpDateIn.Value;
            TimeSpan difference = finish.Subtract(start);
            int roundedUp;
            if (rbPerDay.IsChecked == true)
            { 
                roundedUp = (int)Math.Ceiling(difference.TotalDays);
            }
            else
            {
                roundedUp = (int)Math.Ceiling(difference.TotalHours);
            }
            tbDuration.Text = roundedUp.ToString();
        }
    }
}
