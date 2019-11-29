using Connection;
using controllibrary;
using Dna.user;
using Dna.userdata;
using localdb;
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
    class profilebody : uiapp
    {
        StackPanel panel = new StackPanel()
        {
            Margin = new Thickness(20, 0, 20, 20),
            FlowDirection = FlowDirection.RightToLeft
        };
        Label lblfullname = new Label() { Content = "نام کامل : " };
        TextBox txtfullname = new TextBox();

        Label lbldescribtion = new Label() { Content = "توضیحات" };
        textbox txtdescribtion = new textbox();

        Label lblcity = new Label() { Content = "شهر : " };
        TextBox txtcity = new TextBox() { MaxLength = 20 };

        Label lblnationalcode = new Label() { Content = "شناسه ملی : " };
        TextBox txtnationalcode = new TextBox() { FlowDirection = FlowDirection.LeftToRight };

        Label lbltell = new Label() { Content = "تلفن تماس : " };
        TextBox txttell = new TextBox() { FlowDirection = FlowDirection.LeftToRight };

        Label lblgender = new Label() { Content = "ماهیت : " };
        ComboBox cmbgender = new ComboBox()
        {
            ItemsSource = new string[] { "نامشخص", "مرد", "زن", "کسب و کار" }
        };

        Button btnsave = new Button()
        {
            Content = "ثبت اطلاعات",
            Padding = new Thickness(5),
            HorizontalAlignment = HorizontalAlignment.Center,
            MinWidth = 200
        };
        client client;
        long userid = default;
        syncdb<s_fulluser> syncdb;
        public override void create(long userid)
        {
            this.userid = userid;
            syncdb = new syncdb<s_fulluser>(userid);
            syncdb.notify(userid, reset);
            designing();
            client = new client(userid);
            btnsave.Click += Btnsave_Click;
        }

        s_user user = default;
        private void reset(s_fulluser obj)
        {
            user = obj.user;
            txtcity.Text = user.city;
            txtdescribtion.Text = user.description;
            txtfullname.Text = user.fullname;
            txtnationalcode.Text = user.nationalcode;
            txttell.Text = user.tell;
            cmbgender.SelectedIndex = (int)user.nature;
        }
        private void designing()
        {
            panel.Children.Add(lblfullname);
            panel.Children.Add(txtfullname);
            panel.Children.Add(lbldescribtion);
            panel.Children.Add(txtdescribtion);
            panel.Children.Add(lblcity);
            panel.Children.Add(txtcity);
            panel.Children.Add(lbltell);
            panel.Children.Add(txttell);
            panel.Children.Add(lblnationalcode);
            panel.Children.Add(txtnationalcode);
            panel.Children.Add(lblgender);
            panel.Children.Add(cmbgender);
            panel.Children.Add(btnsave);
            foreach (FrameworkElement i in panel.Children)
            {
                i.Margin = new Thickness(2);
            }
            btnsave.Margin = new Thickness(10);
        }
        async void Btnsave_Click(object sender, RoutedEventArgs e)
        {
            user.city = txtcity.Text;
            user.description = txtdescribtion.Text;
            user.fullname = txtfullname.Text;
            user.nationalcode = txtnationalcode.Text;
            user.nature = (e_nature)cmbgender.SelectedIndex;
            user.tell = txttell.Text;
            await save();
        }

        async Task save()
        {
            loadbox.mainwaiting();
            if (!await valid())
                return;
            var rsv = await client.question(new q_upsertuser() { user = user }) as q_upsertuser.done;
            if (rsv.error_duplicate)
                await messagebox.maindilog((null, "این نام قبلا به ثبت رسیده است. لطفا آن را تغییر دهید"));
            loadbox.mainrelease();
        }
        async Task<bool> valid()
        {
            string text = txtfullname.Text;
            if (text.Length < 5)
            {
                await messagebox.maindilog("طول نام انتخاب شده کمتر از حد مجاز است");
                return false;
            }
            if (text.Length > 25)
            {
                await messagebox.maindilog("طول نام انتخاب شده بیشتر از حد مجاز است");
                return false;
            }
            if (text.Contains("  "))
            {
                await messagebox.maindilog("میان کلمات تنها از یک فاصله استفاده کنید");
                return false;
            }
            if (text.First() == ' ' || text.Last() == ' ')
            {
                await messagebox.maindilog("در ابتدا یا انتهای نام فاصله نگذارید");
                return false;
            }
            foreach (var i in text)
            {
                if (i == ' ')
                    continue;
                if (!char.IsLetter(i))
                {
                    await messagebox.maindilog("در نام از کارکتر غیر مجاز استفاده شده است");
                    return false;
                }
            }
            return true;
        }
        public override FrameworkElement element => panel;
    }
}