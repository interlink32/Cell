using Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace controllibrary
{
    public class userselector : uibase
    {
        public event Action<long> user_e;
        StackPanel panel = new StackPanel()
        {
            Orientation = Orientation.Horizontal,
            FlowDirection = FlowDirection.RightToLeft,
            Margin = new Thickness(20, 0, 20, 0)
        };
        public override FrameworkElement element => panel;
        Label lable = new Label();
        ComboBox combo = new ComboBox() { MinWidth = 250 };
        public userselector()
        {
            combo.ItemsSource = alluser.list;
            alluser.reset_e += Alluser_reset_e;
            combo.SelectionChanged += Combo_SelectionChanged;
            panel.Children.Add(lable);
            panel.Children.Add(combo);
            lable.Content = "انتخاب کاربر : ";
            autoselect();
            alluser.list.CollectionChanged += List_CollectionChanged;
        }
        private void List_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            autoselect();
        }
        private void autoselect()
        {
            if (selected == null)
                combo.SelectedItem = alluser.list.FirstOrDefault();
        }
        private void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (inp)
                return;
            var dv = selected;
            if (dv == null)
                user_e?.Invoke(0);
            else
                user_e?.Invoke(dv.id);
        }
        bool inp = false;
        private void Alluser_reset_e()
        {
            run(delegate ()
            {
                var dv = selected;
                inp = true;
                combo.ItemsSource = null;
                combo.ItemsSource = alluser.list;
                inp = false;
                combo.SelectedItem = dv;
            });
        }
        public userinfo selected => combo.SelectedValue as userinfo;
    }
}