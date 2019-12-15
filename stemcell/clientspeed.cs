using Dna;
using System;
using System.Collections.Generic;
using System.Text;

namespace stemcell
{
    public class clientspeed : clientlogin
    {
        timeout outer;
        public clientspeed(string chromosome, long userid) : base(chromosome, userid, false)
        {
            outer = new timeout(1000, expired);
            runing();
        }
        private void expired()
        {
            connect = false;
            tcp?.Close();
        }
        async void runing()
        {
            try
            {
                await login();
                writebyte(netid.connectpulse);
                outer.start();
                var dv = await receivebyte();
                outer.end();
                if (dv != netid.connectpulse)
                    throw new Exception("khjudughhfhvjhcbjfnbjcbfjhbhfbvhbdndf");
                runing();
            }
            catch (Exception e)
            {
                _ = e.Message;
                Console.Beep();
                runing();
            }
        }
        public override byte clienttype => netid.clientspeed;
    }
}