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
        dbendreaderuser db;
        public body(long user) : base(user)
        {
            db = new dbendreaderuser(user);
            body.SelectionMode = DataGridSelectionMode.Single;
            add(hdr_contact, clm_contact);
            add(hdr_fullname, clm_fulname);
            add(hdr_code, clm_code);
            add(hdr_city, clm_city);
            add(hdr_des, clm_des);
        }

        protected async override void reset()
        {
            loadbox.mainwaiting();
            var dv = await client.question(new q_searchprofile()
            {
                fullname = hdr_fullname.text,
                nationalcode = hdr_code.text
            }) as q_searchprofile.done;
            List<item> items = new List<item>();
            item item;
            foreach (var i in dv.profiles)
            {
                item = new item();
                item.client = client;
                item.z_refresh = this.body.Items.Refresh;
                item.copy(i);
                item.contactf = validcontact(i.id);
                items.Add(item);
            }
            source = items;
            loadbox.mainrelease();
        }
        private bool validcontact(long id)
        {
            var dv = db.dbentity.FindOne(i => i.id == id);
            return dv?.contact?.valid ?? false;
        }
        public class item : s_profile
        {
            public client client = null;
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
                if (contactf)
                {
                    if (await messagebox.maindilog((null, removemessage()), "حذف شود", "لغو درخواست") == 0)
                    {
                       
                        contactf = false;
                        z_refresh?.Invoke();
                    }
                }
                if (value)
                {
                    if (await messagebox.maindilog((null, addmessage()), "اضافه شود", "لغو درخواست") == 0)
                    {
                        var dv = await client.question(new q_upsertcontact()
                        {
                            partner = id,
                            mysetting = e_contactsetting.ordinary
                        });
                        contactf = true;
                        z_refresh?.Invoke();
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