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
            return null;
        }
    }
}