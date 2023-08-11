using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Data.SqlClient;
using WpfApp2.DataSet1TableAdapters;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Reflection;
using System.ComponentModel;
using System.IO;
using System.Data.OleDb;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для employees.xaml
    /// </summary>
    public partial class employees : Window
    {
        DataSet1 dataset;
        EmployeesTableAdapter employeesTA;
        JobTableAdapter jobtitleTA;
        employees_jobTableAdapter employees_jobtitleTA;

        int ID_Employee;
        int ID_Job;

        SqlConnection connection = new SqlConnection("Data Source=LAPTOP-3CDDHCO8\\SQLEXPRESS; Database=BankDB; Persist Security Info=false; User ID='sa'; Password='sa'; MultipleActiveResultSets=True; Trusted_Connection=False");

        public employees()
        {
            InitializeComponent();
            dataset = new DataSet1();
            employeesTA = new EmployeesTableAdapter();
            jobtitleTA = new JobTableAdapter();
            employees_jobtitleTA = new employees_jobTableAdapter();

            employeesTA.Fill(dataset.Employees);
            jobtitleTA.Fill(dataset.Job);
            employees_jobtitleTA.Fill(dataset.employees_job);

            jobCB.ItemsSource = dataset.Job.DefaultView;
            jobCB.DisplayMemberPath = "Title";
            jobCB.SelectedValuePath = "ID_Job";
            jobCB.SelectedItem = 0;

            dataEmployee.ItemsSource = dataset.employees_job.DefaultView;
            dataEmployee.SelectionMode = DataGridSelectionMode.Single;
            dataEmployee.SelectedValuePath = "ID_Employee";
            dataEmployee.SelectedValuePath = "ID_Job";
            dataEmployee.CanUserAddRows = false;
            dataEmployee.CanUserDeleteRows = false;
            dataEmployee.IsReadOnly = true;
        }

        private void dataEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)dataEmployee.SelectedItem;
            if (dataRowView != null)
            {
                fam.Text = dataRowView.Row.Field<String>("Фамилия");
                im.Text = dataRowView.Row.Field<String>("Имя");
                otch.Text = dataRowView.Row.Field<String>("Отчество");
                login.Text = dataRowView.Row.Field<String>("Логин");
                password.Text = dataRowView.Row.Field<String>("Пароль");
                //jobCB.SelectedValue = dataRowView.Row.Field<string>("Должность");

                ID_Employee = dataRowView.Row.Field<int>("ID_Employee");
                ID_Job = dataRowView.Row.Field<int>("ID_Job");
            }
        }

        private void exit(object sender, RoutedEventArgs e)
        {
            MainWindow MainWindow = new MainWindow();
            MainWindow.Show();
            this.Close();
        }

        private void delete(object sender, RoutedEventArgs e)
        {
            if (dataEmployee.SelectedItem != null)
            {
                employeesTA.DeleteQuery(ID_Employee);
                employeesTA.Fill(dataset.Employees);
                employees_jobtitleTA.Fill(dataset.employees_job);

                errorFam.Content = " ";
                errorName.Content = " ";
                errorOtch.Content = " ";
                errorLogin.Content = " ";
                errorPass.Content = " ";
                errorDate.Content = " ";
                errorJob.Content = " ";
            }
        }

        private void update(object sender, RoutedEventArgs e)
        {
            string sql = string.Format("SELECT* FROM[dbo].[Employees] WHERE[Login] = '" + login.Text + "'");
            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                connection.Open();
                SqlDataReader reader;
                reader = cmd.ExecuteReader();

                int i = 0;
                while (reader.Read())
                {
                    i++;
                }

                if (i == 0)
                {
                    if (check() == true)
                    {
                        employeesTA.UpdateQuery(fam.Text,
                    im.Text,
                    otch.Text,
                    birthday.SelectedDate.ToString(),
                    login.Text,
                    password.Text,
                    (int)jobCB.SelectedValue,
                    ID_Employee
                    );
                        employeesTA.Fill(dataset.Employees);
                        employees_jobtitleTA.Fill(dataset.employees_job);

                        errorFam.Content = " ";
                        errorName.Content = " ";
                        errorOtch.Content = " ";
                        errorLogin.Content = " ";
                        errorPass.Content = " ";
                        errorDate.Content = " ";
                        errorJob.Content = " ";
                    }
                }
                else
                {
                    errorLogin.Content = "Введите уникальный логин";
                }
                connection.Close();
            }
        }

        private void add(object sender, RoutedEventArgs e)
        {
            string sql = string.Format("SELECT* FROM[dbo].[Employees] WHERE[Login] = '" + login.Text + "'");
            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                connection.Open();
                SqlDataReader reader;
                reader = cmd.ExecuteReader();

                int i = 0;
                while (reader.Read())
                {
                    i++;
                }

                if (i == 0)
                {
                    if (check() == true)
                    {
                        employeesTA.Insert(fam.Text,
                    im.Text,
                    otch.Text,
                    birthday.DisplayDate,
                    login.Text,
                    password.Text,
                    (int)jobCB.SelectedValue
                    );
                        employeesTA.Fill(dataset.Employees);
                        employees_jobtitleTA.Fill(dataset.employees_job);

                        errorFam.Content = " ";
                        errorName.Content = " ";
                        errorOtch.Content = " ";
                        errorLogin.Content = " ";
                        errorPass.Content = " ";
                        errorDate.Content = " ";
                        errorJob.Content = " ";
                    }
                }
                else
                {
                    errorLogin.Content = "Введите уникальный логин";
                }
                connection.Close();
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

            if (password.Text.Length == 0) { errorPass.Content = "Введите пароль"; return false; }
            if (login.Text.Length == 0) { errorLogin.Content = "Введите логин"; return false; }

            if (Regex.Match(password.Text, "[!@#&%*_\\-.]").Length == 0) { errorPass.Content = "Введите спецсимволы"; return false; }
            if (Regex.Match(login.Text, "[!@#&%*_\\-.]").Length == 0) { errorLogin.Content = "Введите спецсимволы"; return false; }

            if (Regex.Match(password.Text, "\\d").Length == 0) { errorPass.Content = "Введите цифры"; return false; }
            if (Regex.Match(login.Text, "\\d").Length == 0) { errorLogin.Content = "Введите цифры"; return false; }

            if (Regex.Match(password.Text, "[A-Za-z]").Length == 0) { errorPass.Content = "Введите латинские символы"; return false; }
            if (Regex.Match(login.Text, "[A-Za-z]").Length == 0) { errorLogin.Content = "Введите латинские символы"; return false; }

            if (Regex.Match(password.Text, "[А-Яа-я]").Length != 0) { errorPass.Content = "Удалите буквы кириллицы"; return false; }
            if (Regex.Match(login.Text, "[А-Яа-я]").Length != 0) { errorLogin.Content = "Удалите буквы кириллицы"; return false; }

            if (Regex.Match(password.Text, " ").Length != 0) { errorPass.Content = "Удалите пробелы"; return false; }
            if (Regex.Match(login.Text, " ").Length != 0) { errorLogin.Content = "Удалите пробелы"; return false; }
            if (Regex.Match(im.Text, " ").Length != 0) { errorName.Content = "Удалите пробелы"; return false; }
            if (Regex.Match(otch.Text, " ").Length != 0) { errorOtch.Content = "Удалите пробелы"; return false; }
            if (Regex.Match(fam.Text, " ").Length != 0) { errorFam.Content = "Удалите пробелы"; return false; }

            if (jobCB.SelectedItem == null) { errorJob.Content = "Выберете должность"; return false; }

            return true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataEmployee.Columns[0].Visibility = Visibility.Hidden;
            dataEmployee.Columns[1].Visibility = Visibility.Hidden;
        }
    }
}
