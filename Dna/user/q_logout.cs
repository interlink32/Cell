using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class q_logout : question
    {
        public string token = default;
        public override e_permission z_permission => e_permission.free;
    }
}