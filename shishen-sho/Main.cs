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
            lblTitle.Parent = pictureBox1;
            lblTitle.BackColor = Color.Transparent;

            //부모가 pictureBox1로 지정되면서 로케이션을 새로 지정
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnStart_Click_1(object sender, EventArgs e)
        {
            Difficulty difficulty = new Difficulty();
            this.Hide();
            difficulty.Show();
        }

        private void btnMulti_Click_1(object sender, EventArgs e)
        {
            MultiPlay multi = new MultiPlay();
            this.Hide();
            multi.Show();
        }

        private void btnExplain_Click_1(object sender, EventArgs e)
        {
            Explain explain = new Explain();
            this.Hide();
            explain.Show();
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void btnScore_Click(object sender, EventArgs e)
        {
            Score score = new Score();
            this.Hide();
            score.Show();
        }
    }
}
