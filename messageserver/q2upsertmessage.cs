using Connection;
using Dna;
using Dna.common;
using Dna.message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace messageserver
{
    class q2upsertmessage : service<q_upsertmessage>
    {
        public async override Task<answer> getanswer(q_upsertmessage question)
        {
            await Task.CompletedTask;
            if (question.text == null || question.text == "")
                return error.create("kgkdkdkvmfmdmvmfmmcmsdmbmd");
            s_message message = new s_message();
            if (question.id == 0)
                message.id = db.create(question.z_user);
            else
            {
                var dv = db.getid(question.id);
                if (dv.sender != question.z_user)
                    return error.create("lfbkfkbjfjbjfkbkmbkdmbfmbkfnbm");
                if (DateTime.Now - dv.time > TimeSpan.FromMinutes(5))
                    return new q_upsertmessage.done() { error_timeout = true };
                message.id = question.id;
                message.edited = true;
            }
            message.text = question.text;
            db.upsert(question.z_user, question.partner, message);
            return new q_upsertmessage.done();
        }
    }
}