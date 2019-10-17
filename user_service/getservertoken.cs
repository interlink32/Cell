using Connection;
using Dna;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user_service
{
    class getservertoken : myservice<q_getservertoken>
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
                dbtoken.Delete(i => i.user == dv.id);
                r_token token = new r_token()
                {
                    user = dv.id,
                    value = basic.random.Next() + basic.random.Next().ToString()
                };
                dbtoken.Insert(token);
                dbuser.Upsert(new r_user()
                {
                    callerid = question.chromosome.ToString(),
                    fullname = question.chromosome.ToString(),
                    id = (long)question.chromosome,
                    general = false
                });
                return new q_getservertoken.done() { token = token.value };
            }
        }
    }
}