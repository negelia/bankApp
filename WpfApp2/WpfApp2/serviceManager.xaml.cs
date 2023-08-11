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
    /// Логика взаимодействия для serviceManager.xaml
    /// </summary>
    public partial class serviceManager : Window
    {
        public serviceManager()
        {
            InitializeComponent();
        }

        private void exit(object sender, RoutedEventArgs e)
        {
            MainWindow MainWindow = new MainWindow();
            MainWindow.Show();
            this.Close();
        }

        private void client(object sender, RoutedEventArgs e)
        {
            clients clients = new clients();
            clients.Show();
            this.Close();
        }

        private void services(object sender, RoutedEventArgs e)
        {
            service service = new service();
            service.Show();
            this.Close();
        }
    }
}
