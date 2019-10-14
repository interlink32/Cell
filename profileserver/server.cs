using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Connection;
using Dna;

namespace profileserver
{
    class server : Connection.mainserver
    {
        public override service[] elements => new service[]
        {
            new upsert(),
            new load()
        };
        public override byte[] privatekey => resource.all_private_key;
        public override IPEndPoint endpoint => new IPEndPoint(reference.localip(), 10002);
        public override string password => "kgjjjfjbjvjcnvjfjbkndfjbjcnbjcn";
        public override e_chromosome id => e_chromosome.profile;
    }
}