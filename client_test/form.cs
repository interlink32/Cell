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
    public partial class form : Form
    {
        public form()
        {
            InitializeComponent();
            gene_tester.report_e += Test_new_report;
            mmm();
        }

        async void mmm()
        {
            await Task.Delay(1000);
            //client_pool pool = new client_pool(1);
            client client = new client();
            client.notify_e += Client_notify_e;
            var dv = await client.login("1000", "1000pass");
            var rsv = await client.question(new f_sum()
            {
                a = 10,
                b = 20
            });
        }

        private void Client_notify_e(notify obj)
        {
            
        }

        private void Test_new_report()
        {
            string dv = "";
            dv += "Counter: " + report.counter + "\r\n";
            dv += "Avrage: " + report.avrage + "\r\n";
            dv += "Max: " + report.max + "\r\n";
            dv += "Min: " + report.min + "\r\n";
            Invoke(new Action(() =>
            {
                lbl.Text = dv;
            }));
        }
    }
}