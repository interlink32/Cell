using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dna
{
    public abstract class s_contact
    {
        [BsonId]
        public long partnerid { get; set; }
    }
}