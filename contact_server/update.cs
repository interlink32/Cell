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
            var db = dbcontact(question.z_user);
            var dv = db.FindOne(i => i.contact == question.contact);
            if (dv == null)
                return new developer_error() { code = "gkdgjjbdnvndbkdmbmdnbb" };
            dv.mysetting = question.state;
            db.Update(dv);
            return null;
        }
    }
}