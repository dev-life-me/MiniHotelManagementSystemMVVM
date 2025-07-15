using BLL.Services;
using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace TruongAnhTuanWPF.ViewModel
{
    public class AdminCustomerViewModel : INotifyPropertyChanged
    {
        private readonly BLL.Services.ManageCustomerService _manageCustomerService;
        public ObservableCollection<Customer> Customers { get; set; }

        private Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set { _selectedCustomer = value; OnPropertyChanged(); }
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand SearchCommand { get; }
        public string SearchKeyword { get; set; }
        public ICommand DeleteCommand { get; }
        public ICommand RefreshCommand { get; }

        public AdminCustomerViewModel(BLL.Services.ManageCustomerService manageCustomerService)
        {
            _manageCustomerService = manageCustomerService;
            Customers = new ObservableCollection<Customer>(_manageCustomerService.GetAll());
            AddCommand = new RelayCommand(_ => AddCustomer());
            EditCommand = new RelayCommand(_ => EditCustomer(), _ => SelectedCustomer != null);
            SearchCommand = new RelayCommand(_ => Search());
            DeleteCommand = new RelayCommand(_ => DeleteCustomer(), _ => SelectedCustomer != null);
            RefreshCommand = new RelayCommand(_ => Refresh());
        }

        private void AddCustomer()
        {
            var dialog = new TruongAnhTuanWPF.View.CustomerEditDialog();
            var vm = new CustomerEditDialogViewModel();
            dialog.DataContext = vm;
            vm.RequestClose += result => { if (result) dialog.DialogResult = true; else dialog.DialogResult = false; };
            if (dialog.ShowDialog() == true)
            {
                var newCustomer = new DAL.Entities.Customer
                {
                    CustomerFullName = vm.CustomerFullName,
                    EmailAddress = vm.EmailAddress,
                    Telephone = vm.Telephone,
                    CustomerBirthday = vm.DateOfBirth.HasValue ? DateOnly.FromDateTime(vm.DateOfBirth.Value) : (DateOnly?)null,
                    CustomerStatus = vm.Status,
                    Password = vm.Password
                };
                _manageCustomerService.Add(newCustomer);
                Customers.Add(newCustomer);
            }
        }

        private void EditCustomer()
        {
            if (SelectedCustomer == null) return;
            var dialog = new TruongAnhTuanWPF.View.CustomerEditDialog();
            var vm = new CustomerEditDialogViewModel(SelectedCustomer);
            dialog.DataContext = vm;
            vm.RequestClose += result => { if (result) dialog.DialogResult = true; else dialog.DialogResult = false; };
            if (dialog.ShowDialog() == true)
            {
                SelectedCustomer.CustomerFullName = vm.CustomerFullName;
                SelectedCustomer.EmailAddress = vm.EmailAddress;
                SelectedCustomer.Telephone = vm.Telephone;
                SelectedCustomer.CustomerBirthday = vm.DateOfBirth.HasValue ? DateOnly.FromDateTime(vm.DateOfBirth.Value) : (DateOnly?)null;
                SelectedCustomer.CustomerStatus = vm.Status;
                SelectedCustomer.Password = vm.Password;
                _manageCustomerService.Update(SelectedCustomer);
                Refresh();
            }
        }

        private void DeleteCustomer()
        {
            if (SelectedCustomer == null) return;
            var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa khách hàng {SelectedCustomer.CustomerFullName}?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                _manageCustomerService.Remove(SelectedCustomer);
                Customers.Remove(SelectedCustomer);
            }
        }

        private void Refresh()
        {
            Customers.Clear();
            foreach (var c in _manageCustomerService.GetAll())
                Customers.Add(c);
        }

        private void Search()
        {
            Customers.Clear();
            foreach (var c in _manageCustomerService.Search(SearchKeyword))
                Customers.Add(c);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
} 