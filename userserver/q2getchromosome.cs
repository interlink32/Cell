using stemcell;
using Dna;
using Dna.user;
using servercell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using core;

namespace userserver
{
    class q2getchromosome : service<q_getchromosome>
    {
        private s_chromosome[] chromosome_infos;
        public q2getchromosome()
        {
            List<s_chromosome> list = new List<s_chromosome>();
            list.Add(basic.userchromosome);
            list.Add(new s_chromosome()
            {
                chromosome = e_chromosome.userdata,
                endpoint = basic.userchromosome.endpoint,
                publickey = basic.userchromosome.publickey
            });
            list.Add(new s_chromosome()
            {
                chromosome = e_chromosome.message,
                endpoint = new IPEndPoint(reference.validip(), 10002).ToString(),
                publickey = resource.all_public_key
            });
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