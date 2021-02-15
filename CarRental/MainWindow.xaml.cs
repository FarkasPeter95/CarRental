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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace CarRental
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void miSwitchUser_Click(object sender, RoutedEventArgs e)
        {
            Login window = new Login();
            window.Show();
            CloseAllWindowsExcept(window.Title);
        }

        private void miAddClient_Click(object sender, RoutedEventArgs e)
        {
            AddClient window = new AddClient();
            OneInstanceOnly(window);
        }

        private void miEditClient_Click(object sender, RoutedEventArgs e)
        {
            ClientEdit window = new ClientEdit();
            OneInstanceOnly(window);
        }

        private void miAddCar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void miEditCar_Click(object sender, RoutedEventArgs e)
        {
            CarEdit window = new CarEdit();
            OneInstanceOnly(window);
        }

        private void miRental_Click(object sender, RoutedEventArgs e)
        {
            RentalEdit window = new RentalEdit();
            OneInstanceOnly(window);
        }


        private void miAllRentals_Click(object sender, RoutedEventArgs e)
        {
            AllRentals window = new AllRentals();
            OneInstanceOnly(window);
        }

        private void CloseAllWindowsExcept(String title)
        {
            for (int intCounter = Application.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
            {
                if (Application.Current.Windows[intCounter].Title != title)
                {
                    Application.Current.Windows[intCounter].Close();
                }
            }

        }

        private void OneInstanceOnly(Window window)
        {
            int j = 0, k = 0;
            for (int intCounter = Application.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
            {
                if (Application.Current.Windows[intCounter].Title == window.Title)
                {
                    j++;
                    k = intCounter;
                }
            }

            if (j <= 1)
            {
                window.Show();
            }
            else
            {
                // MessageBox.Show("Nem nyitható meg új ablak példány!", "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
                Application.Current.Windows[k].Focus();
                window.Close();
            }

        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            CloseAllWindowsExcept("Bejelentkezés");
        }
       
    }
}


