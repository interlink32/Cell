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
                    
                };
            }
        }
        public override byte[] private_key => throw new NotImplementedException();

        public override IPEndPoint endpoint => throw new NotImplementedException();
    }
}
