using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace core
{
    public abstract class corebase
    {
        public const int buffersize = 1450;
        SemaphoreSlim locker = new SemaphoreSlim(1, 1);
        internal async Task sendall(byte[] data)
        {
            await locker.WaitAsync();
            await sendpart(BitConverter.GetBytes(data.Length));
            int n = 0;
            int m = 0;
            int counter = 0;
            while (n != data.Length)
            {
                m = data.Length - n;
                m = Math.Min(buffersize, m);
                await sendpart(split(data, n, m));
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
            locker.Release();
        }
        public async void notify(int val)
        {
            try
            {
                if (val >= 0)
                    throw new Exception("lbkbgkbkvmfkbkcmfdkbmc");
                await locker.WaitAsync();
                await sendpart(BitConverter.GetBytes(val));
                locker.Release();
            }
            catch
            {
                tcp?.Close();
            }
        }
       internal abstract TcpClient tcp { get; }
        async Task sendpart(byte[] data)
        {
            await tcp.GetStream().WriteAsync(data, 0, data.Length);
        }
        internal async Task<byte[]> receiveall()
        {
            var dv = await receivepart(4);
            var size = BitConverter.ToInt32(dv, 0);
            if (size == 0)
                throw new Exception("lhfkkfknkfknkgjbjkfkbkd");
            if (size < 0)
                return dv;
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
            return Combine(l.ToArray());
        }
        async Task<byte[]> receivepart(int size)
        {
            byte[] data = new byte[size];
            await tcp.GetStream().ReadAsync(data, 0, size);
            return data;
        }
        public static byte[] Combine(params byte[][] arrays)
        {
            byte[] rv = new byte[arrays.Sum(a => a.Length)];
            int offset = 0;
            foreach (byte[] array in arrays)
            {
                Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                offset += array.Length;
            }
            return rv;
        }
        public static byte[] split(byte[] val, int offset, int length)
        {
            byte[] dv = new byte[length];
            Buffer.BlockCopy(val, offset, dv, 0, length);
            return dv;
        }
    }
}