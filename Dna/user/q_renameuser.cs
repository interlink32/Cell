using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class q_renameuser : question
    {
        public string fullname = default;
        public class done : answer
        {
            public bool p_duplicate = default;
        }
    }
}