using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices.WindowsRuntime;

using Windows.Devices.Enumeration;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Foundation;

using Common.Service;
using static Common.Service.ToolboxIdentifications;

namespace BleCommunication
{
    class BluetoothLEManager
    {
        #region ***Member***
        private bool isDetected;
        private BluetoothLEAdvertisementWatcher watcher;
        private BluetoothLEDevice device;
        private string companyID;
        private string manufacturerData;
        private ReadOnlyCollection<GattDeviceService> serviceList;
        private ReadOnlyCollection<GattCharacteristic> characteristicList;
        private Dictionary<GattCharacteristic, TypedEventHandler<GattCharacteristic, GattValueChangedEventArgs>> eventDict;
        #endregion

        #region ***CallBacks***
        public event Action<string, bool> LogAdd;
        public event Action Detected;
        private TypedEventHandler<BluetoothLEAdvertisementWatcher, BluetoothLEAdvertisementReceivedEventArgs> scan_cache;
        #endregion

        #region ***Property***
        public string FindName { get; set; }
        public BluetoothLEDevice Device => device;
        public string DeviceName => device.Name;
        public string DeviceAddress => device.BluetoothAddress.ToString("X");
        public string DeviceID => device.DeviceId;
        public string CompanyID => companyID;
        public string ManufactureData => manufacturerData;
        public ReadOnlyCollection<GattDeviceService> ServiceList => serviceList;
        public ReadOnlyCollection<GattCharacteristic> CharacteristicList => characteristicList;
        #endregion

        public BluetoothLEManager()
        {
            watcher = new BluetoothLEAdvertisementWatcher();
            watcher.SignalStrengthFilter.SamplingInterval = TimeSpan.FromMilliseconds(100);
            watcher.ScanningMode = BluetoothLEScanningMode.Active;
            eventDict = new Dictionary<GattCharacteristic, TypedEventHandler<GattCharacteristic, GattValueChangedEventArgs>>();
        }
        ~BluetoothLEManager()
        {
            watcher = null;
        }
        public async Task SetServices()
        {
            var sRet = await device.GetGattServicesAsync();
            serviceList = sRet.Services.ToList().AsReadOnly();
            return;
        }
        public async Task SetCharacteristics(Guid serviceUuid)
        {
            var service = serviceList.First(s => s.Uuid == serviceUuid);
            var cRet = await service.GetCharacteristicsAsync();
            characteristicList = cRet.Characteristics.ToList().AsReadOnly();
            return;
        }
        public GattDeviceService GetServiceFromList(Guid uuid)
        {
            return serviceList.First(s => s.Uuid == uuid);
        }
        public GattCharacteristic GetCharacteristicFromList(Guid uuid)
        {
            return characteristicList.First(c => c.Uuid == uuid);
        }
        public void Scan(string deviceName)
        {
            FindName = deviceName;
            watcher.Received += Watcher_Received;
            scan_cache = Watcher_Received;
            var task = Task.Run(new Action(watcherRun));
        }
        public async Task<byte[]> Read(Guid characteristicUuid)
        {
            var characteristic = GetCharacteristicFromList(characteristicUuid);
            //var rRet = await characteristic.ReadValueAsync();
            var rRet = await characteristic.ReadValueAsync(BluetoothCacheMode.Uncached);
            var rx = rRet.Value.ToArray();
            Console.WriteLine($"<<< {BitConverter.ToString(rx).Replace('-', ' ')}");
            return rx;
        }
        public async Task Write(Guid characteristicUuid, byte[] tx)
        {
            Console.WriteLine($">>> {BitConverter.ToString(tx).Replace('-', ' ')}");
            var characteristic = GetCharacteristicFromList(characteristicUuid);
            await characteristic.WriteValueAsync(tx.AsBuffer(), GattWriteOption.WriteWithoutResponse);
            //await characteristic.WriteValueAsync(tx.AsBuffer(), GattWriteOption.WriteWithResponse);
            return;
        }
        public async Task NortifyEnable(Guid characteristicUuid, TypedEventHandler<GattCharacteristic, GattValueChangedEventArgs> callback)
        {
            var characteristic = GetCharacteristicFromList(characteristicUuid);
            if (!eventDict.ContainsKey(characteristic)) { eventDict.Add(characteristic, callback); }
            characteristic.ValueChanged += callback;
            await characteristic.WriteClientCharacteristicConfigurationDescriptorAsync(
                            GattClientCharacteristicConfigurationDescriptorValue.Notify);
            return;
        }
        public async Task IndicateEnable(Guid characteristicUuid, TypedEventHandler<GattCharacteristic, GattValueChangedEventArgs> callback)
        {
            var characteristic = GetCharacteristicFromList(characteristicUuid);
            if (!eventDict.ContainsKey(characteristic)) { eventDict.Add(characteristic, callback); }
            characteristic.ValueChanged += callback;
            await characteristic.WriteClientCharacteristicConfigurationDescriptorAsync(
                            GattClientCharacteristicConfigurationDescriptorValue.Indicate);
            return;
        }
        public async Task NortifyDisable(Guid characteristicUuid)
        {
            var characteristic = GetCharacteristicFromList(characteristicUuid);
            if (eventDict.TryGetValue(characteristic, out var callback)) characteristic.ValueChanged -= callback;
            await characteristic.WriteClientCharacteristicConfigurationDescriptorAsync(
                            GattClientCharacteristicConfigurationDescriptorValue.None);
            return;
        }
        public async Task IndicateDisable(Guid characteristicUuid)
        {
            var characteristic = GetCharacteristicFromList(characteristicUuid);
            if (eventDict.TryGetValue(characteristic, out var callback)) characteristic.ValueChanged -= callback;
            await characteristic.WriteClientCharacteristicConfigurationDescriptorAsync(
                            GattClientCharacteristicConfigurationDescriptorValue.None);
            return;
        }

