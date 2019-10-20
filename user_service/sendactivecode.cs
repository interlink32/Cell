using Dna;
using Dna.common;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user_service
{
    class sendactivecode : myservice<q_sendactivecode>
    {
        public async override Task<answer> getanswer(q_sendactivecode question)
        {
            await Task.CompletedTask;
            var dv = dbactivecode.FindOne(i => i.device == question.device && i.callerid == question.callerid);
            if (dv == null)
            {
                dv = new r_activecode()
                {
                    activecode = "12345",
                    callerid = question.callerid,
                    device = question.device
                };
                dbactivecode.Insert(dv);
                //Requires code to SMS activecode

            }
            return new q_sendactivecode.done();
        }
    }
}