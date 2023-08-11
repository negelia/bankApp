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
    /// Логика взаимодействия для clients.xaml
    /// </summary>
    public partial class clients : System.Windows.Window
    {
        DataSet1 dataset;
        dataEmployee1 dataEmployee;
        ClientTableAdapter clientTA;

        int ID_Client;
        public clients()
        {
            InitializeComponent();
            dataset = new DataSet1();
            dataEmployee = new dataEmployee1();
            clientTA = new ClientTableAdapter();
            
            clientTA.Fill(dataset.Client);

            dataClient.ItemsSource = dataset.Client.DefaultView;
            dataClient.SelectionMode = DataGridSelectionMode.Single;
            dataClient.SelectedValuePath = "ID_Client";
            dataClient.CanUserAddRows = false;
            dataClient.CanUserDeleteRows = false;
            dataClient.IsReadOnly = true;
        }

        private void dataClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)dataClient.SelectedItem;
            if (dataRowView != null)
            {
                fam.Text = dataRowView.Row.Field<string>("Surname");
                im.Text = dataRowView.Row.Field<string>("Name");
                otch.Text = dataRowView.Row.Field<string>("Middle_Name");

                ID_Client = dataRowView.Row.Field<int>("ID_Client");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataClient.Columns[0].Visibility = Visibility.Hidden;
            dataClient.Columns[5].Visibility = Visibility.Hidden;
            dataClient.Columns[6].Visibility = Visibility.Hidden;

            dataClient.Columns[1].Header = "Фамилия";
            dataClient.Columns[2].Header = "Имя";
            dataClient.Columns[3].Header = "Отчество";
            dataClient.Columns[4].Header = "Дата рождения";

        }

        private void add(object sender, RoutedEventArgs e)
        {
            if (check() == true)
            {
                clientTA.Insert(fam.Text,
                    im.Text,
                    otch.Text,
                    birthday.DisplayDate
                    );
                clientTA.Fill(dataset.Client);

                errorFam.Content = " ";
                errorName.Content = " ";
                errorOtch.Content = " ";
                errorDate.Content = " ";
            }
        }

        private void update(object sender, RoutedEventArgs e)
        {
            if (check() == true)
            {
                clientTA.UpdateQuery(fam.Text,
                    im.Text,
                    otch.Text,
                    birthday.SelectedDate.ToString(),
                    ID_Client
                    );
                clientTA.Fill(dataset.Client);

                errorFam.Content = " ";
                errorName.Content = " ";
                errorOtch.Content = " ";
                errorDate.Content = " ";
            }
        }

        private void delete(object sender, RoutedEventArgs e)
        {
            if (dataClient.SelectedItem != null)
            {
                clientTA.DeleteQuery(ID_Client);
                clientTA.Fill(dataset.Client);

                errorFam.Content = " ";
                errorName.Content = " ";
                errorOtch.Content = " ";
                errorDate.Content = " ";
            }
        }

        private void exit(object sender, RoutedEventArgs e)
        {
            serviceManager serviceManager = new serviceManager();
            serviceManager.Show();
            this.Close();
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
                System.Data.DataTable table = dataset.Client;
                xlWorksheet.Name = table.TableName;
                for (int j = 1; j < table.Columns.Count + 1; j++)
                {

                    ExcelApp.Cells[1, j] = table.Columns[j - 1].ColumnName;

                }

                for (int k = 0; k < table.Rows.Count; k++)
                {
                    for (int l = 0; l < table.Columns.Count; l++)
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
            if (fam.Text.Length == 0) { errorFam.Content = "Введите фамилию"; return false; }
            if (Regex.Match(fam.Text, "[!@#&%*_\\-.]").Length != 0) { errorFam.Content = "Удалите спецсимволы"; return false; }
            if (Regex.Match(fam.Text, "\\d").Length != 0) { errorFam.Content = "Удалите цифры"; return false; }
            if (Regex.Match(fam.Text, "[A-Za-z]").Length != 0) { errorFam.Content = "Удалите латинские символы"; return false; }
            if (Regex.Match(fam.Text, "[А-Яа-я]").Length == 0) { errorFam.Content = "Введите буквы кириллицы"; return false; }

            if (im.Text.Length == 0) { errorName.Content = "Введите имя"; return false; }
            if (Regex.Match(im.Text, "[!@#&%*_\\-.]").Length != 0) { errorName.Content = "Удалите спецсимволы"; return false; }
            if (Regex.Match(im.Text, "\\d").Length != 0) { errorName.Content = "Удалите цифры"; return false; }
            if (Regex.Match(im.Text, "[A-Za-z]").Length != 0) { errorName.Content = "Удалите латинские символы"; return false; }

            if (Regex.Match(otch.Text, "[!@#&%*_\\-.]").Length != 0) { errorOtch.Content = "Удалите спецсимволы"; return false; }
            if (Regex.Match(otch.Text, "\\d").Length != 0) { errorOtch.Content = "Удалите цифры"; return false; }
            if (Regex.Match(otch.Text, "[A-Za-z]").Length != 0) { errorOtch.Content = "Удалите латинские символы"; return false; }

            if (birthday.GetValue(DatePicker.SelectedDateProperty) == null) { errorDate.Content = "Выберете дату"; return false; }

            if (Regex.Match(im.Text, " ").Length != 0) { errorName.Content = "Удалите пробелы"; return false; }
            if (Regex.Match(otch.Text, " ").Length != 0) { errorOtch.Content = "Удалите пробелы"; return false; }
            if (Regex.Match(fam.Text, " ").Length != 0) { errorFam.Content = "Удалите пробелы"; return false; }

            return true;
        }
    }
}
