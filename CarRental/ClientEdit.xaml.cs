using CarRental.ServiceReference1;
using CarRentalServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for ClientEdit.xaml
    /// </summary>
    public partial class ClientEdit : Window
    {
        public ClientEdit()
        {
            InitializeComponent();
            RentalServiceClient service = new RentalServiceClient();
            dgClient.ItemsSource = service.GetAllClients();           
        }

       /* private void btnClear_Click(object sender, RoutedEventArgs e)
        {
              tbClientID.Text = "";
              tbIdCardNumber.Text = "";
              tbName.Text = "";
              dpBirthDate.SelectedDate = null;           
              tbZipCode.Text = "";
              tbCity.Text = "";
              tbAdress.Text = "";
              tbPhoneNumber.Text = "";
              tbClientFilter.Text = "";
              dgClient.SelectedItem = null;
              btnSave.IsEnabled = false;
              btnDelete.IsEnabled = false;
              btnAdd.IsEnabled = true;
        }*/

       /* private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Client c = new Client();
            try {
                c.Idcardnumber = tbIdCardNumber.Text;
                c.Name = tbName.Text;
                c.Zipcode = tbZipCode.Text;
                c.Phonenumber = tbPhoneNumber.Text;
                c.Adress = tbAdress.Text;
                c.Birthdate = dpBirthDate.SelectedDate.Value;
                c.City = tbCity.Text;
            }
            catch (System.InvalidOperationException)
            {

                MessageBox.Show("22Hozzáadás nem lehetséges, nem megfelelő adatok!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            RentalServiceClient service = new RentalServiceClient();

            if (service.AddClient(c) == 1)
            {
                dgClient.ItemsSource = service.GetAllClients();
                MessageBox.Show("Ügyfél hozzáadva", "Sikeres felvétel", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else if (service.AddClient(c) == 0)
            {
                MessageBox.Show("Hozzáadás nem lehetséges, nem megfelelő adatok!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }*/


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Client c = new Client();
                c.Id = Convert.ToInt32(tbClientID.Text);
                c.Idcardnumber = tbIdCardNumber.Text;
                c.Name = tbName.Text;
                c.Zipcode = tbZipCode.Text;
                c.Phonenumber = tbPhoneNumber.Text;
                c.Adress = tbAdress.Text;
                if (dpBirthDate.SelectedDate.Value != null)
                {
                    c.Birthdate = dpBirthDate.SelectedDate.Value;
                }
                c.City = tbCity.Text;

                RentalServiceClient service = new RentalServiceClient();

                if (service.UpdateClient(c) == 1)
                {
                    dgClient.ItemsSource = service.GetAllClients();
                    MessageBox.Show("Ügyfél módosítva", "Sikeres mentés", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else if (service.UpdateClient(c) == 0)
                {
                    MessageBox.Show("Hozzáadás nem lehetséges, nem megfelelő adatok!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (System.InvalidOperationException)
            {

                MessageBox.Show("Hozzáadás nem lehetséges, nem megfelelő adatok!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Client c = new Client();

            if (tbClientID.Text != "")
            {


                MessageBoxResult result = MessageBox.Show("Biztos, hogy törli " + c.Name + " adatait az adatbázisból?",
                                              "Megerősítés",
                                              MessageBoxButton.YesNo,
                                              MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    c.Id = Convert.ToInt32(tbClientID.Text);
                    RentalServiceClient service = new RentalServiceClient();

                    if (service.DeleteClient(c) == 1)
                    {
                        dgClient.ItemsSource = service.GetAllClients();
                        MessageBox.Show("Ügyfél törölve", "Sikeres mentés", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else if (service.DeleteClient(c) == 0)
                    {
                        MessageBox.Show("Hiba történt!");

                    }
                }
            }
            else
            {
                MessageBox.Show("Törlés nem lehetséges, nincs kijelölve ügyfél!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
                                            
        }

        private void dgClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {                
                btnSave.IsEnabled = true;
                btnDelete.IsEnabled = true;
        }

        private void tbClientFilter_TextChanged(object sender, TextChangedEventArgs e)
        {

            /*   RentalServiceClient service = new RentalServiceClient();
               if (tbClientFilter.Text != "")
               {
                   List<Client> clientList = new List<Client>();
                   Client c = new Client();

                   // c.Id = Convert.ToInt32(tbClientFilter.Text);
                   c.Name = tbClientFilter.Text;

                   //clientList.Add(service.SearchClientByName(c));
                   dgClient.ItemsSource = service.SearchClientByName();
               }
               else
               {
                   dgClient.ItemsSource = service.GetAllClients();
               }*/
            dgClient.SelectedItem = null;
            btnDelete.IsEnabled = false;
            btnSave.IsEnabled = false;

            if (tbClientFilter.Text != "")
            {
                TextBox textBoxName = (TextBox)sender;
                string filterText = textBoxName.Text;
                ICollectionView cv = CollectionViewSource.GetDefaultView(dgClient.ItemsSource);

                if (!string.IsNullOrEmpty(filterText))
                {
                    cv.Filter = o =>
                    {
                        /* change to get data row value */
                        Client c = o as Client;
                        return (c.Name.ToUpper().Contains(filterText.ToUpper()));
                        /* end change to get data row value */
                    };
                }
            }
            else
            {
                RentalServiceClient service = new RentalServiceClient();
                dgClient.ItemsSource = service.GetAllClients();
            }
            
        }


        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string columnName = "";

            dgClient.SelectedItem = null;
            btnDelete.IsEnabled = false;
            btnSave.IsEnabled = false;
            switch (cobColumn.Text)
            {
                case "Azonosító":
                    columnName = "ClientID";
                    break;
                case "Név":
                    columnName = "Name";
                    break;
                case "Szig. szám":
                    columnName = "IdCardNumber";
                    break;     
            }
            try
            {
                RentalServiceClient service = new RentalServiceClient();
                if (service.SearchClient(columnName, tbClientFilter.Text) != null)
                {
                  
                    dgClient.ItemsSource = service.SearchClient(columnName, tbClientFilter.Text);
                }
                else
                {
                    System.Windows.MessageBox.Show("Keresés sikertelen, nem megfelelő adatok!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    tbClientFilter.Text = "";
                    dgClient.ItemsSource = service.GetAllClients();
                }
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Keresés sikertelen, nem megfelelő adatok!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                tbClientFilter.Text = "";
            }

        }

        private void btnClearSearch_Click(object sender, RoutedEventArgs e)
        {
            tbClientFilter.Text = "";
            RentalServiceClient service = new RentalServiceClient();
            dgClient.ItemsSource = service.GetAllClients();
            btnDelete.IsEnabled = false;
            btnSave.IsEnabled = false;
        }
    }



 

}
