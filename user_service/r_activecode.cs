using Dna;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user_service
{
    public class r_activecode : question
    {
        public long id { get; set; }
        public string callerid { get; set; }
        public long randomvalue { get; set; }
        public string activecode { get; set; }
        public DateTime time { get; set; } = DateTime.Now;
    }
}