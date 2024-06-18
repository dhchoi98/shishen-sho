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
    public partial class Result : MetroFramework.Forms.MetroForm
    {
        public Result(int finalScore)
        {
            InitializeComponent();
            lblFinalScore.Text = "최종 점수: " + finalScore;
        }

        private void Result_FormClosing(object sender, FormClosingEventArgs e)
        {
            Main main = new Main();
            main.Show();
        }
    }
}
