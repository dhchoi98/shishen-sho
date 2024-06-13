using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace shishen_sho
{
    public partial class MultiPlay_InRoom : MetroFramework.Forms.MetroForm
    {
        private string username;
        private MultiPlay multiplay;
        private MultiPlay_ShowRoom showRoom;
        private bool host = false; // 방장일 때

        private UdpClient udpClient;
        private string hostIpAddress;
 
        private Thread listenThread;

        public MultiPlay_InRoom(string username, MultiPlay multiPlay) // 방장
        {
            InitializeComponent();
            this.username = username;
            this.multiplay = multiPlay;
            host = true;
            // 방 생성 후 브로드캐스트 메시지 전송
            StartBroadcasting();
        }
        public MultiPlay_InRoom(string username, MultiPlay_ShowRoom showRoom, string ipAddress) // 게스트
        {
            InitializeComponent();
            this.username = username;
            this.showRoom = showRoom;
            this.hostIpAddress = ipAddress;
        }

        private void StartBroadcasting()
        {
            Thread broadcastThread = new Thread(new ThreadStart(BroadcastHost));
            broadcastThread.IsBackground = true;
            broadcastThread.Start();
        }

        private void BroadcastHost()
        {

            UdpClient udpClient = new UdpClient();
            udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            udpClient.EnableBroadcast = true;
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Broadcast, 8888);
            byte[] message = Encoding.ASCII.GetBytes("I am the host");

            while (true)
            {
                udpClient.Send(message, message.Length, endPoint);
                Thread.Sleep(1000); // 1초마다 브로드캐스트
                Console.WriteLine("브로드 캐스트");
            }
        }
        private void MultiPlay_CreateRoom_Load(object sender, EventArgs e)
        {
            
        }

        private void metroLabel2_Click(object sender, EventArgs e)
        {

        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            multiplay.Show();
            this.Close();
        }
   
        private void metroButton2_Click(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
