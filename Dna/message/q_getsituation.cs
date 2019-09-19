using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.message
{
    public class q_getsituation : question
    {
        public long partner = default;
        public class done : answer
        {
            public long chat_id = default;
            /// <summary>
            /// Last Visited By Partner
            /// </summary>
            public DateTime? last_visit = default;
        }
    }
}