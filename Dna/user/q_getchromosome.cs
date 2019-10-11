using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class q_getchromosome : question
    {
        public override e_permission z_permission => e_permission.free;
        public class done : answer
        {
            public s_chromosome[] items = null;
        }
    }
}