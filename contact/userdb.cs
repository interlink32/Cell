using Connection;
using Dna.user;
using Dna.usercontact;
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
        public Task search(string name, e_contactsetting mysetting, e_contactsetting partnersetting)
        {
            return null;
        }
        public Task save(long contact, e_contactsetting mysetting)
        {
            return null;
        }
    }
}