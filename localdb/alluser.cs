using Connection;
using Dna;
using Dna.user;
using Dna.userdata;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace localdb
{
    public class alluser
    {
        static ocollection<s_user> listf = null;
        static object lockobject = new object();
        public static ocollection<s_user> list
        {
            get
            {
                lock (lockobject)
                {
                    if (listf == null)
                    {
                        listf = new ocollection<s_user>();
                        checkusers();
                    }
                }
                return listf;
            }
        }
        static async void checkusers()
        {
            var all = client.getalluser();
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
        public static event Action<bool, long> addremove_e;
        static void add(long user)
        {
            list.Add(new s_user()
            {
                id = user,
                fullname = "Loading user : " + user
            });
            var db = alldb.dbuser(user);
            db.notify(user, reset);
            db.search(i => i.id == user);
            addremove_e?.Invoke(true, user);
        }
        private static void reset(s_fulluser obj)
        {
            var dv2 = list.First(i => i.id == obj.id);
            dv2.fullname = obj.user.fullname;
            list.reset();
        }
        static void remove(s_user user)
        {
            list.Remove(user);
            client.close(user.id);
            addremove_e?.Invoke(false, user.id);
        }
    }
}