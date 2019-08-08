using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cell
{
    public abstract class server_base<T> : client_base where T : request
    {
        public abstract Task<response> answer(T request);
    }
}