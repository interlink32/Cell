using Connection;
using Dna;
using Dna.user;
using Dna.userdata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace userserver
{
    class q2getusertoken : myservice<q_getusertoken>
    {
        public async override Task<answer> getanswer(q_getusertoken question)
        {
            await Task.CompletedTask;
            var user = db.findone(i => i.callerid == question.callerid);
            if (user == null || user.activecode != question.activecode)
                return new q_getusertoken.invalidactivecode();
            if (!user.active)
            {
                user.active = true;
                db.upsert(user, false);
                db.upsert(user.id, user.id, e_contactsetting.connect);
            }
            return new q_getusertoken.done()
            {
                token = user.token,
                user = user.id
            };
        }
    }
}