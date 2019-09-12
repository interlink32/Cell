using Dna;
using Dna.user;
using Dna.common;
using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Connection
{
    class server_side : core
    {
        internal long z_user = 0;
        Func<question, Task<answer>> get_Answer;
        internal client client = null;
        public server_side(TcpClient tcp, byte[] main_key, Func<question, Task<answer>> get_answer) : base(main_key)
        {
            this.tcp = tcp;
            get_Answer = get_answer;
            ThreadPool.QueueUserWorkItem(reading);
        }
        public event Action<server_side> disconnect_e;
        internal void dispose()
        {
            get_Answer = null;
            tcp?.Dispose();
            tcp = null;
            disconnect_e?.Invoke(this);
        }
        async void reading(object o)
        {
            try
            {
                if (!(await read() is question req))
                    throw new Exception("fjbhdjbjdjkgndjgjdkvg");
                switch (req)
                {
                    case f_set_key dv:
                        {
                            key32 = crypto.Decrypt(dv.key32, main_key);
                            iv16 = crypto.Decrypt(dv.iv16, main_key);
                            write(null);
                        }
                        break;
                    case f_get_chromosome_info dv:
                        {
                            var res = await get_Answer(req);
                            write(res);
                        }
                        break;
                    case f_autologin dv:
                        {
                            var res = await get_Answer(req);
                            if (res is f_autologin.done done)
                                z_user = done.id;
                            write(res);
                        }
                        break;
                    case f_login dv:
                        {
                            var res = await get_Answer(req);
                            if (res is f_login.done done)
                                z_user = done.id;
                            write(res);
                        }
                        break;
                    case f_intrologin dv:
                        {

                            var rsv = await client.question(new f_introcheck()
                            {
                                introcode = dv.introcode
                            });
                            switch (rsv)
                            {
                                case f_introcheck.done dv2:
                                    {
                                        z_user = dv2.userid;
                                        write(new f_intrologin.done());
                                    }
                                    break;
                                case f_introcheck.invalidcode dv2:
                                    {
                                        write(new developer_error() { code = "kgknjfkmfmbmfmbmcmd" });
                                        await Task.Delay(1000 * 5);
                                        repotr.error_e?.Invoke(this, "invalid introcode");
                                    }
                                    break;
                            }
                        }
                        break;
                    default:
                        {
                            if (z_user == 0)
                                write(new login_required());
                            else
                            {
                                req.z_user = z_user;
                                var res = await get_Answer(req);
                                write(res);
                            }
                        }
                        break;
                }
                ThreadPool.QueueUserWorkItem(reading);
            }
            catch
            {
                dispose();
            }
        }
    }
}