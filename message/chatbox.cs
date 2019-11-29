using Connection;
using controllibrary;
using Dna.message;
using localdb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace message
{
    class chatbox : loadbox
    {
        DockPanel panel = new DockPanel();
        ListBox body = new ListBox() { BorderThickness = new Thickness(), Height = 500 };
        Border border = new Border()
        {
            Padding = new Thickness(4),
            BorderThickness = new Thickness(2),
            CornerRadius = new CornerRadius(10),
            BorderBrush = Brushes.Gray,
            Margin = new Thickness(4)
        };
        TextBox box = new TextBox() { Padding = new Thickness(2), BorderThickness = new Thickness() };
        public override FrameworkElement element => panel;
        public readonly long userid;
        public readonly long partnerid;
        client client;
        syncdb<s_message,messageui> dbend;
        private ScrollViewer scrollViewer;
        public chatbox(long userid, long partnerid)
        {
            this.userid = userid;
            this.partnerid = partnerid;
            client = new client(userid);
            dbend = new syncdb<s_message, messageui>(userid);
            panel.Children.Add(body);
            DockPanel.SetDock(body, Dock.Top);
            border.Child = box;
            panel.Children.Add(border);
            DockPanel.SetDock(border, Dock.Bottom);
            box.KeyDown += Box_KeyDown;
            body.ItemsSource = dbend.list;
            dbend.list.reset_e += List_reset_e;
            dbend.search(i => i.partner == partnerid);
            setscroll();
        }
        private void List_reset_e(ocollection<messageui> obj)
        {
            if (scrollViewer != null)
                scrollViewer.ScrollToBottom();
        }
        async void setscroll()
        {
            try
            {
                var border = (Border)VisualTreeHelper.GetChild(body, 0);
                scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);
                scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
                scrollViewer.ScrollToBottom();
            }
            catch
            {
                await Task.Delay(500);
                setscroll();
            }
        }
        async void Box_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter || box.Text == "")
                return;
            waiting();
            await client.question(new q_upsertmessage() { partner = partnerid, text = box.Text });
            box.Text = null;
            release();
        }
    }
}