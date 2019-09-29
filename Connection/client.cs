using Dna;
using Dna.common;
using Dna.user;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Connection
{
    public class client
    {
        questioner user_item = null;
        public readonly string user_name;
        string password = null;
        public client(string user_name, string password = null)
        {            
            this.user_name = user_name;
            this.password = password;
            string path = reference.root("");
            Directory.CreateDirectory(path);
        }
        TaskCompletionSource<bool> connect_source;
        public async Task<bool> connect()
        {
            if (connect_source != null)
                throw new Exception("kvjdjbndjbnnbnncnb");
            connect_source = new TaskCompletionSource<bool>();
            user_item = new questioner(this, reference.get_user_info());
            qlist.Add(user_item);
            send_pulse();
            return await connect_source.Task;
        }
        public async Task logout()
        {
            await question(new q_logout()
            {
                device = s.load(user_name).device
            });
            s.remove(user_name);
            close();
        }
        public static string[] all_user()
        {
            return s.db_device.FindAll().Select(i => i.user_name).ToArray();
        }
        static Action<(string user, bool login)> user_ef;
        public static event Action<(string user, bool login)> user_e
        {
            add
            {
                user_ef += value;
                if (users == null)
                {
                    users = new List<string>();
                    check_users();
                }
            }
            remove
            {
                user_ef -= value;
            }
        }
        static List<string> users = null;
        static async void check_users()
        {
            List<string> new_list = new List<string>(all_user());
            foreach (var i in new_list)
            {
                if (!users.Contains(i))
                {
                    users.Add(i);
                    user_ef?.Invoke((i, true));
                }
            }
            foreach (var i in users.ToArray())
            {
                if (!new_list.Contains(i))
                {
                    users.Remove(i);
                    user_ef?.Invoke((i, false));
                }
            }
            await Task.Delay(200);
            check_users();
        }

        public event Action disconnect_e = null;
        internal async Task login_item(client_item core)
        {
            switch (core)
            {
                case questioner sw:
                    core = qlist.FirstOrDefault(i => i == sw);
                    break;
                case notifier sw:
                    core = nlist.FirstOrDefault(i => i == sw);
                    break;
            }
            if (core == null)
                return;
            if (core == user_item)
            {
                if (!await autologin())
                {
                    if (!await login(password))
                    {
                        foreach (var i in nlist)
                            i.close();
                        foreach (var i in qlist)
                            i.close();
                        closeF = true;
                        disconnect_e?.Invoke();
                        connect_source.SetResult(false);
                    }
                }
                password = null;
                await create_items();
            }
            else
            {
                var td = s.load(user_name);
                var rsv = await user_item.question(new q_get_introcode()
                {
                    divce = td.device
                }) as q_get_introcode.done;
                var rsv2 = await core.q(new q_intrologin()
                {
                    introcode = rsv.introcode,
                    accept_notifications = core is notifier
                });
                if (!(rsv2 is q_intrologin.done))
                    throw new Exception("lvkdlbmfkvkxmkblcc");
            }
        }
        public event Action<chromosome> reconnect_e;
        internal void reconnect(client_item client_item)
        {
            if (!closeF)
                reconnect_e?.Invoke(client_item.info.chromosome);
        }
        async Task<bool> autologin()
        {
            var dv = s.load(user_name);
            if (dv == null)
                return false;
            else
            {
                var rsv = await user_item.q(new q_autologin()
                {
                    device = dv.device
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
                            s.remove(user_name);
                            return false;
                        }
                }
                throw new Exception("kgjdhhdhvdhjdhbjsjghfgs");
            }
        }

        TaskCompletionSource<s_chromosome_info[]> info_source = new TaskCompletionSource<s_chromosome_info[]>();
        public async Task<s_chromosome_info> infos(string chromosome)
        {
            if (chromosome == Dna.chromosome.user.ToString())
                return reference.get_user_info();
            var dv = await info_source.Task;
            return dv.First(i => i.chromosome.ToString() == chromosome);
        }

        bool ready_items = false;
        public event Action<client> login_e;
        async Task create_items()
        {
            if (ready_items)
                return;
            ready_items = true;
            var rsv = await user_item.q(new q_get_chromosome_info());
            if (!(rsv is q_get_chromosome_info.done done))
                throw new Exception("lbjjbnfjbjcjdjbkckb,fd");
            info_source.SetResult(done.items);
            connect_source.SetResult(true);
        }

        public event Action<notify> notify_e;
        internal void notify(notify notify)
        {
            notify_e?.Invoke(notify);
        }
        SemaphoreSlim qlocking = new SemaphoreSlim(1, 1);
        public async Task<answer> question(question question)
        {
            await qlocking.WaitAsync();
            var dv = qlist.FirstOrDefault(i => i.info.chromosome.ToString() == question.z_chromosome);
            if (dv == null)
            {
                dv = new questioner(this, await infos(question.z_chromosome));
                qlist.Add(dv);
            }
            qlocking.Release();
            return await dv.question(question);
        }
        List<notifier> nlist = new List<notifier>();
        SemaphoreSlim nlocking = new SemaphoreSlim(1, 1);
        public async void active_notify(chromosome chromosome)
        {
            await nlocking.WaitAsync();
            var dv = nlist.FirstOrDefault(i => i.info.chromosome == chromosome);
            if (dv == null)
                nlist.Add(new notifier(this, await infos(chromosome.ToString())));
            nlocking.Release();
        }
        async void send_pulse()
        {
            await nlocking.WaitAsync();
            foreach (var i in nlist)
                i.send();
            nlocking.Release();
            await Task.Delay(1000);
            send_pulse();
        }

        List<questioner> qlist = new List<questioner>();
        public long z_user { get; private set; }
        async Task<bool> login(string password)
        {
            var rsv = await user_item.q(new q_login()
            {
                user_name = user_name,
                password = password
            });
            switch (rsv)
            {
                case q_login.done sw:
                    {
                        z_user = sw.id;
                        s.save(new token_device()
                        {
                            user_name = user_name,
                            device = sw.device
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
        internal bool closeF = false;
        public void close()
        {
            foreach (var i in qlist)
            {
                i.close();
            }
            closeF = true;
        }
    }
}