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
using Microsoft.Office.Interop.Excel;
using System.Globalization;
using System.Text.RegularExpressions;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для service.xaml
    /// </summary>
    public partial class service : System.Windows.Window
    {
        DataSet1 dataset;
        ServiceTableAdapter serviceTA;
        ClientTableAdapter clientTA;
        serviceViewTableAdapter serviceViewTA;

        int ID_Service;
        int ID_Client;
        public service()
        {
            InitializeComponent();
            dataset = new DataSet1();
            serviceTA = new ServiceTableAdapter();
            clientTA = new ClientTableAdapter();
            serviceViewTA = new serviceViewTableAdapter();

            serviceTA.Fill(dataset.Service);
            clientTA.Fill(dataset.Client);
            serviceViewTA.Fill(dataset.serviceView);

            clientCB.ItemsSource = dataset.Client.DefaultView;
            clientCB.DisplayMemberPath = "Surname";
            clientCB.SelectedValuePath = "ID_Client";
            clientCB.SelectedItem = 0;

            dataService.ItemsSource = dataset.serviceView.DefaultView;
            dataService.SelectionMode = DataGridSelectionMode.Single;
            dataService.SelectedValuePath = "ID_Service";
            dataService.SelectedValuePath = "ID_Client";
            dataService.CanUserAddRows = false;
            dataService.CanUserDeleteRows = false;
            dataService.IsReadOnly = true;
        }

        private void dataService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)dataService.SelectedItem;
            if (dataRowView != null)
            {
                service1.Text = dataRowView.Row.Field<String>("Услуга");

                ID_Service = dataRowView.Row.Field<int>("ID_Service");
                ID_Client = dataRowView.Row.Field<int>("ID_Client");
            }
        }

        private void add(object sender, RoutedEventArgs e)
        {
            if (check() == true)
            {
                serviceTA.Insert(service1.Text,
                    Convert.ToDecimal(price.Text),
                    (int)clientCB.SelectedValue
                    );
                serviceTA.Fill(dataset.Service);
                serviceViewTA.Fill(dataset.serviceView);

                errorPrice.Content = " ";
                errorClient.Content = " ";
                errorService.Content = " ";
            }
        }

        private void update(object sender, RoutedEventArgs e)
        {
            if (check() == true)
            {
                serviceTA.UpdateQuery(service1.Text,
                    Convert.ToDecimal(price.Text),
                    (int)clientCB.SelectedValue,
                    ID_Service
                    );
                serviceTA.Fill(dataset.Service);
                serviceViewTA.Fill(dataset.serviceView);

                errorPrice.Content = " ";
                errorClient.Content = " ";
                errorService.Content = " ";
            }
        }

        private void delete(object sender, RoutedEventArgs e)
        {
            if (dataService.SelectedItem != null)
            {
                serviceTA.DeleteQuery(ID_Service);
                serviceTA.Fill(dataset.Service);
                serviceViewTA.Fill(dataset.serviceView);

                errorPrice.Content = " ";
                errorClient.Content = " ";
                errorService.Content = " ";
            }

        }

        private void exit(object sender, RoutedEventArgs e)
        {
            serviceManager serviceManager = new serviceManager();
            serviceManager.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataService.Columns[0].Visibility = Visibility.Hidden;
            dataService.Columns[1].Visibility = Visibility.Hidden;
        }
        private void export(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                Workbook xlWorkbook = ExcelApp.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);

                Sheets xlSheets = null;
                Worksheet xlWorksheet = null;
                xlSheets = ExcelApp.Sheets;
                xlWorksheet = (Worksheet)xlSheets.Add(xlSheets[1], Type.Missing, Type.Missing, Type.Missing);
                System.Data.DataTable table = dataset.serviceView;
                xlWorksheet.Name = table.TableName;
                for (int j = 3; j < table.Columns.Count + 1; j++)
                {

                    ExcelApp.Cells[1, j] = table.Columns[j - 1].ColumnName;

                }

                for (int k = 0; k < table.Rows.Count; k++)
                {
                    for (int l = 2; l < table.Columns.Count; l++)
                    {
                        ExcelApp.Cells[k + 2, l + 1] = table.Rows[k].ItemArray[l].ToString();
                    }
                }
                ExcelApp.Columns.AutoFit();
                ((Worksheet)ExcelApp.ActiveWorkbook.Sheets[ExcelApp.ActiveWorkbook.Sheets.Count]).Delete();
                ExcelApp.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сбой в работе Excel");
            }
        }
        public bool check()
        {
            if (service1.Text.Length == 0) { errorService.Content = "Введите название"; return false; }
            if (price.Text.Length == 0) { errorPrice.Content = "Введите сумму"; return false; }

            if (clientCB.SelectedItem == null) { errorClient.Content = "Выберете клиента"; return false; }

            if (Regex.Match(price.Text, "^[.][0-9]+$|^[0-9]*[,]{0,1}[0-9]*$").Length == 0) { errorPrice.Content = "Введите сумму в формате : xx,xx"; return false; }

            return true;
        }
    }
}
