using Dna;
using Dna.common;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connection
{
    public class q_center
    {
        q_item q_item = null;
        string root = null;
        public q_center(string root)
        {
            this.root = root;
            q_item = new q_item(this, reference.get_user_info());
            list.Add(q_item);
            q_item.start = true;
        }
        public event Func<Task<(string userid,string password)>> user_password_e = null;
        internal async Task login_side(q_item core)
        {
            var dv = list.FirstOrDefault(i => i == core);
            if (dv == null)
                return;
            if (dv == q_item)
            {
                if (!await autologin())
                    while (true)
                    {
                        var info = await user_password_e.Invoke();
                        if (await login(info.userid, info.password))
                            break;
                    }
                await create_items();
            }
            else
            {
                var td = await s.load<token_device>(root + "td");
                var rsv = await q_item.question(new q_get_introcode()
                {
                    token = td.token,
                    divce = td.device
                }) as q_get_introcode.done;
                var rsv2 = await core.q(new q_intrologin()
                {
                    introcode = rsv.introcode
                });
                if (!(rsv2 is q_intrologin.done))
                    throw new Exception("lvkdlbmfkvkxmkblcc");
            }
        }

        public event Action<q_center> login_e;
        async Task<bool> autologin()
        {
            var dv = await s.load<token_device>(root + "td");
            if (dv == null)
                return false;
            else
            {
                var rsv = await q_item.q(new q_autologin()
                {
                    divice = dv.device,
                    token = dv.token
                });
                switch (rsv)
                {
                    case q_autologin.done done:
                        {
                            z_user = done.id;
                            return true;
                        }
                    case q_autologin.invalid_token invalid:
                        {
                            s.save(root + "td", null);
                            return false;
                        }
                }
                throw new Exception("kgjdhhdhvdhjdhbjsjghfgs");
            }
        }

        bool ready_items = false;
        async Task create_items()
        {
            if (ready_items)
                return;
            ready_items = true;
            var rsv = await q_item.q(new q_get_chromosome_info());
            if (!(rsv is q_get_chromosome_info.done done))
                throw new Exception("lbjjbnfjbjcjdjbkckb,fd");
            foreach (var i in done.items)
                list.Add(new q_item(this, i));
            login_e?.Invoke(this);
        }
        public event Action<notify> notify_e;
        internal void notify(notify notify)
        {
            notify_e?.Invoke(notify);
        }
        public async Task<answer> question(question question)
        {
            var dv = list.First(i => i.info.chromosome.ToString() == question.z_chromosome);
            return await dv.question(question);
        }
        class token_device
        {
            public double device = 0;
            public double token = 0;
        }

        List<q_item> list = new List<q_item>();
        public long z_user { get; private set; }
        public async Task<bool> login(string userid, string password)
        {
            Random random = new Random();
            var divce = random.NextDouble();
            var rsv = await q_item.q(new q_login()
            {
                userid = userid,
                divce = divce,
                password = password
            });
            switch (rsv)
            {
                case q_login.done sw:
                    {
                        z_user = sw.id;
                        s.save(root + "td", new token_device()
                        {
                            device = divce,
                            token = sw.token
                        });
                        return true;
                    }
                case q_login.invalid sw:
                    {
                        return false;
                    }
            }
            throw new Exception("lgjcjjbjcdjbkdfjkvkdjgj");
        }
    }
}