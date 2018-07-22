using LibUsbDotNet;
using LibUsbDotNet.Main;
using System;

namespace ReSpeakerSharp
{
    public class Tuning
    {
        public ReSpeakerMicArray Device { get; private set; }

        /// <summary>
        /// Tuning API
        /// <seealso cref="http://wiki.seeedstudio.com/ReSpeaker_Mic_Array_v2.0/#getting-started"/>
        /// <seealso cref="https://github.com/respeaker/usb_4_mic_array/blob/master/tuning.py"/>
        /// </summary>
        public Tuning(ReSpeakerMicArray device) => Device = device;

        /// <summary>
        /// DOA angle. Current value. Orientation depends on build configuration(0 ... 359).
        /// </summary>
        public int Direction => DoaAngle;
        /// <summary>
        /// DOA angle. Current value. Orientation depends on build configuration(0 ... 359).
        /// </summary>
        public int DoaAngle
        {
            //'DOAANGLE': (21, 0, 'int', 359, 0, 'ro', 'DOA angle. Current value. Orientation depends on build configuration.')
            get => ReadInt(21, 0);
        }

        /// <summary>
        /// VAD voice activity status. false (no voice activity), true (voice activity)
        /// </summary>
        public bool IsVoice => VoiceActivity;
        /// <summary>
        /// VAD voice activity status. false (no voice activity), true (voice activity)
        /// </summary>
        public bool VoiceActivity
        {
            //'VOICEACTIVITY': (19, 32, 'int', 1, 0, 'ro', 'VAD voice activity status.', '0 = false (no voice activity)', '1 = true (voice activity)'),
            get => ReadBool(19, 32);
        }

        /// <summary>
        /// AEC Path Change Detection. false (no path change detected), true (path change detected)
        /// </summary>
        public bool AecPathChange
        {
            //'AECPATHCHANGE': (18, 25, 'int', 1, 0, 'ro', 'AEC Path Change Detection.', '0 = false (no path change detected)', '1 = true (path change detected)'),
            get => ReadBool(18, 25);
        }
        /// <summary>
        /// Current RT60 estimate in seconds(0.25 ... 0.9).
        /// </summary>
        public float RT60
        {
            // 'RT60': (18, 26, 'float', 0.9, 0.25, 'ro', 'Current RT60 estimate in seconds'),
            get => ReadFloat(18, 26);
        }
        /// <summary>
        /// AEC far-end silence detection status. false (signal detected), true (silence detected)
        /// </summary>
        public bool AecSilenceMode
        {
            // 'AECSILENCEMODE': (18, 31, 'int', 1, 0, 'ro', 'AEC far-end silence detection status. ', '0 = false (signal detected) ', '1 = true (silence detected)'),
            get => ReadBool(18, 31);
        }
        /// <summary>
        /// FSB Update Decision. false (FSB was not updated), true (FSB was updated)
        /// </summary>
        public bool FsbUpdated
        {
            // 'FSBUPDATED': (19, 23, 'int', 1, 0, 'ro', 'FSB Update Decision.', '0 = false (FSB was not updated)', '1 = true (FSB was updated)'),
            get => ReadBool(19, 23);
        }
        /// <summary>
        /// FSB Path Change Detection. false (no path change detected), true (path change detected)
        /// </summary>
        public bool FsbPathChange
        {
            // 'FSBPATHCHANGE': (19, 24, 'int', 1, 0, 'ro', 'FSB Path Change Detection.', '0 = false (no path change detected)', '1 = true (path change detected)'),
            get => ReadBool(19, 24);
        }
        /// <summary>
        /// High-pass Filter on microphone signals.
        /// 0 = OFF, 1 = ON - 70 Hz cut-off, 2 = ON - 125 Hz cut-off, 3 = ON - 180 Hz cut-off
        /// </summary>
        public int HpfOnOff
        {
            // 'HPFONOFF': (18, 27, 'int', 3, 0, 'rw', 'High-pass Filter on microphone signals.', '0 = OFF', '1 = ON - 70 Hz cut-off', '2 = ON - 125 Hz cut-off', '3 = ON - 180 Hz cut-off'),
            get => ReadInt(18, 27);
            set => WriteInt(18, 27, value);
        }
        /// <summary>
        /// RT60 Estimation for AES. false = OFF, true = ON
        /// </summary>
        public bool Rt60On
        {
            // 'RT60ONOFF': (18, 28, 'int', 1, 0, 'rw', 'RT60 Estimation for AES. 0 = OFF 1 = ON'),
            get => ReadBool(18, 28);
            set => WriteBool(18, 28, value);
        }

