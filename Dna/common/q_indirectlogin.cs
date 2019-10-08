using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.common
{
    public class q_indirectlogin : question
    {
        public string token = default;
        public bool acceptnotifications = false;
        public class done : answer { }
    }
}