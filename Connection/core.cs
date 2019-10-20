using Converter;
using Dna;
using Dna.common;
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
        static converter converter = new converter();
        internal byte[] mainkey = null;
        internal byte[] key32 = null;
        internal byte[] iv16 = null;

        public TcpClient tcp = null;
        internal async Task write(gene gene)
        {
            if (gene == null)
                await tcp.GetStream().WriteAsync(BitConverter.GetBytes(-1), 0, 4);
            else
            {
                var data = converter.change(gene);
                if (key32 != null)
                    data = crypto.Encrypt(data, key32, iv16);
                data = Combine(BitConverter.GetBytes(data.Length), data);
                await tcp.GetStream().WriteAsync(data, 0, data.Length);
            }
        }
        public event Action pulse_e;
        internal async Task<gene> read()
        {
            var data = new byte[4];
            await tcp.GetStream().ReadAsync(data, 0, data.Length);
            var len = BitConverter.ToInt32(data, 0);
            switch (len)
            {
                case -1: return new voidanswer();
                case -2:
                    {
                        pulse_e.Invoke();
                        return await read();
                    }
            }
            data = new byte[len];
            await tcp.GetStream().ReadAsync(data, 0, len);
            if (key32 != null)
                data = crypto.Decrypt(data, key32, iv16);
            var dv = converter.change(data) as gene;
            return dv;
        }
        public async void sendnotify()
        {
            await tcp.GetStream().WriteAsync(BitConverter.GetBytes(-2), 0, 4);
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