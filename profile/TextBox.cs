using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace profile
{
    class TextBox : System.Windows.Controls.TextBox
    {
        public TextBox()
        {
            Padding = new Thickness(5);
            VerticalContentAlignment = VerticalAlignment.Center;
        }
    }
    class ComboBox : System.Windows.Controls.ComboBox
    {
        public ComboBox()
        {
            Padding = new Thickness(5);
            VerticalContentAlignment = VerticalAlignment.Center;
        }
    }
}