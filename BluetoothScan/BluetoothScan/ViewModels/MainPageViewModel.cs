using BluetoothScan.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace BluetoothScan.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private INavigationService _navigationService { get; }
        private IPageDialogService _dialogService { get; }

        public MainPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
           : base(navigationService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            Title = "Bluetooth Device Scan";

            DeviceSelectedCommand = new DelegateCommand<BTDeviceInfo>(DeviceSelected);
            ScanStartCommand = new DelegateCommand(ScanStart);
        }

        public DelegateCommand<BTDeviceInfo> DeviceSelectedCommand { get; }
        public DelegateCommand ScanStartCommand { get; }

        private ObservableCollection<BTDeviceInfo> _BluetoothDevices;
        public ObservableCollection<BTDeviceInfo> BluetoothDevices
        {
            get => _BluetoothDevices;
            set => SetProperty(ref _BluetoothDevices, value);
        }

        private string _ScanStatus;
        public string ScanStatus
        {
            get { return _ScanStatus; }
            set { SetProperty(ref _ScanStatus, value); }
        }

        // It navigates to device details page
        private async void DeviceSelected(BTDeviceInfo device)
        {
            //var navigationParams = new NavigationParameters
            //{
            //    { "bluetoothDevice", device }
            //};


            //await _navigationService.NavigateAsync("DevicePage", navigationParams);
            await _dialogService.DisplayAlertAsync("Alert", $"You selected {device.DeviceName}", "OK");
        }

        // It grabs the blutooth devices data from Android project using DI. 
        public void ScanStart()
        {
             BluetoothDevices = Xamarin.Forms.DependencyService.Get<IBluetoothDeviceManager>().ScanDevices();
        }

        public override async void OnNavigatingTo(INavigationParameters parameters)
        {
           
        }
    }
}