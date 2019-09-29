using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class q_create_user : question
    {
        public string user_id = default;
        public string password = default;
        public class done : answer { }
        public class duplicate : answer { }
    }
}