        /// <summary>
        /// Target power level of the output signal(1e-08 ... 0.99).
        /// [−inf .. 0] dBov (default: −23dBov = 10log10(0.005))
        /// </summary>
        public float AgcDesiredLevel
        {
            // 'AGCDESIREDLEVEL': (19, 2, 'float', 0.99, 1e-08, 'rw', 'Target power level of the output signal. ', '[−inf .. 0] dBov (default: −23dBov = 10log10(0.005))'),
            get => ReadFloat(19, 2);
            set => WriteFloat(19, 2, value);
        }

        /// <summary>
        /// Limit on norm of AEC filter coefficients(0.25 ... 16)
        /// </summary>
        public float AecNorm
        {
            //'AECNORM': (18, 19, 'float', 16, 0.25, 'rw', 'Limit on norm of AEC filter coefficients'),
            get => ReadFloat(18, 19);
            set => WriteFloat(18, 19, value);
        }

        /// <summary>
        /// Threshold for signal detection in AEC(1e-09 ... 1).
        /// [-inf .. 0] dBov (Default: -80dBov = 10log10(1x10-8))
        /// </summary>
        public float AecSilenceLevel
        {
            //'AECSILENCELEVEL': (18, 30, 'float', 1, 1e-09, 'rw', 'Threshold for signal detection in AEC [-inf .. 0] dBov (Default: -80dBov = 10log10(1x10-8))'),
            get => ReadFloat(18, 30);
            set => WriteFloat(18, 30, value);
        }
        /// <summary>
        /// Automatic Gain Control. false = OFF, true = ON
        /// </summary>
        public bool AgcOn
        {
            //'AGCONOFF': (19, 0, 'int', 1, 0, 'rw', 'Automatic Gain Control. ', '0 = OFF ', '1 = ON'),
            get => ReadBool(19, 0);
            set => WriteBool(19, 0, value);
        }
        /// <summary>
        /// Maximum AGC gain factor(1 ... 1000).
        /// [0 .. 60] dB (default 30dB = 20log10(31.6))
        /// </summary>
        public float AgcMaxGain
        {
            //'AGCMAXGAIN': (19, 1, 'float', 1000, 1, 'rw', 'Maximum AGC gain factor. ', '[0 .. 60] dB (default 30dB = 20log10(31.6))'),
            get => ReadFloat(19, 1);
            set => WriteFloat(19, 1, value);
        }
        /// <summary>
        /// Current AGC gain factor(1 ... 1000).
        /// [0 .. 60] dB (default: 0.0dB = 20log10(1.0))
        /// </summary>
        public float AgcGain
        {
            //'AGCGAIN': (19, 3, 'float', 1000, 1, 'rw', 'Current AGC gain factor. ', '[0 .. 60] dB (default: 0.0dB = 20log10(1.0))'),
            get => ReadFloat(19, 3);
            set => WriteFloat(19, 3, value);
        }
        /// <summary>
        /// Ramps-up / down time-constant in seconds. (0.1 ... 1)
        /// </summary>
        public float AgcTime
        {
            //'AGCTIME': (19, 4, 'float', 1, 0.1, 'rw', 'Ramps-up / down time-constant in seconds.'),
            get => ReadFloat(19, 4);
            set => WriteFloat(19, 4, value);
        }
        /// <summary>
        /// Comfort Noise Insertion.  false = OFF, true = ON
        /// </summary>
        public bool CniOn
        {
            //'CNIONOFF': (19, 5, 'int', 1, 0, 'rw', 'Comfort Noise Insertion.', '0 = OFF', '1 = ON'),
            get => ReadBool(19, 5);
            set => WriteBool(19, 5, value);
        }
        /// <summary>
        /// Adaptive beamformer updates. false = Adaptation enabled, true = Freeze adaptation, filter only
        /// </summary>
        public bool FreezeOn
        {
            //'FREEZEONOFF': (19, 6, 'int', 1, 0, 'rw', 'Adaptive beamformer updates.', '0 = Adaptation enabled', '1 = Freeze adaptation, filter only'),
            get => ReadBool(19, 6);
            set => WriteBool(19, 6, value);
        }
        /// <summary>
        /// Stationary noise suppression. false = OFF, true = ON
        /// </summary>
        public bool StatNoiseOn
        {
            //'STATNOISEONOFF': (19, 8, 'int', 1, 0, 'rw', 'Stationary noise suppression.', '0 = OFF', '1 = ON'),
            get => ReadBool(19, 8);
            set => WriteBool(19, 8, value);
        }
        /// <summary>
        /// Over-subtraction factor of stationary noise(0 ... 3).
        /// min .. max attenuation
        /// </summary>
        public float GammaNs
        {
            //'GAMMA_NS': (19, 9, 'float', 3, 0, 'rw', 'Over-subtraction factor of stationary noise. min .. max attenuation'),
            get => ReadFloat(19, 9);
            set => WriteFloat(19, 9, value);
        }
        /// <summary>
        /// Gain-floor for stationary noise suppression(0 ... 1).
        /// [−inf .. 0] dB (default: −16dB = 20log10(0.15))
        /// </summary>
        public float MinNs
        {
            //'MIN_NS': (19, 10, 'float', 1, 0, 'rw', 'Gain-floor for stationary noise suppression.', '[−inf .. 0] dB (default: −16dB = 20log10(0.15))'),
            get => ReadFloat(19, 10);
            set => WriteFloat(19, 10, value);
        }
        /// <summary>
        /// Non-stationary noise suppression. false = OFF, true = ON
        /// </summary>
        public bool NonStatNoiseOn
        {
            //'NONSTATNOISEONOFF': (19, 11, 'int', 1, 0, 'rw', 'Non-stationary noise suppression.', '0 = OFF', '1 = ON'),
            get => ReadBool(19, 11);
            set => WriteBool(19, 11, value);
        }
        /// <summary>
        /// Over-subtraction factor of non- stationary noise(0 ... 3).
        /// min .. max attenuation
        /// </summary>
        public float GammaNn
        {
            //'GAMMA_NN': (19, 12, 'float', 3, 0, 'rw', 'Over-subtraction factor of non- stationary noise. min .. max attenuation'),
            get => ReadFloat(19, 12);
            set => WriteFloat(19, 12, value);
        }
        /// <summary>
        /// Gain-floor for non-stationary noise suppression(0 ... 1).
        /// [−inf .. 0] dB (default: −10dB = 20log10(0.3))
        /// </summary>
        public float MinNn
        {
            //'MIN_NN': (19, 13, 'float', 1, 0, 'rw', 'Gain-floor for non-stationary noise suppression.', '[−inf .. 0] dB (default: −10dB = 20log10(0.3))'),
            get => ReadFloat(19, 13);
            set => WriteFloat(19, 13, value);
        }
        /// <summary>
        /// Echo suppression. false = OFF, true = ON
        /// </summary>
        public bool EchoOn
        {
            //'ECHOONOFF': (19, 14, 'int', 1, 0, 'rw', 'Echo suppression.', '0 = OFF', '1 = ON'),
            get => ReadBool(19, 14);
            set => WriteBool(19, 14, value);
        }
        /// <summary>
        /// Over-subtraction factor of echo (direct and early components) (0 ... 3).
        /// min .. max attenuation
        /// </summary>
        public float GammaE
        {
            //'GAMMA_E': (19, 15, 'float', 3, 0, 'rw', 'Over-subtraction factor of echo (direct and early components). min .. max attenuation'),
            get => ReadFloat(19, 15);
            set => WriteFloat(19, 15, value);
        }
        /// <summary>
        /// Over-subtraction factor of echo (tail components) (0 ... 3). min .. max attenuation
        /// </summary>
        public float GammaETail
        {
            //'GAMMA_ETAIL': (19, 16, 'float', 3, 0, 'rw', 'Over-subtraction factor of echo (tail components). min .. max attenuation'),
            get => ReadFloat(19, 16);
            set => WriteFloat(19, 16, value);
        }
        /// <summary>
        /// Over-subtraction factor of non-linear echo(0 ... 5). min .. max attenuation
        /// </summary>
        public float GammaENl
        {
            //'GAMMA_ENL': (19, 17, 'float', 5, 0, 'rw', 'Over-subtraction factor of non-linear echo. min .. max attenuation'),
            get => ReadFloat(19, 17);
            set => WriteFloat(19, 17, value);
        }
        /// <summary>
        /// Non-Linear echo attenuation. false = OFF, true = ON
        /// </summary>
        public bool NlAttenOn
        {
            //'NLATTENONOFF': (19, 18, 'int', 1, 0, 'rw', 'Non-Linear echo attenuation.', '0 = OFF', '1 = ON'),
            get => ReadBool(19, 18);
            set => WriteBool(19, 18, value);
        }
        /// <summary>
        /// Non-Linear AEC training mode. 
        /// 0 = OFF, 1 = ON - phase 1, 2 = ON - phase 2
        /// </summary>
        public int NlAecMode
        {
            // 'NLAEC_MODE': (19, 20, 'int', 2, 0, 'rw', 'Non-Linear AEC training mode.', '0 = OFF', '1 = ON - phase 1', '2 = ON - phase 2'),
            get => ReadInt(19, 20);
            set => WriteInt(19, 20, value);
        }
        /// <summary>
        /// Speech detection status.
        /// false (no speech detected), true (speech detected)
        /// </summary>
        public bool SpeechDetected
        {
            //'SPEECHDETECTED': (19, 22, 'int', 1, 0, 'ro', 'Speech detection status.', '0 = false (no speech detected)', '1 = true (speech detected)'),
            get => ReadBool(19, 22);
        }
        /// <summary>
        /// Transient echo suppression.
        /// false = OFF, true = ON
        /// </summary>
        public bool TransientOn
        {
            //'TRANSIENTONOFF': (19, 29, 'int', 1, 0, 'rw', 'Transient echo suppression.', '0 = OFF', '1 = ON'),
            get => ReadBool(19, 29);
            set => WriteBool(19, 29, value);
        }
        /// <summary>
        /// Stationary noise suppression for ASR.
        /// false = OFF, true = ON
        /// </summary>
        public bool StatNoiseOnSr
        {
            //'STATNOISEONOFF_SR': (19, 33, 'int', 1, 0, 'rw', 'Stationary noise suppression for ASR.', '0 = OFF', '1 = ON'),
            get => ReadBool(19, 33);
            set => WriteBool(19, 33, value);
        }
        /// <summary>
        /// Non-stationary noise suppression for ASR.
        /// false = OFF, true = ON
        /// </summary>
        public bool NonStatNoiseOnSr
        {
            //'NONSTATNOISEONOFF_SR': (19, 34, 'int', 1, 0, 'rw', 'Non-stationary noise suppression for ASR.', '0 = OFF', '1 = ON'),
            get => ReadBool(19, 34);
            set => WriteBool(19, 34, value);
        }
        /// <summary>
        /// Over-subtraction factor of stationary noise for ASR(0 ... 3).
        /// [0.0 .. 3.0] (default: 1.0)
        /// </summary>
        public float GammaNsSr
        {
            //'GAMMA_NS_SR': (19, 35, 'float', 3, 0, 'rw', 'Over-subtraction factor of stationary noise for ASR. ', '[0.0 .. 3.0] (default: 1.0)'),
            get => ReadFloat(19, 35);
            set => WriteFloat(19, 35, value);
        }
        /// <summary>
        /// Over-subtraction factor of non-stationary noise for ASR(0 ... 3). 
        /// [0.0 .. 3.0] (default: 1.1)
        /// </summary>
        public float GammaNnSr
        {
            //'GAMMA_NN_SR': (19, 36, 'float', 3, 0, 'rw', 'Over-subtraction factor of non-stationary noise for ASR. ', '[0.0 .. 3.0] (default: 1.1)'),
            get => ReadFloat(19, 36);
            set => WriteFloat(19, 36, value);
        }
        /// <summary>
        /// Gain-floor for stationary noise suppression for ASR(0 ... 1).
        /// [−inf .. 0] dB (default: −16dB = 20log10(0.15))
        /// </summary>
        public float MinNsSr
        {
            //'MIN_NS_SR': (19, 37, 'float', 1, 0, 'rw', 'Gain-floor for stationary noise suppression for ASR.', '[−inf .. 0] dB (default: −16dB = 20log10(0.15))'),
            get => ReadFloat(19, 37);
            set => WriteFloat(19, 37, value);
        }
        /// <summary>
        /// Gain-floor for non-stationary noise suppression for ASR(0 ... 1).
        /// [−inf .. 0] dB (default: −10dB = 20log10(0.3))
        /// </summary>
        public float MinNnSr
        {
            //'MIN_NN_SR': (19, 38, 'float', 1, 0, 'rw', 'Gain-floor for non-stationary noise suppression for ASR.', '[−inf .. 0] dB (default: −10dB = 20log10(0.3))'),
            get => ReadFloat(19, 38);
            set => WriteFloat(19, 38, value);
        }
        /// <summary>
        /// Set the threshold for voice activity detection(0 ... 1000).
        /// [−inf .. 60] dB (default: 3.5dB 20log10(1.5))
        /// </summary>
        public float GammaVadSr
        {
            //'GAMMAVAD_SR': (19, 39, 'float', 1000, 0, 'rw', 'Set the threshold for voice activity detection.', '[−inf .. 60] dB (default: 3.5dB 20log10(1.5))'),
            get => ReadFloat(19, 39);
            set => WriteFloat(19, 39, value);
        }

