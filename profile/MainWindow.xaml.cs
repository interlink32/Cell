using Connection;
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
        ComboBox cmbuser = new ComboBox();
        List<body> bodies = new List<body>();
        public MainWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
            SizeToContent = SizeToContent.WidthAndHeight;
            Content = panel;
            panel.Children.Add(cmbuser);
            cmbuser.SelectionChanged += ComboBox_SelectionChanged;
            autoselect();
            alluser.reset_e += Alluser_reset_e;
            cmbuser.ItemsSource = alluser.list;
        }
        private void Alluser_reset_e()
        {
            run(delegate ()
            {
                var dv = selected;
                cmbuser.SelectedItem = null;
                cmbuser.SelectedItem = dv;
            });
        }
        void run(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }
        private void autoselect()
        {
            if (body == null)
                cmbuser.SelectedItem = alluser.list.FirstOrDefault();
        }

        body body = null;
        userinfo selected => cmbuser.SelectedValue as userinfo;
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (body != null)
            {
                panel.Children.Remove(body.panel);
                body = null;
            }
            if (selected != null)
            {
                var dv = bodies.FirstOrDefault(i => i.userid == selected.id);
                if (dv == null)
                {
                    dv = new body(selected.id);
                    bodies.Add(dv);
                }
                body = dv;
                panel.Children.Add(dv.panel);
            }
        }
    }
}