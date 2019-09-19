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
                    await client.question(new q_getsituation()
                    {
                        
                    });
                }
            }
            inp = false;
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
                    client.user_password_e += Client_user_password_e;
                    client.login_e += Client_login_e;
                }
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
