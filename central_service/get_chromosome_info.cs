using Connection;
using Dna;
using Dna.central;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace central_service
{
    class get_chromosome_info : service_gene<f_get_chromosome_info>
    {
        private s_chromosome_info[] chromosome_infos;
        public get_chromosome_info()
        {
            List<s_chromosome_info> list = new List<s_chromosome_info>();
            list.Add(new s_chromosome_info()
            {
                chromosome = chromosome.user,
                endpoint = new IPEndPoint(reference.valid_ip(), 10001).ToString(),
                public_key = resource.user_public_key
            });
            list.Add(new s_chromosome_info()
            {
                chromosome = chromosome.test,
                endpoint = new IPEndPoint(reference.valid_ip(), 10002).ToString(),
                public_key = resource.test_public_key
            });
            chromosome_infos = list.ToArray();
        }
        public async override Task<response> get_answer(f_get_chromosome_info request)
        {
            await Task.CompletedTask;
            return new f_get_chromosome_info.done()
            {
                chromosome_infos = chromosome_infos
            };
        }
    }
}