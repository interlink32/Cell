using Dna;
using Dna.user;
using Dna.common;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Connection
{
    class signalizer : clientitem
    {
        public signalizer(long userid, string chromosome) : base(userid, chromosome.ToString()) { }
        public event Action action_e;
        protected async override Task cycle()
        {
            await receivenotify();
            action_e?.Invoke();
        }
        internal async void sendpulse()
        {
            try
            {
                if (connected)
                {
                    byte[] buff = new byte[] { 56 };
                    await tcp.GetStream().WriteAsync(buff, 0, buff.Length);
                }
            }
            catch
            {
                Console.Beep(4000, 500);
                close();
            }
        }
    }
}