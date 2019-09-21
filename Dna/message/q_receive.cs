using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.message
{
    public class q_receive : question
    {
        public long contact = default;
        public int first_index = default;
        public int last_index = int.MaxValue;
        public class done : answer
        {
            public s_message[] messages = default;
        }
    }
}