using Dna;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Text;

namespace Connection
{
    class n_receiver
    {
        public event Action<notify> notify_e = null;
        public n_receiver(s_chromosome_info info)
        {

        }
    }
}
