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
using System.Data;
using System.Data.SqlClient;
using WpfApp2.DataSet1TableAdapters;
using System.Globalization;
using System.Text.RegularExpressions;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для customerView.xaml
    /// </summary>
    public partial class customerView : Window
    {
        DataSet1 dataset;
        //CustomerTableAdapter customer;
        Credit_AgreementTableAdapter credit;
        ServiceTableAdapter serviceTA;
        ClientTableAdapter client;

        public customerView()
        {
            InitializeComponent();

            dataset = new DataSet1();
            //customer = new CustomerTableAdapter();
            credit = new Credit_AgreementTableAdapter();
            serviceTA = new ServiceTableAdapter();
            client = new ClientTableAdapter();

            //customer.Fill(dataset.Customer);
            credit.Fill(dataset.Credit_Agreement);
            serviceTA.Fill(dataset.Service);
            client.Fill(dataset.Client);

            //dataCustomer.ItemsSource = dataset.Customer.DefaultView;
            dataCustomer.SelectionMode = DataGridSelectionMode.Single;
            dataCustomer.SelectedValuePath = "ID_Client";
            dataCustomer.CanUserAddRows = false;
            dataCustomer.CanUserDeleteRows = false;
            dataCustomer.IsReadOnly = true;
        }

        private void export(object sender, RoutedEventArgs e)
        {

        }

        private void delete(object sender, RoutedEventArgs e)
        {

        }

        private void exit(object sender, RoutedEventArgs e)
        {

        }

        private void search(object sender, RoutedEventArgs e)
        {

        }

        private void dataCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
