using Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace central_service
{
    class service_im : service
    {
        public override service_gene[] elements => new service_gene[]
        {
            new get_chromosome_info()
        };
        public override byte[] private_key => resource.central_private_key;
        public override IPEndPoint endpoint => new IPEndPoint(reference.local_ip(), 10000);
    }
}