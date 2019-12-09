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
    class server : mainserver
    {
        public server()
        {
            ini();
        }
        public override service[] elements
        {
            get
            {
                return new service[]
                {
                    new q2getchromosome(),
                    new q2getusertoken(),
                    new q2login(),
                    new q2sendactivecode(),
                    new q2getservertoken(),
                    new q2loaddiff(),
                    new q2upsertuser(),
                    new q2searchuser(),
                    new q2upsertcontact(),
                    new q2test()
                };
            }
        }
        public override byte[] privatekey => resource.user_private_key;
        public override IPEndPoint endpoint => new IPEndPoint(reference.localip(), 10001);
        public override string password => "kfkbfkbfmbmgkbkcmbmfmbkf";

        public override e_chromosome chromosome => e_chromosome.user;

        private void ini()
        {
            db.upsert(new s2user()
            {
                fullname = "firstuser",
                general = false,
                id = 1000 * 1000 * 100
            }, false);
            createitem(e_chromosome.user, "kfkbfkbfmbmgkbkcmbmfmbkf");
            createitem(e_chromosome.message, "lhlflbkfbkdbcdvdcfhdhvgdgvhdbvhdnvjcjsd");
        }
        private static void createitem(e_chromosome chromosome, string password)
        {
            myservice<q_getusertoken>.dbserverinfo.Upsert(new s2serverinfo()
            {
                id = (int)chromosome,
                password = password
            });
        }
    }
}