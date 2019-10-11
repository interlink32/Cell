using Connection;
using Dna;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace user_service
{
    class getusertoken : myservice<q_getusertoken>
    {
        public async override Task<answer> getanswer(q_getusertoken question)
        {
            await Task.CompletedTask;
            var activecode = dbactivecode.FindOne(i => i.callerid == question.callerid && i.randomvalue == question.randomvalue && i.activecode == question.activecode);
            if (activecode == null)
                return new q_getusertoken.invalid();
            var user = dbuser.FindOne(i => i.callerid == question.callerid);
            if (user == null)
            {
                user = new r_user()
                {
                    callerid = question.callerid,
                    fullname = question.callerid
                };
                dbuser.Insert(user);
            }
            var token = new r_token()
            {
                user = user.id,
                value = basic.random.Next() + basic.random.Next().ToString()
            };
            dbtoken.Insert(token);
            return new q_getusertoken.done()
            {
                token = token.value,
                user = user.clone()
            };
        }
    }
}