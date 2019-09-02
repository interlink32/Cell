using Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace server_test
{
    class service_im : service
    {
        public override service_gene[] elements
        {
            get
            {
                return new service_gene[]
                {
                    new message()
                };
            }
        }
        public override byte[] private_key => store.test_private_key;
        public override IPEndPoint endpoint => new IPEndPoint(reference.local_ip(), 10002);

        public override string userid => "3";

        public override string password => "3pass";
    }
}