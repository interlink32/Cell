using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.message
{
    public class q_upsertmessage : question
    {
        public long id = default;
        public long partner = default;
        public string text = default;
        public class done : answer
        {
            public bool error_timeout = default;
        }
    }
}