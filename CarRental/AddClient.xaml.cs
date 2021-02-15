using CarRental.ServiceReference1;
using CarRentalServices;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for AddClient.xaml
    /// </summary>
    public partial class AddClient : Window
    {
        public AddClient()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Client c = new Client();
            try
            {
                c.Idcardnumber = tbIdCardNumber.Text;
                c.Name = tbName.Text;
                c.Zipcode = tbZipCode.Text;
                c.Phonenumber = tbPhoneNumber.Text;
                c.Adress = tbAdress.Text;
                c.Birthdate = dpBirthDate.SelectedDate.Value;
                c.City = tbCity.Text;

                RentalServiceClient service = new RentalServiceClient();

                if (service.AddClient(c) == 1)
                {
                    MessageBox.Show("Ügyfél hozzáadva", "Sikeres felvétel", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else if (service.AddClient(c) == 0)
                {
                    MessageBox.Show("Hozzáadás nem lehetséges, nem megfelelő adatok!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
            catch (System.InvalidOperationException)
            {

                MessageBox.Show("Hozzáadás nem lehetséges, nem megfelelő adatok!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }         

        }
    }
}
