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
    // 게임 중 일시정지 시 나타나는 폼.
    // 게임을 계속 진행하는 Continue, 게임을 다시 시작하는 Restart
    // main화면으로 돌아가는 Exit버튼으로 이루어짐.
    public partial class Pause : MetroFramework.Forms.MetroForm
    {
        public Pause()
        {
            InitializeComponent();

            //Dialog의 accept버튼을 continue버튼으로,
            //cancel버튼을 restart버튼으로 설정.
            this.AcceptButton=btnContinue;
            this.CancelButton = btnRestart;
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            //DialogResult를 OK로 설정하고 닫음.
            this.DialogResult=DialogResult.OK;
            this.Close();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            //난이도를 선택하는 창을 띄우고
            //DialogResult를 Cancel로 설정 후 닫음.
            Difficulty difficulty = new Difficulty();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
            difficulty.Show();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            //종료.
            Main main = new Main();
            Application.Exit();
        }
    }
}
