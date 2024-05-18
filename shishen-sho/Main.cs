using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace shishen_sho
{
    public partial class Main : Form
    {
        Difficulty difficulty;
        Explain explain;
        MultiPlay multiPlay;

        public Main()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            difficulty = new Difficulty();
            difficulty.Owner = this;
            difficulty.Show();
            this.Visible = false;
        }

        private void btnExplain_Click(object sender, EventArgs e)
        {
            explain = new Explain();
            explain.Owner = this;
            explain.Show();
            this.Visible = false;
        }

        private void btnMulti_Click(object sender, EventArgs e)
        {
            multiPlay = new MultiPlay();
            multiPlay.Owner = this;
            multiPlay.Show();
            this.Visible = false;
        }
    }
}
