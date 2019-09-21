using Connection;
using Dna;
using Dna.contact;
using Dna.message;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace messanger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            txt_chat.ReadOnly = true;
            txt_id.TextChanged += Txt_id_TextChanged;
            txt_id.KeyDown += Txt_id_KeyDown;
            txt_partner.TextChanged += Txt_partner_TextChanged;
            txt_partner.KeyDown += Txt_partner_KeyDown;
            txt_partner.AutoCompleteCustomSource = new AutoCompleteStringCollection();
            txt_partner.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txt_partner.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txt_send.KeyDown += Txt_send_KeyDown;
        }
        async void Txt_send_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txt_send.Text == "")
                    return;
                txt_send.Enabled = false;
                var dv = await client.question(new q_send()
                {
                    contact = contact_id,
                    text = txt_send.Text
                }) as q_send.doen;
                set(dv.message);
                txt_send.Text = null;
                txt_send.Enabled = true;
            }
        }

        bool inp = false;
        long contact_id = 0;
        async void Txt_partner_KeyDown(object sender, KeyEventArgs e)
        {
            if (inp)
                return;
            inp = true;
            if (e.KeyCode == Keys.Enter)
            {
                if (int.TryParse(txt_partner.Text, out partner_id))
                {
                    var dv = await client.question(new q_loadFpartner() { partner = partner_id }) as q_loadFpartner.done;
                    contact_id = dv.contact.id;
                    await receive();
                }
            }
            inp = false;
        }

        private async Task receive()
        {
            var rsv = await client.question(new Dna.message.q_receive()
            {
                contact = contact_id
            }) as q_receive.done;
            Invoke(new Action(() =>
            {
                txt_chat.Text = null;
                set(rsv.messages);
                sending();
            }));
        }

        private void set(params s_message[] messages)
        {
            foreach (var i in messages)
            {
                txt_chat.Text += i.sender + ": " + i.text + "\r\n";
            }
        }
        private void sending()
        {
            txt_send.Enabled = true;
        }

        private void Txt_partner_TextChanged(object sender, EventArgs e)
        {
            txt_partner.BackColor = Color.LightPink;
            txt_send.Enabled = false;
        }

        client client = null;
        int id = default;
        int partner_id = default;
        private void Txt_id_KeyDown(object sender, KeyEventArgs e)
        {
            txt_id.BackColor = Color.LightPink;
            if (e.KeyCode == Keys.Enter)
            {
                if (int.TryParse(txt_id.Text, out id))
                {
                    client = new client(id.ToString());
                    client.notify_e += Client_notify_e;
                    client.user_password_e += Client_user_password_e;
                    client.login_e += Client_login_e;
                }
            }
        }

        async void Client_notify_e(notify obj)
        {
            switch (obj)
            {
                case n_new_message sw:
                    {
                        await receive();
                    }
                    break;
            }
        }

        async void Client_login_e(client obj)
        {
            client.active_notify(chromosome.message);
            var dv = await client.question(new q_loadall()) as q_loadall.done;
            var all = dv.contacts.Select(i => i.members.First(j => j.person != id).person).ToArray();  
            Invoke(new Action(() =>
            {
                txt_partner.AutoCompleteCustomSource.AddRange(all.Select(i => i.ToString()).ToArray());
                txt_id.BackColor = Color.LightBlue;
                txt_partner.Enabled = true;
            }));
        }

        private async Task<(string userid, string password)> Client_user_password_e()
        {
            await Task.CompletedTask;
            return (id.ToString(), id + "pass");
        }
        private void Txt_id_TextChanged(object sender, EventArgs e)
        {
            txt_send.Enabled = false;
            txt_partner.Text = "";
        }
    }
}
