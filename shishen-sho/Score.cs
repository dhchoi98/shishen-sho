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
        public int listViewWidth; 
        public Score()
        {
            InitializeComponent();
            listViewWidth = ListViewScore.Width;
            InitializeListView();
        }
        private void InitializeListView()
        {
            // 글씨 크기 조정 (15포인트)
            ListViewScore.Font = new Font(ListViewScore.Font.FontFamily, 15);

            // 열 추가 및 너비 설정
            ListViewScore.Columns.Add("Mode", listViewWidth / 2, HorizontalAlignment.Left);
            ListViewScore.Columns.Add("Score", listViewWidth / 2, HorizontalAlignment.Left);
            ListViewScore.GridLines = true; // 그리드 라인 활성화
            ListViewScore.View = View.Details; // 상세 보기 모드
            
            ListViewScore.OwnerDraw = true;

            // DrawColumnHeader 이벤트 핸들러 추가
            ListViewScore.DrawColumnHeader += new DrawListViewColumnHeaderEventHandler(ListViewScore_DrawColumnHeader);
        }
        
        private void ListViewScore_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            // 원하는 글씨 크기 및 스타일로 열 제목을 그리기
            using (Font headerFont = new Font("Arial", 15, FontStyle.Regular))
            {
                e.Graphics.FillRectangle(Brushes.DeepSkyBlue, e.Bounds);
                e.Graphics.DrawRectangle(Pens.White, e.Bounds);
                e.Graphics.DrawString(e.Header.Text, headerFont, Brushes.White, e.Bounds);
            }
        }

        public void AddScore(int score, int mode)
        {
            
            // Mode 항목 추가
            string modeText = null;
            if (mode == 15)
            {
                modeText = "Easy";
            }
            else if (mode == 10)
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

        private void btnBack_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            this.Hide();
            main.Show();
        }
    }
}
