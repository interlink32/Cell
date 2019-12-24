using Dna;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace stemcell
{
    public class clientspeed : clientlogin
    {
        timeout outer;
        public clientspeed(e_chromosome chromosome) : base(byteid.clientspeed, chromosome.ToString(), 0, false)
        {
            outer = new timeout(1000, expired);
            runing();
        }
        private void expired()
        {
            connect = false;
            tcp?.Close();
        }
        public static int n { get; private set; }
        async void runing()
        {
            try
            {
                await login();
                writebyte((byte)byteid.connectpulse);
                outer.start();
                var dv = await receivebyte();
                outer.end();
                if (dv != (byte)byteid.connectpulse)
                    throw new Exception("khjudughhfhvjhcbjfnbjcbfjhbhfbvhbdndf");
                n++;
                await Task.Delay(10);
                runing();
            }
            catch (Exception e)
            {
                _ = e.Message;
                Console.Beep();
                connect = false;
                tcp?.Close();
                runing();
            }
        }
    }
}