using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace shishen_sho
{
    public partial class MultiPlay : MetroFramework.Forms.MetroForm
    {
        Main main = null;
        public MultiPlay(Main main)
        {
            InitializeComponent();
            this.main = main;
        }


        private void MultiPlay_Load(object sender, EventArgs e)
        {

        }

        private void metroLabel1_Click(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            MultiPlay_InRoom inRoom = new MultiPlay_InRoom(textBox1.Text, this);
            this.Hide();
            inRoom.Show();
        }


     

        private void metroButton3_Click(object sender, EventArgs e)
        { 
            main.Show(); 
            this.Close();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            MultiPlay_ShowRoom showRoom = new MultiPlay_ShowRoom(textBox1.Text, this);
            this.Hide();
            showRoom.Show();
        }
    }
}
