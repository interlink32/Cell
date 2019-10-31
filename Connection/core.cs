using Converter;
using Dna;
using Dna.common;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Connection
{
    public class core
    {
        public long userid { get; internal set; }
        public bool connected { get; internal set; }
        static converter converter = new converter();
        internal byte[] mainkey = null;
        internal byte[] key32 = null;
        internal byte[] iv16 = null;
        public core(string chromosome)
        {
            this.chromosome = chromosome;
        }
        public TcpClient tcp = null;
        internal async Task write(gene gene)
        {
            if (gene == null)
                await write(getlen(-1));
            else
            {
                var data = converter.change(gene);
                if (key32 != null)
                    data = crypto.Encrypt(data, key32, iv16);
                data = Combine(getlen(data.Length), data);
                await write(data);
            }
        }
        private static byte[] getlen(int val)
        {
            return BitConverter.GetBytes(val);
        }

        SemaphoreSlim locker = new SemaphoreSlim(1, 1);
        private readonly string chromosome;

        private async Task write(byte[] data)
        {
            await locker.WaitAsync();
            await tcp.GetStream().WriteAsync(data, 0, data.Length);
            locker.Release();
        }
        internal async Task<gene> servicread()
        {
            int len = await getlen();
            if (len == -3)
                return await servicread();
            else
                return await getgen(len);
        }
        internal async Task<gene> clientread()
        {
            int len = await getlen();
            switch (len)
            {
                case -1: return new voidanswer();
                case -2:
                    {
                        client.notify(userid, chromosome);
                        return await clientread();
                    }
                case -3:
                    return await clientread();
            }
            return await getgen(len);
        }
        private async Task<gene> getgen(int len)
        {
            var data = new byte[len];
            await tcp.GetStream().ReadAsync(data, 0, len);
            if (key32 != null)
                data = crypto.Decrypt(data, key32, iv16);
            var dv = converter.change(data) as gene;
            return dv;
        }
        private async Task<int> getlen()
        {
            var data = new byte[4];
            await tcp.GetStream().ReadAsync(data, 0, data.Length);
            var len = BitConverter.ToInt32(data, 0);
            return len;
        }
        internal async Task chacknotify()
        {
            if (tcp.Available >= 4)
            {
                var dv = await getlen();
                if (dv == -2)
                    client.notify(userid, chromosome);
                else
                    throw new Exception("kgkdjbjfjbjcdbjdnvjd");
            }
        }
        public async void sendnotify()
        {
            if (connected)
                try
                {
                    await write(getlen(-2));
                }
                catch { connected = false; }
        }
        public async void sendpalse()
        {
            if (connected)
                try
                {
                    await write(getlen(-3));
                }
                catch { connected = false; }
        }
        public static byte[] Combine(params byte[][] arrays)
        {
            byte[] rv = new byte[arrays.Sum(a => a.Length)];
            int offset = 0;
            foreach (byte[] array in arrays)
            {
                System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                offset += array.Length;
            }
            return rv;
        }
    }
}