using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace shishen_sho
{
    public partial class MultiPlay_ShowRooms : Form
    {
        private string userName;
        public MultiPlay_ShowRooms()
        {
            InitializeComponent();
        }

        public MultiPlay_ShowRooms(string userName)
        {
            InitializeComponent();
            this.userName = userName;
        }
        private void MultiPlay_ShowRooms_Load(object sender, EventArgs e)
        {

        }
    }
}
