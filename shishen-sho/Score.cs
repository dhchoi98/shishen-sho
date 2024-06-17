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
    public partial class Score : MetroFramework.Forms.MetroForm
    {
        public Score()
        {
            InitializeComponent();
            InitializeListView();
        }
        private void InitializeListView()
        {
            // 열 추가 및 너비 설정
            ListViewScore.Columns.Add("Mode", 100, HorizontalAlignment.Left);
            ListViewScore.Columns.Add("Score", 100, HorizontalAlignment.Left);
            ListViewScore.GridLines = true; // 그리드 라인 활성화
            ListViewScore.View = View.Details; // 상세 보기 모드
        }
        private void Score_Load(object sender, EventArgs e)
        {
            
        }

        public void AddScore(int score, int mode)
        {
            
            // Mode 항목 추가
            string modeText = null;
            if (mode == 1)
            {
                modeText = "Easy";
            }
            else if (mode == 2)
            {
                modeText = "Normal";
            }
            else
            {
                modeText = "Hard";
            }

            ListViewItem scoreItem = new ListViewItem(modeText); // Score 값을 첫 번째 열에 추가

            scoreItem.SubItems.Add(score.ToString()); // Mode 값을 두 번째 열에 추가

            ListViewScore.Items.Add(scoreItem); 
        }
    }
}
