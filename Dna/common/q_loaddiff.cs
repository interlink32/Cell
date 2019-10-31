using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.common
{
    public class q_loaddiff : question
    {
        public long index = default;
        public q_loaddiff(e_chromosome chromosome)
        {
            z_redirect = chromosome;
        }
        public override e_permission z_permission => e_permission.server;
        public class doen : answer
        {
            public long currentindex = default;
            public long[] updatedentity = default;
            public long[] deletedentity = default;
        }
    }
}