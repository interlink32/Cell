using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.contact
{
    public class q_update : question
    {
        public long contact = default;
        public e_state state = default;
        public class done : answer
        {
            public s_contact contact = default;
        }
    }
}