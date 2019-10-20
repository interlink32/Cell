using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user_service
{
    public class r_login
    {
        public long id { get; set; }
        public long user { get; set; }
        public long device { get; set; }
        public string token { get; set; }
    }
}