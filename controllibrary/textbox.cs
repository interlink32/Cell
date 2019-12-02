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
    public class textbox : TextBox
    {
        public textbox()
        {
            Padding = new Thickness(6);
        }
    }
    public class textboxborder : uibase
    {
        public Border border = new Border();
        public textbox textbox = new textbox();
        public textboxborder()
        {
            border.Child = textbox;
            border.CornerRadius = new CornerRadius(10);
            border.Padding = new Thickness(4);
            border.BorderThickness = new Thickness(2);
            border.Margin = new Thickness(4);
            textbox.BorderThickness = new Thickness();
            border.BorderBrush = Brushes.Gray;
        }
        public override FrameworkElement element => border;
    }
    public class combo : ComboBox
    {
        public combo()
        {
            Padding = new Thickness(6);
        }
    }
}