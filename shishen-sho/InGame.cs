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


            private const int BOARD_WIDTH = 16; // 게임판 너비
            private const int BOARD_HEIGHT = 8; // 게임판 높이
            private string[,] board = new string[BOARD_WIDTH, BOARD_HEIGHT]; // 게임판 데이터 (문자열)
            private Button[,] buttons = new Button[BOARD_WIDTH, BOARD_HEIGHT]; // 버튼 배열
            private Point selectedTile1 = new Point(-1, -1); // 첫 번째 선택된 타일 좌표
            private Point selectedTile2 = new Point(-1, -1); // 두 번째 선택된 타일 좌표


            // 게임 시작 시 게임판 초기화
            private void InitializeBoard()
            {
                for (int i = 0; i < BOARD_WIDTH; i++)
                {
                    for (int j = 0; j < BOARD_HEIGHT; j++)
                    {
                        string buttonName = $"button_{i}_{j}";
                        buttons[i, j] = this.Controls.Find(buttonName, true).FirstOrDefault() as Button;
                        if (buttons[i, j] != null)
                        {
                            buttons[i, j].Click += TileButtonClick;
                            //board[i, j] = buttons[i, j].Tag?.ToString() ?? ""; // 실제 타일 종류 설정 필요
                            // 임시로 테스트용 타일 설정
                            board[i, j] = (i + j) % 2 == 0 ? "bamboo1" : "";
                        }
                    }
                }
            }

            // 타일 버튼 클릭 이벤트 핸들러
            private void TileButtonClick(object sender, EventArgs e)
            {
                Button clickedButton = sender as Button;
                if (clickedButton == null) return;

                int x = clickedButton.Location.X / clickedButton.Width;
                int y = clickedButton.Location.Y / clickedButton.Height;
                Point clickedTile = new Point(x, y);

                if (selectedTile1 == new Point(-1, -1)) // 첫 번째 타일 선택
                {
                    selectedTile1 = clickedTile;
                    clickedButton.BackColor = Color.Yellow; // 선택된 타일 표시
                }
                else if (selectedTile2 == new Point(-1, -1)) // 두 번째 타일 선택
                {
                    selectedTile2 = clickedTile;
                    clickedButton.BackColor = Color.Yellow;

                    if (FindPath(selectedTile1, selectedTile2))
                    {
                        // 경로 찾음, 처리 (타일 제거 및 UI 업데이트)
                        board[selectedTile1.X, selectedTile1.Y] = "";
                        board[selectedTile2.X, selectedTile2.Y] = "";
                        buttons[selectedTile1.X, selectedTile1.Y].Visible = false;
                        buttons[selectedTile2.X, selectedTile2.Y].Visible = false;
                        ResetTileColors();
                    }
                    else
                    {
                        // 경로 없음, 메시지 출력 등
                        MessageBox.Show("경로를 찾을 수 없습니다.");
                        ResetTileColors();
                    }

                    selectedTile1 = new Point(-1, -1);
                    selectedTile2 = new Point(-1, -1);
                }
            }

            // 깊이 우선 탐색 (DFS) 알고리즘
            private bool FindPath(Point start, Point end)
            {
                var visited = new HashSet<Point>();
                var stack = new Stack<(Point, int)>(); // (좌표, 꺾인 횟수)
                stack.Push((start, 0));

                while (stack.Count > 0)
                {
                    var (current, turns) = stack.Pop();
                    visited.Add(current);

                    if (current == end)
                        return true; // 경로 찾음

                    foreach (var neighbor in GetNeighbors(current))
                    {
                        if (!visited.Contains(neighbor) && IsValidTurn(current, neighbor, turns))
                            stack.Push((neighbor, turns + (IsTurn(current, neighbor) ? 1 : 0)));
                    }
                }
                return false; // 경로 없음
            }

            // 이웃 칸 좌표 반환
            private List<Point> GetNeighbors(Point current)
            {
                var neighbors = new List<Point>();
                int[] dx = { -1, 0, 1, 0 };
                int[] dy = { 0, -1, 0, 1 };

                for (int i = 0; i < 4; i++)
                {
                    int nx = current.X + dx[i];
                    int ny = current.Y + dy[i];
                    if (nx >= 0 && nx < BOARD_WIDTH && ny >= 0 && ny < BOARD_HEIGHT && board[nx, ny] == "")
                        neighbors.Add(new Point(nx, ny));
                }
                return neighbors;
            }

            // 유효한 이동인지 판단 (꺾임 횟수, 장애물, 경로 길이)
            private bool IsValidTurn(Point current, Point next, int turns)
            {
                return turns < 2 && !HasObstacleBetween(current, next) && GetDistance(current, next) <= 2;
            }

            // 직각으로 꺾이는 이동인지 판단
            private bool IsTurn(Point current, Point next)
            {
                return current.X != next.X && current.Y != next.Y;
            }

            // 두 칸 사이에 장애물이 있는지 판단 (Bresenham's line algorithm)
            private bool HasObstacleBetween(Point start, Point end)
            {
                int dx = Math.Abs(end.X - start.X);
                int dy = Math.Abs(end.Y - start.Y);
                int sx = (start.X < end.X) ? 1 : -1;
                int sy = (start.Y < end.Y) ? 1 : -1;
                int err = dx - dy;
                int x = start.X;
                int y = start.Y;

                while (true)
                {
                    if (x == end.X && y == end.Y) break; // 목표 지점 도달
                    if (board[x, y] != "") return true; // 장애물 발견

                    int e2 = 2 * err;
                    if (e2 > -dy) { err -= dy; x += sx; }
                    if (e2 < dx) { err += dx; y += sy; }
                }
                return false; // 장애물 없음
            }

            // 두 칸 사이의 거리 계산
            private int GetDistance(Point p1, Point p2)
            {
                return Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y);
            }

            // 타일 색상 초기화
            private void ResetTileColors()
            {
                foreach (Button button in buttons)
                {
                    button.BackColor = default(Color); // 기본 색상으로 변경
                }
            }
        }
    }

}
}
