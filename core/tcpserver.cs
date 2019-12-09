using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace core
{
    public abstract class tcpserver : corebase
    {
        private readonly TcpClient tcpf;
        private readonly byte[] privetkey;
        byte[] key32 = default;
        byte[] iv16 = default;
        public tcpserver(TcpClient tcp, byte[] privetkey)
        {
            tcpf = tcp;
            this.privetkey = privetkey;
            tcp.SendBufferSize = tcp.ReceiveBufferSize = buffersize;
            runing();
        }
        internal override TcpClient tcp => tcpf;
        protected abstract void close();
        async void runing()
        {
            try
            {
                var dv = await receiveall();
                dv = await answer(dv);
                await sendall(dv);
                runing();
            }
            catch
            {
                tcp.Close();
                close();
            }
        }
        async Task<byte[]> answer(byte[] data)
        {
            if (key32 == null)
            {
                data = crypto.Decrypt(data, privetkey);
                key32 = split(data, 0, 32);
                iv16 = split(data, 32, 16);
                return crypto.Encrypt(Combine(key32, iv16), key32, iv16);
            }
            else
            {
                data = crypto.Decrypt(data, key32, iv16);
                data = await getanswer(data);
                data = crypto.Encrypt(data, key32, iv16);
                return data;
            }
        }
        protected abstract Task<byte[]> getanswer(byte[] data);
    }
}