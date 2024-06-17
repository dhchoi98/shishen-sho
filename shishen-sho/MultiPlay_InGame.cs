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
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace shishen_sho
{
    public partial class MultiPlay_InGame : MetroFramework.Forms.MetroForm
    {
        // 클릭한 PictureBox를 저장해주는 변수 2개
        private PictureBox firstClicked = null;
        private PictureBox secondClicked = null;
        private int totalTime;
        private int score;


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
            InitializePictureBoxEvents();
            // 타이머 1초마다 초기화
            gameTimer.Interval = 1000;
            gameTimer.Tick += GameTimer_Tick;
            // gameTimer.Start();

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

        // 게스트
        public MultiPlay_InGame(int minutes, string hostIpAddress)
        {
            InitializeComponent();
            InitializePictureBoxEvents();
            // 타이머 1초마다 초기화
            gameTimer.Interval = 1000;
            gameTimer.Tick += GameTimer_Tick;
            // gameTimer.Start();


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
                        Console.WriteLine("상대방 : " + szMessage);
                }
            }
            catch
            {
                Console.WriteLine("데이터를 읽는 과정에서 오류가 발생");
            }
        }

        void Send()
        {
            while (m_bConnect)
            {
                Thread.Sleep(500);
                try
                {
                    m_Write.WriteLine("남은패 : ");
                    m_Write.Flush();
                    Console.WriteLine(">>> : ");
                }
                catch
                {
                    Console.WriteLine("데이터 전송 실패");
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

        private void progressBar_Click(object sender, EventArgs e)
        {

        }
    }
}
