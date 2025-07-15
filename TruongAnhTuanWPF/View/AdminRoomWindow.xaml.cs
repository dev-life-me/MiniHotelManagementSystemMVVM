using System.Windows;
using TruongAnhTuanWPF.ViewModel;

namespace TruongAnhTuanWPF.View
{
    public partial class AdminRoomWindow : Window
    {
        public AdminRoomWindow()
        {
            InitializeComponent();
            DataContext = new AdminRoomViewModel(App._manageRoomServiceSingleton);
        }
    }
} 