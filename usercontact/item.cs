using Connection;
using controllibrary;
using Dna.userdata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace usercontact
{
    class item : s_fulluser
    {
        public e_contactsetting ownersetting
        {
            get { return contact.ownersetting; }
            set
            {
                setownersetting(value);
            }
        }
        async void setownersetting(e_contactsetting value)
        {
            loadbox.mainwaiting();
            await client.question(owner, new q_upsertcontact() { partner = id, setting = value });
            loadbox.mainrelease();
        }
    }
}