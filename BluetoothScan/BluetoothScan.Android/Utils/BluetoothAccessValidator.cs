using Android;
using Android.Provider;
using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.Content.PM;
using Android.Locations;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using BluetoothScan.Models;
using Xamarin.Forms;
using System;
using Android.OS;

namespace BluetoothScan.Droid
{
    public static class BluetoothAccessValidator
    {
        // It checks all needed permissions are granted
        // And asks for permissions if a permission access denied 
        // NOTE: from V23 it is required to have GPS Enabled, in addition to granted location premissions.
        public static void RequestAccessIfNeeded(Activity activity)
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                const int locationPermissionsRequestCode = 1000;

                var locationPermissions = new[]
                {
                    Manifest.Permission.AccessCoarseLocation,
                    Manifest.Permission.AccessFineLocation
                };

                // Has permission to access coarse location?
                var coarseLocationPermissionGranted =
                    ContextCompat.CheckSelfPermission(activity, Manifest.Permission.AccessCoarseLocation);

                // Has permission to access fine location?
                var fineLocationPermissionGranted =
                    ContextCompat.CheckSelfPermission(activity, Manifest.Permission.AccessFineLocation);

                // Request permission from the user if permissions are not granted
                if (coarseLocationPermissionGranted == Permission.Denied ||
                    fineLocationPermissionGranted == Permission.Denied)
                {
                    ActivityCompat.RequestPermissions(activity, locationPermissions, locationPermissionsRequestCode);
                }

                var locationManager = (LocationManager)Forms.Context.GetSystemService(Context.LocationService);

                // It asks the user to enable GPS for scanning BLE-only devices
                if (locationManager.IsProviderEnabled(LocationManager.GpsProvider) == false)
                {
                    var gpsSettingIntent = new Intent(Settings.ActionLocationSourceSettings);
                    Forms.Context.StartActivity(gpsSettingIntent);
                }  
            }
        }
    }
}