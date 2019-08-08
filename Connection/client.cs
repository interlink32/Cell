using Converter;
using Dna;
using System;
using System.Threading.Tasks;

namespace Connection
{
    public class client
    {
        public static converter converter = new converter();
        public Task<response> question(request request)
        {
            var dv = converter.change(request);
            return null;
        }
    }
}
