using MetroFramework;
using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace shishen_sho
{
    public partial class InGame : MetroFramework.Forms.MetroForm
    {

        private int totalTime;
        public InGame(int minutes)
        {
            InitializeComponent();

            // 타이머 1초마다 초기화
            gameTimer.Interval = 1000;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();
            // 시간을 표시할 라벨 초기화
            this.Controls.Add(timeLabel);
            // 게임 시작 시간 설정
            TimeLeft = TimeSpan.FromMinutes(minutes);

            progressBar.Style = MetroColorStyle.Silver;
            totalTime = minutes * 60;
            progressBar.Minimum = 0;
            progressBar.Maximum = totalTime;
            progressBar.Value = progressBar.Maximum;  // 시작 값은 0에서 시작

        }
        private TimeSpan TimeLeft;
        /// <summary>

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            // 시간 감소
            TimeLeft -= TimeSpan.FromSeconds(1);
            progressBar.Value -= 1;
            if (progressBar.Value < progressBar.Maximum / 2) // 제한시간 반 남으면 분홍색
                progressBar.Style = MetroColorStyle.Pink;
            if (progressBar.Value < 60)  // 제한시간 1분 남으면 빨간색
                progressBar.Style = MetroColorStyle.Red;
            // 시간 표시 업데이트
            timeLabel.Text = TimeLeft.ToString("mm':'ss");
            // 시간이 0이 되면 타이머 중지
            if (TimeLeft <= TimeSpan.Zero)
            {
            
                gameTimer.Stop();
                MessageBox.Show("실패하였습니다");
                this.Close();
            }
        }


        private void InGame_Load(object sender, EventArgs e)
        {

        }

        private void progressBar_Click(object sender, EventArgs e)
        {

        }
    }
}
