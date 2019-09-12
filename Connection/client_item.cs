using Dna;
using Dna.common;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Connection
{
    abstract class client_item : core
    {
        internal readonly s_chromosome_info info;
        client_center client = null;
        public client_item(client_center client, s_chromosome_info chromosome_Info)
        {
            main_key = chromosome_Info.public_key;
            this.client = client;
            info = chromosome_Info;
            run_cycle();
        }
        protected abstract Task cycle();
        private async void run_cycle()
        {
            await Task.Run(cycle);
            await Task.Delay(10);
            run_cycle();
        }
        public async Task<answer> q(question question)
        {
            await write(question);
            return await read() as answer;
        }

        bool connected = false;
        internal async Task connect()
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
            await client.login_side(this);
            connected = true;
        }
    }
}