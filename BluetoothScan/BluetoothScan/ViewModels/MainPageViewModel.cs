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
        #region properties

        private INavigationService _navigationService { get; }
        private IPageDialogService _dialogService { get; }
        private readonly IBluetoothDeviceManager _bluetoothDeviceManager;

        public DelegateCommand<BTDeviceInfo> DeviceSelectedCommand { get; }
        public DelegateCommand ScanStartCommand { get; }
        #endregion

        #region Ctor

        public MainPageViewModel(INavigationService navigationService
            , IPageDialogService dialogService,
            IBluetoothDeviceManager bluetoothDeviceManager)
         : base(navigationService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            _bluetoothDeviceManager = bluetoothDeviceManager;

            Title = "Bluetooth Device Scan";

            DeviceSelectedCommand = new DelegateCommand<BTDeviceInfo>(DeviceSelected);
            ScanStartCommand = new DelegateCommand(ScanStart, () => !IsExecuting )
                .ObservesProperty(() => IsExecuting)
                .ObservesProperty(() => ScanStatus);
        }
        #endregion


        #region view model

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

        private bool _isExecuting;
        public bool IsExecuting
        {
            get => _isExecuting;
            set => SetProperty(ref _isExecuting, value);
        }

        #endregion

        #region functions
        // It navigates to device details page
        private async void DeviceSelected(BTDeviceInfo device)
        {
            await _dialogService.DisplayAlertAsync("Alert", $"You selected {device.DeviceName}", "OK");
        }

        // It grabs the blutooth devices data from Android project using DI. 
        public void ScanStart()
        {
            BluetoothDevices = _bluetoothDeviceManager.ScanDevices();
        } 
        #endregion

        public override async void OnNavigatingTo(INavigationParameters parameters)
        {
           
        }
    }
}