using Dna;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace stemcell
{
    class questioner : clientlogin
    {
        tcpbase tcpbase;
        internal bool inp;
        public questioner(string chromosome, long userid) : base(byteid.questioner, chromosome, userid, true)
        {
            tcpbase = new tcpbase(gettcp);
        }
        private TcpClient gettcp()
        {
            return tcp;
        }
        SemaphoreSlim locker = new SemaphoreSlim(1, 1);
        public async Task<answer> question(question question)
        {
            try
            {
                await locker.WaitAsync();
                await login();
                var data = converter.change(question);
                data = crypto.Encrypt(data, Key32, Iv16);
                await tcpbase.sendall(data);
                data = await tcpbase.receiveall();
                data = crypto.Decrypt(data, Key32, Iv16);
                var rt = converter.change(data);
                locker.Release();
                return rt as answer;
            }
            catch (Exception e)
            {
                _ = e.Message;
                Console.Beep();
                tcp?.Close();
                connect = false;
                locker.Release();
                return await this.question(question);
            }
        }
        internal void close()
        {
            tcp?.Close();
        }
    }
}