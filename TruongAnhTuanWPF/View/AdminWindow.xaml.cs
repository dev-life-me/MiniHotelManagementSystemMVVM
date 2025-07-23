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
using TruongAnhTuanWPF.ViewModel;

namespace TruongAnhTuanWPF.View
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void ManageCustomer_Click(object sender, RoutedEventArgs e)
        {
            var customerWindow = new AdminCustomerWindow();
            customerWindow.ShowDialog();
        }

        private void ManageRoom_Click(object sender, RoutedEventArgs e)
        {
            var roomWindow = new AdminRoomWindow();
            roomWindow.ShowDialog();
        }

        private void ManageBooking_Click(object sender, RoutedEventArgs e)
        {
            var bookingWindow = new AdminBookingWindow();
            bookingWindow.ShowDialog();
        }

        private void Statistics_Click(object sender, RoutedEventArgs e)
        {
            var statisticsWindow = new StatisticsWindow();
            statisticsWindow.ShowDialog();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.DataContext = new LoginViewModel(App._authServiceSingleton);
            loginWindow.Show();
            this.Close();
        }
    }
}
