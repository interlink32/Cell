using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.message
{
    public class q_getsituation : question
    {
        public long contact = default;
        public class done : answer
        {
            /// <summary>
            /// Last Visited By Partner
            /// </summary>
            public DateTime? last_visit = default;
        }
    }
}