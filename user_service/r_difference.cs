using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user_service
{
    public class r_difference
    {
        [BsonId]
        public long index { get; set; }
        public long userid { get; set; }
        public r_diffstate state { get; set; }
    }
    public enum r_diffstate
    {
        update,
        delete
    }
}