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
        //client_pool client_Pool = new client_pool();
        public form()
        {
            InitializeComponent();
            ThreadPool.QueueUserWorkItem(start);
        }
        client client = new client("a1");
        async void start(object o)
        {
            await Task.Delay(500);
            Invoke(new Action(report));
            client.userid_password_e += Client_userid_password_e;
            await client.login();
             m();
        }
        Random random = new Random();
        private async void m()
        {
            var p = new q_sum()
            {
                a = random.Next(),
                b = random.Next()
            };
            var dv = await client.question(p);
            if ((dv as q_sum.done).result != p.a + p.b)
                throw new Exception("");
            m();
        }
        private async Task<(string userid, string password)> Client_userid_password_e()
        {
            await Task.CompletedTask;
            return ("1", "1pass");
        }

        async void report()
        {
            string dv = "";
            dv += "Counter: " + Connection.report.cunter;
            lbl.Text = dv;
            await Task.Delay(100);
            Invoke(new Action(report));
        }
    }
}