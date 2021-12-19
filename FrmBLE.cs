using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices.WindowsRuntime;

using Windows.Devices.Bluetooth.GenericAttributeProfile;
using static Common.Service.ToolboxIdentifications;

namespace BleCommunication
{
    
    public partial class FrmBLE : Form
    {
        private FileManager fileManager;
        private BluetoothLEManager bleManager;
        private BindingList<string> logList;
        private Dictionary<Guid, string> uuidDict;
        private Action<string, bool> logAddAct;
        private bool file_save_enable;
        //static System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        //public static bool IsRunning { get { return sw.IsRunning; } }

        private PrevSensorData prevSensorData = new PrevSensorData();

        public FrmBLE()
        {
            InitializeComponent();
            fileManager = new FileManager("log.csv");
            bleManager = new BluetoothLEManager();
            bleManager.LogAdd += logAddEvent;
            bleManager.Detected += detectedEvent;
            logAddAct += logAdd;
            logList = new BindingList<string>();
            lbxLog.DataSource = logList;
            uuidDict = new Dictionary<Guid, string>
            {
                { GattCharacteristicsUuid.MEMS_DATA, "service1_data" },
                { GattCharacteristicsUuid.MEMS_CONF, "service1_conf" },
                { GattCharacteristicsUuid.MEMS_POW,  "service1_pow"  },
                { GattCharacteristicsUuid.DFU_NOBOND, "dfu"},
            };
            prevSensorData.init();
        }

        ~FrmBLE()
        {
            fileManager.Dispose();
            Dispose();
        }

        private string TxData
        {
            get
            {
                string tx = (cbxAccell.Checked ? "AA" : "55") + " "
                          + (cbxGyro.Checked ? "AA" : "55") + " "
                          + (cbxCompass.Checked ? "AA" : "55") + " "
                          + (cbxTemp.Checked ? "AA" : "55") + " "
                          + Convert.ToInt32(tbxHz.Text).ToString("X2") + " "
                          + Convert.ToInt32(tbxTimes.Text).ToString("X2");
                return tx;
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            bleManager.Scan(GattServiceNames.WEARABLE_DEVICE);
        }

        private async void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (cbxCharacteristic.SelectedItem == null) return;
            var uuid = (Guid)cbxCharacteristic.SelectedItem;
            switch (uuidDict[uuid])
            {
                case "service1_data":
                    //sw.Stop();
                    //sw.Reset();
                    prevSensorData.init();
                    cbxPrint.Enabled = true;
                    if (file_save_enable) fileManager.Close();
                    await bleManager.NortifyDisable(uuid);
                    break;
                case "service1_pow":
                    await bleManager.IndicateDisable(uuid);
                    break;
                default:
                    break;
            }
        }

