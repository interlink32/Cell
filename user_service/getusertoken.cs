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
            bool device = dbdevice.Exists(i => i.id == question.device.id && i.randomcode == question.device.randomcode);
            if (device)
                return new q_getusertoken.invaliddevice();
            var activecode = dbactivecode.FindOne(i => i.device == question.device.id && i.callerid == question.callerid && i.activecode == question.activecode);
            if (activecode == null)
                return new q_getusertoken.invalidactivecode();
            var user = dbuser.FindOne(i => i.callerid == question.callerid);
            if (user == null)
            {
                user = new r_user()
                {
                    callerid = question.callerid,
                    fullname = question.callerid,
                    general = true
                };
                dbuser.Insert(user);
            }
            dbtoken.Delete(i => i.user == user.id && i.device == question.device.id);
            var token = new r_login()
            {
                user = user.id,
                device = question.device.id,
                token = "" + basic.random.Next(1000, 9999) + basic.random.Next().ToString()
            };
            dbtoken.Insert(token);
            return new q_getusertoken.done()
            {
                token = token.token,
                user = user.id
            };
        }
    }
}