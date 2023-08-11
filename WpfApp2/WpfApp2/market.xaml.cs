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
    /// Логика взаимодействия для market.xaml
    /// </summary>
    public partial class market : System.Windows.Window
    {
        DataSet1 dataset;
        marketViewTableAdapter marketViewTA;
        ServiceTableAdapter serviceTA;
        marketPlanTableAdapter marketTA;

        int ID_Market;
        int ID_Service;
        public market()
        {
            InitializeComponent();
            dataset = new DataSet1();
            marketViewTA = new marketViewTableAdapter();
            serviceTA = new ServiceTableAdapter();
            marketTA = new marketPlanTableAdapter();

            marketViewTA.Fill(dataset.marketView);
            serviceTA.Fill(dataset.Service);
            marketTA.Fill(dataset.marketPlan);

            serviceCB.ItemsSource = dataset.Service.DefaultView;
            serviceCB.DisplayMemberPath = "Title";
            serviceCB.SelectedValuePath = "ID_Service";
            serviceCB.SelectedItem = 0;

            dataMarket.ItemsSource = dataset.marketView.DefaultView;
            dataMarket.SelectionMode = DataGridSelectionMode.Single;
            dataMarket.SelectedValuePath = "ID_Market";
            dataMarket.SelectedValuePath = "ID_Service";
            dataMarket.CanUserAddRows = false;
            dataMarket.CanUserDeleteRows = false;
            dataMarket.IsReadOnly = true;
        }

        private void dataMarket_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)dataMarket.SelectedItem;
            if (dataRowView != null)
            {
                service.Text = dataRowView.Row.Field<String>("Маркетинговый план");

                ID_Market = dataRowView.Row.Field<int>("ID_Market");
                ID_Service = dataRowView.Row.Field<int>("ID_Service");
            }
        }

        private void add(object sender, RoutedEventArgs e)
        {
            if (check() == true)
            {
                marketTA.Insert(service.Text,
                    dateService.DisplayDate,
                    Convert.ToDecimal(price.Text),
                    (int)serviceCB.SelectedValue
                    );
                marketTA.Fill(dataset.marketPlan);
                marketViewTA.Fill(dataset.marketView);

                errorDate.Content = " ";
                errorPrice.Content = " ";
                errorService.Content = " ";
                errorservice.Content = " ";
            }
        }

        private void exit(object sender, RoutedEventArgs e)
        {
            MainWindow MainWindow = new MainWindow();
            MainWindow.Show();
            this.Close();
        }


        public bool check()
        {
            if (service.Text.Length == 0) { errorService.Content = "Введите название"; return false; }
            if (price.Text.Length == 0) { errorPrice.Content = "Введите сумму"; return false; }

            if (serviceCB.SelectedItem == null) { errorservice.Content = "Выберете услугу"; return false; }

            if (Regex.Match(price.Text, "^[.][0-9]+$|^[0-9]*[,]{0,1}[0-9]*$").Length == 0) { errorPrice.Content = "Введите сумму в формате : xx,xx"; return false; }

            if (dateService.GetValue(DatePicker.SelectedDateProperty) == null) { errorDate.Content = "Выберете дату"; return false; }

            return true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataMarket.Columns[0].Visibility = Visibility.Hidden;
            dataMarket.Columns[1].Visibility = Visibility.Hidden;
        }

        private void update(object sender, RoutedEventArgs e)
        {
            if (check() == true)
            {
                marketTA.UpdateQuery(service.Text,
                    dateService.SelectedDate.ToString(),
                    Convert.ToDecimal(price.Text),
                    (int)serviceCB.SelectedValue,
                    ID_Market
                    );
                marketTA.Fill(dataset.marketPlan);
                marketViewTA.Fill(dataset.marketView);

                errorDate.Content = " ";
                errorPrice.Content = " ";
                errorService.Content = " ";
                errorservice.Content = " ";
            }
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
                System.Data.DataTable table = dataset.marketView;
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
        private void delete(object sender, RoutedEventArgs e)
        {
            if (dataMarket.SelectedItem != null)
            {
                marketTA.DeleteQuery(ID_Market);
                marketTA.Fill(dataset.marketPlan);
                marketViewTA.Fill(dataset.marketView);

                errorDate.Content = " ";
                errorPrice.Content = " ";
                errorService.Content = " ";
                errorservice.Content = " ";
            }
        }
    }
}
