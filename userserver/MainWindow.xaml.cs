using Dna.user;
using servercell;
using stemcell;
using System;
using System.Collections.Generic;
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

namespace userserver
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        server server = new server();
        public MainWindow()
        {
            InitializeComponent();
            Height = 200;
            Width = 300;
            WindowState = WindowState.Minimized;
            Title = "userserver";
        }
    }
}