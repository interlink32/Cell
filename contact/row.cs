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
        private r_connectionsetting mysetting1;

        public long id { get; set; }
        public s_user partner { get; set; }
        public r_connectionsetting mysetting
        {
            get => mysetting1;
            set => mysetting1 = value;
        }
        public r_connectionsetting partnersettin { get; set; }
    }
}
