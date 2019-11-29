using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.userdata
{
    public class q_upsertuser : question
    {
        public s_user user = default;
        public class done : answer
        {
            public bool error_duplicate = default;
            public bool error_invalidfullname = default;
        }
    }
}