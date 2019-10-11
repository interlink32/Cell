using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dna
{
    public abstract class question : gene
    {
        [JsonIgnore]
        public virtual e_permission z_permission { get; } = e_permission.user;
    }
}