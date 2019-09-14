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
            var dv = await notify(new n_test()
            {
                z_receiver = request.receiver,
                value = request.value,
                z_sender = request.z_user
            });
            return new q_test.done()
            {
                delivery = dv
            };
        }
    }
}