        private void WriteBool(byte id, byte data, bool value)
        {
            WriteInt(id, data, value ? 1 : 0);
        }
        private void WriteInt(byte id, byte data, int value)
        {
            byte[] buf = new byte[12];
            Array.Copy(BitConverter.GetBytes((int)data), 0, buf, 0, 4);
            Array.Copy(BitConverter.GetBytes(value), 0, buf, 4, 4);
            Array.Copy(BitConverter.GetBytes((int)1), 0, buf, 8, 4);

            byte requestType = (byte)(UsbCtrlFlags.Direction_Out | UsbCtrlFlags.RequestType_Vendor | UsbCtrlFlags.Recipient_Device);
            UsbSetupPacket packet = new UsbSetupPacket(requestType,
                0, 0, id, (short)buf.Length);
            Device.Device.ControlTransfer(ref packet, buf, buf.Length, out int len);
        }
        private void WriteFloat(byte id, byte data, float value)
        {
            byte[] buf = new byte[12];
            Array.Copy(BitConverter.GetBytes((int)data), 0, buf, 0, 4);
            Array.Copy(BitConverter.GetBytes(value), 0, buf, 4, 4);
            Array.Copy(BitConverter.GetBytes((int)0), 0, buf, 8, 4);

            byte requestType = (byte)(UsbCtrlFlags.Direction_Out | UsbCtrlFlags.RequestType_Vendor | UsbCtrlFlags.Recipient_Device);
            UsbSetupPacket packet = new UsbSetupPacket(requestType,
                0, 0, id, (short)buf.Length);
            Device.Device.ControlTransfer(ref packet, buf, buf.Length, out int len);
        }

