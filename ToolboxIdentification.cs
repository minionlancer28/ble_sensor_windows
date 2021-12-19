/*Copyright (c) 2015, Nordic Semiconductor ASA
 *
 *Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
 *
 *1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
 *
 *2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other 
 *materials provided with the distribution.
 *
 *3. Neither the name of the copyright holder nor the names of its contributors may be used to endorse or promote products derived from this software without specific 
 *prior written permission.
 *
 *THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
 *PURPOSE ARE DISCLAIMED. *IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF *SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, *DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 *ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED *OF THE POSSIBILITY OF SUCH DAMAGE.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.GenericAttributeProfile;

namespace Common.Service
{
	public static class ToolboxIdentifications
	{
		public static class GattServiceNames
		{
			public const string ALERT_NOTIFICATION = "AlertNotification";
			public const string BATTERY = "Battery";
			public const string BLOOD_PRESSURE = "BloodPressure";
			public const string CURRENT_TIME = "CurrentTime";
			public const string CYCLING_POWER = "CyclingPower";
			public const string CYCLING_SPEED_AND_CADENCE = "CyclingSpeedAndCadence";
			public const string DEVICE_INFORMATION = "DeviceInformation";
			public const string GENERIC_ACCESS = "GenericAccess";
			public const string GENERIC_ATTRIBUTES = "GenericAttribute";
			public const string GLUCOSE = "Glucose";
			public const string HEALTH_THERMOMETER = "HealthThermometer";
			public const string HEART_RATE = "HeartRate";
			public const string HUMAN_INTERFACE_DEVICE = "HumanInterfaceDevice";
			public const string IMMEDIATE_ALERT = "ImmediateAlert";
			public const string LINK_LOSS = "LinkLoss";
			public const string LOCATION_AND_NAVIGATION = "LocationAndNavigation";
			public const string NEXT_DST_CHANGE = "NextDstChange";
			public const string PHONE_ALERT_STATUS = "PhoneAlertStatus";
			public const string REFERENCE_TIME_UPDATE = "ReferenceTimeUpdate";
			public const string RUNNING_SPEED_AND_CADENCE = "RunningSpeedAndCadence";
			public const string SCAN_PARAMETERS = "ScanParameters";
			public const string TX_POWER = "TxPower";
			public const string DEVICE_FIRMWARE_UPDATE = "DeviceFirmwareUpdate";
			public const string NORDIC_UART = "Nordic_UART";
            public const string NORDIC_HRM = "Nordic_HRM";
            public const string NORDIC_DFU = "DfuTarg";
            public const string WEARABLE_DEVICE = "WearDev";
            //public const string PACKET = "Packet";
        }

		public static class GattServiceUuids
		{
			//public static readonly Guid DeviceFirmwareUpdate = new Guid("00001530-1212-efde-1523-785feabcd123");
            public static readonly Guid DeviceFirmwareUpdate = new Guid("0000fe59-0000-1000-8000-00805f9b34fb");
            public static readonly Guid Nordic_UART = new Guid("6E400001-B5A3-F393-E0A9-E50E24DCCA9E");
            public static readonly Guid MEMS_Service = new Guid("1074f00d-8a96-fe1e-c5a5-a27d11f5c777");
            public static readonly Guid TEMP_Service = new Guid("1074f0ce-8a96-fe1e-c5a5-a27d11f5c777");
            public static readonly Guid Bleno_Service = new Guid("12345678-1234-5678-1234-56789abcdef0");
		}

		public static class GattCharacteristicsUuid
		{
			//public static readonly Guid DFUControlPoint = new Guid("00001531-1212-efde-1523-785feabcd123");
			//public static readonly Guid DFUPacket = new Guid("00001532-1212-efde-1523-785feabcd123");
			//public static readonly Guid DFUVersion = new Guid("00001534-1212-efde-1523-785feabcd123");
            public static readonly Guid DFUControlPoint = new Guid("8EC90001-F315-4F60-9FB8-838830DAEA50");
            public static readonly Guid DFUPacket = new Guid("8EC90002-F315-4F60-9FB8-838830DAEA50");
            public static readonly Guid DFU_NOBOND = new Guid("8EC90003-F315-4F60-9FB8-838830DAEA50");
            public static readonly Guid DFU_BOND = new Guid("8EC90004-F315-4F60-9FB8-838830DAEA50");
            public static readonly Guid TX = new Guid("6E400002-B5A3-F393-E0A9-E50E24DCCA9E");
			public static readonly Guid RX = new Guid("6E400003-B5A3-F393-E0A9-E50E24DCCA9E");
            public static readonly Guid MEMS_DATA = new Guid("1074beef-8a96-fe1e-c5a5-a27d11f5c777");
            public static readonly Guid MEMS_CONF = new Guid("1074cafe-8a96-fe1e-c5a5-a27d11f5c777");
            public static readonly Guid MEMS_POW  = new Guid("107401CE-8a96-fe1e-c5a5-a27d11f5c777");
            //public static readonly Guid TEMP_DATA = new Guid("1074bead-8a96-fe1e-c5a5-a27d11f5c777");
            //public static readonly Guid TEMP_CONF = new Guid("1074feed-8a96-fe1e-c5a5-a27d11f5c777");
            public static readonly Guid Bleno_Char = new Guid("12345678-1234-5678-1234-56789abcdef1");
        }
		public static class GATTProfileNames
		{
			public const string PROXIMITY = "Proximity";
			public const string DEVICE_FIRMWARE_UPDATE = "DeviceFirmwareUpdate";
		}
		public static class ToolboxProfilesShortNames
		{
			public const string HEART_RATE = "HRM";
			public const string GLUCOSE = "BGM";
			public const string BLOOD_PRESSURE = "BPM";
			public const string CYCLING_SPEED_AND_CADENCE = "CSC";
			public const string DEVICE_FIRMWARE_UPDATE = "DFU";
			public const string HEALTH_THERMOMETER = "HTM";
			public const string PROXIMITY = "PROXIMITY";
			public const string RUNNING_SPEED_AND_CADENCE = "RSC";
			public const string NORDIC_UART = "UART";
		}

		public static class PageId
		{
			public const string HEART_RATE = "Heart Rate Monitor";
			public const string DEVICE_FIRMWARE_UPDATE = "Device Firmware Update";
			public const string BLOOD_PRESSURE = "Blood Pressure";
			public const string CYCLING_SPEED_AND_CADENCE = "Cycling Speed and Cadence";
			public const string GLUCOSE = "Glucose Monitor";
			public const string HEALTH_THERMOMETER = "Health Thermometer";
			public const string PROXIMITY = "Proximity Monitor";
			public const string RUNNING_SPEED_AND_CADENCE = "Running Speed and Cadence";
			public const string MAIN_PAGE = "Main Page";
			public const string NORDIC_UART = "Nordic UART";
		}
	}
}