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
    /// Логика взаимодействия для credit.xaml
    /// </summary>
    public partial class credit : System.Windows.Window
    {
        DataSet1 dataset;
        EmployeesTableAdapter employeesTA;
        ClientTableAdapter clientTA;
        Credit_AgreementTableAdapter creditTA;
        creditViewTableAdapter creditViewTA;

        int ID_Employee;
        int ID_Client;
        int ID_Credit_Agreement;
        public credit()
        {
            InitializeComponent();
            dataset = new DataSet1();
            employeesTA = new EmployeesTableAdapter();
            clientTA = new ClientTableAdapter();
            creditTA = new Credit_AgreementTableAdapter();
            creditViewTA = new creditViewTableAdapter();

            employeesTA.Fill(dataset.Employees);
            clientTA.Fill(dataset.Client);
            creditTA.Fill(dataset.Credit_Agreement);
            creditViewTA.Fill(dataset.creditView);

            EmployeeCB.ItemsSource = dataset.Employees.DefaultView;
            EmployeeCB.DisplayMemberPath = "Surname";
            EmployeeCB.SelectedValuePath = "ID_Employee";
            EmployeeCB.SelectedItem = 0;

            ClientCB.ItemsSource = dataset.Client.DefaultView;
            ClientCB.DisplayMemberPath = "Surname";
            ClientCB.SelectedValuePath = "ID_Client";
            ClientCB.SelectedItem = 0;

            dataCredit.ItemsSource = dataset.creditView.DefaultView;
            dataCredit.SelectionMode = DataGridSelectionMode.Single;
            dataCredit.SelectedValuePath = "ID_Employee";
            dataCredit.SelectedValuePath = "ID_Client";
            dataCredit.SelectedValuePath = "ID_Credit_Agreement";
            dataCredit.CanUserAddRows = false;
            dataCredit.CanUserDeleteRows = false;
            dataCredit.IsReadOnly = true;
        }

        private void dataCredit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)dataCredit.SelectedItem;
            if (dataRowView != null)
            {
                title.Text = dataRowView.Row.Field<String>("Кредитный договор");
                //price.Text = dataRowView.Row.Field<String>("Сумма кредита");

                ID_Employee = dataRowView.Row.Field<int>("ID_Employee");
                ID_Client = dataRowView.Row.Field<int>("ID_Client");
                ID_Credit_Agreement = dataRowView.Row.Field<int>("ID_Credit_Agreement");
            }
        }

        private void add(object sender, RoutedEventArgs e)
        {
            if (check() == true)
            {
                creditTA.Insert(title.Text,
                    dateCredit.DisplayDate,
                    Convert.ToDecimal(price.Text),
                    (int)ClientCB.SelectedValue,
                    (int)EmployeeCB.SelectedValue
                    );
                creditTA.Fill(dataset.Credit_Agreement);

                creditViewTA.Fill(dataset.creditView);

                errorTitle.Content = " ";
                errorDateCredit.Content = " ";
                errorEmployee.Content = " ";
            }
        }

        private void update(object sender, RoutedEventArgs e)
        {
            if (check() == true)
            {
                creditTA.UpdateQuery(title.Text,
                    dateCredit.SelectedDate.ToString(),
                    Convert.ToDecimal(price.Text),
                    (int)ClientCB.SelectedValue,
                    (int)EmployeeCB.SelectedValue,
                    ID_Credit_Agreement
                    );
                creditTA.Fill(dataset.Credit_Agreement);

                creditViewTA.Fill(dataset.creditView);

                errorTitle.Content = " ";
                errorDateCredit.Content = " ";
                errorEmployee.Content = " ";
            }
        }

        private void delete(object sender, RoutedEventArgs e)
        {
            if (dataCredit.SelectedItem != null)
            {
                creditTA.DeleteQuery(ID_Credit_Agreement);
                creditTA.Fill(dataset.Credit_Agreement);
                creditViewTA.Fill(dataset.creditView);

                errorTitle.Content = " ";
                errorDateCredit.Content = " ";
                errorEmployee.Content = " ";
            }
        }

        private void exit(object sender, RoutedEventArgs e)
        {
            MainWindow MainWindow = new MainWindow();
            MainWindow.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataCredit.Columns[0].Visibility = Visibility.Hidden;
            dataCredit.Columns[1].Visibility = Visibility.Hidden;
            dataCredit.Columns[2].Visibility = Visibility.Hidden;
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
                System.Data.DataTable table = dataset.creditView;
                xlWorksheet.Name = table.TableName;
                for (int j = 4; j < table.Columns.Count + 1; j++)
                {

                    ExcelApp.Cells[1, j] = table.Columns[j - 1].ColumnName;

                }

                for (int k = 0; k < table.Rows.Count; k++)
                {
                    for (int l = 3; l < table.Columns.Count; l++)
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
            if (dateCredit.GetValue(DatePicker.SelectedDateProperty) == null) { errorDateCredit.Content = "Выберете дату"; return false; }

            if (EmployeeCB.SelectedItem == null) { errorEmployee.Content = "Выберете сотрудника"; return false; }

            if (Regex.Match(price.Text, "^[.][0-9]+$|^[0-9]*[,]{0,1}[0-9]*$").Length == 0) { errorPrice.Content = "Введите сумму в формате : xx,xx"; return false; }

            return true;
        }
    }
}
