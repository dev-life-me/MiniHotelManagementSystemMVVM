
using System.Windows;
using TruongAnhTuanWPF.ViewModel;

namespace TruongAnhTuanWPF.View
{
    public partial class AdminCustomerWindow : Window
    {
        public AdminCustomerWindow()
        {
            InitializeComponent();
            DataContext = new AdminCustomerViewModel(App._manageCustomerServiceSingleton);
        }
    }
} 