using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.contact
{
    public class q_loadFpartner : question
    {
        public long partner = default;
        public class done : answer
        {
            public s_contact contact = default;
        }
    }
}