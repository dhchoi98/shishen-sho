using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace shishen_sho
{
    public partial class Difficulty : MetroFramework.Forms.MetroForm
    {
        public Difficulty()
        {
            InitializeComponent();
            //일단 타이머 난이도는 나중에 저희가 직접 해보고 제한시간을 정해봅시다!
            easybutton.Click += (sender, e) => StartGame(15); // 15분
            normalbutton.Click += (sender, e) => StartGame(10); // 10분
            hardbutton.Click += (sender, e) => StartGame(5); // 5분
        }

        private void StartGame(int minutes)
        {
            // 폼 숨김
            this.Hide();
            // 게임 폼을 해당 시간으로 설정하여 열기 + 변수 minutes 로 통제
            InGame gameForm = new InGame(minutes);
            gameForm.Show();
        }
        private void Difficulty_Load(object sender, EventArgs e)
        {

        }
    }
}
