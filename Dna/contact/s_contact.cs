using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dna.contact
{
    public class s_contact
    {
        public long id { get; set; }
        public string nickname { get; set; }
        public e_connectionsetting mysetting { get; set; }
        public e_connectionsetting partnersetting { get; set; }
        public s_user partner { get; set; }
    }
}