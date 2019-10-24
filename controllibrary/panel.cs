using Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace controllibrary
{
    public abstract class panel : uibase
    {
        StackPanel stack = new StackPanel();
        userselector userselector;
        public override FrameworkElement element => stack;
        public panel()
        {
            userselector = new userselector(text);
            stack.Children.Add(userselector.element);
            userselector.user_e += Userselector_user_e;
            alluser.remove_e += Alluser_remove_e;
        }
        public abstract string text { get; }
        private void Alluser_remove_e(long obj)
        {
            var dv = list.FirstOrDefault(i => i.userid == obj);
            dv.close();
            list.Remove(dv);
        }
        public abstract uiapp createapp(long userid);
        List<uiapp> list = new List<uiapp>();
        uiapp uiapp = null;
        private void Userselector_user_e(long obj)
        {
            if (uiapp != null)
            {
                stack.Children.Remove(uiapp.element);
                uiapp = null;
            }
            if (obj == 0)
                return;
            uiapp = list.FirstOrDefault(i => i.userid == obj);
            if (uiapp == null)
            {
                uiapp = createapp(obj);
                list.Add(uiapp);
            }
            stack.Children.Add(uiapp.element);
        }
    }
}