using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dna
{
    public abstract class sharedentity
    {
        [BsonId]
        public long sharedid { get; set; }
        public long itemid { get; set; }
        public abstract string itemtype { get; }
    }
}