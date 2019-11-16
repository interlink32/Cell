using Connection;
using controllibrary;
using Dna.profile;
using Dna.user;
using Dna.usercontact;
using localdb;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace searchuser
{
    public class body : grid
    {
        lableheder hdr_contact = new lableheder() { MinWidth = 40 };
        checkcolumn clm_contact = new checkcolumn(nameof(item.contact), style.checkbox(), style.checkbox());

        textheder hdr_fullname = new textheder("نام کامل");
        textcolumn clm_fulname = new textcolumn(nameof(item.fullname), style.textblock(), style.textbox());

        textheder hdr_code = new textheder("شناسه ملی");
        textcolumn clm_code = new textcolumn(nameof(item.nationalcode), style.textblock(FlowDirection.LeftToRight), style.textbox(FlowDirection.LeftToRight));

        textheder hdr_city = new textheder("شهر");
        textcolumn clm_city = new textcolumn(nameof(item.city), style.textblock(), style.textbox());

        textheder hdr_des = new textheder("توضیحات") { MinWidth = 300 };
        textcolumn clm_des = new textcolumn(nameof(item.description), style.textblock(), style.textbox());

        public body()
        {
            body.SelectionMode = DataGridSelectionMode.Single;
            add(hdr_contact, clm_contact);
            add(hdr_fullname, clm_fulname);
            add(hdr_code, clm_code);
            add(hdr_city, clm_city);
            add(hdr_des, clm_des);
            source = items;
        }
        long userid = default;
        client client = default;
        dbendconsumer<s_profile, s_usercontact> db;
        public override void create(long userid)
        {
            this.userid = userid;
            client = new client(userid);
            db = new dbendconsumer<s_profile, s_usercontact>(userid);
            db.update_e += Db_update_e;
        }
        private void Db_update_e(dbend<s_profile, s_usercontact>.full obj)
        {
            var dv = items.FirstOrDefault(i => i.id == obj.id);
            if (dv != null)
            {
                dv.copy(obj.entity);
                dv.contactf = obj.contact?.valid ?? false;
            }
            body.Items.Refresh();
        }
        List<item> items = new List<item>();
        protected async override void reset()
        {
            loadbox.mainwaiting();
            items.Clear();
            var dv = await client.question(new q_searchprofile()
            {
                fullname = hdr_fullname.text,
                nationalcode = hdr_code.text
            }) as q_searchprofile.done;
            item item;
            foreach (var i in dv.profiles)
            {
                item = new item(userid, i, db.exists(NewMethod(i)));
                item.effect = effect;
                items.Add(item);
            }
            body.Items.Refresh();
            loadbox.mainrelease();
        }

        private static System.Linq.Expressions.Expression<Func<dbend<s_profile, s_usercontact>.full, bool>> NewMethod(s_profile i)
        {
            return j => j.id == i.id && j.contact.valid;
        }

        async Task effect(item arg)
        {
            if (arg.contact)
                await client.question(new q_upsertcontact() { partner = arg.id, mysetting = e_contactsetting.ordinary });
            else
                await client.question(new q_upsertcontact() { partner = arg.id, mysetting = e_contactsetting.none });
            body.Items.Refresh();
        }
        public class item : s_profile
        {
            public Func<item, Task> effect = default;
            public item(long owner, s_profile profile, bool contact)
            {
                copy(profile);
                this.owner = owner;
                contactf = contact;
            }

            private readonly long owner;
            public bool contactf = false;
            public bool contact
            {
                get => contactf;
                set
                {
                    set(value);
                }
            }
            async void set(bool value)
            {
                if (id == owner)
                    return;
                if (contactf)
                {
                    if (await messagebox.maindilog((null, removemessage()), "حذف شود", "لغو درخواست") == 0)
                    {
                        contactf = false;
                        await effect(this);
                    }
                }
                if (value)
                {
                    if (await messagebox.maindilog((null, addmessage()), "اضافه شود", "لغو درخواست") == 0)
                    {
                        contactf = true;
                        await effect(this);
                    }
                }
            }
            private string addmessage()
            {
                return "آیا مطمعن هستید که میخواهید " + fullname + " را به مخاطبان خود اضافه کنید؟";
            }
            private string removemessage()
            {
                return "آیا مطمعن هستید که میخواهید " + fullname + " را از لیست مخاطبان خود حذف کنید؟";
            }
        }
    }
}