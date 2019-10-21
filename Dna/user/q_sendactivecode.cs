using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class q_sendactivecode : question
    {
        public string callerid = default;
        public override e_permission z_permission => e_permission.free;
        public class done : answer { }
    }
}