        private async void btnRead_Click(object sender, EventArgs e)
        {
            if (cbxCharacteristic.SelectedItem == null) return;
            var uuid = (Guid)cbxCharacteristic.SelectedItem;

            try
            {
                switch (uuidDict[uuid])
                {
                    case "service1_data":
                        cbxPrint.Enabled = false;
                        file_save_enable = cbxPrint.Checked;
                        if (file_save_enable)
                        {
                            fileManager.Open();
                            await fileManager.Write("time,gFx,gFy,gFz,wx,wy,wz,Bx,By,Bz,interpolation");
                        }
#if true
                        //sw.Start();
                        await bleManager.NortifyEnable(uuid, mems_ValueChanged);
#else
                        var rb = await bleManager.Read(uuid);
                        dspReadCharacteristic(rb);
#endif
                        break;
                    case "service1_conf":
                        var rByte = await bleManager.Read(uuid);
                        //var rx = bytesToStr(rByte);
                        this.Invoke(new Action<byte[]>(configDisplay), rByte);
                        break;
                    case "service1_pow":
                        await bleManager.IndicateEnable(uuid, mems_Indication);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                fileManager.Close();
            }
        }

        private async void btnWrite_Click(object sender, EventArgs e)
        {
            if (cbxCharacteristic.SelectedItem == null) return;
            var uuid = (Guid)cbxCharacteristic.SelectedItem;

            try
            {
                switch (uuidDict[uuid])
                {
                    case "service1_conf":
                        var tx = strToBytes(TxData);
                        await bleManager.Write(uuid, tx);

                        var rByte = await bleManager.Read(uuid);
                        //var rx = bytesToStr(rByte);
                        break;
                    case "dfu":
                        byte[] b = { 0x01 };
                        await bleManager.IndicateEnable(uuid, dfu_Indication);
                        await bleManager.Write(uuid, b);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private async void cbxService_SelectedIndexChanged(object sender, EventArgs e)
        {
            var uuid = (Guid)cbxService.SelectedItem;
            Console.WriteLine($"Selected Service : UUID={uuid}");

            await bleManager.SetCharacteristics(uuid);

            cbxCharacteristicDisplay();
        }
        private void cbxCharacteristic_SelectedIndexChanged(object sender, EventArgs e)
        {
            var uuid = (Guid)cbxCharacteristic.SelectedItem;
            Console.WriteLine($"Selected Characteristic : UUID={uuid}");
        }

        private async void logAdd(string item, bool savefile = false)
        {
            if (logList.Count > 50) logList.RemoveAt(0);
            //var str = DateTime.Now.ToString("HH:mm:ss.fff") + "," + item;
            //var str = sw.ElapsedTicks.ToString("D12");
            //var str1 = str.Substring(0, str.Length - 7);
            //var str2 = str.Substring(str.Length - 7);
            //str2 = str2.Substring(0, str2.Length - 4) + "," + item;
            //var str3 = str1 + "." + str2;
            //            var str = sw.ElapsedTicks.ToString() + "," + item;
            logList.Add(item);
            lbxLog.TopIndex = lbxLog.Items.Count - 1;

            if (savefile & file_save_enable)
            {
                await fileManager.Write(item);

            }
        }
        private void logAddEvent(string item, bool savefile = false)
        {
            this.Invoke(logAddAct, item, savefile);
        }
        private void detectedEvent()
        {
            this.Invoke(new Action(findDeviceDisplay));
        }
        private void findDeviceDisplay()
        {
            lblDeviceName.Text = "Device : " + bleManager.DeviceName;
            lblMacAddress.Text = "MAC : " + bleManager.DeviceAddress;
            cbxServiceDisplay();
        }
        private void configDisplay(byte[] cfg)
        {
            cbxAccell.Checked = (cfg[0] == 0xAA) ? true : false;
            cbxGyro.Checked = (cfg[1] == 0xAA) ? true : false;
            cbxCompass.Checked = (cfg[2] == 0xAA) ? true : false;
            cbxTemp.Checked = (cfg[3] == 0xAA) ? true : false;
            tbxHz.Text = cfg[4].ToString();
            tbxTimes.Text = cfg[5].ToString();
        }
        private void cbxServiceDisplay()
        {
            cbxService.Items.Clear();
            foreach (var service in bleManager.ServiceList)
            {
                cbxService.Items.Add(service.Uuid);
                Console.WriteLine($"Service...");
                Console.WriteLine($"...UUID={service.Uuid}");
                Console.WriteLine($"...AttributeHandle=0x{service.AttributeHandle.ToString("X2")}");
            }
        }
        private void cbxCharacteristicDisplay()
        {
            cbxCharacteristic.Items.Clear();
            foreach (var characteristic in bleManager.CharacteristicList)
            {
                cbxCharacteristic.Items.Add(characteristic.Uuid);
                Console.WriteLine($"Characteristic...");
                Console.WriteLine($"...UUID={characteristic.Uuid}");
                Console.WriteLine($"...AttributeHandle=0x{characteristic.AttributeHandle.ToString("X2")}");
                Console.WriteLine($"...Properties={characteristic.CharacteristicProperties}");
                Console.WriteLine($"...ProtectionLevel={characteristic.ProtectionLevel}");
            }
        }
        private String floatFormat(float num)
        {
            return num.ToString("0.00");
        }
#if true
        private void mems_ValueChanged(GattCharacteristic sender, GattValueChangedEventArgs eventArgs)
        {
//            float x, y, z, temp;
//            string[] sens = { "Accel", "Gyro", "Compass" };
            var bytes = eventArgs.CharacteristicValue.ToArray();
#if false
            logAddEvent($"index {bytes.First()}", true);

            (x, y, z) = getAccel(bytes);
            logAddEvent($"{sens[0]} x:{x.ToString("0.00000")} y:{y.ToString("0.00000")} z:{z.ToString("0.00000")}", true);

            (x, y, z) = getGyro(bytes);
            logAddEvent($"{sens[1]} x:{x.ToString("0.00000")} y:{y.ToString("0.00000")} z:{z.ToString("0.00000")}", true);

            (x, y, z) = getCompass(bytes);
            logAddEvent($"{sens[2]} x:{x.ToString("0.00000")} y:{y.ToString("0.00000")} z:{z.ToString("0.00000")}", true);

            temp = getTemperature(bytes);
            logAddEvent($"Temp {temp.ToString("0.00000")}℃", true);
#endif

            //logAddEvent($"index {bytes.First()}", true);
            var idx = bytes[0] & 0xFF;
            if (prevSensorData.isBegin())
            {                
                prevSensorData.sensorValueIdx = idx;
            }
            var (gFx, gFy, gFz) = getAccel(bytes, 0 * 6 + 1);
            var (wx, wy, wz) = getGyro(bytes, 1 * 6 + 1);
            var (Bx, By, Bz) = getCompass(bytes, 2 * 6 + 1);

            int idxInterval = prevSensorData.getInterval(idx);
            if ( idxInterval > 1)
            {
                for (int k = 0; k < idxInterval - 1; k++)
                {
                    prevSensorData.time += 0.05f;

                    float _gFx = prevSensorData.gFx + ((gFx - prevSensorData.gFx) / (float)idxInterval) * (k + 1);
                    float _gFy = prevSensorData.gFy + ((gFy - prevSensorData.gFy) / (float)idxInterval) * (k + 1);
                    float _gFz = prevSensorData.gFz + ((gFz - prevSensorData.gFz) / (float)idxInterval) * (k + 1);

                    float _wx = prevSensorData.wx + ((wx - prevSensorData.wx) / (float)idxInterval) * (k + 1);
                    float _wy = prevSensorData.wy + ((wy - prevSensorData.wy) / (float)idxInterval) * (k + 1);
                    float _wz = prevSensorData.wz + ((wz - prevSensorData.wz) / (float)idxInterval) * (k + 1);

                    float _bx = prevSensorData.bx + ((Bx - prevSensorData.bx) / (float)idxInterval) * (k + 1);
                    float _by = prevSensorData.by + ((By - prevSensorData.by) / (float)idxInterval) * (k + 1);
                    float _bz = prevSensorData.bz + ((Bz - prevSensorData.bz) / (float)idxInterval) * (k + 1);
                    logAddEvent($"{floatFormat(prevSensorData.time)},{_gFx},{_gFy},{_gFz},{_wx},{_wy},{_wz},{_bx},{_by},{_bz},1", true);                    
                }
            }
                //logAddEvent($"{sens[i]} x:{x} y:{y} z:{z}", true);
                logAddEvent($"{floatFormat(prevSensorData.time + 0.05f)}, {gFx},{gFy},{gFz},{wx},{wy},{wz},{Bx},{By},{Bz},0", true);

            prevSensorData.setData(idx, prevSensorData.time + 0.05f, gFx, gFy, gFz, wx,wy,wz,Bx,By,Bz);

            //var t = getTemperature(bytes, 18 + 1);
            //logAddEvent($"Temp {t}℃", true);

        }
#else
            private void mems_ValueChanged(GattCharacteristic sender, GattValueChangedEventArgs eventArgs)
        {
            string[] sens = { "Accel", "Gyro", "Compass" };
            var bytes = eventArgs.CharacteristicValue.ToArray();
#if false
            dspReadCharacteristic(bytes);
#else
            //logAddEvent($"index {bytes.First()}", true);
            var (gFx, gFy, gFz) = getHalf(bytes, 0 * 6 + 1);
            var (wx, wy, wz) = getHalf(bytes, 1 * 6 + 1);
            var (Bx, By, Bz) = getHalf(bytes, 2 * 6 + 1);
            //logAddEvent($"{sens[i]} x:{x} y:{y} z:{z}", true);
            logAddEvent($"{gFx},{gFy},{gFz},{wx},{wy},{wz},{Bx},{By},{Bz}", true);
            var t = Half.ToHalf(bytes, 18 + 1);
            //logAddEvent($"Temp {t}℃", true);
#endif
        }
#endif

        private void mems_Indication(GattCharacteristic sender, GattValueChangedEventArgs eventArgs)
        {
            string[] lvls = { "Non", "High", "Middle", "Low", "Bottom" };
            var bytes = eventArgs.CharacteristicValue.ToArray();
            var pow = lvls[bytes.First()];
            var charge = bytes[1] == 0xAA ? "Charging" : "Not Charge";
            var version = bytes[2];

            logAddEvent($"Battery Level {pow} {charge} Ver.{version}", true);
        }
        private void temp_ValueChanged(GattCharacteristic sender, GattValueChangedEventArgs eventArgs)
        {
            var bytes = eventArgs.CharacteristicValue.ToArray();
            var temp = Half.ToHalf(bytes, 0);
            logAddEvent($"Temperature {temp}", true);
        }
        private void dfu_Indication(GattCharacteristic sender, GattValueChangedEventArgs eventArgs)
        {
            var bytes = eventArgs.CharacteristicValue.ToArray();
            logAddEvent(BitConverter.ToString(bytes).Replace('-', ' '));
        }
        public void dspReadCharacteristic(byte[] bytes)
        {
            var len = bytes.Length / 20;
            //Console.WriteLine($"last index : {bytes.First()}");
            for (int i = 0; i < len; i++)
            {
                //Console.WriteLine($"Read Data {i}");
                for (int j = 0; j < 3; j++)
                {
                    var offset = (i * 20) + (0 * 6 + 1);
                    var (gFx, gFy, gFz) = getHalf(bytes, offset);
                    offset = (i * 20) + (1 * 6 + 1);
                    var (wx, wy, wz) = getHalf(bytes, offset);
                    offset = (i * 20) + (2 * 6 + 1);
                    var (Bx, By, Bz) = getHalf(bytes, offset);
                    Console.Write($"{gFx},{gFy},{gFz},{wx},{wy},{wz},{Bx},{By},{Bz}");
                }
                //var t = Half.ToHalf(bytes, (i * 20) + 18 + 1);
                //Console.WriteLine($"{t}");
            }
            Console.WriteLine("\n");
        }
        private (Half, Half, Half) getHalf(byte[] bytes, int offset)
        {
            Half x, y, z;

            x = Half.ToHalf(bytes, offset);
            y = Half.ToHalf(bytes, offset + 2);
            z = Half.ToHalf(bytes, offset + 4);

            return (x, y, z);
        }
        private byte[] strToBytes(string str)
        {
            var bytes = str.Split(' ').Select(s => Convert.ToByte(s, 16)).ToArray();
            return bytes;
        }
        private string bytesToStr(byte[] bytes)
        {
            var str = BitConverter.ToString(bytes).Replace("-", " ");
            return str;
        }

        private (float, float, float) getAccel(byte[] bytes, int offset)
        {
            Int16 tempX, tempY, tempZ;
            float x, y, z;

            (tempX, tempY, tempZ) = getInt16(bytes, offset);

            x = (float)((double)tempX * 32.0 / 65536.0);
            y = (float)((double)tempY * 32.0 / 65536.0);
            z = (float)((double)tempZ * 32.0 / 65536.0);

            return (x, y, z);
        }

        private (float, float, float) getGyro(byte[] bytes, int offset)
        {
            Int16 tempX, tempY, tempZ;
            float x, y, z;

            (tempX, tempY, tempZ) = getInt16(bytes, offset);

            x = (float)((double)tempX * 4000.0 / 65536.0);
            y = (float)((double)tempY * 4000.0 / 65536.0);
            z = (float)((double)tempZ * 4000.0 / 65536.0);

            return (x, y, z);
        }

        private (float, float, float) getCompass(byte[] bytes, int offset)
        {
            Int16 tempX, tempY, tempZ;
            float x, y, z;

            (tempX, tempY, tempZ) = getInt16(bytes, offset);

            x = (float)((double)tempX * 600.0 / 4096.0);
            y = (float)((double)tempY * 600.0 / 4096.0);
            z = (float)((double)tempZ * 600.0 / 4096.0);

            return (x, y, z);
        }

        private float getTemperature(byte[] bytes, int offset)
        {
            Int16 temp;
            float temperature;

            temp = bytesToInt16(bytes, offset);

            temperature = (float)(-45.0 + 175.0 * (double)temp / 65535.0);

            return temperature;
        }

        private (Int16, Int16, Int16) getInt16(byte[] bytes, int offset)
        {
            Int16 x, y, z;

            x = bytesToInt16(bytes, offset);
            y = bytesToInt16(bytes, offset + 2);
            z = bytesToInt16(bytes, offset + 4);

            return (x, y, z);
        }

        private Int16 bytesToInt16(byte[] bytes, int offset)
        {
            ushort temp1, temp2;

            temp1 = bytes[offset];
            temp2 = bytes[offset + 1];
 
            return (Int16)((int)temp1 + ((int)temp2 * 256));
        }
    }
}
