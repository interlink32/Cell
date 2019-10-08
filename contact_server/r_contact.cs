using Dna.contact;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contact_server
{
    public class r_contact
    {
        [BsonId]
        public long partner { get; set; }
        public long contact { get; set; }
        public string nickname { get; set; }
        public e_connectionsetting mysetting { get; set; }
        public e_connectionsetting partnersetting { get; set; }
    }
}