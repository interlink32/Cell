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
    public class notifier : clientlogin
    {
        Action sync;
        timeout outer;
        public const int timeuot = 2000;
        public notifier(string chromosome, long userid) : base(netid.notifier, chromosome, userid, false)
        {
            outer = new timeout(timeuot * 3, expired);
            runing();
        }
        private void expired()
        {
            connect = false;
            tcp?.Close();
        }
        async void runing()
        {
            try
            {
                if (await login())
                    newnotify(chromosome, userid);
                outer.start();
                var dv = await receive();
                outer.end();
                if (dv == (byte)netid.newnotify)
                    newnotify(chromosome, userid);
                runing();
            }
            catch (Exception e)
            {
                connect = false;
                tcp?.Close();
                _ = e.Message;
                Console.Beep();
                await Task.Delay(1000);
                runing();
            }
        }
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

        static List<notifier> list = new List<notifier>();
        static SemaphoreSlim locker = new SemaphoreSlim(1, 1);
        public static async void add(e_chromosome chromosome, long userid, Action sync)
        {
            notifier dv = await get(chromosome.ToString(), userid);
            dv.sync += sync;
        }
        private static async Task<notifier> get(string chromosome, long userid)
        {
            await locker.WaitAsync();
            var dv = list.FirstOrDefault(i => i.chromosome == chromosome && i.userid == userid);
            if (dv == null)
            {
                dv = new notifier(chromosome, userid);
                list.Add(dv);
            }
            locker.Release();
            return dv;
        }
    }
}