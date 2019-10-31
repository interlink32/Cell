using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace localdb
{
    class diff
    {
        [BsonId]
        public long index { get; set; }
        public long itemid { get; set; }
    }
}