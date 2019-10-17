using Dna.contact;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contact
{
    class row
    {
        public long id { get; set; }
        public s_user partner { get; set; }
        public r_connectionsetting mysetting { get; set; }
        public r_connectionsetting partnersettin { get; set; }
    }
}