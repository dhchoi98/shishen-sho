using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace shishen_sho
{
    public partial class MultiPlay_ShowRoom : MetroFramework.Forms.MetroForm
    {
        private string username;
        private MultiPlay multiplay;
        private UdpClient udpClient;
        private Thread listenThread;

        private List<string> discoveredHostsList = new List<string>();

        private bool room1 = false;
        private bool room2 = false;
        private bool room3 = false;

        public MultiPlay_ShowRoom()
        {
            InitializeComponent();
        }

        public MultiPlay_ShowRoom(string username, MultiPlay multiPlay)
        {
            InitializeComponent();
            this.username = username;
            this.multiplay = multiPlay;
            StartListening();
        }

        private void MultiPlay_CreateRoom_Load(object sender, EventArgs e)
        {
            metroLabel1.Text = username;
        }

        private void metroLabel5_Click(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            multiplay.Show();
            this.Close();
        }

        private void StartListening()
        {
            listenThread = new Thread(new ThreadStart(ListenForHosts));
            listenThread.IsBackground = true;
            listenThread.Start();
        }

        private void ListenForHosts()
        {
            udpClient = new UdpClient();
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 8888);
            udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);


            udpClient.ExclusiveAddressUse = false;
            udpClient.Client.Bind(endPoint);
            
            HashSet<string> discoveredHosts = new HashSet<string>();
            Console.WriteLine("함수시작");
            Console.WriteLine(discoveredHosts.Count);

            while (discoveredHosts.Count < 3)
            {
                //Console.WriteLine("while문 진입");
                byte[] data = udpClient.Receive(ref endPoint);
                string message = Encoding.ASCII.GetString(data);
          
                //Console.WriteLine("받기");
                Console.WriteLine(message);

                if (message == "I am the host" && !discoveredHosts.Contains(endPoint.Address.ToString()))
                {
                    Console.Out.WriteLine("호스트 발견"); 
                    discoveredHosts.Add(endPoint.Address.ToString());
                    discoveredHostsList.Add(endPoint.Address.ToString());

                    byte[] data2 = udpClient.Receive(ref endPoint);
                    string hostUserName = Encoding.ASCII.GetString(data2);

                    Console.WriteLine(endPoint.Address.ToString());

                    if (discoveredHosts.Count == 1)
                    {
                        room1 = true;
                        UpdateRoomStatus(hostUserName);
                    }
                    else if (discoveredHosts.Count == 2)
                    {
                        room2 = true;
                        UpdateRoomStatus(hostUserName);
                    }
                    else if (discoveredHosts.Count == 3)
                    {
                        room3 = true;
                        UpdateRoomStatus(hostUserName);
                    }
                }
            }
        }

        private void UpdateRoomStatus(string hostUserName)
        {
            // UI 스레드에서 UI 요소 업데이트
            this.Invoke((MethodInvoker)delegate
            {
                if (room1)
                {
                    room1Available.Text = "입장 가능";
                    nickname1.Text = hostUserName;
                    enterButton1.Enabled = true;
                }
                
                if (room2)
                {
                    room2Available.Text = "입장 가능";
                    enterButton2.Enabled = true;
                }
                if (room3)
                {
                    room3Available.Text = "입장 가능";
                    enterButton3.Enabled = true;
                }
            });
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {

        }

        private void enterButton1_Click(object sender, EventArgs e)
        {
            MultiPlay_InRoom inRoom = new MultiPlay_InRoom(nickname1.Text, username, this, discoveredHostsList[0]);
            listenThread.Abort();
            this.Hide();
            inRoom.Show();

        }

        private void metroPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

