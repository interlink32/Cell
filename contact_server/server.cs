using Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace contact_server
{
    class server : Connection.server
    {
        public override service[] elements => new service[]
        {
         new upsert(),
         new update(),
         new loadallcontact(),
         new loadFcontact()
        };
        public override byte[] private_key => resource.all_private_key;
        public override IPEndPoint endpoint => new IPEndPoint(reference.local_ip(), 10003);
        public override string user_name => "contact_server";
        public override string password => "mgjdjbjdbkdbkgfvjdjdnvbjdmd";
    }
}