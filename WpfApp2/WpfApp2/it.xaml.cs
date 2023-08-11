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
    /// Логика взаимодействия для it.xaml
    /// </summary>
    public partial class it : System.Windows.Window
    {
        DataSet1 dataset;
        FunctionsTableAdapter functionsTA;
        EmployeesTableAdapter employeesTA;
        JobTableAdapter jobTA;
        functionsViewTableAdapter functionsViewTA;

        int ID_Functions;
        int ID_Employee;
        int ID_Job;
        public it()
        {
            InitializeComponent();
            dataset = new DataSet1();
            functionsTA = new FunctionsTableAdapter();
            employeesTA = new EmployeesTableAdapter();
            jobTA = new JobTableAdapter();
            functionsViewTA = new functionsViewTableAdapter();

            functionsTA.Fill(dataset.Functions);
            employeesTA.Fill(dataset.Employees);
            jobTA.Fill(dataset.Job);
            functionsViewTA.Fill(dataset.functionsView);

            employeeCB.ItemsSource = dataset.Employees.DefaultView;
            employeeCB.DisplayMemberPath = "Surname";
            employeeCB.SelectedValuePath = "ID_Employee";
            employeeCB.SelectedItem = 0;

            jobCB.ItemsSource = dataset.Job.DefaultView;
            jobCB.DisplayMemberPath = "Title";
            jobCB.SelectedValuePath = "ID_Job";
            jobCB.SelectedItem = 0;

            dataIT.ItemsSource = dataset.functionsView.DefaultView;
            dataIT.SelectionMode = DataGridSelectionMode.Single;
            dataIT.SelectedValuePath = "ID_Functions";
            dataIT.SelectedValuePath = "ID_Employee";
            dataIT.CanUserAddRows = false;
            dataIT.CanUserDeleteRows = false;
            dataIT.IsReadOnly = true;

        }

        private void dataIT_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)dataIT.SelectedItem;
            if (dataRowView != null)
            {
                service.Text = dataRowView.Row.Field<String>("Функция");

                ID_Employee = dataRowView.Row.Field<int>("ID_Employee");
                ID_Functions = dataRowView.Row.Field<int>("ID_Functions");
                ID_Job = dataRowView.Row.Field<int>("ID_Job");
            }
        }

        private void add(object sender, RoutedEventArgs e)
        {
            if (check() == true)
            {
                functionsTA.Insert(service.Text,
                    (int)employeeCB.SelectedValue
                    );
                functionsTA.Fill(dataset.Functions);
                functionsViewTA.Fill(dataset.functionsView);

                errorService.Content = " ";
                errorEmployee.Content = " ";
                errorJob.Content = " ";
            }
        }

        private void update(object sender, RoutedEventArgs e)
        {
            if (check() == true)
            {
                functionsTA.UpdateQuery(service.Text,
                    (int)employeeCB.SelectedValue,
                    ID_Functions
                    );
                functionsTA.Fill(dataset.Functions);
                functionsViewTA.Fill(dataset.functionsView);

                errorService.Content = " ";
                errorEmployee.Content = " ";
                errorJob.Content = " ";
            }
        }

        private void delete(object sender, RoutedEventArgs e)
        {
            if (dataIT.SelectedItem != null)
            {
                functionsTA.DeleteQuery(ID_Functions);
                functionsTA.Fill(dataset.Functions);
                functionsViewTA.Fill(dataset.functionsView);

                errorService.Content = " ";
                errorEmployee.Content = " ";
                errorJob.Content = " ";
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
            if (service.Text.Length == 0) { errorService.Content = "Введите функцию"; return false; }
            if (employeeCB.SelectedItem == null) { errorEmployee.Content = "Выберете сотрудника"; return false; }
            if (jobCB.SelectedItem == null) { errorJob.Content = "Выберете должность"; return false; }

            return true;
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
                System.Data.DataTable table = dataset.functionsView;
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
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataIT.Columns[0].Visibility = Visibility.Hidden;
            dataIT.Columns[1].Visibility = Visibility.Hidden;
            dataIT.Columns[2].Visibility = Visibility.Hidden;
        }
    }
}
