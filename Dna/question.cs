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
    }
}