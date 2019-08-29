using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
        }
    }
}
