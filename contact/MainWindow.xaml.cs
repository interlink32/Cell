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

namespace contact
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StackPanel panel = new StackPanel();
        ComboBox cmb_user = new ComboBox();
        public MainWindow()
        {
            InitializeComponent();
            Content = panel;
            panel.Children.Add(new Label()
            {
                Content = "Selected account :"
            });
            panel.Children.Add(cmb_user);
            panel.Children.Add(new Label()
            {
                Content = "Conatacts :",
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                Padding = new Thickness(10)
            });
            client.user_e += Client_user_e;
            cmb_user.ItemsSource = accounts;
            cmb_user.SelectionChanged += Cmb_user_SelectionChanged;
            cmb_user.SelectedItem = "javad";
        }
        List<manager> managers = new List<manager>();
        manager manager = null;
        private void Cmb_user_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dv = managers.FirstOrDefault(i => i.account == cmb_user.SelectedItem.ToString());
            if (dv == null)
            {
                dv = new manager(cmb_user.SelectedItem.ToString());
                managers.Add(dv);
            }
            if (manager != null)
                panel.Children.Remove(manager.grid);
            manager = dv;
            panel.Children.Add(manager.grid);
        }

        ObservableCollection<string> accounts = new ObservableCollection<string>(client.alluser());
        private void Client_user_e((string user, bool login) obj)
        {
            if (obj.login)
                accounts.Add(obj.user);
            else
                accounts.Remove(obj.user);
        }
    }
}