using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contactserver
{
    class r_diff
    {
        [BsonId]
        public long index { get; set; }
        public long partnerid { get; set; }
        public difftype diiftype { get; set; }
    }
    enum difftype
    {
        contactupdate,
        entityupdate,
        deleted
    }
}