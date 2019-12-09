using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
        SemaphoreSlim locker = new SemaphoreSlim(1, 1);
        public bool notifier;

        async void runing()
        {
            try
            {
                await locker.WaitAsync();
                var dv = await receiveall();
                if (!notifier)
                {
                    dv = await process(dv);
                    await sendall(dv);
                }
                locker.Release();
                runing();
            }
            catch (Exception e)
            {
                _ = e.Message;
                tcp.Close();
                close();
                locker.Release();
            }
        }
        public async override void sendpulse()
        {
            try
            {
                await locker.WaitAsync();
                base.sendpulse();
                locker.Release();
            }
            catch (Exception e)
            {
                _ = e.Message;
                tcp.Close();
                close();
                locker.Release();
            }
        }
        async Task<byte[]> process(byte[] data)
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