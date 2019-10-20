using Connection;
using Dna;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace user_service
{
    class createdevice : myservice<q_createdevice>
    {
        public async override Task<answer> getanswer(q_createdevice question)
        {
            await Task.CompletedTask;
            r_device device = new r_device()
            {
                name = question.devicename,
                randomcode = "" + basic.random.Next(100, 999) + basic.random.Next()
            };
            dbdevice.Insert(device);
            return new q_createdevice.done()
            {
                device = new s_device()
                {
                    id = device.id,
                    randomcode = device.randomcode
                }
            };
        }
    }
}