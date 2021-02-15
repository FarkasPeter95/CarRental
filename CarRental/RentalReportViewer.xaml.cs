using CarRental.ServiceReference1;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    public partial class RentalReportViewer : Window
    {

        public RentalReportViewer()
        {
            InitializeComponent();
            reportViewer.Reset();
            RentalServiceClient service = new RentalServiceClient();
            DataTable dt = service.GetReportData(RentalEdit.rentalId).RentalsTable;
            ReportDataSource ds = new ReportDataSource("DataSet1", dt);
            reportViewer.LocalReport.DataSources.Add(ds);
            reportViewer.LocalReport.ReportEmbeddedResource = "CarRental.RentalReport.rdlc";
            reportViewer.RefreshReport();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            //reportViewer.Reset();
            //RentalServiceClient service = new RentalServiceClient();
            //DataTable dt = service.GetReportData(Convert.ToInt32(tbID.Text)).RentalsTable;
            //ReportDataSource ds = new ReportDataSource("DataSet1", dt);
            //reportViewer.LocalReport.DataSources.Add(ds);
            //reportViewer.LocalReport.ReportEmbeddedResource = "CarRental.RentalReport.rdlc";
            //reportViewer.RefreshReport();
        }
    }
}
