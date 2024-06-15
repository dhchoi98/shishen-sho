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
    public partial class MultiPlay_MessageBox : MetroFramework.Forms.MetroForm
    {
        public MultiPlay_MessageBox()
        {
            InitializeComponent();
            this.FormClosing += MultiPlay_MessageBox_FormClosing; // FormClosing 이벤트 추가
        }
        private void MultiPlay_MessageBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 폼이 닫히지 않도록 설정
            e.Cancel = true;
        }
        private void MultiPlay_MessageBox_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
