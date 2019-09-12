using Dna;
using Dna.user;
using Dna.common;
using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Connection
{
    class a_item : core
    {
        internal long z_user = 0;
        Func<question, Task<answer>> get_Answer;
        internal a_center a = null;
        internal q_center q = null;
        public a_item(TcpClient tcp, Func<question, Task<answer>> get_answer)
        {
            this.tcp = tcp;
            get_Answer = get_answer;
            ThreadPool.QueueUserWorkItem(reading);
        }
        async void reading(object o)
        {
            try
            {
                if (!(await read() is question req))
                    throw new Exception("fjbhdjbjdjkgndjgjdkvg");
                switch (req)
                {
                    case q_set_key dv:
                        {
                            key32 = crypto.Decrypt(dv.key32, main_key);
                            iv16 = crypto.Decrypt(dv.iv16, main_key);
                            await write(null);
                        }
                        break;
                    case q_get_chromosome_info dv:
                        {
                            var res = await get_Answer(req);
                            await write(res);
                        }
                        break;
                    case q_autologin dv:
                        {
                            var res = await get_Answer(req);
                            if (res is q_autologin.done done)
                                z_user = done.id;
                            await write(res);
                        }
                        break;
                    case q_login dv:
                        {
                            var res = await get_Answer(req);
                            if (res is q_login.done done)
                                z_user = done.id;
                            await write(res);
                        }
                        break;
                    case q_intrologin dv:
                        {

                            var rsv = await q.question(new q_introcheck()
                            {
                                introcode = dv.introcode
                            });
                            switch (rsv)
                            {
                                case q_introcheck.done dv2:
                                    {
                                        z_user = dv2.userid;
                                        await write(new q_intrologin.done());
                                    }
                                    break;
                                case q_introcheck.invalidcode dv2:
                                    {
                                        await write(new developer_error() { code = "kgknjfkmfmbmfmbmcmd" });
                                        await Task.Delay(1000 * 5);
                                        report.error_e?.Invoke(this, "invalid introcode");
                                    }
                                    break;
                            }
                        }
                        break;
                    default:
                        {
                            if (z_user == 0)
                                await write(new login_required());
                            else
                            {
                                req.z_user = z_user;
                                var res = await get_Answer(req);
                                await write(res);
                            }
                        }
                        break;
                }
                ThreadPool.QueueUserWorkItem(reading);
            }
            catch(Exception e)
            {
                var dv = e.Message;
                dv = null;
                get_Answer = null;
                tcp.Close();
                tcp = null;
                a.remove(this);
            }
        }
    }
}