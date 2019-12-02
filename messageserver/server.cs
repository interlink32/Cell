using Connection;
using Dna;
using servercell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace messageserver
{
    class server : mainserver
    {
        public override service[] elements => new service[]
        {
            new q2loadmessage(),
            new q2upsertmessage()
        };
        public override byte[] privatekey => resource.all_private_key;
        public override IPEndPoint endpoint => new IPEndPoint(reference.validip(), 10002);
        public override e_chromosome chromosome => e_chromosome.message;
        public override string password => "lhlflbkfbkdbcdvdcfhdhvgdgvhdbvhdnvjcjsd";
    }
}