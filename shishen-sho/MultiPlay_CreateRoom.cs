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
    public partial class MultiPlay_CreateRoom : Form
    {
        private string username;
        public MultiPlay_CreateRoom()
        {
            InitializeComponent();
        }

        public MultiPlay_CreateRoom(string username)
        {
            InitializeComponent();
            this.username = username;
        }
        private void MultiPlay_CreateRoom_Load(object sender, EventArgs e)
        {

        }
    }
}
