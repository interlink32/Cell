using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dna
{
    public abstract class entityrelation
    {
        [BsonId]
        public long partnerid { get; set; }
    }
}