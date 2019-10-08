using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.contact
{
    public class q_upsert : question
    {
        public long partner = default;
        public string nickname = default;
        public e_connectionsetting mysetting = default;
        public class done : answer
        {
            public s_contact contact = default;
        }
    }
}