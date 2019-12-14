using stemcell;
using Dna;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace stemcell
{
    public class clientitem
    {
        internal bool inp = default;
        public readonly string chromosome;
        public readonly long userid;
        public clientitem(string chromosome, long userid)
        {
            this.chromosome = chromosome;
            this.userid = userid;
        }
        tcpclient tcpclient;
        SemaphoreSlim locker = new SemaphoreSlim(1, 1);
        async Task login()
        {
            if (tcpclient != null)
                return;
            var info = await basic.getchromosome(chromosome);
            var ipend = info.Getgetendpoint();
            tcpclient = new tcpclient(ipend.Address, ipend.Port, info.publickey);
            if (userid != 0)
            {
                long token = await s.gettoken(userid);
                var rsv = await question_(new q_login()
                {
                    token = token
                });
                if (!(rsv is q_login.done))
                {
                    Console.Beep(1000, 500);
                    throw new Exception("bkdkbmfbcmfmbmmbm");
                }
            }
        }
        public async Task<answer> question(question question)
        {
            try
            {
                await locker.WaitAsync();
                await login();
                var answer = await question_(question);
                locker.Release();
                return answer;
            }
            catch
            {
                Console.Beep();
                tcpclient?.close();
                tcpclient = null;
                locker.Release();
                return await this.question(question);
            }
        }
        public async Task<answer> question_(question question)
        {
            var data = converter.change(question);
            data = await tcpclient.question(data);
            var answer = converter.change(data) as answer;
            return answer;
        }
        internal void close()
        {
            tcpclient?.close();
        }
    }
}