using Converter;
using Dna;
using Dna.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Connection
{
    class core
    {
        static converter converter = new converter();
        internal byte[] main_key = null;
        internal byte[] key32 = null;
        internal byte[] iv16 = null;
        internal core(byte[] main_key)
        {
            this.main_key = main_key;
        }
        
        public TcpClient tcp = null;
        public event Action<string> error_e;
        public async void write(gene gene)
        {
            try
            {
                if (gene == null)
                    await tcp.GetStream().WriteAsync(new byte[4], 0, 4);
                else
                {
                    var data = converter.change(gene);
                    if (key32 != null)
                        data = await crypto.Encrypt(data, key32, iv16);
                    data = Combine(BitConverter.GetBytes(data.Length), data);
                    await tcp.GetStream().WriteAsync(data, 0, data.Length);
                }
            }
            catch (Exception e)
            {
                error_e?.Invoke("write: " + e.Message);
            }
        }
        public async Task<gene> read()
        {
            try
            {
                var data = new byte[4];
                await tcp.GetStream().ReadAsync(data, 0, data.Length);
                var len = BitConverter.ToInt32(data, 0);
                if (len == 0)
                    return null;
                data = new byte[len];
                await tcp.GetStream().ReadAsync(data, 0, len);
                if (key32 != null)
                    data = await crypto.Decrypt(data, key32, iv16);
                return converter.change(data) as gene;
            }
            catch (Exception e)
            {
                error_e?.Invoke("read: " + e.Message);
                return null;
            }
        }
        private byte[] Combine(params byte[][] arrays)
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