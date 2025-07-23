using System.Windows;
using TruongAnhTuanWPF.ViewModel;

namespace TruongAnhTuanWPF.View
{
    public partial class AdminBookingWindow : Window
    {
        public AdminBookingWindow()
        {
            InitializeComponent();
            DataContext = new AdminBookingViewModel(App._manageBookingServiceSingleton, App._manageBookingDetailServiceSingleton, App._manageCustomerServiceSingleton, App._manageRoomServiceSingleton);
        }
    }
} 