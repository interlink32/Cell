using Connection;
using Dna;
using Dna.common;
using Dna.profile;
using Dna.user;
using Dna.usercontact;
using LiteDB;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace classlibrary
{
    public class database
    {
        static ObservableCollection<fulluser> list = new ObservableCollection<fulluser>();
        static Expression<Func<fulluser, bool>> usersearch = default;
        public static void search(Expression<Func<fulluser, bool>> func)
        {
            usersearch = func;
            resetsearch();
        }
        private static void resetsearch()
        {
            if (usersearch == null)
            {
                list.Clear();
                return;
            }
            var dv = dbuser.Find(usersearch);
            fulluser fulluser;
            foreach (var i in list.ToArray())
            {
                fulluser = dv.FirstOrDefault(j => j.id == i.id);
                if (fulluser == null)
                    list.Remove(i);
                else
                    i.copy(fulluser);
            }
        }

        static LiteDatabase db;
        static LiteCollection<fulluser> dbuser => db.GetCollection<fulluser>();
        public static bool start(string uniqueappname)
        {
            Mutex app = new Mutex(true, uniqueappname);
            if (!app.WaitOne(0, false))
                return false;
            db = new LiteDatabase(reference.root(uniqueappname));
            return true;
        }
        public static void adduser(long user)
        {
            client.notifyadd(e_chromosome.contact, user, resetcontact);
        }
        static async void resetcontact(long user)
        {
            var dv = await client.question(new q_loadcontact(e_chromosome.contact), user) as q_loadcontact.done;
            await updateentity(dv.updatedentity);
            await updatecontact(dv.updatedcontact);
            deletecontact(dv.deletedcontact);
            resetsearch();
        }
        private static void deletecontact(long[] ids)
        {
            dbuser.Delete(i => ids.Contains(i.id));
        }
        private async static Task updatecontact(long[] ids)
        {
            if (ids.Length == 0)
                return;
            var rsv = await client.question(new q_loadusercontact() { partnerids = ids }) as q_loadusercontact.done;
            fulluser fulluser;
            foreach (var i in rsv.usercontacts)
            {
                fulluser = dbuser.FindOne(j => j.id == i.partnerid);
                if (fulluser == null)
                    fulluser = new fulluser()
                    {
                        id = i.partnerid,
                        profile = new s_profile(),
                        usercontact = new s_usercontact()
                    };
                fulluser.usercontact.copy(i);
                dbuser.Upsert(fulluser);
            }
        }
        private static async Task updateentity(long[] ids)
        {
            if (ids.Length == 0)
                return;
            var rsv = await client.question(new q_loadallprofile() { ids = ids }) as q_loadallprofile.done;
            fulluser fulluser;
            foreach (var i in rsv.profiles)
            {
                fulluser = dbuser.FindOne(j => j.id == i.id);
                if (fulluser == null)
                    fulluser = new fulluser()
                    {
                        id = i.id,
                        profile = new s_profile(),
                        usercontact = new s_usercontact()
                    };
                fulluser.profile.copy(i);
                dbuser.Upsert(fulluser);
            }
        }
    }
}