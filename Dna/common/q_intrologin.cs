using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.common
{
    public class q_intrologin : question
    {
        public double introcode = default;
        public double device = default;
        public bool accept_notifications = false;
        public class done : answer { }
    }
}