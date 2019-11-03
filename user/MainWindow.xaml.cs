using Connection;
using localdb;
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

namespace user
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        body body;
        public MainWindow()
        {
            InitializeComponent();
            alluser.addremove_e += Alluser_addremove_e;
            body = new body();
            Content = body.panel;
            ResizeMode = ResizeMode.NoResize;
            SizeToContent = SizeToContent.WidthAndHeight;
        }
        List<dbend> dbs = new List<dbend>();
        private void Alluser_addremove_e(bool arg1, long arg2)
        {
            if (arg1)
            {
                dbendwriteruser dv = new dbendwriteruser(arg2);
                dbs.Add(dv);
            }
            else
            {
                dbs.RemoveAll(i => i.userid == arg2);
            }
        }
    }
}