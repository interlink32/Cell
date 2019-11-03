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
            var user = dbuser.FindOne(i => i.callerid == question.callerid);
            if (user == null || user.activecode != question.activecode)
                return new q_getusertoken.invalidactivecode();
            if (!user.active)
            {
                user.active = true;
                dbuser.Update(user);
            }
            return new q_getusertoken.done()
            {
                token = user.token,
                user = user.id
            };
        }
    }
}