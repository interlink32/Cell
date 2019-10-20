using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.user
{
    public class q_rename : question
    {
        public string fullname = default;
        public class done : answer { }
        public class duplicate : answer { }
    }
}