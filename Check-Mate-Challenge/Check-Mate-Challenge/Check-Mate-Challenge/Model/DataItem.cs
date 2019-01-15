using System;
using System.Collections.Generic;
using System.Text;

namespace Check_Mate_Challenge.Model
{
    public class Bluetooth 
    {
        public Bluetooth(string name, string macAddress)
        {
            Name = name;
            MacAddress = macAddress;
        }

        public string MacAddress { get; }

        public string Name { get; set; }

      
    }
}
