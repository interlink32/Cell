using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class q_introcheck : question
    {
        public double introcode = default;
        public class done : answer
        {
            public long userid = 0;
        }
        public class invalidcode : answer { }
    }
}