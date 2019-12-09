using stemcell;
using Dna;
using Dna.user;
using LiteDB;
using servercell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace userserver
{
    abstract class myservice<T> : service<T> where T : question
    {
        public static LiteCollection<s2serverinfo> dbserverinfo => db.dbserverinfo;
    }
}