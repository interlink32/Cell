using Connection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace server_test
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            new serviece_im(new IPEndPoint(reference.local_ip(), 10000));
        }
    }
}