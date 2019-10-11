using Connection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace user
{
    class body
    {
        public StackPanel panel = new StackPanel() { Width = 400 };
        ListBox lstaccounts = new ListBox() { MinHeight = 100 };
        TextBlock lbl = new TextBlock()
        {
            Padding = new Thickness(5),
            Margin = new Thickness(5),
            FlowDirection = FlowDirection.RightToLeft
        };
        WatermarkTextBox txt = new WatermarkTextBox();
        StackPanel btnpanel = new StackPanel()
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Center,
            Margin = new Thickness(10),
            Height = 40
        };
        Button btnback = new Button()
        {
            Content = "اصلاح شماره تلفن",
            Padding = new Thickness(5),
            MinWidth = 120,
            Margin = new Thickness(0, 0, 5, 0)
        };
        Button btnsubmit = new Button()
        {
            Content = "ارسال",
            Padding = new Thickness(5),
            MinWidth = 120
        };
        ObservableCollection<userinfo> userinfos;
        public body()
        {
            panel.Margin = new Thickness(10);
            panel.Children.Add(lstaccounts);
            lbl.Inlines.Add(subject);
            lbl.Inlines.Add(describtion);
            panel.Children.Add(lbl);
            panel.Children.Add(txt);
            btnpanel.Children.Add(btnback);
            btnpanel.Children.Add(btnsubmit);
            panel.Children.Add(btnpanel);
            txt.PreviewTextInput += Txtphone_PreviewTextInput;
            txt.TextChanged += Txt_TextChanged;
            txt.KeyDown += Txt_KeyDown;
            btnsubmit.Click += Btnsubmit_Click;
            resetsource();
            basic.user_e += Basic_user_e;
            reset();
            lstaccounts.SelectionChanged += Lstaccounts_SelectionChanged;
            btnback.Click += Btnback_Click;
        }

        private void resetsource()
        {
            userinfos = new ObservableCollection<userinfo>(basic.alluser());
            lstaccounts.ItemsSource = userinfos;
        }
        private void Btnback_Click(object sender, RoutedEventArgs e)
        {
            var dv = phone;
            phone = null;
            txt.Text = dv;
            txt.CaretIndex = txt.Text.Length;
        }
        userinfo selected => lstaccounts.SelectedValue as userinfo;
        private void Lstaccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selected == null)
                return;
            txt.Text = selected.fullname;
        }
        private void Basic_user_e((userinfo user, bool login) obj)
        {
            if (obj.login)
                userinfos.Add(obj.user);
            else
                userinfos.Remove(obj.user);
            reset();
        }
        async void Btnsubmit_Click(object sender, RoutedEventArgs e)
        {
            switch (state)
            {
                case e_state.submitphone:
                    {
                        phone = txt.Text;
                        txt.Text = null;
                        await basic.sendactivecode(phone);
                    }
                    break;
                case e_state.submitcode:
                    {
                        var dv = await basic.login(phone, txt.Text);
                        if (dv)
                        {
                            txt.Text = phone;
                            phone = null;
                            txt.CaretIndex = txt.Text.Length;
                        }
                        else
                        {
                            Xceed.Wpf.Toolkit.MessageBox.Show("کد فعال سازی به درستی وارد نشده است.");
                        }
                    }
                    break;
                case e_state.logout:
                    {
                        await basic.logout(selected.id);
                    }
                    break;
            }
            reset();
        }
        private void Txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Btnsubmit_Click(null, null);
        }
        private void Txtphone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (phone == null)
            {
                long val;
                if (!long.TryParse(e.Text, out val))
                {
                    e.Handled = true;
                    SystemSounds.Asterisk.Play();
                }
                if (txt.Text.Length >= 11)
                    e.Handled = true;
            }
            else
            {
                long val;
                if (!long.TryParse(e.Text, out val))
                {
                    e.Handled = true;
                    SystemSounds.Asterisk.Play();
                }
                if (txt.Text.Length >= 5)
                    e.Handled = true;
            }
        }

        string phone = default;
        void reset()
        {
            subject.Text = describtion.Text = null;
            string dvphone = txt.Text;
            var dv = userinfos.FirstOrDefault(i => i.fullname == dvphone);
            if (dv == null)
            {
                lstaccounts.SelectedItem = null;
                if (phone == null)
                    phonepro();
                else
                    activecodepro();
            }
            else
            {
                subject.Text = "حساب کاربری فعال";
                lstaccounts.SelectedItem = dv;
                btnsubmit.Content = "خروج از حساب";
                state = e_state.logout;
                btnsubmit.IsEnabled = true;
            }
        }
        private void nicknamepro()
        {
            subject.Text = "نام دلخواه : ";
            describtion.Text = "لطفا یک نام دلخواه برای خود انتخاب کنید.";
            btnback.IsEnabled = false;
            btnback.Content = null;
            if (txt.Text.Length >= 5 && txt.Text.Length <= 20)
            {
                btnsubmit.IsEnabled = true;
                btnsubmit.Content = "ارسال";
            }
            else
            {
                btnsubmit.IsEnabled = false;
                btnsubmit.Content = null;
                state = e_state.none;
            }
        }
        Run subject = new Run() { FontWeight = FontWeights.Bold };
        Run describtion = new Run() { FontWeight = FontWeights.Normal };
        private void activecodepro()
        {
            subject.Text = "کد فعال سازی : ";
            describtion.Text = "این کد تا دقایقی دیگر بر روی تلفن همراه شما پیامک خواهد شد.";
            btnback.IsEnabled = true;
            btnback.Content = "اصلاح شماره تلفن";
            txt.Focus();
            if (txt.Text.Length == 5)
            {
                btnsubmit.IsEnabled = true;
                btnsubmit.Content = "ورود";
                state = e_state.submitcode;
            }
            else
            {
                btnsubmit.IsEnabled = true;
                btnsubmit.Content = null;
                state = e_state.none;
            }
        }
        enum e_state
        {
            submitphone,
            submitcode,
            logout,
            none
        }
        e_state state = e_state.none;
        private void phonepro()
        {
            subject.Text = "شماره تلفن همراه";
            btnback.IsEnabled = false;
            btnback.Content = null;
            txt.Focus();
            if (txt.Text.Length == 11)
            {
                btnsubmit.Content = "ارسال پیامک";
                btnsubmit.IsEnabled = true;
                state = e_state.submitphone;
            }
            else
            {
                btnsubmit.Content = null;
                btnsubmit.IsEnabled = false;
                state = e_state.none;
            }
        }
        private void Txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            reset();
        }
    }
}