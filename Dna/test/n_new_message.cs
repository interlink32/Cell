using System;
using System.Collections.Generic;
using System.Text;

namespace Dna.test
{
    public class n_new_message : notify
    {
        public long sender = 0;
        public string message = null;

        public n_new_message(long receiver) : base(receiver)
        {
        }
    }
}