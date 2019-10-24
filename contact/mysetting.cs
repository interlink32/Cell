using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Dna.usercontact;

namespace contact
{
    class mysetting : DataGridComboBoxColumn
    {
        StackPanel heder = new StackPanel();
        public ComboBox filter = new ComboBox() ;
        Label label = new Label() { Content = "تنظیمات من" };
        public mysetting()
        {
            filter.ItemsSource = r_connectionsetting.list;
            filter.SelectedIndex = 0;
            ItemsSource = r_connectionsetting.list.Where(i => i.setting != e_contactsetting.any).ToArray();
            heder.Children.Add(filter);
            heder.Children.Add(label);
            heder.MinWidth = 120;
            Header = heder;
            SelectedItemBinding = new Binding(nameof(row.mysetting));
        }
        public e_contactsetting contactsetting
        {
            get
            {
                var dv = filter.SelectedValue as r_connectionsetting;
                return dv.setting;
            }
        }
    }
}