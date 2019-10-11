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
using Xceed.Wpf.Toolkit;

namespace profile
{
    class body
    {
        public StackPanel panel = new StackPanel()
        {
            Width = 400,
            Margin = new Thickness(20, 20, 20, 40),
            FlowDirection = FlowDirection.RightToLeft
        };
        TextBlock txtblock = new TextBlock() { Padding = new Thickness(10) };
        Label lblfullname = new Label() { Content = "نام و نام خانوادگی : " };
        WatermarkTextBox txtfullname = new WatermarkTextBox();
        Label lblnationalcode = new Label() { Content = "کدملی : " };
        WatermarkTextBox txtnationalcode = new WatermarkTextBox() { FlowDirection = FlowDirection.LeftToRight };
        Label lbltell = new Label() { Content = "تلفن تماس : " };
        WatermarkTextBox txttell = new WatermarkTextBox() { FlowDirection = FlowDirection.LeftToRight };
        Label lblgender = new Label() { Content = "جنسیت : " };
        WatermarkComboBox cmbgender = new WatermarkComboBox()
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
            panel.Children.Add(txtblock);
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
            await client.question(q);
            panel.IsEnabled = true;
        }
        void run(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
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