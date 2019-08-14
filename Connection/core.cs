using Converter;
using Dna;
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
        public TcpClient tcp = null;
        static converter converter = new converter();
        public async void write(gene gene)
        {
            var data = converter.change(gene);
            var dv = Combine(BitConverter.GetBytes(data.Length), data);
            await tcp.GetStream().WriteAsync(dv, 0, dv.Length);
        }
        public async Task<gene> read()
        {
            var buff = new byte[4];
            await tcp.GetStream().ReadAsync(buff, 0, buff.Length);
            var len = BitConverter.ToInt32(buff, 0);
            buff = new byte[len];
            await tcp.GetStream().ReadAsync(buff, 0, len);
            return converter.change(buff) as gene;
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