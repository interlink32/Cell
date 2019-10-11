using Dna;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user_service
{
    public class r_serverinfo
    {
        public int id { get; set; }
        public e_chromosome chromosome { get; set; }
        public string password { get; set; }
    }
}
