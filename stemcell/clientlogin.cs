using Dna;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace stemcell
{
    public abstract class clientlogin
    {
        internal readonly string chromosome;
        internal readonly long userid;
        private readonly bool encrypt;
        internal bool connect;
        public clientlogin(string chromosome, long userid, bool encrypt)
        {
            this.chromosome = chromosome;
            this.userid = userid;
            this.encrypt = encrypt;
        }
        internal async Task<bool> login()
        {
            if (connect)
                return false;
            var info = await basic.getchromosome(chromosome);
            var data = crypto.Encrypt(await getlogindata(), info.publickey);
            tcp = new TcpClient();
            var endpoint = info.Getgetendpoint();
            await tcp.ConnectAsync(endpoint.Address, endpoint.Port);
            writebyte(clienttype);
            await tcp.GetStream().WriteAsync(data, 0, data.Length);
            var dv = await receivebyte();
            if (dv != netid.login)
            {
                throw new Exception("kgjfjbjfjbjfnbjvnfnbjfnbnfjbjfn");
            }
            connect = true;
            return true;
        }
        public TcpClient tcp { get; private set; }
        public abstract byte clienttype { get; }
        protected byte[] Key32 { get; private set; }
        protected byte[] Iv16 { get; private set; }
        protected async Task<byte> receivebyte()
        {
            byte[] data = new byte[1];
            await tcp.GetStream().ReadAsync(data, 0, data.Length);
            return data[0];
        }
        protected void writebyte(byte data)
        {
            tcp.GetStream().WriteByte(data);
        }
        async Task<byte[]> getlogindata()
        {
            var token = await s.gettoken(userid);
            var data = BitConverter.GetBytes(token);
            if (encrypt)
            {
                var dv = crypto.create_symmetrical_keys();
                Key32 = dv.key32;
                Iv16 = dv.iv16;
                data = crypto.combine(data, Key32, Iv16);
            }
            return data;
        }
    }
}