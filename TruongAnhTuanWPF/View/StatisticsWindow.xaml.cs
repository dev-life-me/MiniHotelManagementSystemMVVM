using System;
using System.Linq;
using System.Windows;
using DAL.Entities;

namespace TruongAnhTuanWPF.View
{
    public partial class StatisticsWindow : Window
    {
        public StatisticsWindow()
        {
            InitializeComponent();
        }

        private void StatisticButton_Click(object sender, RoutedEventArgs e)
        {
            if (FromDatePicker.SelectedDate == null || ToDatePicker.SelectedDate == null)
            {
                ResultTextBlock.Text = "Vui lòng chọn đủ ngày bắt đầu và kết thúc.";
                RoomStatisticsGrid.ItemsSource = null;
                return;
            }
            var from = DateOnly.FromDateTime(FromDatePicker.SelectedDate.Value);
            var to = DateOnly.FromDateTime(ToDatePicker.SelectedDate.Value);
            if (from > to)
            {
                ResultTextBlock.Text = "Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc.";
                RoomStatisticsGrid.ItemsSource = null;
                return;
            }
            var reservations = App._manageBookingServiceSingleton.GetAllReservations()
                .Where(r => r.BookingDate != null && r.BookingDate.Value >= from && r.BookingDate.Value <= to)
                .ToList();
            var totalRevenue = reservations.Sum(r => r.TotalPrice ?? 0);
            ResultTextBlock.Text = $"Tổng số đơn đặt phòng: {reservations.Count}\nTổng doanh thu: {totalRevenue:N0} VND";

            // Thống kê theo phòng
            var roomStats = reservations
                .SelectMany(r => r.BookingDetails)
                .GroupBy(d => d.Room.RoomNumber)
                .Select(g => new
                {
                    RoomNumber = g.Key,
                    Count = g.Count(),
                    Revenue = g.Sum(d => d.ActualPrice.GetValueOrDefault() * (d.EndDate.DayNumber - d.StartDate.DayNumber))
                })
                .OrderByDescending(x => x.Revenue)
                .ToList();
            RoomStatisticsGrid.ItemsSource = roomStats;
        }
    }
} 