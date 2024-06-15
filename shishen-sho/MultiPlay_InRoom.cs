using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace shishen_sho
{
    public partial class MultiPlay_InRoom : MetroFramework.Forms.MetroForm
    {
        private string hostname;
        private string guestname;

        private MultiPlay multiplay;
        private MultiPlay_ShowRoom showRoom;
        private bool host = false; // 방장일 때

        private bool p1Ready = false;
        private bool p2Ready = false;

        private UdpClient udpClient;
        private string hostIpAddress;

        private Thread broadcastThread;
        private Thread udpSendThread;

        private bool isBroadcasting = true; // 브로드캐스트 스레드 종료 플래그
        private bool isSendingGuest = true; // 게스트 스레드 종료 플래그

        public MultiPlay_InRoom(string username, MultiPlay multiPlay) // 방장
        {
            InitializeComponent();
            this.hostname = username;
            this.multiplay = multiPlay;
            host = true;
            readyButton1.Enabled = true;
            player1Name.Text = username;
            // 방 생성 후 브로드캐스트 메시지 전송
            StartBroadcasting();
        }
        public MultiPlay_InRoom(string hostname, string guestname, MultiPlay_ShowRoom showRoom, string ipAddress) // 게스트
        {
            InitializeComponent();
            this.hostname = hostname;
            this.guestname = guestname;
            this.showRoom = showRoom;
            this.hostIpAddress = ipAddress;
            readyButton2.Enabled = true;
            player2Name.Text = guestname;
            player1Name.Text = hostname; 
            StartGuest();
        }

        private void StartBroadcasting()
        {
            broadcastThread = new Thread(new ThreadStart(BroadcastHost));
            broadcastThread.IsBackground = true;
            broadcastThread.Start();
        }

        private void StartGuest()
        {
            udpSendThread = new Thread(new ThreadStart(udpSendGuest));
            udpSendThread.IsBackground = true;
            udpSendThread.Start();
        }

        private void BroadcastHost()
        {
            UdpClient udpClient = new UdpClient();
            UdpClient receiveUdpClient = new UdpClient();

            udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            udpClient.EnableBroadcast = true;
            receiveUdpClient.Client.ReceiveTimeout = 2000; // 수신 타임아웃 2초 설정

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Broadcast, 8888);
            
            byte[] message = Encoding.ASCII.GetBytes("I am the host");
            byte[] hostname = Encoding.ASCII.GetBytes(this.hostname);


            IPEndPoint receiveEndPoint = new IPEndPoint(IPAddress.Any, 8887);
            receiveUdpClient.Client.Bind(receiveEndPoint);

            while (isBroadcasting)
            {
                // 메시지 전송
                udpClient.Send(message, message.Length, endPoint);
                Thread.Sleep(1000); // 1초마다 브로드캐스트
                udpClient.Send(hostname, hostname.Length, endPoint);
                Thread.Sleep(1000);
                Console.WriteLine("브로드 캐스트");

                // 데이터 수신
                try
                {
                    byte[] receivedData = receiveUdpClient.Receive(ref receiveEndPoint);
                    string receivedMessage = Encoding.ASCII.GetString(receivedData);

                    Console.WriteLine("수신된 메시지: " + receivedMessage);

                    // 수신된 메시지 처리
                    if (receivedMessage.Equals("ready"))
                    {
                        p2Ready = true;
                        Console.WriteLine("게스트가 준비 완료 상태입니다.");
                        isBroadcasting = false;
                        UpdateRoomStatus();
                    }
                    else
                    {
                        // 추가 처리 필요 시 여기에 작성
                        this.guestname = receivedMessage;
                        UpdateRoomStatus();

                        Console.WriteLine("게스트 이름 수신: " + receivedMessage);
                    }

                }
                catch (SocketException ex)
                {
                    Console.WriteLine("수신 중 오류 발생: " + ex.Message);
                }
            }

            udpClient.Close();
        }


        private void udpSendGuest()
        {
            UdpClient udpClient = new UdpClient();
            udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            try
            {
                IPAddress hostIpAddressParsed = IPAddress.Parse(hostIpAddress); // 호스트 IP 주소 변환
                IPAddress localHostIpAddress = IPAddress.Parse("127.0.0.1"); // 로컬 호스트 IP 주소

                Console.WriteLine("Host IP Address: " + hostIpAddressParsed);
                IPEndPoint hostEndPoint = new IPEndPoint(hostIpAddressParsed, 8887);
                IPEndPoint localEndPoint = new IPEndPoint(localHostIpAddress, 8887);

                byte[] guestName = Encoding.ASCII.GetBytes(guestname);
                byte[] ready = Encoding.ASCII.GetBytes("ready");

                while (isSendingGuest)
                {
                    // 호스트 IP 주소로 전송
                    udpClient.Send(guestName, guestName.Length, hostEndPoint);

                    // 로컬 호스트 IP 주소로 전송
                    udpClient.Send(guestName, guestName.Length, localEndPoint);
                   
 
                    if (p2Ready)
                    {
                        // 준비 상태 메시지를 호스트와 로컬 호스트로 전송
                        udpClient.Send(ready, ready.Length, hostEndPoint);
                        udpClient.Send(ready, ready.Length, localEndPoint); 
                        Console.WriteLine("게스트 준비 완료 전송");
                    }
                    Thread.Sleep(1000);
                }

                udpClient.Close();
            }
            catch (FormatException ex)
            {
                Console.WriteLine("잘못된 IP 주소 형식: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("게스트 스레드 중 오류 발생: " + ex.Message);
            }
        }

        private void UpdateRoomStatus()
        {
            // UI 스레드에서 UI 요소 업데이트
            this.Invoke((MethodInvoker)delegate
            {
                player2Name.Text = guestname;

                if (p2Ready)
                {
                    readyButton2.Text = "준비완료";
                    if(p1Ready)
                        startButton.Enabled = true;
                }
                
            });
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            StopThreads();
            if (multiplay != null)
                multiplay.Show();
            else
                showRoom.Show();
            this.Close();
        }

        private void StopThreads()
        {
            if (broadcastThread != null && broadcastThread.IsAlive)
            {
                broadcastThread.Abort();
            }

            if (udpSendThread != null && udpSendThread.IsAlive)
            {
                udpSendThread.Abort();
            }
        }


        private void readyButton1_Click(object sender, EventArgs e)
        {
            if (readyButton1.Text.Equals("준비"))
            {
                readyButton1.Text = "준비완료";
                p1Ready = true;
            }
            else
            {
                readyButton1.Text = "준비";
                p1Ready = false;
            }
        }

        private void readyButton2_Click(object sender, EventArgs e)
        {
            if (readyButton2.Text.Equals("준비"))
            {
                readyButton2.Text = "준비완료";
                p2Ready = true;
            }
            else
            {
                readyButton2.Text = "준비";
                p2Ready = false;
            }
        }
        
        // 스타트 버튼
        private void metroButton2_Click(object sender, EventArgs e)
        {

        }

        private void MultiPlay_InRoom_Load(object sender, EventArgs e)
        {

        }
    }
}
