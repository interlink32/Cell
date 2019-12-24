using Dna;
using Dna.user;
using stemcell;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace servercell
{
    abstract class clientport
    {
        public long userid { get; private set; }
        internal readonly mainserver mainserver;
        protected readonly TcpClient tcp;
        private readonly byte[] privatekey;
        private readonly bool decrypt;
        private readonly bool loginrequired;
        public clientport(mainserver mainserver, TcpClient tcp, byte[] privatekey, bool decrypt, bool loginrequired = true)
        {
            this.mainserver = mainserver;
            this.tcp = tcp;
            this.privatekey = privatekey;
            this.decrypt = decrypt;
            this.loginrequired = loginrequired;
            login();
        }
        protected byte[] key32 { get; private set; }
        protected byte[] iv16 { get; private set; }
        async void login()
        {
            try
            {
                byte[] data = new byte[128];
                await tcp.GetStream().ReadAsync(data, 0, data.Length);
                data = crypto.Decrypt(data, privatekey);
                var token = BitConverter.ToInt64(data, 0);
                if (decrypt)
                {
                    key32 = crypto.split(data, 8, 32);
                    iv16 = crypto.split(data, 40, 16);
                }
                if (token == 0)
                {
                    if (loginrequired)
                        throw new Exception("fkdbfkbkfmbkgjbjfnjbnvjbmm");
                }
                else
                {
                    Func<question, Task<answer>> func = null;
                    if (mainserver.chromosome == e_chromosome.user)
                        func = mainserver.getanswer;
                    else
                        func = mainserver.question;
                    var dv = await func.Invoke(new q_getid() { token = token }) as q_getid.done;
                    if (dv.error_invalid)
                    {
                        writebyte((byte)byteid.invalid);
                        await Task.Delay(100);
                        tcp.Close();
                        return;
                    }
                    userid = dv.userid;
                }
                writebyte((byte)byteid.login);
                start();
            }
            catch
            {
                tcp.Close();
            }
        }
        protected abstract void start();
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
    }
}