using Dna;
using Dna.contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contact_server
{
    class loadFcontact : myservice<q_loadcontact>
    {
        public async override Task<answer> getanswer(q_loadcontact question)
        {
            await Task.CompletedTask;
            return null;
        }
    }
}