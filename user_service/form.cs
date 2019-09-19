using Connection;
using Dna.user;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace user_service
{
    public partial class form : Form
    {
        server service = new server();
        public form()
        {
            InitializeComponent();
            report();
        }
        async void report()
        {
            string dv = "";
            dv += "Counter: " + Connection.report.cunter;
            lbl.Text = dv;
            await Task.Delay(100);
            report();
        }
    }
}