using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user_service
{
    public class r_token
    {
        [BsonId]
        public string value { get; set; }
        public long user { get; set; }
        public DateTime time { get; set; } = DateTime.Now;
    }
}