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
            if (question.randomvalue == 0)
                return new developer_error();
            var dv = dbactivecode.FindOne(i => i.callerid == question.callerid && i.randomvalue == question.randomvalue);
            if (dv == null)
            {
                dv = new r_activecode()
                {
                    activecode = "12345",
                    callerid = question.callerid,
                    randomvalue = question.randomvalue
                };
                dbactivecode.Insert(dv);

            }
            return new q_sendactivecode.done();
        }
    }
}