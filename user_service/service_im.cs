using Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace user_service
{
    class service_im : service
    {
        public override service_gene[] elements
        {
            get
            {
                return new service_gene[]
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
        public override IPEndPoint endpoint => new IPEndPoint(reference.local_ip(), 10000);

        public override string userid => "1";

        public override string password => "1pass";
    }
}