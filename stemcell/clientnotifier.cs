using Dna;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace stemcell
{
    public class clientnotifier
    {
        private readonly string chromosome;
        internal readonly long userid;
        TcpClient tcp = default;
        Action sync;
        public clientnotifier(string chromosome, long userid)
        {
            this.chromosome = chromosome;
            this.userid = userid;
            runing();
        }
        public const int timeuot = 1000 * 5; 
        async Task login()
        {
            if (connect)
                return;
            var token = await s.gettoken(userid);
            var data = BitConverter.GetBytes(token);
            var info = await basic.getchromosome(chromosome);
            data = crypto.Encrypt(data, info.publickey);
            tcp = new TcpClient();
            var endpoint = info.Getgetendpoint();
            await tcp.ConnectAsync(endpoint.Address, endpoint.Port);
            tcp.GetStream().WriteByte(netid.notifier);
            await tcp.GetStream().WriteAsync(data, 0, data.Length);
            var dv = await receive();
            if (dv != netid.login)
            {
                throw new Exception("kgjfjbjfjbjfnbjvnfnbjfnbnfjbjfn");
            }
            connect = true;
            newnotify(chromosome, userid);
        }

        long n = 1;
        async void runing()
        {
            try
            {
                await login();
                live();
                var dv = await receive();
                n++;
                if (dv == netid.newnotify)
                    newnotify(chromosome, userid);
                runing();
            }
            catch(Exception e)
            {
                _ = e.Message;
                Console.Beep();
                await Task.Delay(500);
                runing();
            }
        }
        async void live()
        {
            var dv = n;
            await Task.Delay(timeuot);
            if (dv == n)
            {
                connect = false;
                tcp?.Close();
            }
        }
        bool connect = default;
        async Task<byte> receive()
        {
            byte[] rt = new byte[1];
            await tcp.GetStream().ReadAsync(rt, 0, rt.Length);
            return rt[0];
        }
        //----------------------------------
        static async void newnotify(string chromosome, long userid)
        {
            var dv = await get(chromosome, userid);
            dv.sync?.Invoke();
        }
        public static async void remove(long userid)
        {
            await locker.WaitAsync();
            var dv = list.Where(i => i.userid == userid).ToArray();
            foreach (var i in dv)
            {
                i.sync = null;
                list.Remove(i);
            }
            locker.Release();
        }

        static List<clientnotifier> list = new List<clientnotifier>();
        static SemaphoreSlim locker = new SemaphoreSlim(1, 1);
        public static async void add(e_chromosome chromosome, long userid, Action sync)
        {
            clientnotifier dv = await get(chromosome.ToString(), userid);
            dv.sync += sync;
        }
        private static async Task<clientnotifier> get(string chromosome, long userid)
        {
            await locker.WaitAsync();
            var dv = list.FirstOrDefault(i => i.chromosome == chromosome && i.userid == userid);
            if (dv == null)
            {
                dv = new clientnotifier(chromosome, userid);
                list.Add(dv);
            }
            locker.Release();
            return dv;
        }
    }
}