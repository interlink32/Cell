using Connection;
using controllibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace contact
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StackPanel panel = new StackPanel();
        userselector userselector = new userselector("مخاطبان");
        public MainWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
            SizeToContent = SizeToContent.WidthAndHeight;
            Content = panel;
            panel.Children.Add(userselector.element);
            userselector.user_e += Userselector_user_e;
        }
        List<body> list = new List<body>();
        body body = null;
        private void Userselector_user_e(long obj)
        {
            if (body != null)
            {
                panel.Children.Remove(this.body.element);
                body = null;
            }
            body = list.FirstOrDefault(i => i.user == obj);
            if (body == null)
            {
                body = new body(obj);
                list.Add(body);
            }
            panel.Children.Add(body.element);
        }
    }
}