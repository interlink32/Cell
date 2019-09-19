using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace message_server
{
    public class conversation
    {
        public long id { get; set; }
        public long[] members { get; set; }
    }
}