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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {

        public static User userLoggedIn = new User();

       
        public Login()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            RentalServiceClient service = new RentalServiceClient();

            if (service.Login(tbUserName.Text,pwbPassword.Password) == 1)
            {
                MainWindow mw = new MainWindow();
                userLoggedIn = service.GetUserByeUserName(tbUserName.Text);
                mw.lblLogin.Content =  "Bejelentkezve: " + userLoggedIn.Fullname;
                mw.Show();
                Close();

            }
            else
            {
                MessageBox.Show("Hibás felhasználónév vagy jelszó!");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
