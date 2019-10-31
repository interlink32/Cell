using Connection;
using Dna;
using Dna.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace contact
{
    class usercolumn : DataGridTextColumn
    {
        StackPanel stack = new StackPanel() { MinWidth = 200 };
        public TextBox filter = new TextBox() { Padding = new Thickness(4, 2, 4, 2) };
        Label label = new Label() { Content = "کاربران" };
        public usercolumn()
        {
            stack.Children.Add(filter);
            stack.Children.Add(label);
            Header = stack;
            Binding = new Binding(nameof(row.partner));
        }
    }
}