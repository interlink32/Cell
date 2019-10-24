using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.common
{
    public class q_loadcontact : question
    {
        public long index = default;
        public q_loadcontact(e_chromosome chromosome)
        {
            z_redirect = chromosome;
        }
        public class done : answer
        {
            public long currentindex = default;
            public long[] updatedentity = default;
            public long[] updatedcontact = default;
            public long[] deletedcontact = default;
        }
    }
}