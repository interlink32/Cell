using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace controllibrary
{
    public class textbox : TextBox
    {
        public textbox()
        {
            Padding = new Thickness(6);
        }
    }
    public class combo : ComboBox
    {
        public combo()
        {
            Padding = new Thickness(6);
        }
    }
}