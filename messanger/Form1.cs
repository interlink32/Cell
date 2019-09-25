using Connection;
using Dna;
using Dna.contact;
using Dna.message;
using LiteDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace messanger
{
    public partial class Form1 : Form
    {
        LiteDatabase db = new LiteDatabase("d://messanger.db");
        public Form1()
        {
            InitializeComponent();
            FormClosing += Form1_FormClosing;
            txt_chat.ReadOnly = true;
            txt_id.TextChanged += Txt_id_TextChanged;
            txt_partner.TextChanged += Txt_partner_TextChanged;
            txt_id.KeyDown += Txt_id_KeyDown;
            txt_partner.KeyDown += Txt_partner_KeyDown;
            txt_send.KeyDown += Txt_send_KeyDown;
        }
        private void Txt_partner_TextChanged(object sender, EventArgs e)
        {
            run(() =>
            {
                txt_send.Enabled = false;
                txt_send.Text = null;
                txt_partner.BackColor = Color.LightPink;
                partner = contact = default;
                txt_chat.Text = null;
                last_index = 0;
            });
        }
        async void Txt_send_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txt_send.Enabled = false;
                var dv = await client.question(new q_send()
                {
                    contact = contact,
                    text = txt_send.Text
                });
                await load();
                txt_send.Text = null;
                txt_send.Enabled = true;
            }
        }
        void add(s_message message)
        {
            run(() =>
            {
                if (message.sender == user)
                {
                    txt_chat.SelectionColor = Color.Brown;
                }
                else
                {
                    txt_chat.SelectionColor = Color.Black;
                }
                txt_chat.AppendText(message.sender + " : " + message.text + "\r\n");
                last_index = message.id;
                txt_chat.ScrollToCaret();
            });
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            close_db();
        }
        int last_index = default;
        async Task load()
        {
            var dv = await client.question(new q_receive()
            {
                contact = contact,
                first_index = last_index + 1
            }) as q_receive.done;
            foreach (var i in dv.messages)
            {
                db_message.Upsert(i);
                add(i);
            }
        }
        void load_local()
        {
            var dv = db_message.FindAll().ToArray();
            foreach (var i in dv)
                add(i);
        }
        private void close_db()
        {
            db?.Dispose();
            db = null;
        }
        long contact = default;
        async void Txt_partner_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txt_partner.BackColor == Color.LightPink)
            {
                if (!long.TryParse(txt_partner.Text, out partner))
                    return;
                txt_partner.Enabled = false;
                var dv = db_contact.FindOne(i => i.included(user, partner));
                if (dv == null)
                {
                    var rsv = await client.question(new q_loadFpartner()
                    {
                        partner = partner
                    }) as q_loadFpartner.done;
                    db_contact.Insert(rsv.contact);
                    dv = rsv.contact;
                }
                contact = dv.id;
                load_local();
                await load();
                run(send_pro);
                txt_partner.Enabled = true;
            }
        }
        private void send_pro()
        {
            txt_partner.BackColor = Color.LightBlue;
            txt_send.Enabled = true;
            txt_send.Focus();
            Console.Beep();
        }
        private LiteCollection<s_contact> db_contact => db.GetCollection<s_contact>();
        private LiteCollection<s_message> db_message => db.GetCollection<s_message>(contact.ToString());

        client client = default;
        private void Txt_id_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txt_id.BackColor == Color.LightPink)
            {
                if (!long.TryParse(txt_id.Text, out user))
                    return;
                client = new client(txt_id.Text);
                client.login_e += Client_login_e;
                client.user_password_e += Client_user_password_e;
            }
        }
        private async Task<(string userid, string password)> Client_user_password_e()
        {
            await Task.CompletedTask;
            return (txt_id.Text, txt_id.Text + "pass");
        }

        private void Client_login_e(client obj)
        {
            client.notify_e += Client_notify_e;
            client.active_notify(chromosome.message);
            client.reconnect_e += Client_reconnect_e;
            run(partner_change);
        }

        async void Client_reconnect_e(chromosome obj)
        {
            await load();
        }

        private void partner_change()
        {
            txt_id.BackColor = Color.LightBlue;
            txt_partner.Enabled = true;
            Console.Beep();
            txt_partner.Focus();
        }
        void Client_notify_e(notify obj)
        {
            if (obj is n_new_message)
            {
                load().Wait();
                Console.Beep(4000, 100);
            }
        }
        private void Txt_id_TextChanged(object sender, EventArgs e)
        {
            user_change();
        }

        long user = default;
        long partner = default;
        private void run(Action action)
        {
            Invoke(new Action(action));
        }

        void user_change()
        {
            client?.close();
            client = null;
            user = partner = contact = default;
            last_index = default;
            txt_id.BackColor = Color.LightPink;
            txt_partner.Text = null;
            txt_partner.BackColor = Color.LightPink;
            txt_partner.Enabled = false;
            txt_chat.Text = null;
            txt_send.Text = null;
            txt_send.Enabled = false;
            last_index = 0;
        }
    }
}