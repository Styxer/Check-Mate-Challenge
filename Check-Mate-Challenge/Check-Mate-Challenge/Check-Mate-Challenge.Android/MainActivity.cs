using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Check_Mate_Challenge.Droid;
using Check_Mate_Challenge.Model;
using Prism;
using Prism.Ioc;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

[assembly: Dependency(typeof(MainActivity))]
namespace Check_Mate_Challenge.Droid
{
    [Activity(Label = "Check_Mate_Challenge", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

      //  private static MainActivity _instance;
        private bool _isReceiveredRegistered;
        private BluetoothDeviceReceiver _receiver;

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            // Register for broadcasts when a device is discovered
            _receiver = new BluetoothDeviceReceiver();

            //TODO REMOVE
            StartScanning();
            // Register broadcast listeners
            RegisterBluetoothReceiver();
            ////////////////////

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));
        }


        private static void StartScanning()
        {
            if (!BluetoothDeviceReceiver.Adapter.IsDiscovering) BluetoothDeviceReceiver.Adapter.StartDiscovery();
        }

        private static void CancelScanning()
        {
            if (BluetoothDeviceReceiver.Adapter.IsDiscovering) BluetoothDeviceReceiver.Adapter.CancelDiscovery();
        }

        private void RegisterBluetoothReceiver()
        {
            if (_isReceiveredRegistered) return;

            RegisterReceiver(_receiver, new IntentFilter(BluetoothDevice.ActionFound));
            RegisterReceiver(_receiver, new IntentFilter(BluetoothAdapter.ActionDiscoveryStarted));
            RegisterReceiver(_receiver, new IntentFilter(BluetoothAdapter.ActionDiscoveryFinished));
            _isReceiveredRegistered = true;
        }

        private void UnregisterBluetoothReceiver()
        {
            if (!_isReceiveredRegistered) return;

            UnregisterReceiver(_receiver);
            _isReceiveredRegistered = false;
        }


        public ObservableCollection<Bluetooth> getNearByDevices()
        {
            throw new System.NotImplementedException("TODO");
        }

        private ObservableCollection<Bluetooth> getBondedDevices()
        {
            var items = new ObservableCollection<Bluetooth>();

            items.Concat(
                BluetoothDeviceReceiver.Adapter.BondedDevices.Select(
                    bluetoothDevice => new Bluetooth(
                        bluetoothDevice.Name,
                        bluetoothDevice.Address
                    )
                )
            );

            return items;
          
        }



        protected override void OnDestroy()
        {
            base.OnDestroy();

            // Make sure we're not doing discovery anymore
            CancelScanning();

            // Unregister broadcast listeners
            UnregisterBluetoothReceiver();
        }

        protected override void OnPause()
        {
            base.OnPause();

            // Make sure we're not doing discovery anymore
            CancelScanning();

            // Unregister broadcast listeners
            UnregisterBluetoothReceiver();
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}

