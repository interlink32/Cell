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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client_test
{
    public partial class form : Form
    {
        client_pool client_Pool = new client_pool();
        public form()
        {
            InitializeComponent();
            start();
        }
        async void start()
        {
            await Task.Delay(1000);
            ThreadPool.QueueUserWorkItem(client_Pool.start);
            Invoke(new Action(report));
        }
        async void report()
        {
            string dv = "";
            dv += "Counter: " + Connection.report.counter + "\r\n";
            dv += "Avrage: " + Connection.report.avrage + "\r\n";
            dv += "Max: " + Connection.report.max + "\r\n";
            dv += "Min: " + Connection.report.min + "\r\n";
            lbl.Text = dv;
            await Task.Delay(100);
            Invoke(new Action(report));
        }
    }
}