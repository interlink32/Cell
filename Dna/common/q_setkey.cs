using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.common
{
    public class q_setkey : question
    {
        public byte[] key32 = null;
        public byte[] iv16 = null;
        public sealed override e_permission z_permission => e_permission.free;
        public class done : answer { }
    }
}