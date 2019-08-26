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
    public partial class form : Form
    {
        service_im service = new service_im();
        public form()
        {
            InitializeComponent();
        }
    }
}