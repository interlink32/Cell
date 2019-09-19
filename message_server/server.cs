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
        public override Connection.service[] elements => new Connection.service[]
        {

        };
        public override byte[] private_key => resource.all_private_key;

        public override IPEndPoint endpoint => new IPEndPoint(reference.local_ip(), 100002);

        public override string userid => "2";

        public override string password => "2pass";
    }
}