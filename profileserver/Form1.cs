using Connection;
using Dna;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace profileserver
{
    public partial class Form1 : Form
    {
        server server = new server();
        public Form1()
        {
            InitializeComponent();
            client.notifyadd((long)e_chromosome.profile, e_chromosome.user, m);
        }
        private void m(long obj)
        {
            Console.Beep();
        }
    }
}