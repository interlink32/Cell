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

namespace profile
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StackPanel panel = new StackPanel();
        userselector userselector = new userselector();
        List<body> bodies = new List<body>();
        public MainWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
            SizeToContent = SizeToContent.WidthAndHeight;
            Content = panel;
            panel.Children.Add(userselector.element);
            userselector.user_e += Userselector_user_e;
        }
        body body = null;
        private void Userselector_user_e(long obj)
        {
            if (body != null)
            {
                panel.Children.Remove(body.panel);
                body = null;
            }
            if (obj != 0)
            {
                body = bodies.FirstOrDefault(i => i.userid == obj);
                if (body == null)
                {
                    body = new body(obj);
                    bodies.Add(body);
                }
                panel.Children.Add(body.panel);
            }
        }
        void run(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }
    }
}