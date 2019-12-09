using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Sockets;

namespace core
{
    public class tcpclient : corebase
    {
        private readonly IPAddress iP;
        private readonly int port;
        private readonly byte[] publickey;
        private byte[] key32 = null;
        private byte[] iv16 = null;
        public tcpclient(IPAddress iP, int port, byte[] publickey)
        {
            this.iP = iP;
            this.port = port;
            this.publickey = publickey;
        }
        public async Task<byte[]> question(byte[] data)
        {
                await ini();
                var dvdata = crypto.Encrypt(data, key32, iv16);
                await sendall(dvdata);
                dvdata = await receiveall();
                dvdata = crypto.Decrypt(dvdata, key32, iv16);
                return dvdata;
        }
        public async Task<int> getnotify()
        {
            var dv = await receiveall();
            return BitConverter.ToInt32(dv, 0);
        }
        public void close()
        {
            tcpf?.Close();
        }
        TcpClient tcpf = null;
        internal override TcpClient tcp
        {
            get
            {
                return tcpf;
            }
        }
        async Task ini()
        {
            if (tcpf != null && tcpf.Connected)
                return;
            tcpf = new TcpClient();
            await tcpf.ConnectAsync(iP, port);
            var dv = crypto.create_symmetrical_keys();
            byte[] keys = Combine(dv.key32, dv.iv16);
            byte[] data = crypto.Encrypt(keys, publickey);
            await sendall(data);
            data = await receiveall();
            data = crypto.Decrypt(data, dv.key32, dv.iv16);
            if (!data.SequenceEqual(keys))
                throw new Exception("bkfknfkbfkmnmfknmkfkbmm");
            key32 = dv.key32;
            iv16 = dv.iv16;
        }
    }
}