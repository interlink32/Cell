using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class q_serverlogin : question
    {
        public string serverid = default;
        public string password = default;
        public class done : answer
        {
            public long userid = default;
            public string token = default;
        }
    }
}