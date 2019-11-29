using controllibrary;
using Dna;
using Dna.userdata;
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
    class member : Border, Is_entity
    {
        public event Action<member> click_e;
        public member()
        {
            MinWidth = 120;
            MouseDown += Body_PreviewMouseDown;
        }
        private void Body_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            click_e?.Invoke(this);
        }
        public void open(bool val)
        {
            if (val)
                BorderThickness = new Thickness(2, 2, 0, 2);
            else
                BorderThickness = new Thickness(2);
        }

        TextBlock body = new TextBlock() { HorizontalAlignment = HorizontalAlignment.Center };
        public s_fulluser fulluser = new s_fulluser();
        public long id { get => fulluser.id; set => fulluser.id = value; }
        public void update(long owner, s_entity entity)
        {
            Child = body;
            Padding = new Thickness(10);
            Margin = new Thickness(1);
            BorderThickness = new Thickness(2);

            fulluser.update(owner, entity);
            body.Inlines.Clear();

            switch (fulluser.contact.ownersetting)
            {
                case e_contactsetting.favorite:
                    {
                        BorderBrush = Brushes.Blue;
                        Background = Brushes.LightBlue;
                    }
                    break;
                case e_contactsetting.connect:
                    {
                        BorderBrush = Brushes.Black;
                        Background = Brushes.LightGray;
                    }
                    break;
                case e_contactsetting.disconnect:
                    {
                        BorderBrush = Brushes.Red;
                        Background = Brushes.LightPink;
                    }
                    break;
            }

            switch (fulluser.contact.partnersetting)
            {
                case e_contactsetting.favorite:
                    {
                        body.Inlines.Add(new Run() { Text = "+ ", Foreground = Brushes.Blue });
                        body.Inlines.Add(new Run() { Text = fulluser.user.fullname, Foreground = Brushes.Black });
                    }
                    break;
                case e_contactsetting.connect:
                    {
                        body.Inlines.Add(new Run() { Text = fulluser.user.fullname, Foreground = Brushes.Black });
                    }
                    break;
                case e_contactsetting.disconnect:
                    {
                        body.Inlines.Add(new Run() { Text = "! ", Foreground = Brushes.Black });
                        body.Inlines.Add(new Run() { Text = fulluser.user.fullname, Foreground = Brushes.Black });
                    }
                    break;
                case e_contactsetting.blocked:
                    {
                        body.Inlines.Add(new Run() { Text = "! ", Foreground = Brushes.Red });
                        body.Inlines.Add(new Run() { Text = fulluser.user.fullname, Foreground = Brushes.Black });
                    }
                    break;
            }
        }
    }
}