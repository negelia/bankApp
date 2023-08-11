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
    /// Логика взаимодействия для buh.xaml
    /// </summary>
    public partial class buh : System.Windows.Window
    {
        DataSet1 dataset;
        Financial_PlanTableAdapter planTA;
        Credit_AgreementTableAdapter creditTA;
        StatementTableAdapter statementTA;
        buhViewTableAdapter buhViewTA;

        int ID_Plan;
        int ID_Statement;
        int ID_Credit_Agreement;

        public buh()
        {
          InitializeComponent();

            dataset = new DataSet1();
            planTA = new Financial_PlanTableAdapter();
            creditTA = new Credit_AgreementTableAdapter();
            statementTA = new StatementTableAdapter();
            buhViewTA = new buhViewTableAdapter();

            planTA.Fill(dataset.Financial_Plan);
            creditTA.Fill(dataset.Credit_Agreement);
            statementTA.Fill(dataset.Statement);
            buhViewTA.Fill(dataset.buhView);

            PlanCB.ItemsSource = dataset.Financial_Plan.DefaultView;
            PlanCB.DisplayMemberPath = "Sum";
            PlanCB.SelectedValuePath = "ID_Plan";
            PlanCB.SelectedItem = 0;

            CreditCB.ItemsSource = dataset.Credit_Agreement.DefaultView;
            CreditCB.DisplayMemberPath = "Sum";
            CreditCB.SelectedValuePath = "ID_Credit_Agreement";
            CreditCB.SelectedItem = 0;

            dataBuh.ItemsSource = dataset.buhView.DefaultView;
            dataBuh.SelectionMode = DataGridSelectionMode.Single;
            dataBuh.SelectedValuePath = "ID_Credit_Agreement";
            dataBuh.SelectedValuePath = "ID_Plan";
            dataBuh.SelectedValuePath = "ID_Statement";
            dataBuh.CanUserAddRows = false;
            dataBuh.CanUserDeleteRows = false;
            dataBuh.IsReadOnly = true;
        }

        private void dataBuh_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)dataBuh.SelectedItem;
            if (dataRowView != null)
            {
                buhTitle.Text = dataRowView.Row.Field<String>("Отчётность");

                ID_Credit_Agreement = dataRowView.Row.Field<int>("ID_Credit_Agreement");
                ID_Plan = dataRowView.Row.Field<int>("ID_Plan");
                ID_Statement = dataRowView.Row.Field<int>("ID_Statement");
            }
        }

        private void add(object sender, RoutedEventArgs e)
        {
            if (check() == true)
            {
                statementTA.Insert(buhTitle.Text,
                    dateBuh.DisplayDate,
                    (int)PlanCB.SelectedValue,
                    (int)CreditCB.SelectedValue
                    );
                statementTA.Fill(dataset.Statement);
                buhViewTA.Fill(dataset.buhView);

                errorTitle.Content = " ";
                errorPlan.Content = " ";
                errorDate.Content = " ";
                errorCredit.Content = " ";
            }
        }

        private void update(object sender, RoutedEventArgs e)
        {
            if (check() == true)
            {
                statementTA.UpdateQuery(buhTitle.Text,
                    dateBuh.SelectedDate.ToString(),
                    (int)PlanCB.SelectedValue,
                    (int)CreditCB.SelectedValue,
                    ID_Statement
                    );
                statementTA.Fill(dataset.Statement);
                buhViewTA.Fill(dataset.buhView);

                errorTitle.Content = " ";
                errorPlan.Content = " ";
                errorDate.Content = " ";
                errorCredit.Content = " ";
            }
        }

        private void delete(object sender, RoutedEventArgs e)
        {
            if (dataBuh.SelectedItem != null)
            {
                statementTA.DeleteQuery(ID_Statement);
                statementTA.Fill(dataset.Statement);
                buhViewTA.Fill(dataset.buhView);

                errorTitle.Content = " ";
                errorPlan.Content = " ";
                errorDate.Content = " ";
                errorCredit.Content = " ";
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
            if (buhTitle.Text.Length == 0) { errorTitle.Content = "Введите название"; return false; }

            if (PlanCB.SelectedItem == null) { errorPlan.Content = "Укажите убытки"; return false; }
            if (CreditCB.SelectedItem == null) { errorCredit.Content = "Укажите доходы"; return false; }

            if (dateBuh.GetValue(DatePicker.SelectedDateProperty) == null) { errorDate.Content = "Выберете дату"; return false; }

            return true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataBuh.Columns[0].Visibility = Visibility.Hidden;
            dataBuh.Columns[1].Visibility = Visibility.Hidden;
            dataBuh.Columns[2].Visibility = Visibility.Hidden;
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
                System.Data.DataTable table = dataset.buhView;
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
    }
}
