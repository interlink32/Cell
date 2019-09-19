using Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace user_service
{
    class server : Connection.server
    {
        public override Connection.service[] elements
        {
            get
            {
                return new Connection.service[]
                {
                    new get_chromosome_info(),
                    new get_introcode(),
                    new login(),
                    new get_userid(),
                    new autologin()
                };
            }
        }
        public override byte[] private_key => resource.user_private_key;
        public override IPEndPoint endpoint => new IPEndPoint(reference.local_ip(), 10001);

        public override string userid => "1";

        public override string password => "1pass";
    }
}