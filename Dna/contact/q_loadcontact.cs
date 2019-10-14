using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.contact
{
    public class q_loadcontact : question
    {
        public long contact = default;
        public class done : answer
        {
            public s_contact contact = default;
        }
    }
}