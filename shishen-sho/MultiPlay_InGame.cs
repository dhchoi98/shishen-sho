using MetroFramework;
using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace shishen_sho
{
    public partial class MultiPlay_InGame : MetroFramework.Forms.MetroForm
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
        int remainingCount = 0;


        private TcpListener server = null;
        private TcpClient guest = null;
        private TcpClient hClient = null;
        private string hostIpAddress = null;
        private IPAddress localAddr = IPAddress.Parse("127.0.0.1");
        private IPAddress hostAddr;
        private int port = 8886;
        public bool m_bConnect = false;
        public bool m_bStop = false;

        private Thread thHost;
        private Thread thSend;
        private Thread thReader;

        public NetworkStream m_Stream;
        public StreamReader m_Read;
        public StreamWriter m_Write;

        // 방장
        public MultiPlay_InGame(int minutes)
        {
            InitializeComponent();
            InitializeGraph();
            // 타이머 1초마다 초기화
            gameTimer.Interval = 1000;
            gameTimer.Tick += GameTimer_Tick;
            // gameTimer.Start();

            
            // 게임 시작 시간 설정
            TimeLeft = TimeSpan.FromMinutes(minutes);

            progressBar.Style = MetroColorStyle.Silver;
            totalTime = minutes * 60;
            progressBar.Minimum = 0;
            progressBar.Maximum = totalTime;
            progressBar.Value = progressBar.Maximum;  // 시작 값은 0에서 시작

            score = 0;
            lblScore.Text = "Score: 0";
           
        }

        // 게스트
        public MultiPlay_InGame(int minutes, string hostIpAddress)
        {
            InitializeComponent();
            InitializeGraph();
            // 타이머 1초마다 초기화
            gameTimer.Interval = 1000;
            gameTimer.Tick += GameTimer_Tick;
            // gameTimer.Start();


            // 게임 시작 시간 설정
            TimeLeft = TimeSpan.FromMinutes(minutes);

            progressBar.Style = MetroColorStyle.Silver;
            totalTime = minutes * 60;
            progressBar.Minimum = 0;
            progressBar.Maximum = totalTime;
            progressBar.Value = progressBar.Maximum;  // 시작 값은 0에서 시작

            score = 0;
            lblScore.Text = "Score: 0";

            this.hostIpAddress = hostIpAddress;
            hostAddr = IPAddress.Parse(hostIpAddress);

        }

        private async void InGame_Load(object sender, EventArgs e)
        {
            ShufflePictureBoxes();

            if (hostIpAddress == null)
            {
                BroadcastStartMessage();
            }

            // 비동기 소켓 연결 작업 시작
            await PerformSocketConnectionAsync();

            // 소켓 연결이 완료된 후에 코드 실행
            if (m_bConnect)
            {
                m_bStop = true;
                if (hostIpAddress == null)
                {
                    thHost = new Thread(new ThreadStart(ServerStart));
                    thHost.Start();
                }
                thSend = new Thread(new ThreadStart(Send));
                thSend.Start();
            }
            gameTimer.Start();
        }

        private void BroadcastStartMessage()
        {
            UdpClient udpClient = new UdpClient();
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Broadcast, 8889);
            byte[] message = Encoding.ASCII.GetBytes("start");

            for (int i = 0; i < 5; i++)
            {
                // 메시지 전송
                Console.WriteLine("게임시작 메세지 전송");
                udpClient.Send(message, message.Length, endPoint);
                Thread.Sleep(1000);
            }
        }

        private async Task PerformSocketConnectionAsync()
        {
            MultiPlay_MessageBox messageBox = new MultiPlay_MessageBox();
            this.Enabled = false;

            var task = Task.Run(() =>
            {
                if (hostIpAddress == null) // 호스트 입장, 소켓 연결 대기
                {
                    try
                    {
                        server = new TcpListener(port);
                        server.Start();

                        m_bStop = true;
                        Console.WriteLine("클라이언트 접속 대기중");

                        while (!m_bConnect)
                        {
                            hClient = server.AcceptTcpClient();

                            if (hClient.Connected)
                            {
                                m_bConnect = true;
                                Console.WriteLine("클라이언트 접속");
                            }
                        }
                    }
                    catch
                    {
                        Console.WriteLine("시작 도중에 오류 발생");
                        return;
                    }
                }
                else // 게스트 입장, 소켓 연결
                {
                    Thread.Sleep(2000);
                    Connect();
                }
            });

            messageBox.Shown += async (s, e) =>
            {
                Console.WriteLine("소켓 연결 시작");

                // 소켓 연결 작업을 비동기적으로 수행
                await task;

                Console.WriteLine("소켓 연결 완료");

                // 메시지 박스를 닫기
                messageBox.Invoke(new Action(() => messageBox.Close()));
            };

            messageBox.ShowDialog();

            // 메시지 박스가 닫힌 후 InGame 폼의 컨트롤을 활성화
            this.Enabled = true;

            // 추가 작업
            Console.WriteLine("메시지 박스가 닫혔습니다. 추가 작업을 수행합니다.");
        }

        public void ServerStart()
        {
            while (m_bStop)
            {
                if (hClient.Connected)
                {
                    m_Stream = hClient.GetStream();
                    m_Read = new StreamReader(m_Stream);
                    m_Write = new StreamWriter(m_Stream);

                    thReader = new Thread(new ThreadStart(Receive));
                    thReader.Start();
                }
            }
        }

        public void ServerStop()
        {
            if (!m_bStop)
                return;

            server.Stop();
            CloseStreams();
            thReader.Abort();
            thHost.Abort();

            Console.WriteLine("서비스 종료");
        }

        public void Disconnect()
        {
            if (!m_bConnect)
                return;

            m_bConnect = false;
            CloseStreams();
            thReader.Abort();

            Console.WriteLine("상대방과 연결 중단");
        }

        private void CloseStreams()
        {
            m_Read.Close();
            m_Write.Close();
            m_Stream.Close();
        }

        public void Connect()
        {
            guest = new TcpClient();

            try
            {
                guest.Connect("127.0.0.1", port);
            }
            catch
            {
                Console.WriteLine("게스트 : 로컬 IP 연결 실패, hostAddr로 재시도");
                try
                {
                    guest.Connect(hostAddr, port);
                }
                catch
                {
                    m_bConnect = false;
                    Console.WriteLine("게스트 : hostAddr로 연결 실패");
                    return;
                }
            }

            m_bConnect = true;
            Console.WriteLine("게스트 : 서버에 연결");

            m_Stream = guest.GetStream();
            m_Read = new StreamReader(m_Stream);
            m_Write = new StreamWriter(m_Stream);

            thReader = new Thread(new ThreadStart(Receive));
            thReader.Start();
        }

        public void Receive()
        {
            try
            {
                while (m_bConnect)
                {
                    string szMessage = m_Read.ReadLine();

                    if (szMessage != null)
                        ProcessReceivedMessage(szMessage);

                }
            }
            catch
            {
                Console.WriteLine("데이터를 읽는 과정에서 오류가 발생");
            }
        }

        private void ProcessReceivedMessage(string message)
        {
            if (message.StartsWith("remain"))
            {
                string[] parts = message.Split(':');
                if (parts.Length == 2 && int.TryParse(parts[1].Trim(), out int remaining))
                {
                    // remaining 값을 처리
                    Console.WriteLine("남은 패: " + remaining);
                    // 여기에 remaining 값을 사용하는 로직을 추가합니다.
                }
            }
            else if (message.StartsWith("score"))
            {
                string[] parts = message.Split(':');
                if (parts.Length == 2 && int.TryParse(parts[1].Trim(), out int score))
                {
                    // score 값을 처리
                    Console.WriteLine("점수: " + score);
                    // 여기에 score 값을 사용하는 로직을 추가합니다.
                }
            }
            else
            {
                // 기타 메시지 처리
                Console.WriteLine("알 수 없는 메시지: " + message);
            }
        }


        void Send()
        {
            while (m_bConnect)
            {
                Thread.Sleep(500);
                try
                {
                    m_Write.WriteLine("remain : " + remainingCount);
                    m_Write.WriteLine("score : " + score);
                    m_Write.Flush();
              
                }
                catch
                {
                    Console.WriteLine("데이터 전송 실패");
                }
            }
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
                        graph[row - 1, col - 1] = pictureBox;
                        pictureBox.Click += new EventHandler(PictureBox_Click);
                    }
                    else
                    {
                        int row = ((i - 65) / 8) + 1;
                        int col = 9 + (i - 65) % 8;
                        graph[row - 1, col - 1] = pictureBox;
                        pictureBox.Click += new EventHandler(PictureBox_Click);
                    }
                }
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
            remainingCount = 0;
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
            Console.WriteLine("check for match : start");
            if (firstClicked.Tag != null && secondClicked.Tag != null &&
                firstClicked.Tag.ToString() == secondClicked.Tag.ToString())
            {
                Console.WriteLine("check for match : if");
                var path = CheckPathAndHide();
                if (path != null)
                {
                    Console.WriteLine("check for match : if1");
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

            }
            firstClicked.Padding = new Padding(0);
            firstClicked.BackColor = Color.Transparent;
            secondClicked.Padding = new Padding(0);
            secondClicked.BackColor = Color.Transparent;

            firstClicked = null;
            secondClicked = null;
            Console.WriteLine("check for match : return");
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}