        private bool ReadBool(byte id, byte data)
        {
            return !(ReadInt(id, data) == 0);
        }
        private int ReadInt(byte id, byte data)
        {
            byte cmd = (byte)(0x80 | data);
            cmd |= 0x40;

            short length = 8;

            byte requestType = (byte)(UsbCtrlFlags.Direction_In | UsbCtrlFlags.RequestType_Vendor | UsbCtrlFlags.Recipient_Device);
            UsbSetupPacket packet = new UsbSetupPacket(requestType, 
                0, cmd, id, length);

            byte[] buf = new byte[8];
            Device.Device.ControlTransfer(ref packet, buf, buf.Length, out int len);
            return BitConverter.ToInt32(buf, 0);
        }
        private float ReadFloat(byte id, byte data)
        {
            byte cmd = (byte)(0x80 | data);

            short length = 8;

            byte requestType = (byte)(UsbCtrlFlags.Direction_In | UsbCtrlFlags.RequestType_Vendor | UsbCtrlFlags.Recipient_Device);
            UsbSetupPacket packet = new UsbSetupPacket(requestType,
                0, cmd, id, length);

            byte[] buf = new byte[8];
            Device.Device.ControlTransfer(ref packet, buf, buf.Length, out int len);
            int a = BitConverter.ToInt32(buf, 0);
            int b = BitConverter.ToInt32(buf, 4);
            return (float)(a * Math.Pow(2, b));
        }
    }
}
