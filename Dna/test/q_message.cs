using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.test
{
    public class q_message : question
    {
        public long user = 0;
        public long value = 0;
        public class done : answer
        {
            public bool receive = false;
        }
    }
}