using Dna.contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using Xceed.Wpf.Toolkit;

namespace contact
{
    class partnersetting : DataGridTextColumn
    {
        StackPanel pannel = new StackPanel();
        public WatermarkComboBox filter = new WatermarkComboBox() { Watermark = "Search" };
        Label label = new Label() { Content = "Partner Setting" };
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
        public e_connectionsetting connectionsetting
        {
            get
            {
                var dv = filter.SelectedValue as r_connectionsetting;
                return dv.setting;
            }
        }
    }
}