using System;
using System.Collections.Generic;
using System.Text;

namespace Dna
{
    public abstract class request : gene
    {
        public long z_user = 0;
        public long z_app = 0;
        public long z_platform = 0;
    }
}