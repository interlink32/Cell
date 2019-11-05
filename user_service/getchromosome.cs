using Connection;
using Dna;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace userserver
{
    class getchromosome : service<q_getchromosome>
    {
        private s_chromosome[] chromosome_infos;
        public getchromosome()
        {
            List<s_chromosome> list = new List<s_chromosome>();
            list.Add(reference.userchromosome);
            list.Add(new s_chromosome()
            {
                chromosome = e_chromosome.profile,
                endpoint = new IPEndPoint(reference.validip(), 10002).ToString(),
                publickey = resource.all_public_key
            });
            list.Add(new s_chromosome()
            {
                chromosome = e_chromosome.usercontact,
                endpoint = new IPEndPoint(reference.validip(), 10003).ToString(),
                publickey = resource.all_public_key
            });
            //list.Add(new s_chromosome()
            //{
            //    chromosome = e_chromosome.message,
            //    endpoint = new IPEndPoint(reference.validip(), 10004).ToString(),
            //    publickey = resource.all_public_key
            //});
            chromosome_infos = list.ToArray();
        }
        public async override Task<answer> getanswer(q_getchromosome request)
        {
            await Task.CompletedTask;
            return new q_getchromosome.done()
            {
                items = chromosome_infos
            };
        }
    }
}