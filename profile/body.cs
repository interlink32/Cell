using Connection;
using controllibrary;
using Dna.profile;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace profile
{
    class body : uiapp
    {
        StackPanel panel = new StackPanel()
        {
            Margin = new Thickness(20, 0, 20, 20),
            FlowDirection = FlowDirection.RightToLeft
        };
        Label lblfullname = new Label() { Content = "نام کامل : " };
        TextBox txtfullname = new TextBox();
        Label lblnationalcode = new Label() { Content = "شناسه ملی : " };
        TextBox txtnationalcode = new TextBox() { FlowDirection = FlowDirection.LeftToRight };
        Label lbltell = new Label() { Content = "تلفن تماس : " };
        TextBox txttell = new TextBox() { FlowDirection = FlowDirection.LeftToRight };
        Label lblgender = new Label() { Content = "ماهیت : " };
        ComboBox cmbgender = new ComboBox()
        {
            ItemsSource = new string[] { "نامشخص", "مرد", "زن", "کسب و کار" }
        };
        Label lbladdress = new Label() { Content = "آدرس : " };
        TextBox txtaddress = new TextBox() { AcceptsReturn = true, MinHeight = 50, TextWrapping = TextWrapping.Wrap };
        Button btnsave = new Button()
        {
            Content = "ثبت اطلاعات",
            Padding = new Thickness(5),
            HorizontalAlignment = HorizontalAlignment.Center,
            MinWidth = 200
        };
        client client;
        public body(long userid)
        {
            designing();
            this.useridf = userid;
            client = new client(userid);
            btnsave.Click += Btnsave_Click;
            client.notifyadd(Dna.e_chromosome.profile, userid, reset);
        }
        public override void close()
        {
            client.notifyremove(reset);
        }
        private void reset(long obj)
        {
            run(load);
            Console.Beep();
        }
        private void designing()
        {
            panel.Children.Add(lblfullname);
            panel.Children.Add(txtfullname);
            panel.Children.Add(lblnationalcode);
            panel.Children.Add(txtnationalcode);
            panel.Children.Add(lbltell);
            panel.Children.Add(txttell);
            panel.Children.Add(lblgender);
            panel.Children.Add(cmbgender);
            panel.Children.Add(lbladdress);
            panel.Children.Add(txtaddress);
            panel.Children.Add(btnsave);
            foreach (FrameworkElement i in panel.Children)
            {
                i.Margin = new Thickness(2);
            }
            btnsave.Margin = new Thickness(10);
        }
        async void Btnsave_Click(object sender, RoutedEventArgs e)
        {
            panel.IsEnabled = false;
            await savefullname();
            await saveprofile();
            panel.IsEnabled = true;
        }

        async Task savefullname()
        {
            if (txtfullname.Text == profile.fullname)
                return;
            if (!valid())
                return;
            var dv = await client.question(new q_renameuser() { fullname = txtfullname.Text }) as q_renameuser.done;
            if (dv.p_duplicate)
                MessageBox.Show("این نام قبلا به ثبت رسیده است. لطفا آن را تغییر دهید");
        }
        async Task saveprofile()
        {
            q_updateprofile q = new q_updateprofile()
            {
                //address = txtaddress.Text,
                gender = (e_nature)cmbgender.SelectedIndex,
                nationalcode = txtnationalcode.Text,
                tell = txttell.Text
            };
            if (!valid())
            {
                panel.IsEnabled = true;
                return;
            }
            var rsv = await client.question(q);
        }
        bool valid()
        {
            string text = txtfullname.Text;
            if (text.Length < 5)
            {
                MessageBox.Show("طول نام انتخاب شده کمتر از حد مجاز است");
                return false;
            }
            if (text.Length > 25)
            {
                MessageBox.Show("طول نام انتخاب شده بیشتر از حد مجاز است");
                return false;
            }
            if (text.Contains("  "))
            {
                MessageBox.Show("میان کلمات تنها از یک فاصله استفاده کنید");
                return false;
            }
            if (text.First() == ' ' || text.Last() == ' ')
            {
                MessageBox.Show("در ابتدا یا انتهای نام فاصله نگذارید");
                return false;
            }
            foreach (var i in text)
            {
                if (i == ' ')
                    continue;
                if (!char.IsLetter(i))
                {
                    MessageBox.Show("در نام از کارکتر غیر مجاز استفاده شده است");
                    return false;
                }
            }
            return true;
        }
        s_profile profile = null;
        long useridf = default;
        public override long userid => useridf;
        public override FrameworkElement element => panel;
        async void load()
        {
            panel.IsEnabled = false;
            var dv = await client.question(new q_loadprofile() { id = userid }) as q_loadprofile.done;
            profile = dv.profile;
            txtaddress.Text = profile.location?.text;
            txtfullname.Text = profile.fullname;
            txtnationalcode.Text = profile.nationalcode;
            cmbgender.SelectedIndex = (int)profile.nature;
            txttell.Text = profile.tell;
            panel.IsEnabled = true;
        }
    }
}