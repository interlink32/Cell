using Connection;
using Dna.user;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        int n = 3000;
        async void Button_Click(object sender, RoutedEventArgs e)
        {
            byte[] data = new byte[int.Parse(textbox.Text)];
            basic.random.NextBytes(data);
            var dv = await client.question(client.getalluser()[0], new q_test()
            {
                input = data
            }) as q_test.done;
            if (data.SequenceEqual(dv.output))
                Console.Beep(500, 200);
            else
            {

            }
        }
    }
}