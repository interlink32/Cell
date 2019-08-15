using Connection;
using Dna;
using Dna.test;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client_test
{
    public partial class Form1 : Form
    {
        client client = new client();
        public Form1()
        {
            InitializeComponent();
            client.notify_e += Client_notify_e;
            m();
        }
        async void m()
        {
            var dv = await client.question(new a_value() { value = 10 });
            m();
        }

        private void Client_notify_e(string obj)
        {

        }
    }
}
