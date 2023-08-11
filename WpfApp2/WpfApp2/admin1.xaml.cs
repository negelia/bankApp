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
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для admin1.xaml
    /// </summary>
    public partial class admin1 : Window
    {
        public admin1()
        {
            InitializeComponent();
        }

        private void employees(object sender, RoutedEventArgs e)
        {
            employees window = new employees();
            window.Show();
            this.Close();
        }

        private void market(object sender, RoutedEventArgs e)
        {
            market window = new market();
            window.Show();
            this.Close();
        }

        private void buh(object sender, RoutedEventArgs e)
        {
            buh window = new buh();
            window.Show();
            this.Close();
        }

        private void credit(object sender, RoutedEventArgs e)
        {
            credit window = new credit();
            window.Show();
            this.Close();
        }

        private void finance(object sender, RoutedEventArgs e)
        {
            finance window = new finance();
            window.Show();
            this.Close();
        }

        private void it(object sender, RoutedEventArgs e)
        {
            it window = new it();
            window.Show();
            this.Close();
        }

        private void service(object sender, RoutedEventArgs e)
        {
            customerView window = new customerView();
            window.Show();
            this.Close();
        }

        private void exit(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }
    }
}
