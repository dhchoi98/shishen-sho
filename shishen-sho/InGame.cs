using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace shishen_sho
{
    public partial class InGame : Form
    {
        Pause pause;
        public InGame(int gameTime)
        {
            InitializeComponent();
            pBarTime.Maximum = gameTime;
            pBarTime.Value = gameTime;
            timer1.Start();
        }

        private void picPause_Click(object sender, EventArgs e)
        {
            //아직 원하는 대로 잘 안 됨.

            if (pause == null || pause.IsDisposed)
            {
                pause = new Pause();
                pause.MdiParent = this;
                pause.Show();
            }
            else
            {
                picPause.Enabled = false;
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pBarTime.Value > pBarTime.Minimum)
            {
                pBarTime.Value += -1; ; // 진행률 감소
            }
            else
            {
                timer1.Stop(); // 진행률이 최소값에 도달하면 타이머 정지
                
            }
        }
    }
}
