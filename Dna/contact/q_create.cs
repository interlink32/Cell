using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.contact
{
    public class q_create : question
    {
        public long person = default;
        public class done : answer
        {
            public s_contact contact = default;
        }
    }
}