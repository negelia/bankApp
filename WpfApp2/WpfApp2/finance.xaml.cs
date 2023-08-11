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
    /// Логика взаимодействия для finance.xaml
    /// </summary>
    public partial class finance : System.Windows.Window
    {
        DataSet1 dataset;
        EmployeesTableAdapter employeesTA;
        JobTableAdapter jobtitleTA;
        FunctionsTableAdapter functionsTA;
        Financial_PlanTableAdapter financeTA;
        financeViewTableAdapter financeViewTA;

        int ID_Employee;
        int ID_Job;
        int ID_Functions;
        int ID_Plan;
        public finance()
        {
            InitializeComponent();

            dataset = new DataSet1();
            functionsTA = new FunctionsTableAdapter();
            financeTA = new Financial_PlanTableAdapter();
            financeViewTA = new financeViewTableAdapter();

            functionsTA.Fill(dataset.Functions);
            financeTA.Fill(dataset.Financial_Plan);
            financeViewTA.Fill(dataset.financeView);

            FunctionCB.ItemsSource = dataset.Functions.DefaultView;
            FunctionCB.DisplayMemberPath = "Title";
            FunctionCB.SelectedValuePath = "ID_Functions";
            FunctionCB.SelectedItem = 0;

            dataFinance.ItemsSource = dataset.financeView.DefaultView;
            dataFinance.SelectionMode = DataGridSelectionMode.Single;
            dataFinance.SelectedValuePath = "ID_Functions";
            dataFinance.SelectedValuePath = "ID_Plan";
            dataFinance.CanUserAddRows = false;
            dataFinance.CanUserDeleteRows = false;
            dataFinance.IsReadOnly = true;
        }

        private void dataFinance_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)dataFinance.SelectedItem;
            if (dataRowView != null)
            {
                title.Text = dataRowView.Row.Field<String>("Название");
                //price.Text = dataRowView.Row.Field<String>("Сумма для реализации");

                ID_Plan = dataRowView.Row.Field<int>("ID_Plan");
                ID_Functions = dataRowView.Row.Field<int>("ID_Functions");
            }
        }

        private void add(object sender, RoutedEventArgs e)
        {
            if (check() == true)
            {
                financeTA.Insert(
                    title.Text,
                    date.DisplayDate,
                    Convert.ToDecimal(price.Text),
                    (int)FunctionCB.SelectedValue
                    );
                financeTA.Fill(dataset.Financial_Plan);
                financeViewTA.Fill(dataset.financeView);

                errorDate.Content = " ";
                errorFunction.Content = " ";
                errorPrice.Content = " ";
                errorTitle.Content = " ";
            }
        }

        private void update(object sender, RoutedEventArgs e)
        {
            if (check() == true)
            {
                financeTA.UpdateQuery(
                    title.Text,
                    date.SelectedDate.ToString(),
                    Convert.ToDecimal(price.Text),
                    (int)FunctionCB.SelectedValue,
                    ID_Plan
                    );
                financeTA.Fill(dataset.Financial_Plan);
                financeViewTA.Fill(dataset.financeView);

                errorDate.Content = " ";
                errorFunction.Content = " ";
                errorPrice.Content = " ";
                errorTitle.Content = " ";
            }
        }

        private void delete(object sender, RoutedEventArgs e)
        {
            if (dataFinance.SelectedItem != null)
            {
                financeTA.DeleteQuery(ID_Plan);
                financeTA.Fill(dataset.Financial_Plan);
                financeViewTA.Fill(dataset.financeView);

                errorDate.Content = " ";
                errorFunction.Content = " ";
                errorPrice.Content = " ";
                errorTitle.Content = " ";
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
            dataFinance.Columns[0].Visibility = Visibility.Hidden;
            dataFinance.Columns[1].Visibility = Visibility.Hidden;
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
                System.Data.DataTable table = dataset.financeView;
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
            if (title.Text.Length == 0) { errorTitle.Content = "Введите название"; return false; }
            if (price.Text.Length == 0) { errorPrice.Content = "Введите сумму"; return false; }

            if (FunctionCB.SelectedItem == null) { errorFunction.Content = "Выберете функцию"; return false; }

            if (Regex.Match(price.Text, "^[.][0-9]+$|^[0-9]*[,]{0,1}[0-9]*$").Length == 0) { errorPrice.Content = "Введите сумму в формате : xx,xx"; return false; }

            if (date.GetValue(DatePicker.SelectedDateProperty) == null) { errorDate.Content = "Выберете дату"; return false; }

            return true;
        }
    }
}
