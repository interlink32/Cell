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
    class server : Connection.mainserver
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
                    new getchromosome(),
                    new getusertoken(),
                    new login(),
                    new logout(),
                    new load(),
                    new loadalluser(),
                    new sendactivecode(),
                    new getservertoken(),
                    new rename()
                };
            }
        }
        public override byte[] privatekey => resource.user_private_key;
        public override IPEndPoint endpoint => new IPEndPoint(reference.localip(), 10001);
        public override string password => "kfkbfkbfmbmgkbkcmbmfmbkf";

        public override e_chromosome id => e_chromosome.user;

        private void ini()
        {
            myservice<q_getusertoken>.dbuser.Upsert(new r_user()
            {
                fullname = "firstuser",
                general = false,
                id = 1000 * 1000 * 100
            });
            var dv = myservice<q_getusertoken>.dbserverinfo.FindAll().ToList();
            createitem(e_chromosome.user, "kfkbfkbfmbmgkbkcmbmfmbkf");
            createitem(e_chromosome.profile, "kgjjjfjbjvjcnvjfjbkndfjbjcnbjcn");
            createitem(e_chromosome.contact, "mgjdjbjdbkdbkgfvjdjdnvbjdmd");
        }
        private static void createitem(e_chromosome chromosome, string password)
        {
            myservice<q_getusertoken>.dbserverinfo.Upsert(new r_serverinfo()
            {
                id = (int)chromosome,
                password = password
            });
        }
    }
}