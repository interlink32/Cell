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
    class serverlogin : myservice<q_serverlogin>
    {
        public async override Task<answer> getanswer(q_serverlogin question)
        {
            await Task.CompletedTask;
            var dv = dbserverinfo.FindOne(i => i.name == question.serverid && i.password == question.password);
            if (dv == null)
                return null;
            else
            {
                dbtoken.Delete(i => i.user == dv.id);
                r_token token = new r_token()
                {
                    user = dv.id,
                    value = basic.random.Next() + basic.random.Next().ToString()
                };
                dbtoken.Insert(token);
                return new q_serverlogin.done() { userid = dv.id, token = token.value };
            }
        }
    }
}