using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Check_Mate_Challenge.Droid
{
    public class BluetoothDeviceReceiver : BroadcastReceiver
    {
        public static BluetoothAdapter Adapter => BluetoothAdapter.DefaultAdapter;

        public override void OnReceive(Context context, Intent intent)
        {
            var action = intent.Action;

            // Found a device
            switch (action)
            {
                case BluetoothDevice.ActionFound:
                    // Get the device
                    var device = (BluetoothDevice)intent.GetParcelableExtra(BluetoothDevice.ExtraDevice);

                    // Only update the adapter with items which are not bonded
                    if (device.BondState != Bond.Bonded)
                    {
                      
                    }

                    break;
                case BluetoothAdapter.ActionDiscoveryStarted:
                    Console.WriteLine("Discovery Started...");
                    break;
                case BluetoothAdapter.ActionDiscoveryFinished:
                    Console.WriteLine("Discovery Finished.");
                    break;
                default:
                    break;
            }
        }
    }
}