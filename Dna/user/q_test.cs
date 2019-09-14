using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class q_test : question
    {
        public long receiver = 0;
        public int value = 0;
        public class done : answer
        {
            public bool delivery = false;
        }
    }
}