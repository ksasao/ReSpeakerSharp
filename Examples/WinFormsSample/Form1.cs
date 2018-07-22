using ReSpeakerSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsSample
{
    public partial class Form1 : Form
    {
        ReSpeaker respeaker = new ReSpeaker();
        private PixelRing pixelRing;
        private Tuning tuning;

        int R = 0;
        int G = 0;
        int B = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var devices = respeaker.Find();
            if(devices.Length == 0)
            {
                this.label1.Text = "No ReSpeaker Mic detected.";
                this.trackBarR.Enabled = false;
                this.trackBarG.Enabled = false;
                this.trackBarB.Enabled = false;
                return;
            }
            pixelRing = new PixelRing(devices[0]);
            tuning = new Tuning(devices[0]);
            
            timer1.Start();
            UpdateLED();

            pixelRing.SetColorPallette(0xff, 0xff, 0xff, 0, 0xff, 0x30);
            pixelRing.Think();
        }

        private void trackBarR_Scroll(object sender, EventArgs e)
        {
            R = trackBarR.Value;
            UpdateLED();
        }

        private void trackBarG_Scroll(object sender, EventArgs e)
        {
            G = trackBarG.Value;
            UpdateLED();
        }

        private void trackBarB_Scroll(object sender, EventArgs e)
        {
            B = trackBarB.Value;
            UpdateLED();
        }

        private void UpdateLED()
        {
            pixelRing.Mono(R, G, B);
            this.panel1.BackColor = Color.FromArgb(R, G, B);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            int deg = tuning.Direction;
            this.label1.Text = deg.ToString() + (tuning.IsVoice ? " (VOICE)" : "");
            timer1.Start();
        }
    }
}
