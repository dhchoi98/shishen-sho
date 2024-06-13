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
        // 클릭한 PictureBox를 저장해주는 변수 2개
        private PictureBox firstClicked = null;
        private PictureBox secondClicked = null;
        private int totalTime;
        private int score;
        public InGame(int minutes)
        {
            InitializeComponent();
            InitializePictureBoxEvents();
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
            
            score = 0;
            lblScore.Text = "Score: 0";
            this.Controls.Add(lblScore);
        }
        private void InitializePictureBoxEvents()
        {
            // 폼에 있는 128개의 PictureBox를 반복하여 이벤트 핸들러를 등록
            for (int i = 1; i <= 128; i++)
            {
                // 이름을 통해 PictureBox를 찾음
                PictureBox pictureBox = this.Controls.Find("pictureBox" + i, true).FirstOrDefault() as PictureBox;
                if (pictureBox != null)
                {
                    // PictureBox에 클릭 이벤트 핸들러 추가
                    pictureBox.Click += new EventHandler(PictureBox_Click);
                }
            }
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            // 두 개의 PictureBox가 이미 선택된 경우 더 이상 처리하지 않음
            if (firstClicked != null && secondClicked != null)
                return;

            // 클릭된 PictureBox를 가져옴
            PictureBox clickedPictureBox = sender as PictureBox;
            // 이미 선택된 PictureBox를 다시 클릭하면 선택 취소
            if (firstClicked == clickedPictureBox)
            {
                firstClicked.Padding = new Padding(0);
                firstClicked.BackColor = Color.Transparent;
                firstClicked = null;
                return;
            }

            if (secondClicked == clickedPictureBox)
            {
                secondClicked.Padding = new Padding(0);
                secondClicked.BackColor = Color.Transparent;
                secondClicked = null;
                return;
            }
            // 클릭된 PictureBox가 null이거나 이미지가 없는 경우 처리하지 않음
            if (clickedPictureBox == null || clickedPictureBox.Image == null)
                return;

            // 첫 번째 클릭
            if (firstClicked == null)
            {
                firstClicked = clickedPictureBox;
                firstClicked.Padding = new Padding(0);
                firstClicked.BackColor = Color.LightYellow; // 테두리 색상 설정
                return;
            }

            // 두 번째 클릭
            if (firstClicked != null && firstClicked != clickedPictureBox)
            {
                secondClicked = clickedPictureBox;
                secondClicked.Padding = new Padding(0);
                secondClicked.BackColor = Color.LightYellow; // 테두리 색상 설정
                CheckForMatch();
            }
        }

        // 두 개의 클릭된 PictureBox를 비교하는 메소드
        private async void CheckForMatch()
        {
            // 이미지의 Tag를 비교하여 동일한 경우 두 PictureBox를 숨김
            if (firstClicked.Tag != null && secondClicked.Tag != null &&
                firstClicked.Tag.ToString() == secondClicked.Tag.ToString())
            {
                await Task.Delay(300); // 딜레이//
                firstClicked.Hide();
                secondClicked.Hide();

                score += 500; // 패 매칭 성공 시 점수 500점 추가
                lblScore.Text = "Score: " + score;

                // 모든 PictureBox를 없앴는지 확인
                if (AllPicturesCleared())
                {
                    gameTimer.Stop();
                    AddTimeBonus(); // 남은 시간 점수 추가
                    Result scoreForm = new Result(score);
                    scoreForm.ShowDialog();
                    this.Close();
                }
            }
            else
            {
                // 매칭되지 않으면 테두리 초기화
                firstClicked.Padding = new Padding(0);
                firstClicked.BackColor = Color.Transparent;
                secondClicked.Padding = new Padding(0);
                secondClicked.BackColor = Color.Transparent;
            }
            else
            {
                // 매칭되지 않으면 테두리 초기화
                firstClicked.Padding = new Padding(0);
                firstClicked.BackColor = Color.Transparent;
                secondClicked.Padding = new Padding(0);
                secondClicked.BackColor = Color.Transparent;
            }

            firstClicked = null;
            secondClicked = null;
        }
        private bool AllPicturesCleared() // 모든 타일을 없애면 CLEAR
        {
            for (int i = 1; i <= 128; i++)
            {
                PictureBox pictureBox = this.Controls.Find("pictureBox" + i, true).FirstOrDefault() as PictureBox;
                if (pictureBox != null && pictureBox.Visible)
                {
                    return false;
                }
            }
            return true;
        }
        private void AddTimeBonus() // 시간 남으면 보너스점수로 전환(초당 100점)
        {
            int timeBonus = (int)TimeLeft.TotalSeconds * 100;
            score += timeBonus;
            lblScore.Text = "Score: " + score;
        }
        private TimeSpan TimeLeft;


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
            ShufflePictureBoxes();
        }
        private void ShufflePictureBoxes()
        {
            // 모든 PictureBox를 리스트에 저장
            List<PictureBox> pictureBoxes = new List<PictureBox>();

            // 128개의 PictureBox 중에서 숨겨지지 않은 PictureBox만 리스트에 추가
            for (int i = 1; i <= 128; i++)
            {
                PictureBox pictureBox = this.Controls.Find("pictureBox" + i, true).FirstOrDefault() as PictureBox;
                if (pictureBox != null && pictureBox.Visible)
                {
                    pictureBoxes.Add(pictureBox);
                }
            }

            // 랜덤하게 섞기 위해 Random 객체 생성
            Random random = new Random();

            // Fisher-Yates shuffle 알고리즘을 사용하여 리스트를 섞음
            for (int i = pictureBoxes.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                // 두 PictureBox의 이미지를 교환
                Image tempImage = pictureBoxes[i].Image;
                pictureBoxes[i].Image = pictureBoxes[j].Image;
                pictureBoxes[j].Image = tempImage;

                // 두 PictureBox의 태그를 교환
                object tempTag = pictureBoxes[i].Tag;
                pictureBoxes[i].Tag = pictureBoxes[j].Tag;
                pictureBoxes[j].Tag = tempTag;
            }
        }
        private void ShuffleButton_Click(object sender, EventArgs e)
        {
            ShufflePictureBoxes();
            score -= 3000; // 셔플 버튼 클릭 시 점수 3000점 감소
            if (score < 0) score = 0; // 점수가 음수인 경우는 제외했음
            lblScore.Text = "Score: " + score;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            // 모든 PictureBox를 리스트에 저장
            List<PictureBox> pictureBoxes = new List<PictureBox>();

            // 128개의 PictureBox 중에서 숨겨지지 않은 PictureBox만 리스트에 추가
            for (int i = 1; i <= 128; i++)
            {
                PictureBox pictureBox = this.Controls.Find("pictureBox" + i, true).FirstOrDefault() as PictureBox;
                if (pictureBox != null && pictureBox.Visible)
                {
                    pictureBoxes.Add(pictureBox);
                }
            }
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            //타이머 일시정지
            gameTimer.Stop();
            //modal 대화상자로 정지하는 동안 상호작용할 수 없게 만듦.
            Pause pause = new Pause();
            DialogResult dialog = pause.ShowDialog();

            //continue 버튼을 누른 경우. 타이머 이어서 시작.
            if(dialog == DialogResult.OK)
            {                
                gameTimer.Start();
            }//restart 버튼을 누른 경우.
            else if(dialog == DialogResult.Cancel)
            {
                this.Close();
            }
        }

        private void Quit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
