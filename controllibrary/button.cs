using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace controllibrary
{
    public class button : Button
    {
        public button()
        {
            Padding = new Thickness(4);
            MinWidth = 120;
        }
    }
}