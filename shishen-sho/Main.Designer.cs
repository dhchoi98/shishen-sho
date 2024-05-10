namespace shishen_sho
{
    partial class Main
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnScore = new MetroFramework.Controls.MetroButton();
            this.btnMulti = new MetroFramework.Controls.MetroButton();
            this.btnExplain = new MetroFramework.Controls.MetroButton();
            this.btnStart = new MetroFramework.Controls.MetroButton();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnScore
            // 
            this.btnScore.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnScore.Location = new System.Drawing.Point(595, 820);
            this.btnScore.Name = "btnScore";
            this.btnScore.Size = new System.Drawing.Size(213, 47);
            this.btnScore.TabIndex = 3;
            this.btnScore.Text = "점수판";
            this.btnScore.UseSelectable = true;
            // 
            // btnMulti
            // 
            this.btnMulti.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnMulti.Location = new System.Drawing.Point(595, 658);
            this.btnMulti.Name = "btnMulti";
            this.btnMulti.Size = new System.Drawing.Size(213, 47);
            this.btnMulti.TabIndex = 3;
            this.btnMulti.Text = "멀티 플레이";
            this.btnMulti.UseSelectable = true;
            this.btnMulti.Click += new System.EventHandler(this.btnMulti_Click_1);
            // 
            // btnExplain
            // 
            this.btnExplain.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnExplain.Location = new System.Drawing.Point(595, 758);
            this.btnExplain.Name = "btnExplain";
            this.btnExplain.Size = new System.Drawing.Size(213, 47);
            this.btnExplain.TabIndex = 3;
            this.btnExplain.Text = "게임 설명";
            this.btnExplain.UseSelectable = true;
            this.btnExplain.Click += new System.EventHandler(this.btnExplain_Click_1);
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.White;
            this.btnStart.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnStart.Location = new System.Drawing.Point(595, 590);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(213, 47);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "게임 시작";
            this.btnStart.UseSelectable = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click_1);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.White;
            this.lblTitle.Font = new System.Drawing.Font("한컴 울주 반구대 암각화체", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTitle.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblTitle.Location = new System.Drawing.Point(456, 197);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(493, 111);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "사천성 게임";
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::shishen_sho.Properties.Resources.background;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1412, 997);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1413, 995);
            this.Controls.Add(this.btnExplain);
            this.Controls.Add(this.btnMulti);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnScore);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Main";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox pictureBox1;
        private MetroFramework.Controls.MetroButton btnScore;
        private MetroFramework.Controls.MetroButton btnStart;
        private MetroFramework.Controls.MetroButton btnMulti;
        private MetroFramework.Controls.MetroButton btnExplain;
    }
}

