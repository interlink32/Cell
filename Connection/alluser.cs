using Dna;
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
        static void add(long user)
        {
            list.Add(new userinfo()
            {
                id = user,
                fullname = "ID : " + user + " loading ..."
            });
            client.notifyadd(e_chromosome.user, user, loaduser);
        }
        public static event Action<long> remove_e;
        static void remove(userinfo user)
        {
            list.Remove(user);
            reset_e?.Invoke();
            client.close(user.id);
            remove_e?.Invoke(user.id);
        }
        static async void loaduser(long user)
        {
            var olduser = list.FirstOrDefault(i => i.id == user);
            if (olduser == null)
                return;
            var newuser = await client.getuser(user);
            olduser.fullname = newuser.fullname;
            reset_e?.Invoke();
        }
    }
}