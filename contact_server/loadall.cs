using Dna;
using Dna.contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contact_server
{
    class loadall : my_service<q_loadall>
    {
        public async override Task<answer> get_answer(q_loadall question)
        {
            var dv = await Task.Run(() =>
            {
                return db_contact.Find(i => i.any(question.z_user)).ToArray();
            });
            return new q_loadall.done()
            {
                contacts = dv.Select(i => i.clone()).ToArray()
            };
        }
    }
}