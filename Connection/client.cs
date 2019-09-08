using Dna;
using Dna.common;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connection
{
    public class client
    {
        client_side user_side = null;
        public client()
        {
            user_side = new client_side(this, reference.get_user_info());
            list.Add(user_side);
        }
        internal async Task login(core core)
        {
            var dv = list.FirstOrDefault(i => i == core);
            if (dv == null)
                return;
            if (dv == user_side)
            {
                await autologin();
                create_items();
            }
            else
            {
                var td = await s.load<token_device>("td");
                var rsv = await user_side.question(new f_get_introcode()
                {
                    token = td.token,
                    divce = td.device
                }) as f_get_introcode.done;
                core.write(new f_intrologin()
                {
                    introcode = rsv.introcode
                });
                if (!(await core.read() is f_intrologin.done))
                    throw new Exception("lvkdlbmfkvkxmkblcc");
            }
        }
        public event Func<Task<(string userid, string password)>> userid_password_e;
        async Task autologin()
        {
            //s.save("td", null);
            var dv = await s.load<token_device>("td");
            if (dv == null)
                await login();
            else
            {
                user_side.write(new f_autologin()
                {
                    divice = dv.device,
                    token = dv.token
                });
                var rsv = await user_side.read();
                switch (rsv)
                {
                    case f_autologin.done done:
                        {
                            z_user = done.id;
                        }
                        break;
                    case f_autologin.invalid_token invalid:
                        {
                            s.save("td", null);
                            await login();
                        }
                        break;
                }
            }
        }

        private async Task login()
        {
            while (true)
            {
                var info = await userid_password_e?.Invoke();
                if (await login(info.userid, info.password))
                    return;
            }
        }
        bool ready_items = false;
        async void create_items()
        {
            if (ready_items)
                return;
            ready_items = true;
            var rsv = await user_side.question(new f_get_chromosome_info());
            if (!(rsv is f_get_chromosome_info.done done))
                throw new Exception("lbjjbnfjbjcjdjbkckb,fd");
            foreach (var i in done.chromosome_infos)
                list.Add(new client_side(this, i));
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

        List<client_side> list = new List<client_side>();
        public long z_user { get; private set; }
        async Task<bool> login(string userid, string password)
        {
            Random random = new Random();
            var divce = random.NextDouble();
            user_side.write(new f_login()
            {
                userid = userid,
                divce = divce,
                password = password
            });
            switch (await user_side.read())
            {
                case f_login.done rsv:
                    {
                        z_user = rsv.id;
                        s.save("td", new token_device()
                        {
                            device = divce,
                            token = rsv.token
                        });
                        return true;
                    }
                case f_login.invalid rsv:
                    {
                        return false;
                    }
            }
            throw new Exception("lgjcjjbjcdjbkdfjkvkdjgj");
        }
    }
}