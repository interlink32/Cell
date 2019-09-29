using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user_service
{
    public class s_introcode
    {
        [BsonId]
        public long user_id { get; set; }
        public double device { get; set; }
        public double value { get; set; }
        public DateTime time { get; set; } = DateTime.Now;
    }
}