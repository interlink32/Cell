using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contact_server
{
    class r_log
    {
        [BsonId]
        public long index { get; set; }
        public long contact { get; set; }
        public e_log type { get; set; }
    }
    enum e_log { update, delete }
}