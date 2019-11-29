using Connection;
using controllibrary;
using Dna;
using Dna.userdata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace searchuser
{
    public class item : s_user
    {
        bool contactf = default;
        long owner;
        public bool contact
        {
            get { return contactf; }
            set { set(value); }
        }
        public override void update(long owner, s_entity entity)
        {
            this.owner = owner;
            base.update(owner, entity as s_user);
        }
        public void updatecontact(bool val)
        {
            contactf = val;
        }
        async void set(bool value)
        {
            if (id == owner)
                return;
            if (value)
            {
                if (await messagebox.maindilog((null, addmessage), "اضافه شود", "لغو درخواست") == 0)
                {
                    await client.question(owner, new q_upsertcontact() { partner = id, setting = e_contactsetting.connect });
                }
            }
            else
            {
                if (await messagebox.maindilog((null, removemessage), "حذف شود", "لغو درخواست") == 0)
                {
                    await client.question(owner, new q_upsertcontact() { partner = id, setting = e_contactsetting.disconnect });
                }
            }
        }
        private string addmessage => "آیا مطمعن هستید که میخواهید " + fullname + " را به مخاطبان خود اضافه کنید؟";
        private string removemessage => "آیا مطمعن هستید که میخواهید " + fullname + " را از لیست مخاطبان خود حذف کنید؟";
    }
}
