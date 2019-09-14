using Dna;
using Dna.common;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Connection
{
    abstract class client_item : core
    {
        internal readonly s_chromosome_info info;
        public client client = null;
        public client_item(client client, s_chromosome_info chromosome_Info)
        { 
            main_key = chromosome_Info.public_key;
            this.client = client;
            info = chromosome_Info;
            runing();
        }
        protected abstract Task cycle();
        private async void runing()
        {
            await Task.Run(try_catch);
            await Task.Delay(10);
            runing();
        }
        private async Task try_catch()
        {
            
            try
            {
                await connect();
                await cycle();
            }
            catch
            {
                connected = false;
                key32 = iv16 = null;
            }
        }
        public async Task<answer> q(question question)
        {
            await write(question);
            return await read() as answer;
        }

        bool connected = false;
        async Task connect()
        {
            if (connected)
                return;
            tcp = new TcpClient();
            var endpoint = reference.get_endpoint(info.endpoint);
            await tcp.ConnectAsync(endpoint.Address, endpoint.Port);
            var keys = crypto.create_symmetrical_keys();
            await write(new q_set_key()
            {
                key32 = crypto.Encrypt(keys.key32, main_key),
                iv16 = crypto.Encrypt(keys.iv16, main_key)
            });
            key32 = keys.key32;
            iv16 = keys.iv16;
            if (!(await read() is void_answer))
                throw new Exception("lkdkbjkbkfmbkcskbmdkb");
            await client.login_item(this);
            connected = true;
        }
    }
}