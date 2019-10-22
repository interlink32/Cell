using Dna.profile;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.profile
{
    public class q_upsert : question
    {
        public string fullname { get; set; }
        public string nationalcode { get; set; }
        public string tell { get; set; }
        public e_gender gender { get; set; }
        public string address { get; set; }
        public class done : answer { }

    }
}