        private void watcherRun()
        {
            isDetected = false;
            Console.WriteLine($"Scanning {FindName}...");
            watcher.Start();
            Thread.Sleep(10000);
            if (!isDetected)
            {
                watcher.Stop();
                Console.WriteLine("Stop Scan");
                Console.WriteLine("Device Not Found");
                LogAdd("Device Not Found", false);
            }
            watcher.Received -= scan_cache;
        }
        private async void connectFromDeviceInfo()
        {
            var devices = await DeviceInformation.FindAllAsync(
                GattDeviceService.GetDeviceSelectorFromUuid(
                    ToolboxIdentifications.GattServiceUuids.MEMS_Service)
                );

            var device = devices.First();
            var name = device.Name;
            var id = device.Id;
            Console.WriteLine($"Device.NAME...{name}");
            Console.WriteLine($"Device.ID...{id}");

            var service = await GattDeviceService.FromIdAsync(id);
            var sList = new List<GattDeviceService> { service };
            serviceList = sList.AsReadOnly();
        }
        public async void Watcher_Received(BluetoothLEAdvertisementWatcher sender, BluetoothLEAdvertisementReceivedEventArgs args)
        {
            if (args.Advertisement.LocalName == FindName)
            {
                isDetected = true;
                watcher.Stop();
                Console.WriteLine("Stop Scan");
                Console.WriteLine("Device Found");
                LogAdd("Device Found", false);

                try
                {
                    var name = args.Advertisement.LocalName;
                    var addr = args.BluetoothAddress;
                    Console.WriteLine($"Device.NAME...{name}");
                    Console.WriteLine($"Device.MAC...0x{addr.ToString("X")}");

                    if (args.Advertisement.ManufacturerData.Count != 0)
                    {
                        var manu = args.Advertisement.ManufacturerData.First();
                        companyID = manu.CompanyId.ToString("X4");
                        manufacturerData = BitConverter.ToString(manu.Data.ToArray());
                        Console.WriteLine($"Device.CompanyId...0x{companyID}");
                        Console.WriteLine($"Device.ManufacturerData...{manufacturerData}");
                    }

                    device = await BluetoothLEDevice.FromBluetoothAddressAsync(addr);
                    Console.WriteLine($"Device.ID...{device.DeviceId}");
                    //Console.WriteLine($"Device.ID...{device.BluetoothDeviceId.Id}");
                    //Console.WriteLine($"Device.ID...{device.DeviceInformation.Id}");
                    await SetServices();
                    Detected();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}
