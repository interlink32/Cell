using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class q_get_chromosome_info : question
    {
        public class done : answer
        {
            public s_chromosome_info[] items = null;
        }
    }
}