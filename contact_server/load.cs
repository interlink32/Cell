using Dna;
using Dna.contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contact_server
{
    class load : my_service<q_load>
    {
        public async override Task<answer> get_answer(q_load question)
        {
            var dv = await Task.Run(() =>
            {
                return db_contact.Find(i => i.any(question.z_user)).ToArray();
            });
            return new q_load.done()
            {
                contacts = dv.Select(i => i.clone()).ToArray()
            };
        }
    }
}