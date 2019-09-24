using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.message
{
    public class s_message
    {
        public int id { get; set; }
        public long sender { get; set; }
        public string text { get; set; }
        public DateTime time { get; set; }
    }
}