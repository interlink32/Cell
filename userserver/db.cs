using stemcell;
using Dna.common;
using Dna.userdata;
using LiteDB;
using localdb;
using Newtonsoft.Json;
using servercell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using core;

namespace userserver
{
    static class db
    {
        static LiteDatabase database = new LiteDatabase(reference.root("user.db"));
        internal static IEnumerable<s2user> find(Expression<Func<s2user, bool>> func)
        {
            return main.Find(func);
        }
        internal static LiteCollection<s2serverinfo> dbserverinfo => database.GetCollection<s2serverinfo>();
        static LiteCollection<s2user> main => database.GetCollection<s2user>("main");
        static LiteCollection<s_contact> getcontact(long user) => database.GetCollection<s_contact>("contact_" + user);
        static LiteCollection<diff> getdiff(long user) => database.GetCollection<diff>("diff_" + user);
        internal static s2user findone(Expression<Func<s2user, bool>> func)
        {
            return main.FindOne(func);
        }
        internal static void upsert(s2user user, bool savediff)
        {
            main.Upsert(user);
            if (savediff)
            {
                var dbcontact = getcontact(user.id);
                foreach (var i in dbcontact.FindAll().ToArray())
                {
                    diff.set(getdiff(i.id), user.id, difftype.update);
                    mainserver.notify(i.id);
                }
            }
        }
        public static void upsert(long user, long partner, e_contactsetting contactsetting)
        {
            if (user == partner)
                upsert(user, e_contactsetting.connect, user, e_contactsetting.connect);
            else
            {
                upsert(user, contactsetting, partner, null);
                upsert(partner, null, user, contactsetting);
            }
        }
        static void upsert(long owner, e_contactsetting? ownersetting, long partner, e_contactsetting? partnersetting)
        {
            var dbcontact = getcontact(owner);
            var dbdiff = getdiff(owner);
            var dv = dbcontact.FindOne(i => i.id == partner);
            if (dv == null)
            {
                dv = new s_contact() { id = partner };
                diff.set(dbdiff, partner, difftype.update);
            }
            if (ownersetting != null)
                dv.ownersetting = ownersetting.Value;
            if (partnersetting != null)
                dv.partnersetting = partnersetting.Value;
            
            if (dv.ownersetting == e_contactsetting.disconnect && dv.partnersetting == e_contactsetting.disconnect)
            {
                dbcontact.Delete(i => i.id == partner);
                diff.set(dbdiff, partner, difftype.delete);
            }
            else
            {
                dbcontact.Upsert(dv);
                diff.set(dbdiff, partner, difftype.updatecontact);
            }
            mainserver.notify(owner);
        }
        public static q_loaddiff.done loaddiff(long user, long index)
        {
            var dbdiff = getdiff(user);
            var dv = dbdiff.Find(i => i.index > index).ToArray();
            List<s_fulluser> list = new List<s_fulluser>();
            foreach (var i in dv)
            {
                if (i.state == difftype.delete)
                    continue;
                var fulluser = list.FirstOrDefault(j => j.id == i.entity);
                if (fulluser == null)
                {
                    fulluser = new s_fulluser()
                    {
                        id = i.entity
                    };
                    list.Add(fulluser);
                }
                switch (i.state)
                {
                    case difftype.update:
                        {
                            fulluser.user = main.FindOne(j => j.id == i.entity);
                        }
                        break;
                    case difftype.updatecontact:
                        {
                            var dbcontact = getcontact(user);
                            fulluser.contact = dbcontact.FindOne(j => j.id == i.entity);
                        }
                        break;
                }
            }
            var rt = new q_loaddiff.done()
            {
                currentindex = dv.LastOrDefault()?.index ?? index,
                deleted = dv.Where(i => i.state == difftype.delete).Select(i => i.entity).ToArray(),
                entites = JsonConvert.SerializeObject(list.ToArray())
            };
            return rt;
        }
        internal static bool exists(Expression<Func<s2user, bool>> func)
        {
            return main.Exists(func);
        }
    }
}