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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public DataTable Select(string selectSQL)
        {
            DataTable dataTable = new DataTable("dataBase");
            SqlConnection connection = new SqlConnection("Data Source=LAPTOP-3CDDHCO8\\SQLEXPRESS; Database=BankDB; Persist Security Info=false; User ID='sa'; Password='sa'; MultipleActiveResultSets=True; Trusted_Connection=False");
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = selectSQL;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dataTable);
            return dataTable;
        }

        private void signIn(object sender, RoutedEventArgs e)
        {
            if (check() == true)
            {
                DataTable admin = Select("SELECT * FROM [dbo].[Employees] WHERE [Login] = '" + login.Text + "' AND [Password] = '" + password.Password + "' AND [ID_Job] = '1'");
                DataTable marketUser = Select("SELECT * FROM [dbo].[Employees] WHERE [Login] = '" + login.Text + "' AND [Password] = '" + password.Password + "' AND [ID_Job] = '2'");
                DataTable clients = Select("SELECT * FROM [dbo].[Employees] WHERE [Login] = '" + login.Text + "' AND [Password] = '" + password.Password + "' AND [ID_Job] = '3'");
                DataTable financeUser = Select("SELECT * FROM [dbo].[Employees] WHERE [Login] = '" + login.Text + "' AND [Password] = '" + password.Password + "' AND [ID_Job] = '4'");
                DataTable program = Select("SELECT * FROM [dbo].[Employees] WHERE [Login] = '" + login.Text + "' AND [Password] = '" + password.Password + "' AND [ID_Job] = '5'");
                DataTable buhUser = Select("SELECT * FROM [dbo].[Employees] WHERE [Login] = '" + login.Text + "' AND [Password] = '" + password.Password + "' AND [ID_Job] = '6'");
                DataTable creditUser = Select("SELECT * FROM [dbo].[Employees] WHERE [Login] = '" + login.Text + "' AND [Password] = '" + password.Password + "' AND [ID_Job] = '8'");
                if (admin.Rows.Count > 0)
                {
                    employees employees = new employees();
                    employees.Show();
                    this.Close();

                }
                else if (marketUser.Rows.Count > 0)
                {

                    market market = new market();
                    market.Show();
                    this.Close();

                }
                else if (clients.Rows.Count > 0)
                {

                    serviceManager service = new serviceManager();
                    service.Show();
                    this.Close();

                }
                else if (financeUser.Rows.Count > 0)
                {

                    finance finance = new finance();
                    finance.Show();
                    this.Close();

                }
                else if (program.Rows.Count > 0)
                {

                    it it = new it();
                    it.Show();
                    this.Close();

                }
                else if (buhUser.Rows.Count > 0)
                {

                    buh buh = new buh();
                    buh.Show();
                    this.Close();

                }
                else if (creditUser.Rows.Count > 0)
                {

                    credit credit = new credit();
                    credit.Show();
                    this.Close();

                }
                else MessageBox.Show("Пользователь не найден");
            }
        }

        public bool check()
        {
            if (password.Password.Length == 0) { errorPass.Content = "Введите пароль"; return false; }
            if (login.Text.Length == 0) { errorLogin.Content = "Введите логин"; return false; }

            if (Regex.Match(password.Password, "[!@#&%*_\\-.]").Length == 0) { errorPass.Content = "Введите спецсимволы"; return false; }
            if (Regex.Match(login.Text, "[!@#&%*_\\-.]").Length == 0) { errorLogin.Content = "Введите спецсимволы"; return false; }

            if (Regex.Match(password.Password, "\\d").Length == 0) { errorPass.Content = "Введите цифры"; return false; }
            if (Regex.Match(login.Text, "\\d").Length == 0) { errorLogin.Content = "Введите цифры"; return false; }

            if (Regex.Match(password.Password, "[A-Za-z]").Length == 0) { errorPass.Content = "Введите латинские символы"; return false; }
            if (Regex.Match(login.Text, "[A-Za-z]").Length == 0) { errorLogin.Content = "Введите латинские символы"; return false; }

            if (Regex.Match(password.Password, "[А-Яа-я]").Length != 0) { errorPass.Content = "Удалите буквы кириллицы"; return false; }
            if (Regex.Match(login.Text, "[А-Яа-я]").Length != 0) { errorLogin.Content = "Удалите буквы кириллицы"; return false; }

            if (Regex.Match(password.Password, " ").Length != 0) { errorPass.Content = "Удалите пробелы"; return false; }
            if (Regex.Match(login.Text, " ").Length != 0) { errorLogin.Content = "Удалите пробелы"; return false; }

            return true;
        }
    }
}
