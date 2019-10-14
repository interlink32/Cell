using Dna.user;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Connection
{
    public class alluser
    {
        public static event Action reset_e;
        static ObservableCollection<userinfo> listf = null;
        static object lockobject = new object();
        public static ObservableCollection<userinfo> list
        {
            get
            {
                lock (lockobject)
                {
                    if (listf == null)
                    {
                        listf = new ObservableCollection<userinfo>();
                        start();
                    }
                }
                return listf;
            }
        }
        private static void start()
        {
            checkusers();
            client.sendpulse();
        }
        static async void checkusers()
        {
            var all = s.dbuserlogin.Find(i => i.general).Select(i => i.id).ToArray();
            List<long> newlist = new List<long>(all);
            foreach (var i in newlist)
                if (!list.Any(j => j.id == i))
                    add(i);
            foreach (var i in list.ToArray())
            {
                if (!newlist.Any(j => j == i.id))
                    remove(i);
            }
            await Task.Delay(200);
            checkusers();
        }
        private static void remove(userinfo i)
        {
            list.Remove(i);
            client.remove<n_rename>(i.id, rename);

        }
        static void add(long i)
        {
            list.Add(new userinfo()
            {
                id = i,
                fullname = "ID : " + i + " loading ..."
            });
            client.add<n_rename>(i, rename);
            rename(new n_rename()
            {
                z_receiver = i
            });
        }
        async static void rename(n_rename obj)
        {
            var rsv = await client.question(new q_load()
            {
                userid = obj.z_receiver
            }) as q_load.done;
            list.First(i => i.id == obj.z_receiver).fullname = rsv.user.fullname;
            reset_e?.Invoke();
        }
    }
}