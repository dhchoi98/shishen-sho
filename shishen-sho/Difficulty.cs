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
    public partial class Difficulty : Form
    {
        InGame easy, middle, difficult;

        public Difficulty()
        {
            InitializeComponent();
        }

        private void btnEasy_Click(object sender, EventArgs e)
        {
            easy = new InGame(6000);//10분
            easy.Owner = this;
            easy.Show();
            this.Visible = false;
        }

        private void btnMiddle_Click(object sender, EventArgs e)
        {
            middle = new InGame(4200);//7분
            middle.Owner = this;
            middle.Show();
            this.Visible = false;
        }

        private void btnDifficult_Click(object sender, EventArgs e)
        {
            difficult = new InGame(3000);//5분
            difficult.Owner = this;
            difficult.Show();
            this.Visible = false;
        }
    }
}
