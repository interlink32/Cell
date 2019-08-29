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
                    new get_introcode(),
                    new login(),
                    new get_userid()
                };
            }
        }
        public override byte[] private_key => resource.user_private_key;
        public override IPEndPoint endpoint => new IPEndPoint(reference.local_ip(), 10001);

        public override string userid => "2";

        public override string password => "2pass";
    }
}