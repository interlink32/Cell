using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace controllibrary
{
    public class combocolumn : DataGridTextColumn
    {
        StackPanel pannel = new StackPanel();
        public ComboBox filter = new ComboBox();
        Label label = new Label();
        public combocolumn(string heder, string binding, string[] list)
        {
            label.Content = heder;
            filter.ItemsSource = list;
            filter.SelectedIndex = 0;
            pannel.Children.Add(filter);
            pannel.Children.Add(label);
            Header = pannel;
            pannel.MinWidth = 120;
            Binding = new Binding(binding);
        }
        public int connectionsetting
        {
            get
            {
                return filter.SelectedIndex;
            }
        }
    }
}