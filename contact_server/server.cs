using Connection;
using Dna;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace contact_server
{
    class server : Connection.mainserver
    {
        public override service[] elements => new service[]
        { new upsert(), new update(), new loadallcontact(), new loadFcontact() };
        public override byte[] privatekey => resource.all_private_key;
        public override IPEndPoint endpoint => new IPEndPoint(reference.localip(), 10003);
        public override string password => "mgjdjbjdbkdbkgfvjdjdnvbjdmd";
        public override e_chromosome id => e_chromosome.contact;
    }
}