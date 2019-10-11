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
        ComboBox comboBox = new ComboBox();
        List<body> bodies = new List<body>();
        ObservableCollection<userinfo> userinfos;
        public MainWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
            SizeToContent = SizeToContent.WidthAndHeight;
            Content = panel;
            panel.Children.Add(comboBox);
            comboBox.SelectionChanged += ComboBox_SelectionChanged;
            userinfos = new ObservableCollection<userinfo>(basic.alluser());
            autoselect();
            comboBox.ItemsSource = userinfos;
            basic.user_e += Basic_user_e;
        }

        private void autoselect()
        {
            if (body == null)
                comboBox.SelectedItem = userinfos.FirstOrDefault();
        }

        private void Basic_user_e((userinfo user, bool login) obj)
        {
            if (obj.login)
                userinfos.Add(obj.user);
            else
            {
                userinfos.Remove(userinfos.First(i => i.id == obj.user.id));
            }
            autoselect();
        }

        body body = null;
        userinfo selected => comboBox.SelectedValue as userinfo;
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