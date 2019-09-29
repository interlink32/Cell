using Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace message_server
{
    class server : Connection.server
    {
        public override service[] elements => new service[]
        {
            new send(),
            new receive(),
            new getsituation()
        };
        public override byte[] private_key => resource.all_private_key;

        public override IPEndPoint endpoint => new IPEndPoint(reference.local_ip(), 10002);

        public override string user_name => "message_server";

        public override string password => "khjvjbjdkbkdjbnhchjbndjbxjb";
    }
}