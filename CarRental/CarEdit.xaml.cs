using CarRental.ServiceReference1;
using CarRentalServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
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
    /// Interaction logic for CarEdit.xaml
    /// </summary>
    public partial class CarEdit : Window
    {
        public CarEdit()
        { 
            InitializeComponent();
            RentalServiceClient service = new RentalServiceClient();
            dgCar.ItemsSource = service.GetAllCars();
            
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            RentalServiceClient service = new RentalServiceClient();

            tbLicensePlate.Text = "";
            tbManufacturer.Text = "";
            tbModel.Text = "";
            tbYear.Text = "";
            tbKmClock.Text = "";
            cobFuel.Text = "";
            tbColor.Text = "";
            cbAvailable.IsChecked = false;
            tbRentalPricePerDay.Text = "";
            tbRentalPricePerHour.Text = "";
            tbSeats.Text = "";
            tbVIN.Text = "";
            tbCarFilter.Text = "";
            tbImagePath.Text = "";
            dgCar.SelectedIndex = -1;
            tbCarID.Text = Convert.ToString(service.NextCarId());
            ImageViewer.Source = null;
            btnSave.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnAdd.IsEnabled = true;
            btnDeleteImage.IsEnabled = false;

            
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Car c = new Car();
            try
            {   
                c.Licenseplate = tbLicensePlate.Text;
                c.Manufacturer = tbManufacturer.Text;
                c.Model = tbModel.Text;
                c.Year = Convert.ToInt32(tbYear.Text);
                c.Kmclock = Convert.ToInt32(tbKmClock.Text);
                c.Fuel = cobFuel.Text;                
                c.Color = tbColor.Text;
                c.Seats = Convert.ToInt32(tbSeats.Text);
                c.Vin = tbVIN.Text;
                c.Rentalpriceperday = Convert.ToInt32(tbRentalPricePerDay.Text);
                c.Rentalpriceperhour = Convert.ToInt32(tbRentalPricePerHour.Text);
                c.Available = Convert.ToBoolean(cbAvailable.Content);
                c.Image = "";


                RentalServiceClient service = new RentalServiceClient();

                if (service.AddCar(c) == 1)
                {
                    if (imgLocation != "")
                    {
                        string path = "C:/CarRentalImages/" + tbCarID.Text + System.IO.Path.GetExtension(imgLocation);
                        System.IO.File.Copy(imgLocation, path, true);
                        c.Id = Convert.ToInt32(tbCarID.Text);
                        c.Image = path;
                        service.UpdateCarImage(c);

                    }
                    dgCar.ItemsSource = service.GetAllCars();
                    System.Windows.MessageBox.Show("Autó hozzáadva", "Sikeres felvétel", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else if (service.AddCar(c) == 0)
                {
                    System.Windows.MessageBox.Show("Hozzáadás nem lehetséges, nem megfelelő adatok!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
            catch (Exception ex)
            {

                System.Windows.MessageBox.Show(ex +"Hozzáadás nem lehetséges, nem megfelelő adatok!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Car c = new Car();

            if (tbCarID.Text != "")
            {


                MessageBoxResult result = System.Windows.MessageBox.Show("Biztos, hogy törli a(z) " + c.Manufacturer + " " + c.Model + " adatait az adatbázisból?",
                                                "Megerősítés",
                                              MessageBoxButton.YesNo,
                                              MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    c.Id = Convert.ToInt32(tbCarID.Text);
                    RentalServiceClient service = new RentalServiceClient();

                    if (service.DeleteCar(c) == 1)
                    {
                        if (tbImagePath.Text != "")
                        {
                            File.Delete(tbImagePath.Text);

                        }
                        dgCar.ItemsSource = service.GetAllCars();
                        System.Windows.MessageBox.Show("Autó törölve", "Sikeres mentés", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else if (service.DeleteCar(c) == 0)
                    {
                        System.Windows.MessageBox.Show("Hiba történt!");

                    }
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Törlés nem lehetséges, nincs kijelölve autó!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Car c = new Car();
            RentalServiceClient service = new RentalServiceClient();

            c.Id = Convert.ToInt32(tbCarID.Text);

            // var d = new System.Windows.DataObject(System.Windows.DataFormats.Bitmap, ImageViewer.Source, true);
            // var bmp = d.GetData("System.Drawing.Bitmap") as byte[];
            if(imgLocation != "")
            {        
                string path = "C:/CarRentalImages/" + c.Id.ToString() + System.IO.Path.GetExtension(imgLocation);
                System.IO.File.Copy(imgLocation, path, true);
                c.Image = path;
                service.UpdateCarImage(c);
            }
            
            //  byte[] image = null;
            // FileStream stream = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
            // BinaryReader br = new BinaryReader(stream);
            //  image = br.ReadBytes((int)stream.Length);
            //  c.Image = image;



            c.Licenseplate = tbLicensePlate.Text;
            c.Manufacturer = tbManufacturer.Text;
            c.Model = tbModel.Text;
            c.Year = Convert.ToInt32(tbYear.Text);
            c.Kmclock = Convert.ToInt32(tbKmClock.Text);
            c.Fuel = cobFuel.Text;
            c.Color = tbColor.Text;
            c.Seats = Convert.ToInt32(tbSeats.Text);
            c.Vin = tbVIN.Text;
            c.Rentalpriceperday = Convert.ToInt32(tbRentalPricePerDay.Text);
            c.Rentalpriceperhour = Convert.ToInt32(tbRentalPricePerHour.Text);
            c.Available = Convert.ToBoolean(cbAvailable.Content);

            if (service.UpdateCar(c) == 1)
            {
                dgCar.ItemsSource = service.GetAllCars();
                System.Windows.MessageBox.Show("Autó módosítva", "Sikeres mentés", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (service.UpdateCar(c) == 0)
            {
               System.Windows.MessageBox.Show("Hozzáadás nem lehetséges, nem megfelelő adatok!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dgCar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnSave.IsEnabled = true;
            btnDelete.IsEnabled = true;
            btnAdd.IsEnabled = false;
            imgLocation = "";
            if (tbImagePath.Text == "")
            {
                btnDeleteImage.IsEnabled = false;
            }
            else btnDeleteImage.IsEnabled = true;

            //ImageViewer.Source = new BitmapImage(new Uri(tbModel.Text));           



            if (tbImagePath.Text!="" )
            {
                Uri uri = new Uri(tbImagePath.Text);
                if (File.Exists(uri.LocalPath))
                {
                    BitmapImage imgTemp = new BitmapImage();
                    imgTemp.BeginInit();
                    imgTemp.CacheOption = BitmapCacheOption.OnLoad;
                    imgTemp.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    imgTemp.UriSource = uri;
                    imgTemp.EndInit();
                    ImageViewer.Source = imgTemp;                    
                }
                // ImageViewer.Source = new BitmapImage(new Uri(tbImagePath.Text));
            }
            else
            {
                ImageViewer.Source = null;
            }
        }



        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

            string columnName = "";

            dgCar.SelectedItem = null;
            btnDelete.IsEnabled = false;
            btnAdd.IsEnabled = false;
            btnSave.IsEnabled = false;
            switch (cobColumn.Text)
            {
                case "Azonosító":
                    columnName = "CarID";
                    break;
                case "Márka":
                    columnName = "Manufacturer";
                    break;
                case "Típus":
                    columnName = "Model";
                    break;
                case "Üzemanyag":
                    columnName = "Fuel";
                    break;
                case "Rendszám":
                    columnName = "LicensePlate";
                    break;            
                case "Elérhető":
                    columnName = "Available";
                    break;

            }
            try
            {
                RentalServiceClient service = new RentalServiceClient();
                if (service.SearchCar(columnName, tbCarFilter.Text)!=null)
                {
                   // List<Car> carList = new List<Car>();
                   // Car c = new Car();

                    // c.Id = Convert.ToInt32(tbClientFilter.Text);
                    //c.Name = tbClientFilter.Text;

                    //clientList.Add(service.SearchClientByName(c));

                    dgCar.ItemsSource = service.SearchCar(columnName, tbCarFilter.Text);
                }
                else
                {
                    System.Windows.MessageBox.Show("Keresés sikertelen, nem megfelelő adatok!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    tbCarFilter.Text = "";
                    dgCar.ItemsSource = service.GetAllCars();
                }
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Keresés sikertelen, nem megfelelő adatok!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                tbCarFilter.Text = "";
            }


        }

        private void btnClearSearch_Click(object sender, RoutedEventArgs e)
        {
            tbCarFilter.Text = "";
            RentalServiceClient service = new RentalServiceClient();
            dgCar.ItemsSource = service.GetAllCars();
        }


        string imgLocation = "";
        private void btnBrowseImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                imgLocation = dialog.FileName.ToString();
                tbImagePath.Text = imgLocation;
                ImageViewer.Source = new BitmapImage(new Uri(dialog.FileName));
            }

        }

        private void btnDeleteImage_Click(object sender, RoutedEventArgs e)
        {
            /* Car c = new Car();
             byte[] images = null;
             FileStream stream = new FileStream(imgLocation, FileMode.Open,FileAccess.Read);
             BinaryReader br = new BinaryReader(stream);
             images = br.ReadBytes((int)stream.Length);
             c.Image = images;
             RentalServiceClient service = new RentalServiceClient();
             service.UpdateCar(c);*/
            //  ImageViewer.Source = tbModel.Text;
            /* if (tbImagePath.Text!="")
             {
                 ImageViewer.Source = new BitmapImage(new Uri(tbImagePath.Text));
             }
             else
             {
                 ImageViewer.Source = null;
                 tbImagePath.Text = "";
             }*/
            RentalServiceClient service = new RentalServiceClient();
            Car c = new Car();

            c.Id = Convert.ToInt32(tbCarID.Text);
            c.Manufacturer = tbManufacturer.Text;
            c.Model = tbModel.Text;

            MessageBoxResult result = System.Windows.MessageBox.Show("Biztos, hogy törli a(z) " + c.Manufacturer + " " + c.Model + " autó képét?",
                                             "Megerősítés",
                                           MessageBoxButton.YesNo,
                                           MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                ImageViewer.Source = null;
                imgLocation = "";
                File.Delete(tbImagePath.Text);
                //tbImagePath.Text = "";
                c.Image = "";
                service.UpdateCarImage(c);
                dgCar.ItemsSource = service.GetAllCars();
            }
                    

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        

    }
}
