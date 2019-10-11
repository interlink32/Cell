using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class q_rename : question
    {
        public long user = default;
        public string fullname = default;
        public sealed override e_permission z_permission => e_permission.server;
    }
}