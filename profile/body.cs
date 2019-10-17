using Connection;
using Dna.profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace profile
{
    class body
    {
        public StackPanel panel = new StackPanel()
        {
            Margin = new Thickness(20, 0, 20, 20),
            FlowDirection = FlowDirection.RightToLeft
        };
        Label lblfullname = new Label() { Content = "نام و نام خانوادگی : " };
        TextBox txtfullname = new TextBox();
        Label lblnationalcode = new Label() { Content = "کدملی : " };
        TextBox txtnationalcode = new TextBox() { FlowDirection = FlowDirection.LeftToRight };
        Label lbltell = new Label() { Content = "تلفن تماس : " };
        TextBox txttell = new TextBox() { FlowDirection = FlowDirection.LeftToRight };
        Label lblgender = new Label() { Content = "جنسیت : " };
        ComboBox cmbgender = new ComboBox()
        {
            ItemsSource = new string[] { "نامشخص", "مرد", "زن" }
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
        public readonly long userid;

        public body(long userid)
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

            this.userid = userid;
            client = new client(userid);
            run(load);
            btnsave.Click += Btnsave_Click;
            client.add<n_update>(userid, load);
        }

        async void Btnsave_Click(object sender, RoutedEventArgs e)
        {
            panel.IsEnabled = false;
            q_upsert q = new q_upsert()
            {
                address = txtaddress.Text,
                fullname = txtfullname.Text,
                gender = (e_gender)cmbgender.SelectedIndex,
                nationalcode = txtnationalcode.Text,
                tell = txttell.Text
            };
            if (!valid())
            {
                panel.IsEnabled = true;
                return;
            }
            var rsv = await client.question(q);
            switch (rsv)
            {
                case q_upsert.done done:
                    {

                    }
                    break;
                case q_upsert.duplicatename duplicatename:
                    {
                        MessageBox.Show("این نام قبلا به ثبت رسیده است. لطفا یک نام دیگر انتخاب کنید");
                    }
                    break;
            }
            panel.IsEnabled = true;
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
        void run(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }
        void load(n_update update)
        {
            run(load);
        }
        async void load()
        {
            panel.IsEnabled = false;
            var dv = await client.question(new q_load() { userid = userid }) as q_load.done;
            var pro = dv.profile;
            txtaddress.Text = pro.address;
            txtfullname.Text = pro.fullname;
            txtnationalcode.Text = pro.nationalcode;
            cmbgender.SelectedIndex = (int)pro.gender;
            txttell.Text = pro.tell;
            panel.IsEnabled = true;
        }
    }
}