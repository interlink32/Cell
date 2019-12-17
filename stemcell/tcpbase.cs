using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace stemcell
{
    public class tcpbase
    {
        const int buffersize = 1450;
        readonly Func<TcpClient> gettcp = default;
        public tcpbase(Func<TcpClient> gettcp)
        {
            this.gettcp = gettcp;
        }
        public async Task sendall(byte[] data)
        {
            await sendpart(BitConverter.GetBytes(data.Length));
            int n = 0;
            int m = 0;
            int counter = 0;
            while (n != data.Length)
            {
                m = data.Length - n;
                m = Math.Min(buffersize, m);
                await sendpart(crypto.split(data, n, m));
                n += m;
                if (counter != 0)
                {
                    if (counter % 5 == 0)
                        await Task.Delay(500);
                    else
                        await Task.Delay(10);
                }
                counter++;
            }
        }
        async Task sendpart(byte[] data)
        {
            await gettcp().GetStream().WriteAsync(data, 0, data.Length);
        }
        public async Task<byte[]> receiveall()
        {
            var dv = await receivepart(4);
            var size = BitConverter.ToInt32(dv, 0);
            if (size == 0)
                throw new Exception("lhfkkfknkfknkgjbjkfkbkd");
            int n = 0;
            int m = 0;
            List<byte[]> l = new List<byte[]>();
            while (n != size)
            {
                m = size - n;
                m = Math.Min(m, buffersize);
                var rsv = await receivepart(m);
                if (rsv == null)
                    throw new Exception("kdkbfkbkfmnfmbkfkbkmkbkdmr");
                l.Add(rsv);
                n += m;
            }
            return crypto.combine(l.ToArray());
        }
        async Task<byte[]> receivepart(int size)
        {
            byte[] data = new byte[size];
            await gettcp().GetStream().ReadAsync(data, 0, size);
            return data;
        }
    }
}