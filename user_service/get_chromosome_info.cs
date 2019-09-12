using Connection;
using Dna;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace user_service
{
    class get_chromosome_info : service_gene<q_get_chromosome_info>
    {
        private s_chromosome_info[] chromosome_infos;
        public get_chromosome_info()
        {
            List<s_chromosome_info> list = new List<s_chromosome_info>();
            //list.Add(new s_chromosome_info()
            //{
            //    chromosome = chromosome.test,
            //    endpoint = new IPEndPoint(reference.valid_ip(), 10001).ToString(),
            //    public_key = resource.test_public_key
            //});
            chromosome_infos = list.ToArray();
        }
        public async override Task<answer> get_answer(q_get_chromosome_info request)
        {
            await Task.CompletedTask;
            return new q_get_chromosome_info.done()
            {
                items = chromosome_infos
            };
        }
    }
}