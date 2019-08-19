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
        client_pool pool = new client_pool();
        public Form1()
        {
            InitializeComponent();
            gene_tester.report_e += Test_new_report;
            pool.start();
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