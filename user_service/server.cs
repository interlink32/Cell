using Connection;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace user_service
{
    class server : Connection.server
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
                    new getchromosomeinfo(),
                    new gettoken(),
                    new getuser(),
                    new autologin(),
                    new logout(),
                    new load(),
                    new loadalluser(),
                    new sendactivecode(),
                    new serverlogin()
                };
            }
        }
        public override byte[] privatekey => resource.user_private_key;
        public override IPEndPoint endpoint => new IPEndPoint(reference.localip(), 10001);
        public override string id => "userserver";
        public override string password => "kfkbfkbfmbmgkbkcmbmfmbkf";
        private void ini()
        {
            myservice<q_gettoken>.dbuser.Upsert(new r_user()
            {
                callerid = "firstuser",
                id = 1000 * 1000
            });
            createitem("userserver", "kfkbfkbfmbmgkbkcmbmfmbkf");
        }

        private static void createitem(string name, string password)
        {
            myservice<q_gettoken>.dbserverinfo.Upsert(new r_serverinfo()
            {
                name = name,
                password = password
            });
        }
    }
}