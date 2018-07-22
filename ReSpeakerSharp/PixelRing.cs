using LibUsbDotNet;
using LibUsbDotNet.Main;

namespace ReSpeakerSharp
{
    /// <summary>
    /// PixelRing API
    /// <seealso cref="http://wiki.seeedstudio.com/ReSpeaker_Mic_Array_v2.0/#getting-started"/>
    /// </summary>
    public class PixelRing
    {
        public ReSpeakerMicArray Device { get; private set; }

        public PixelRing(ReSpeakerMicArray device) => Device = device;

        /// <summary>
        /// trace mode, LEDs changing depends on VAD* and DOA*
        /// </summary>
        public void Trace() => SendPixelRingCommand(0, 0);

        /// <summary>
        /// mono mode, set all RGB LED to a single color, 
        /// </summary>
        /// <param name="r">0-255</param>
        /// <param name="g">0-255</param>
        /// <param name="b">0-255</param>
        public void Mono(int r, int g, int b) => SendPixelRingCommand(1, new byte[] { (byte)(r & 0xff), (byte)(g & 0xff), (byte)(b & 0xff), 0 });
        
        /// <summary>
        /// listen mode, similar with trace mode, but not turn LEDs off
        /// </summary>
        public void Listen() => SendPixelRingCommand(2, 0);

        /// <summary>
        /// wait mode
        /// </summary>
        public void Speak() => SendPixelRingCommand(3, 0);

        /// <summary>
        /// speak mode
        /// </summary>
        public void Think() => SendPixelRingCommand(4, 0);
        
        /// <summary>
        /// spin mode
        /// </summary>
        public void Spin() => SendPixelRingCommand(5, 0);
        
        /// <summary>
        /// custom mode, set each LED to its own color
        /// </summary>
        /// <param name="data">[r, g, b, 0] * 12</param>
        public void Customize(byte[] data) => SendPixelRingCommand(6, data);
        
        /// <summary>
        /// set brightness
        /// </summary>
        /// <param name="brightness">brightness (0x00 ... 0x1F)</param>
        public void SetBrightness(int brightness) => SendPixelRingCommand(0x20, brightness);
        
        /// <summary>
        /// set two color palette (r1,g1,b1) and (r2,g2,b2)
        /// together with pixelRing.Think()
        /// </summary>
        /// <param name="r1">R1</param>
        /// <param name="g1">G1</param>
        /// <param name="b1">B1</param>
        /// <param name="r2">R2</param>
        /// <param name="g2">G2</param>
        /// <param name="b2">B2</param>
        public void SetColorPallette(int r1, int g1, int b1, int r2, int g2, int b2)
        {
            SendPixelRingCommand(0x21, new byte[] {
                (byte)(r1 & 0xFF),(byte)(g1 & 0xFF),(byte)(b1 & 0xFF),0,
                (byte)(r2 & 0xFF),(byte)(g2 & 0xFF),(byte)(b2 & 0xFF),0 });
        }
        
        /// <summary>
        /// set center LED
        /// </summary>
        /// <param name="vadLed">0 - off, 1 - on, else - depends on VAD</param>
        public void SetVadLed(int vadLed) => SendPixelRingCommand(0x22, vadLed);
        
        /// <summary>
        /// show volume
        /// </summary>
        /// <param name="volume">0 ... 12</param>
        public void SetVolume(int volume) => SendPixelRingCommand(0x23, volume);
        
        /// <summary>
        /// set pattern
        /// </summary>
        /// <param name="pattern">0 - Google Home pattern, others - Echo pattern</param>
        public void ChangePattern(int pattern) => SendPixelRingCommand(0x24, pattern);

        private int SendPixelRingCommand(byte command, int data)
        {
            return SendPixelRingCommand(command, new byte[] { (byte)(data & 0xFF) });
        }
        private int SendPixelRingCommand(byte command, byte[] data)
        {
            // PyUsb command
            // https://github.com/pyusb/pyusb/blob/master/docs/tutorial.rst
            // ctrl_transfer(usb.util.CTRL_OUT | usb.util.CTRL_TYPE_VENDOR | usb.util.CTRL_RECIPIENT_DEVICE, 0,         command,   0x1C, data, TIMEOUT)
            // The first four parameters are the                                              bmRequestType, bmRequest, wValue and wIndex
            // The fifth parameter is either the data payload for an OUT transfer or the number of bytes to read in an IN transfer. 
            //
            // see also
            // http://libusbdotnet.sourceforge.net/V2/html/6ca38f2f-9888-a154-7691-c844f9f18537.htm

            byte requestType = (byte)(UsbCtrlFlags.Direction_Out | UsbCtrlFlags.RequestType_Vendor | UsbCtrlFlags.Recipient_Device);
            UsbSetupPacket packet = new UsbSetupPacket(requestType, 0, command, 0x1C, (short)data.Length);

            Device.Device.ControlTransfer(ref packet, data, data.Length, out int len);
            return len;
        }
    }
}
