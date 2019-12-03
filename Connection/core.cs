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
        public const int pulseconnect = -3;
        const int pulsevoid = -1;
        protected const int pulsenotify = -2;
        public long userid { get; set; }
        public bool connected { get; set; }
        static converter converter = new converter();
        protected byte[] mainkey = null;
        protected byte[] key32 = null;
        protected byte[] iv16 = null;
        public core(string chromosome)
        {
            this.chromosome = chromosome;
        }
        public TcpClient tcp = null;
        protected async Task write(gene gene)
        {
            if (gene == null)
                await write(getlen(pulsevoid));
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

        public readonly string chromosome;
        private async Task write(byte[] data)
        {
            try
            {
                int n = 0;
                int m = 0;
                while (m != data.Length)
                {
                    m = n + 100;
                    m = Math.Min(m, data.Length);
                    await tcp.GetStream().WriteAsync(data, n, m);
                    await Task.Delay(10);
                    n = m;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        internal async Task<gene> clientread()
        {
            int len = await getlen();
            if (len == pulsevoid)
                return new voidanswer();
            else
                return await readgene(len);
        }
        protected async Task<gene> readgene(int len)
        {
            var data = new byte[len];
            await tcp.GetStream().ReadAsync(data, 0, len);
            try
            {
                if (len == 0)
                    throw new Exception("data len == 0");
                if (key32 != null)
                    data = crypto.Decrypt(data, key32, iv16);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            var dv = converter.change(data) as gene;
            return dv;
        }
        public async Task<int> getlen()
        {
            var data = new byte[4];
            await tcp.GetStream().ReadAsync(data, 0, data.Length);
            var len = BitConverter.ToInt32(data, 0);
            return len;
        }
        public async void sendnotify()
        {
            if (connected)
                try
                {
                    await write(getlen(pulsenotify));
                }
                catch { connected = false; }
        }
        public async void sendpalse()
        {
            if (connected)
                try
                {
                    await write(getlen(pulseconnect));
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