using Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace server_test
{
    class serviece_im : service
    {
        public serviece_im(IPEndPoint endPoint) : base(endPoint) { }
        public override service_gene[] elements
        {
            get
            {
                return new service_gene[]
                {
                    new value_im(),
                    new string_im()
                };
            }
        }
    }
}