using Dna;
using Dna.common;
using Dna.contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contact_server
{
    class update : my_service<q_update>
    {
        public async override Task<answer> get_answer(q_update question)
        {
            await Task.CompletedTask;
            var dv = db_contact.FindOne(i => i.id == question.contact);
            if (dv == null)
                return new developer_error() { code = "kfjbjfjbjfjjdmvmdjckx" };
            else
            {
                var mem = dv.members.FirstOrDefault(i => i.person == question.z_user);
                if (mem == null)
                    return new developer_error() { code = "lgkfjbnfjvjcjdkvkcdk" };
                mem.state = GetState(question.state);
                db_contact.Update(dv);
                return new q_update.done()
                {
                    contact = dv.clone()
                };
            }
        }
    }
}