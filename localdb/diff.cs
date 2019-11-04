using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace localdb
{
    public class diffentity
    {
        [BsonId]
        public long index { get; set; }
        public long itemid { get; set; }
        public bool state { get; set; }
    }
}