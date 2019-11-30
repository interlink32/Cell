using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dna
{
    public abstract class question : gene
    {
        [JsonIgnore]
        public long z_user = 0;
        [JsonIgnore]
        public virtual e_permission z_permission { get; } = e_permission.user;
        public virtual void z_normalize() { }
        public static void nullcheck(ref string val)
        {
            if (val == "")
                val = null;
        }
    }
}