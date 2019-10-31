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
    public class panel : uibase
    {
        StackPanel stack = new StackPanel();
        userselector userselector;
        loadbox loadbox = new loadbox();
        public override FrameworkElement element => stack;
        public panel(string text, Func<long, uiapp> appcreator)
        {
            userselector = new userselector(text);
            userselector.user_e += Userselector_user_e;
            alluser.addremove_e += Alluser_remove_e;
            this.appcreator = appcreator;
            stack.Children.Add(userselector.element);
            stack.Children.Add(loadbox.element);
        }
        private void Alluser_remove_e(bool state, long obj)
        {
            if (!state)
            {
                var dv = list.FirstOrDefault(i => i.userid == obj);
                dv.close();
                list.Remove(dv);
            }
        }
        List<uiapp> list = new List<uiapp>();
        private readonly Func<long, uiapp> appcreator;

        private void Userselector_user_e(long obj)
        {
            loadbox.content = null;
            if (obj == 0)
                return;
            var uiapp = list.FirstOrDefault(i => i.userid == obj);
            if (uiapp == null)
            {
                uiapp = appcreator(obj);
                list.Add(uiapp);
            }
            loadbox.content = uiapp.element;
        }
    }
}