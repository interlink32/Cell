using Dna;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace messageserver
{
    public class s_id : s_entity
    {
        public long sender { get; set; }
        public DateTime time { get; set; }
    }
}