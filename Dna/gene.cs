using System;
using System.Collections.Generic;
using System.Text;

namespace Dna
{
    public abstract class gene
    {
        public abstract string id { get; }
        public abstract string chromosome { get; }
    }
}