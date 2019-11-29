using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.common
{
    public class q_loaddiff : question
    {
        public long index = default;
        public q_loaddiff(string chromosome)
        {
            z_redirect = chromosome;
        }
        public class done : answer
        {
            public long currentindex = default;
            public string entites = default;
            public long[] deleted = default;
        }
    }
}