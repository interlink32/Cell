using Connection;
using Dna.user;
using LiteDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace user
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var dv = client.all_user();
            client.user_e += Client_user_e;
            reset();
            txt_user.TextChanged += Txt_user_TextChanged;
            lst.SelectedIndexChanged += Lst_SelectedIndexChanged;
        }

        private void Lst_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dv = lst.SelectedItem?.ToString();
            if (dv != null)
                txt_user.Text = dv;
        }

        async void run(Action action)
        {
            await Task.Delay(1);
            Invoke(action);
        }
        private void Client_user_e((string user, bool login) obj)
        {
            run(() =>
            {
                if (obj.login)
                {
                    lst.Items.Add(obj.user);
                    Console.Beep();
                }
                else
                {
                    lst.Items.Remove(obj.user);
                    Console.Beep();
                }
                reset();
            });
        }

        private void Txt_user_TextChanged(object sender, EventArgs e)
        {
            reset();
        }
        void reset()
        {
            if (txt_user.Text.Length < 3)
            {
                txt_password.Text = btn.Text = default;
                txt_password.Enabled = btn.Enabled = false;
                return;
            }
            if (client.all_user().Contains(txt_user.Text))
            {
                txt_password.Text = default;
                txt_password.Enabled = false;
                btn.Text = "Logout";
                btn.Enabled = true;
                lst.SelectedItem = txt_user.Text;
            }
            else
            {
                txt_password.Enabled = btn.Enabled = true;
                txt_password.Text = txt_user.Text + "_password";
                btn.Text = "Login";
                lst.SelectedItem = null;
            }
        }
        async void Btn_login_Click(object sender, EventArgs e)
        {
            Enabled = false;
            if (btn.Text == "Login")
            {
                client client = new client(txt_user.Text, txt_password.Text);
                if (await client.connect())
                    client.close();
                else
                    MessageBox.Show("invalid");
            }
            else
            {
                client client = new client(txt_user.Text);
                if (await client.connect())
                   await client.logout();
            }
            Enabled = true;
        }
    }
}