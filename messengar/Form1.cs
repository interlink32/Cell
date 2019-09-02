using Connection;
using Dna.test;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace messengar
{
    public partial class Form1 : Form
    {
        client client;
        public Form1()
        {
            InitializeComponent();
        }

        async void Button1_Click(object sender, EventArgs e)
        {
            try
            {

                client = new client();
                var dv = await client.login(txt_id.Text, txt_id.Text + "pass");
                await client.connect(Dna.chromosome.test);
                client.notify_e += Client_notify_e;
                MessageBox.Show("ok");
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        void Client_notify_e(Dna.notify obj)
        {
            m(obj);
        }

        private void m(Dna.notify obj)
        {
            switch (obj)
            {
                case n_new_message dv:
                    {
                        txt_recieve.Text = dv.message;
                    }
                    break;
            }
        }

        async void Btn_send_Click(object sender, EventArgs e)
        {
            var user = long.Parse(txt_user_id.Text);
            var dv = await client.question(new f_send_message()
            {
                message = txt_send.Text,
                receiver_user = long.Parse(txt_user_id.Text)
            }) as f_send_message.done;
        }
    }
}