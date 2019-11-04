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
    public class panel<T> : uibase where T : uiapp, new()
    {
        StackPanel stack = new StackPanel();
        userselector userselector;
        loadbox loadbox = new loadbox();
        public override FrameworkElement element => stack;
        public panel()
        {
            userselector = new userselector();
            userselector.user_e += Userselector_user_e;
            alluser.addremove_e += Alluser_remove_e;
            stack.Children.Add(userselector.element);
            stack.Children.Add(loadbox.element);
        }
        public void title(string text)
        {
            userselector.titel(text);
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
        List<T> list = new List<T>();
        private void Userselector_user_e(long obj)
        {
            loadbox.content = null;
            if (obj == 0)
                return;
            var uiapp = list.FirstOrDefault(i => i.userid == obj);
            if (uiapp == null)
            {
                uiapp = new T();
                list.Add(uiapp);
            }
            loadbox.content = uiapp.element;
        }
    }
}