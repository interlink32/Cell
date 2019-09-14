using Connection;
using Dna;
using Dna.user;
using System.Threading.Tasks;

namespace user_service
{
    class test : service_gene<q_test>
    {
        public override async Task<answer> get_answer(q_test request)
        {
            var dv = await notify(new Dna.common.n_event()
            {
                z_receiver = request.receiver,
                chromosome = chromosome.user,
                gene = "get_message"
            });
            return new q_test.done()
            {
                delivery = dv
            };
        }
    }
}