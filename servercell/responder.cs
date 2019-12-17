using Dna;
using Dna.common;
using stemcell;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace servercell
{
    class responder : clientport
    {
        tcpbase tcpbase;
        public responder(mainserver mainserver, TcpClient tcp, byte[] privatekey) : base(mainserver, tcp, privatekey, true, false)
        {
            tcpbase = new tcpbase(() => base.tcp);
        }
        protected async override void start()
        {
            question gene;
            byte[] data;
            try
            {
                data = await tcpbase.receiveall();
                data = crypto.Decrypt(data, key32, iv16);
                gene = converter.change(data) as question;
                gene.z_normalize();
                answer answer = await answer_(gene);
                if (answer == null)
                    answer = new voidanswer();
                data = converter.change(answer);
                data = crypto.Encrypt(data, key32, iv16);
                await tcpbase.sendall(data);
                start();
            }
            catch (Exception e)
            {
                _ = e.Message;
                Console.Beep();
                tcp.Close();
            }
        }
        async Task<answer> answer_(question question)
        {
            switch (question.z_permission)
            {
                case e_permission.free:
                    {
                        return await mainserver.getanswer(question);
                    }
                case e_permission.user:
                    {
                        if (userid == 0)
                            return error.create("blfnkgmbknfkbmmbmfmb");
                        else
                        {
                            question.z_user = userid;
                            return await mainserver.getanswer(question);
                        }
                    }
                default:
                    {
                        throw new Exception("dkmrkkfmdmbmc");
                    }
            }
        }
    }
}