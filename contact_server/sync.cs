using Dna;
using Dna.profile;
using localdb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contactserver
{
    class sync : synchronizer<s_profile>
    {
        public sync() : base(e_chromosome.profile, (long)e_chromosome.usercontact)
        {
        }
        protected override void apply(s_profile entity)
        {
            
        }
        protected override void delete(long entity)
        {
            throw new NotImplementedException();
        }
        protected override Task<s_profile[]> getentities(long[] ids)
        {
            throw new NotImplementedException();
        }
    }
}