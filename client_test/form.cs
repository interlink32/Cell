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
        client client1 = null;
        long id1 = 1000;
        long id2 = 1001;
        client client2 = null;
        async void mmm()
        {
            await Task.Delay(1000);
            client1 = new client();
            client1.notify_e += Client1_notify_e;
            var dv = await client1.login(id1.ToString(), "1000pass");

            client2 = new client();

            client2.notify_e += Client2_notify_e;
            var dv2 = await client2.login(id2.ToString(), "1001pass");
            await client2.connect_all();
            var rsv = await client1.question(new f_send_message()
            {
                receiver_user = id2,
                message = "form 1 to 2"
            });
        }
        async void Client2_notify_e(notify obj)
        {
            var rsv = await client2.question(new f_send_message()
            {
                receiver_user = id1,
                message = "from 2 to 1"
            });
        }
        async void Client1_notify_e(notify obj)
        {
            var rsv = await client1.question(new f_send_message()
            {
                receiver_user = id2,
                message = "form 1 to 2"
            });
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