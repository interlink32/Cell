using System;
using System.Collections.Generic;
using System.Text;

namespace Dna
{
    public class s_entity : gene, Is_entity
    {
        public long id { get; set; }
        public virtual void update(long owner, s_entity entity)
        {
            id = entity.id;
        }
    }
}