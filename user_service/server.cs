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
            my_server<q_login>.db_user.Upsert(new s_user()
            {
                id = 1,
                user_name = "default",
                password = "default_password"
            });
            my_server<q_login>.db_user.Upsert(new s_user()
            {
                id = 2,
                user_name = user_name,
                password = password
            });
            my_server<q_login>.db_user.Upsert(new s_user()
            {
                id = 3,
                user_name = "contact_server",
                password = "mgjdjbjdbkdbkgfvjdjdnvbjdmd"
            });
            my_server<q_login>.db_user.Upsert(new s_user()
            {
                id = 4,
                user_name = "message_server",
                password = "khjvjbjdkbkdjbnhchjbndjbxjb"
            });

        }
        public override service[] elements
        {
            get
            {
                return new service[]
                {
                    new get_chromosome_info(),
                    new get_introcode(),
                    new login(),
                    new introcheck(),
                    new autologin(),
                    new logout()
                };
            }
        }
        public override byte[] private_key => resource.user_private_key;
        public override IPEndPoint endpoint => new IPEndPoint(reference.local_ip(), 10001);
        public override string user_name => "user_server";
        public override string password => "kfkbfkbfmbmgkbkcmbmfmbkf";
    }
}