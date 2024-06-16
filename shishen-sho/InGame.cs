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
        private const int ROWS = 8;
        private const int COLS = 16;
        private PictureBox[,] graph = new PictureBox[ROWS, COLS]; // 2차원 배열 그래프

        public InGame(int minutes)
        {
            InitializeComponent();
            InitializeGraph();

            //InitializePictureBoxEvents();
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

        private void InitializeGraph()
        {
            for (int i = 1; i <= 128; i++)
            {
                PictureBox pictureBox = this.Controls.Find("pictureBox" + i, true).FirstOrDefault() as PictureBox;

                if (pictureBox != null)
                {
                    if(i <= 64)
                    {
                        int row = (i - 1) / 8; // 행 계산
                        int col = (i - 1) % 8; // 열 계산
                        graph[row, col] = pictureBox;
                        pictureBox.Click += new EventHandler(PictureBox_Click);
                    }
                    else
                    {
                        int row = (i - 65) / 8;
                        int col = 8 + (i - 65) % 8;
                        graph[row, col] = pictureBox;
                        pictureBox.Click += new EventHandler(PictureBox_Click);
                    }
                }
            }
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
        private void RemainingLabel() // 남은 패가 몇개인지 보여줌
        {
            int remainingCount = 0;
            for (int i = 1; i <= 128; i++)
            {
                PictureBox pictureBox = this.Controls.Find("pictureBox" + i, true).FirstOrDefault() as PictureBox;
                if (pictureBox != null && pictureBox.Visible)
                {
                    remainingCount++;
                }
            }
            remaininglbl.Text = "남은 패: " + remainingCount + "개";
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
            RemainingLabel();
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
            RemainingLabel();
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
        // 타일 색상 초기화

        private void pictureBox66_Click(object sender, EventArgs e)
        {

        }

        // 두 개의 클릭된 PictureBox를 비교하는 메소드
        private async void CheckForMatch()
        {
            MessageBox.Show("CheckFOrMatch");
            // 이미지의 Tag를 비교하여 동일한 경우 두 PictureBox를 숨김
            if (firstClicked.Tag != null && secondClicked.Tag != null &&
                firstClicked.Tag.ToString() == secondClicked.Tag.ToString() && CheckPathAndHide()) //여기 마지막 조건이 없엇어서 그냥 Tag가 같으면 지웠음
            {
                MessageBox.Show("True");
                await Task.Delay(300); // 딜레이//
                firstClicked.Tag = null;
                secondClicked.Tag = null;
                firstClicked.Hide();
                secondClicked.Hide();

                score += 500; // 패 매칭 성공 시 점수 500점 추가
                lblScore.Text = "Score: " + score;

                RemainingLabel();

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

            firstClicked = null;
            secondClicked = null;
        }

        // 경로의 꺾임 횟수 계산 메서드
        private bool CheckPathAndHide()
        {
            Tuple<int, int> firstIndex = FindIndex(firstClicked);
            Tuple<int, int> secondIndex = FindIndex(secondClicked);

            MessageBox.Show("start: " + firstIndex.Item1 +  " " + firstIndex.Item2 + " " + firstClicked.Tag +
                "\n" + "end : " + secondIndex.Item1  + " " + secondIndex.Item2 + " " + secondClicked.Tag );

            // 유효한 인덱스인지 확인 (-1, -1이 아닌 경우)
            if (firstIndex.Item1 != -1 && firstIndex.Item2 != -1 &&
                secondIndex.Item1 != -1 && secondIndex.Item2 != -1)
            {
                return FindPathWithMaxTwoBends(firstIndex, secondIndex);
            }
            else
            {
                // 잘못된 클릭 처리 (예: 메시지 박스 표시)
                MessageBox.Show("Invalid tile selection.");
                return false;
            }
        }

        // 2차원 배열에서 PictureBox 찾기
        private Tuple<int, int> FindIndex(PictureBox pictureBox)
        {
            for (int col = 0; col < COLS; col++)
            {
                for (int row = 0; row < ROWS; row++)
                {
                    if (graph[row, col] == pictureBox)
                    {
                        return Tuple.Create(row, col); // 열, 행 순서로 튜플 생성
                    }
                }
            }
            return Tuple.Create(-1, -1); // PictureBox를 찾지 못한 경우 (-1, -1) 튜플 반환
        }

        private List<Tuple<int, int>> GetNextPoints(Tuple<int, int> current, HashSet<Tuple<int, int>> visited)
        {
            List<Tuple<int, int>> nextPoints = new List<Tuple<int, int>>();
            int x = current.Item1;
            int y = current.Item2;

            if (y > 0)
            {
                MessageBox.Show("start: "  + x  + ", " + (y-1) + " tag : "+ graph[x, y - 1].Tag + "\n " + "end: " + x + ", " + y + " tag : " +graph[x, y].Tag );
                

                if (graph[x, y - 1].Tag == null || graph[x, y - 1].Tag.Equals(graph[x, y].Tag))
                {
                    nextPoints.Add(Tuple.Create(x, y - 1));
                }
            }
            
            if (y < 14)
            {
                MessageBox.Show("start: "  + x + ", " + (y + 1) + " tag : " + graph[x, y + 1].Tag + "\n " + "end: " + x + ", " + y + " tag : " + graph[x, y].Tag);

                if (graph[x, y + 1].Tag == null || graph[x, y + 1].Tag.Equals(graph[x, y].Tag))
                {
                    nextPoints.Add(Tuple.Create(x, y + 1));
                }
            }   
            if (x > 0)
            {
                MessageBox.Show("start: " + (x-1) + ", " + y + " tag : " + graph[x - 1, y].Tag + "\n " + "end: " + x + ", " + y + " tag : " + graph[x, y].Tag);

                if (graph[x - 1, y].Tag == null || graph[x - 1, y].Tag.Equals(graph[x, y].Tag))
                {
                    nextPoints.Add(Tuple.Create(x - 1, y));
                }
            }
                
            if (x < 6)
            {
                MessageBox.Show("start: " + (x + 1) + ", " + y + " tag : " + graph[x + 1, y].Tag + "\n " + "end: " + x + ", " + y + " tag : " + graph[x, y].Tag);

                if (graph[x + 1, y].Tag == null || graph[x + 1, y].Tag.Equals(graph[x, y].Tag))
                {
                    nextPoints.Add(Tuple.Create(x + 1, y));
                }
            }
                

            return nextPoints;
        }

        private bool FindPathWithMaxTwoBends(Tuple<int, int> start, Tuple<int, int> end)
        {
            Stack<(Tuple<int, int> node, int bends)> stack = new Stack<(Tuple<int, int>, int)>();
            HashSet<Tuple<int, int>> visited = new HashSet<Tuple<int, int>>();

            stack.Push((start, 0)); // 시작 노드와 꺾임 횟수 0을 스택에 추가

            while (stack.Count > 0)
            {
                var (current, bends) = stack.Pop(); // 스택에서 노드와 꺾임 횟수를 가져옴

                if (graph[current.Item1, current.Item2].Equals(graph[end.Item1, end.Item2]))
                {
                    return bends <= 2; // 목표 지점에 도착했고 꺾임 횟수가 2 이하이면 true 반환
                }

                if (bends > 2 || visited.Contains(current))
                {
                    continue; // 조건에 맞지 않으면 다음 노드 탐색
                }

                visited.Add(current); // 현재 노드를 방문했다고 표시

                foreach (Tuple<int, int> next in GetNextPoints(current, visited))
                {
                    stack.Push((next, bends + (IsBend(current, next) ? 1 : 0))); // 다음 노드와 꺾임 횟수를 스택에 추가
                }
            }

            return false; // 스택이 비어있으면 경로를 찾지 못한 것
        }


        private bool IsBend(Tuple<int, int> current, Tuple<int, int> next)
        {
            return current.Item1 != next.Item1 && current.Item2 != next.Item2;
        }

        private void progressBar_Click(object sender, EventArgs e)
        {

        }

        /*private bool IsObstacle(Tuple<int, int> point)
        {
            int x = point.Item1;
            int y = point.Item2;

            if (x < 0 || x >= COLS || y < 0 || y >= ROWS)
                return false; // 범위를 벗어나는 경우는 장애물이 아님

            return graph[x, y] != null && graph[x, y].Visible;
        }



        /*private bool IsObstacle(Tuple<int, int> point)
        {
            int x = point.Item1;
            int y = point.Item2;

            // 범위를 벗어나는 경우 false 반환 (장애물이 아님)
            if (x < 0 || x >= COLS || y < 0 || y >= ROWS)
                return false;

            return graph[x, y] != null && graph[x, y].Visible; // 행, 열 순서에 유의 (수정됨)
        }*/
    }
}