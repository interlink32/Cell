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
            client client1 = new client();
            client1.notify_e += Client1_notify_e;
            var dv = await client1.login("1000", "1000pass");

            client client2 = new client();
            client1.notify_e += Client2_notify_e;
            var dv2 = await client1.login("1001", "1000pass");

            var rsv = client1.question(new f_send_message()
            {
                receiver_user=1001,
                message="Hello"
            });
        }

        private void Client2_notify_e(notify obj)
        {
            
        }

        private void Client1_notify_e(notify obj)
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