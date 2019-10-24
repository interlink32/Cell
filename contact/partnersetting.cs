using Dna.usercontact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace contact
{
    class partnersetting : DataGridTextColumn
    {
        StackPanel pannel = new StackPanel();
        public ComboBox filter = new ComboBox();
        Label label = new Label() { Content = "تنظیمات مخاطبان" };
        public partnersetting()
        {
            filter.ItemsSource = r_connectionsetting.list;
            filter.SelectedIndex = 0;
            pannel.Children.Add(filter);
            pannel.Children.Add(label);
            Header = pannel;
            pannel.MinWidth = 120;
            Binding = new Binding(nameof(row.partnersettin));
        }
        public e_contactsetting connectionsetting
        {
            get
            {
                var dv = filter.SelectedValue as r_connectionsetting;
                return dv.setting;
            }
        }
    }
}