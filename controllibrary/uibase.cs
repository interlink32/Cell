using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace controllibrary
{
    public abstract class uibase
    {
        public abstract FrameworkElement element { get; }
        public static void run(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }
    }
}