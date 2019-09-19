using Dna;
using Dna.contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contact_server
{
    class loadFcontact : my_service<q_loadFcontact>
    {
        public async override Task<answer> get_answer(q_loadFcontact question)
        {
            await Task.CompletedTask;
            var dv = db_contact.FindOne(i => i.id == question.contact);
            return new q_loadFcontact.done()
            {
                contact = dv?.clone()
            };
        }
    }
}