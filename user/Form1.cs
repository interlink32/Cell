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
                txt_password.Text = btn_login.Text = default;
                btn_create.Enabled = txt_password.Enabled = btn_login.Enabled = false;
                return;
            }
            if (client.all_user().Contains(txt_user.Text))
            {
                txt_password.Text = default;
                txt_password.Enabled = false;
                btn_login.Text = "Logout";
                btn_login.Enabled = true;
                btn_create.Enabled = false;
                lst.SelectedItem = txt_user.Text;
            }
            else
            {
                txt_password.Enabled = btn_login.Enabled = btn_create.Enabled = true;
                txt_password.Text = txt_user.Text + "_password";
                btn_login.Text = "Login";
                lst.SelectedItem = null;
            }
        }
        async void Btn_login_Click(object sender, EventArgs e)
        {
            Enabled = false;
            if (btn_login.Text == "Login")
            {
                client client = new client(txt_user.Text, txt_password.Text);
                if (await client.connect())
                    client.close();
                else
                {
                    MessageBox.Show("Incorrect username or password");
                }
            }
            else
            {
                client client = new client(txt_user.Text);
                if (await client.connect())
                    await client.logout();
            }
            Enabled = true;
        }
        async void Btn_create_Click(object sender, EventArgs e)
        {
            btn_create.Enabled = false;
            client client = new client("default", "default_password");
            await client.connect();
            var dv = await client.question(new q_create_user()
            {
                user_name = txt_user.Text,
                password = txt_password.Text
            });
            switch (dv)
            {
                case q_create_user.done sw:
                    {

                    }
                    break;
                case q_create_user.duplicate sw:
                    {
                        MessageBox.Show("This user is already registered.");
                    }
                    break;
            }
            btn_create.Enabled = true;
        }
    }
}