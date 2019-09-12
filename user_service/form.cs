using Dna.test;
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
            service.client.login_e += Client_login_e;
            report();
        }
        private void Client_login_e(Connection.q_center obj)
        {
            mmm();
        }
        Random random = new Random();
        async void mmm()
        {
            var p = new q_sum() { a = random.Next(), b = random.Next() };
            var dv = await service.client.question(p);
            if (dv is q_sum.done rsv)
            {
                if (rsv.result != p.a + p.b)
                    throw new Exception("kkjfbjdjvjdjskv");
                mmm();
            }
            else
                throw new Exception("gjfjbjfjd");
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