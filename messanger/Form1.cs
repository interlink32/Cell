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
        }
        bool inp = false;
        async void Txt_partner_KeyDown(object sender, KeyEventArgs e)
        {
            if (inp)
                return;
            inp = true;
            if (e.KeyCode == Keys.Enter)
            {
                if (int.TryParse(txt_id.Text, out partner_id))
                {
                    var dv = await client.question(new q_getsituation()
                    {
                        partner = partner_id
                    }) as q_getsituation.done;
                    if (dv != null)
                    {
                        txt_partner.BackColor = Color.LightBlue;
                        var dv2 = await client.question(new q_receive()
                        {
                            last_index=int.MaxValue
                        });
                    }
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
            var dv = await client.question(new q_load()) as q_load.done;
            txt_partner.AutoCompleteCustomSource = new AutoCompleteStringCollection();
            txt_partner.AutoCompleteCustomSource.AddRange(dv.contacts.Select(i=>i.members.an))
            txt_id.BackColor = Color.LightBlue;
            txt_partner.Enabled = true;
        }

        private async Task<(string userid, string password)> Client_user_password_e()
        {
            await Task.CompletedTask;
            return (id.ToString(), id + "pass");
        }
        private void Txt_id_TextChanged(object sender, EventArgs e)
        {
            txt_send.Enabled = false;
        }
    }
}
