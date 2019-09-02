using Connection;
using Dna;
using Dna.test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace client_test
{
    class client_tester
    {
        client client = null;
        public async Task<bool> start(long id)
        {
            client = new client();
            var dv = await client.login(id.ToString(), id + "pass");
            if (!dv)
                report.login_error++;
            client.notify_e += Client_notify_e;
            await client.connect(chromosome.test);
            return dv;
        }
        class item
        {
            public long user = 0;
            public long value = 0;
        }
        List<item> list = new List<item>();
        SemaphoreSlim locking = new SemaphoreSlim(1, 1);
        public async void connect(long user)
        {
            await locking.WaitAsync();
            var dv = add(user, 0);
            locking.Release();
            send(dv);
        }

        private item add(long user, long value)
        {
            if (list.Any(i => i.user == user))
                throw new Exception("lkdjvjskmfnbnjcnsdnv");
            item item = new item()
            {
                user = user,
                value = value
            };
            list.Add(item);
            return item;
        }

        async void Client_notify_e(notify obj)
        {
            switch (obj)
            {
                case n_message rsv:
                    {
                        var message = rsv;
                        await locking.WaitAsync();
                        var dv = list.FirstOrDefault(i => i.user == message.sender);
                        if (dv == null)
                        {
                            if (message.value != 0)
                                throw new Exception("lkdjbhdjbjdnbfjmgjjdj");
                            dv = add(message.sender, -1);
                        }
                        locking.Release();
                        if (dv.value + 1 != message.value)
                            throw new Exception("lbllbfklclflblf");
                        dv.value = message.value + 1;
                        send(dv);
                    }
                    break;
            }
        }
        private async void send(item item)
        {
            var dv = await client.question(new q_message()
            {
                user = item.user,
                value = item.value
            }) as q_message.done;
            if (!dv.receive)
                throw new Exception("kbkdlbmfknkvmdkbdkbkc");
            report.counter++;
        }
    }
}