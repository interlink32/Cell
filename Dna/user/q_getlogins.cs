using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class q_getlogins : question
    {
        public string deviceid = default;
        public class done : answer
        {
            public s_user[] users = default;
        }
    }
}