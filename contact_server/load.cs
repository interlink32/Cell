using Dna;
using Dna.profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contact_server
{
    class load : myservice<q_load>
    {
        public override Task<answer> getanswer(q_load question)
        {
            throw new NotImplementedException();
        }
    }
}