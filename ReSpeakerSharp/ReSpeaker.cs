using System.Collections.Generic;
using LibUsbDotNet;
using LibUsbDotNet.Main;

namespace ReSpeakerSharp
{
    public class ReSpeaker
    {
        /// <summary>
        /// ReSpeaker devices
        /// </summary>
        public ReSpeakerMicArray[] Devices { get; private set; }
        /// <summary>
        /// Vendor ID
        /// </summary>
        public int Vid { get; private set; }
        /// <summary>
        /// Product ID
        /// </summary>
        public int Pid { get; private set; }

        public ReSpeaker()
        {
        }

        /// <summary>
        /// Find ReSpeaker Mic Array v2.0
        /// </summary>
        /// <returns>Device list</returns>
        public ReSpeakerMicArray[] Find()
        {
            return Find(0x2886, 0x0018, "SEEED DFU");
        }
        /// <summary>
        /// Find USB devices
        /// </summary>
        /// <param name="vid">Vendor ID</param>
        /// <param name="pid">Product ID</param>
        /// <param name="productString">Product String</param>
        /// <returns>Device list</returns>
        public ReSpeakerMicArray[] Find(int vid, int pid, string productString)
        {
            Vid = vid;
            Pid = pid;

            List<ReSpeakerMicArray> devices = new List<ReSpeakerMicArray>();
            var list = GetAllUsbDevices();

            foreach (var d in list)
            {
                if(d.UsbRegistryInfo.Pid == Pid 
                    && d.UsbRegistryInfo.Vid == Vid
                    && d.Info.ProductString == productString)
                {
                    devices.Add(new ReSpeakerMicArray { Device = d });
                }
            }
            Devices = devices.ToArray();
            return Devices;
        }
        /// <summary>
        /// Returns all USB device to open
        /// </summary>
        /// <returns>USB devices</returns>
        public UsbDevice[] GetAllUsbDevices()
        {
            UsbRegDeviceList allDevices = UsbDevice.AllDevices;
            List<UsbDevice> devices = new List<UsbDevice>();

            foreach (UsbRegistry usbRegistry in allDevices)
            {
                if (usbRegistry.Open(out UsbDevice usbDevice))
                {
                    devices.Add(usbDevice);
                }
            }
            return devices.ToArray();
        }
    }
}
