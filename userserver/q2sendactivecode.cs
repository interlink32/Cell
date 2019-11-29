using Connection;
using Dna;
using Dna.common;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace userserver
{
    class q2sendactivecode : myservice<q_sendactivecode>
    {
        public async override Task<answer> getanswer(q_sendactivecode question)
        {
            await Task.CompletedTask;
            var dv = db.findone(i => i.callerid == question.callerid);
            if (dv == null)
            {
                dv = new s2user()
                {
                    callerid = question.callerid,
                    fullname = question.callerid,
                    general = true,
                };
                changetoken(dv);
                changeactivecode(dv);
                db.upsert(dv, false);
            };
            //sms activecode to caller id
            return new q_sendactivecode.done();
        }
        internal static void changetoken(s2user user)
        {
            string newtoken = null;
            do
            {
                newtoken = "" + basic.random.Next() + basic.random.Next(1000, 9999);
            } while (db.exists(i => i.token == newtoken));
            user.token = newtoken;
        }
        internal static void changeactivecode(s2user user)
        {
            user.activecode = "12345";// basic.random.Next(10000, 99999).ToString();
        }
    }
}