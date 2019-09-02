using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.test
{
    public class n_message : notify
    {
        public long sender = 0;
        public long value = 0;
        public n_message(long receiver) : base(receiver)
        {

        }
    }
}