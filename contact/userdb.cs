using Connection;
using Dna.contact;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contact
{
    class userdb
    {
        public ObservableCollection<row> list = new ObservableCollection<row>();
        public Task search(string name, e_connectionsetting mysetting, e_connectionsetting partnersetting)
        {
            return null;
        }
        public Task save(long contact, e_connectionsetting mysetting)
        {
            return null;
        }
    }
}