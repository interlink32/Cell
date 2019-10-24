using Dna;
using Dna.common;
using Dna.usercontact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contact_server
{
    class loadcontact : myservice<q_loadcontact>
    {
        public async override Task<answer> getanswer(q_loadcontact question)
        {
            await Task.CompletedTask;
            var dblog = s.dbdiff(question.z_user);
            var diff = dblog.Find(i => i.index > question.index).ToArray();
            var rt = new q_loadcontact.done()
            {
                currentindex = diff.Last()?.index ?? question.index,
                updatedentity = get(diff, difftype.entityupdate),
                updatedcontact = get(diff, difftype.contactupdate),
                deletedcontact = get(diff, difftype.contactdeleted)
            };
            return rt;
        }
        private static long[] get(r_diff[] diff, difftype difftype)
        {
            return diff.Where(i => i.diiftype == difftype).Select(i => i.partnerid).ToArray();
        }
    }
}