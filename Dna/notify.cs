using System;
using System.Collections.Generic;
using System.Text;

namespace Dna
{
    public class notify : gene
    {
        public long z_receiver = 0;
        public notify(long receiver)
        {
            this.z_receiver = receiver;
        }
    }
}