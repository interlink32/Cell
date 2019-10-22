using System;
using System.Collections.Generic;
using System.Text;

namespace Dna
{
    public class entity
    {
        public long id { get; set; }
        public virtual entity clone()
        {
            return this;
        }
    }
}