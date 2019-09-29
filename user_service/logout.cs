using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dna;
using Dna.common;
using Dna.user;

namespace user_service
{
    class logout : my_server<q_logout>
    {
        public async override Task<answer> get_answer(q_logout question)
        {
            await Task.CompletedTask;
            db_device.Delete(i => i.id == question.device);
            return new void_answer();
        }
    }
}