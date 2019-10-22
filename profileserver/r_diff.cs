using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace profileserver
{
    public class r_diff
    {
        [BsonId]
        public long index { get; set; }
        public long itemid { get; set; }
        public byte state { get; set; }
    }
}