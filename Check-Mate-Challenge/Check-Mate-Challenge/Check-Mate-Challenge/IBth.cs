using Check_Mate_Challenge.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Check_Mate_Challenge
{
    public interface IBth
    {
        void StartScanning();
        void RegisterBluetoothReceiver();
        void CancelScanning();
        void UnregisterBluetoothReceiver();
        ObservableCollection<Bluetooth> getBondedDevices();

        ObservableCollection<Bluetooth> getNearByDevices();
    }
}
