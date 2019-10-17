using Dna;
using Dna.usercontact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contact_server
{
    class log : myservice<q_getdifference>
    {
        public async override Task<answer> getanswer(q_getdifference question)
        {
            await Task.CompletedTask;
            var dblog = s.dblog(question.z_user);
            var logs = dblog.Find(i => i.index > question.fromindex).ToArray();
            var ids = logs.Select(i => i.contact).ToArray();
            var rt = new q_getdifference.doen();
            if (ids.Length > 0)
            {
                var dbcontact = s.dbcontact(question.z_user);
                rt.updatedcontact = dbcontact.Find(i => ids.Contains(i.id)).Select(i => i.clone()).ToArray();
                rt.currentindex = logs.Last().index;
            }
            return rt;
        }
        public static void create(long contact, params long[] users)
        {
            foreach (var i in users)
            {
                var db = s.dblog(i);
                db.Delete(j => j.contact == contact);
                db.Insert(new r_log()
                {
                    contact = contact,
                    type = e_log.update
                });

                notify(i, new n_logcontact());
            }
        }
    }
}