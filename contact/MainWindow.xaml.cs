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
        }
    }
}