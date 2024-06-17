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
        private PictureBox firstClicked = null;
        private PictureBox secondClicked = null;
        private int totalTime;
        private int score;
        private const int ROWS = 8;
        private const int COLS = 16;
        private PictureBox[,] graph = new PictureBox[ROWS, COLS];
        private List<Tuple<int, int>> currentPath = null; // 경로를 저장할 변수
        private int difficulty;
        
        public InGame(int minutes)
        {
            InitializeComponent();
            InitializeGraph();

            this.Paint += InGame_Paint; // Paint 이벤트 등록

            gameTimer.Interval = 1000;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            this.Controls.Add(timeLabel);
            TimeLeft = TimeSpan.FromMinutes(minutes);

            progressBar.Style = MetroColorStyle.Silver;
            totalTime = minutes * 60;
            progressBar.Minimum = 0;
            progressBar.Maximum = totalTime;
            progressBar.Value = progressBar.Maximum;

            score = 0;
            lblScore.Text = "Score: 0";
            this.Controls.Add(lblScore);
            difficulty = minutes;
        }

        private void InitializeGraph()
        {
            for (int i = 1; i <= 128; i++)
            {
                PictureBox pictureBox = this.Controls.Find("pictureBox" + i, true).FirstOrDefault() as PictureBox;

                if (pictureBox != null)
                {
                    if (i <= 64)
                    {
                        int row = ((i - 1) / 8) + 1; // 행 계산
                        int col = ((i - 1) % 8) + 1; // 열 계산
                        graph[row, col] = pictureBox;
                        pictureBox.Click += new EventHandler(PictureBox_Click);
                    }
                    else
                    {
                        int row = ((i - 65) / 8) + 1;
                        int col = 9 + (i - 65) % 8;
                        graph[row, col] = pictureBox;
                        pictureBox.Click += new EventHandler(PictureBox_Click);
                    }
                }
            }
            for(int i = 0; i < 18; i ++)
            {
                PictureBox picture = new PictureBox();
                picture.Tag = null;
                graph[0, i] = picture;
                graph[i, 0] = picture;
                graph[i, 17] = picture;
                graph[8, i] = picture;
            }
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            if (firstClicked != null && secondClicked != null)
                return;

            PictureBox clickedPictureBox = sender as PictureBox;

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

            if (clickedPictureBox == null || clickedPictureBox.Image == null)
                return;

            if (firstClicked == null)
            {
                firstClicked = clickedPictureBox;
                firstClicked.Padding = new Padding(0);
                firstClicked.BackColor = Color.LightYellow; // 테두리 색상 설정
                return;
            }

            if (firstClicked != null && firstClicked != clickedPictureBox)
            {
                secondClicked = clickedPictureBox;
                secondClicked.Padding = new Padding(0);
                secondClicked.BackColor = Color.LightYellow; // 테두리 색상 설정
                CheckForMatch();
            }
        }

        private bool AllPicturesCleared()
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

        private void RemainingLabel()
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

        private void AddTimeBonus()
        {
            int timeBonus = (int)TimeLeft.TotalSeconds * 100;
            score += timeBonus;
            lblScore.Text = "Score: " + score;
        }

        private TimeSpan TimeLeft;

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            TimeLeft -= TimeSpan.FromSeconds(1);
            progressBar.Value -= 1;
            if (progressBar.Value < progressBar.Maximum / 2)
                progressBar.Style = MetroColorStyle.Pink;
            if (progressBar.Value < 60)
                progressBar.Style = MetroColorStyle.Red;
            timeLabel.Text = TimeLeft.ToString("mm':'ss");
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
            List<PictureBox> pictureBoxes = new List<PictureBox>();

            for (int i = 1; i <= 128; i++)
            {
                PictureBox pictureBox = this.Controls.Find("pictureBox" + i, true).FirstOrDefault() as PictureBox;
                if (pictureBox != null && pictureBox.Visible)
                {
                    pictureBoxes.Add(pictureBox);
                }
            }

            Random random = new Random();

            for (int i = pictureBoxes.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                Image tempImage = pictureBoxes[i].Image;
                pictureBoxes[i].Image = pictureBoxes[j].Image;
                pictureBoxes[j].Image = tempImage;

                object tempTag = pictureBoxes[i].Tag;
                pictureBoxes[i].Tag = pictureBoxes[j].Tag;
                pictureBoxes[j].Tag = tempTag;
            }
            RemainingLabel();
        }

        private void ShuffleButton_Click(object sender, EventArgs e)
        {
            ShufflePictureBoxes();
            score -= 3000;
            if (score < 0) score = 0;
            lblScore.Text = "Score: " + score;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            List<PictureBox> pictureBoxes = new List<PictureBox>();

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
            gameTimer.Stop();
            Pause pause = new Pause();
            DialogResult dialog = pause.ShowDialog();

            if (dialog == DialogResult.OK)
            {
                gameTimer.Start();
            }
            else if (dialog == DialogResult.Cancel)
            {
                this.Close();
            }
        }

        private void pictureBox66_Click(object sender, EventArgs e)
        {
        }

        private List<Tuple<int, int>> CheckPathAndHide()
        {
            Tuple<int, int> firstIndex = FindIndex(firstClicked);
            Tuple<int, int> secondIndex = FindIndex(secondClicked);

            if (firstIndex.Item1 != -1 && firstIndex.Item2 != -1 &&
                secondIndex.Item1 != -1 && secondIndex.Item2 != -1)
            {
                return FindPathWithMaxThreeBends(firstIndex, secondIndex);
            }
            else
            {
                return null;
            }
        }
        private void InGame_Paint(object sender, PaintEventArgs e)
        {
            if (currentPath != null && currentPath.Count > 1)
            {
                using (Pen pen = new Pen(Color.Red, 2))
                {
                    for (int i = 0; i < currentPath.Count - 1; i++)
                    {
                        var start = currentPath[i];
                        var end = currentPath[i + 1];
                        var startPoint = new Point(graph[start.Item1, start.Item2].Left + graph[start.Item1, start.Item2].Width / 2,
                                                   graph[start.Item1, start.Item2].Top + graph[start.Item1, start.Item2].Height / 2);
                        var endPoint = new Point(graph[end.Item1, end.Item2].Left + graph[end.Item1, end.Item2].Width / 2,
                                                 graph[end.Item1, end.Item2].Top + graph[end.Item1, end.Item2].Height / 2);

                        e.Graphics.DrawLine(pen, startPoint, endPoint);
                    }
                }
            }
        }
        private async void CheckForMatch()
        {
            if (firstClicked.Tag != null && secondClicked.Tag != null &&
                firstClicked.Tag.ToString() == secondClicked.Tag.ToString())
            {
                var path = CheckPathAndHide();
                if (path != null)
                {
                    currentPath = path;
                    this.Invalidate(); // 경로를 다시 그리기 위해 Invalidate 호출
                    await Task.Delay(300); // 딜레이
                    firstClicked.Tag = null;
                    secondClicked.Tag = null;
                    firstClicked.Hide();
                    secondClicked.Hide();

                    score += 500; // 패 매칭 성공 시 점수 500점 추가
                    lblScore.Text = "Score: " + score;

                    RemainingLabel();

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
                    firstClicked.Padding = new Padding(0);
                    firstClicked.BackColor = Color.Transparent;
                    secondClicked.Padding = new Padding(0);
                    secondClicked.BackColor = Color.Transparent;
                }

                firstClicked = null;
                secondClicked = null;
            }
        }



        private Tuple<int, int> FindIndex(PictureBox pictureBox)
        {
            for (int col = 0; col < graph.GetLength(0); col++)
            {
                for (int row = 0; row < graph.GetLength(1); row++)
                {
                    if (graph[col, row] == pictureBox)
                    {
                        return Tuple.Create(col, row);
                    }
                }
            }
            return Tuple.Create(-1, -1);
        }

        private List<Tuple<int, int>> FindPathWithMaxThreeBends(Tuple<int, int> start, Tuple<int, int> end)
        {
            int startX = start.Item1, startY = start.Item2;
            int endX = end.Item1, endY = end.Item2;

            int[][] directions = new int[][]
            {
        new int[] { -1, 0 },
        new int[] { 1, 0 },
        new int[] { 0, -1 },
        new int[] { 0, 1 }
            };

            Queue<Tuple<int, int, int, int, List<Tuple<int, int>>>> queue = new Queue<Tuple<int, int, int, int, List<Tuple<int, int>>>>();
            bool[,] visited = new bool[ROWS, COLS];

            queue.Enqueue(Tuple.Create(startX, startY, -1, 0, new List<Tuple<int, int>> { Tuple.Create(startX, startY) }));
            visited[startX, startY] = true;

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                int x = node.Item1;
                int y = node.Item2;
                int prevDir = node.Item3;
                int bends = node.Item4;
                var path = node.Item5;

                if (x == endX && y == endY)
                {
                    return path;
                }

                for (int dir = 0; dir < 4; dir++)
                {
                    int nx = x + directions[dir][0];
                    int ny = y + directions[dir][1];

                    if (nx >= 0 && nx < ROWS && ny >= 0 && ny < COLS && !visited[nx, ny] && (graph[nx, ny].Tag == null || (nx == endX && ny == endY)))
                    {
                        int newBends = bends + (dir != prevDir ? 1 : 0);

                        if (newBends <= 3)
                        {
                            var newPath = new List<Tuple<int, int>>(path) { Tuple.Create(nx, ny) };
                            queue.Enqueue(Tuple.Create(nx, ny, dir, newBends, newPath));
                            visited[nx, ny] = true;
                        }
                    }
                }
            }

            return null;
        }

    }
}