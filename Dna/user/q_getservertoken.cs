using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class q_getservertoken : question
    {
        public e_chromosome chromosome = default;
        public string password = default;
        public override e_permission z_permission => e_permission.free;
        public class done : answer
        {
            public long token = default;
        }
    }
}