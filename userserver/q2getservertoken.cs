using stemcell;
using Dna;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace userserver
{
    class q2getservertoken : myservice<q_getservertoken>
    {
        public async override Task<answer> getanswer(q_getservertoken question)
        {
            await Task.CompletedTask;
            var dv = dbserverinfo.FindOne(i => i.id == (int)question.chromosome && i.password == question.password);
            if (dv == null)
            {
                Console.Beep(1000, 1000);
                return null;
            }
            else
            {
                var user = new s2user()
                {
                    callerid = question.chromosome.ToString(),
                    fullname = question.chromosome.ToString(),
                    id = (long)question.chromosome,
                    general = false,
                    active = true
                };
                q2sendactivecode.changetoken(user);
                db.upsert(user, false);
                return new q_getservertoken.done() { token = user.token };
            }
        }
    }
}