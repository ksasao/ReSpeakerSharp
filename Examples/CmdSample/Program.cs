using ReSpeakerSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CmdSample
{
    class Program
    {
        static void Main(string[] args)
        {
            ReSpeaker rs = new ReSpeaker();
            ReSpeakerMicArray[] mics = rs.Find();

            if (mics.Length == 0)
            {
                Console.WriteLine("Mic not found.");
                return;
            }
            else
            {
                Console.WriteLine($"{mics.Length} mic(s) found.");
            }

            Tuning tuning = new Tuning(mics[0]);
            PixelRing ring = new PixelRing(mics[0]);

            while (!Console.KeyAvailable)
            {
                // get direction
                int direction = tuning.Direction;
                Console.WriteLine($"{direction}");

                // set ring led color
                int d = (int)((direction + 270) * 12 / 360) % 12;
                int pos = 4 * (11 - d);

                byte[] col = new byte[4 * 12];
                col[pos + 0] = 0;   // R
                col[pos + 1] = 255; // G
                col[pos + 2] = 255; // B
                ring.Customize(col);

                Thread.Sleep(100);
            }
        }
    }
}
