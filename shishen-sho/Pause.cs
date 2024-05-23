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
    public partial class Pause : Form
    {
        public Pause()
        {
            InitializeComponent();
            this.CenterToParent();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            this.Close ();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            FindForm();
        }
    }
}
