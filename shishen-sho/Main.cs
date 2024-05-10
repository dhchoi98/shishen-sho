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
    public partial class Main : MetroFramework.Forms.MetroForm
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            InGame ingame = new InGame();
            this.Hide();
            ingame.Show();
        }

        private void btnMulti_Click(object sender, EventArgs e)
        {
            MultiPlay multi = new MultiPlay();
            this.Hide();
            multi.Show();
        }

        private void btnExplain_Click(object sender, EventArgs e)
        {
            Explain explain = new Explain();
            this.Hide();
            explain.Show();
        }
    }
}
