using System;
using System.Collections.Generic;
using System.Text;

namespace Dna
{
    public class notify : gene
    {
        public readonly long z_receiver;
        public notify(long receiver)
        {
            this.z_receiver = receiver;
        }
    }
}