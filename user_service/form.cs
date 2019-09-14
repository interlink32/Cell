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
        service_im service = new service_im();
        public form()
        {
            InitializeComponent();
            mmm();
            report();
        }

        Random random = new Random();
        async void mmm()
        {
            service.client.active_notify(Dna.chromosome.user);
            service.client.notify_e += Client_notify_e;
            await Task.Delay(1000);
            m();
        }
        private async void m()
        {
            var dv = await service.client.question(new q_test()
            {
                receiver = 1,
                value = 12
            }) as q_test.done;
        }
        void Client_notify_e(Dna.notify obj)
        {
            m();
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