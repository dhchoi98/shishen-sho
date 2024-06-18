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
            this.gameTitle = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            ((System.ComponentModel.ISupportInitialize)(this.gameTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnScore
            // 
            this.btnScore.BackColor = System.Drawing.Color.White;
            this.btnScore.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnScore.Location = new System.Drawing.Point(416, 533);
            this.btnScore.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnScore.Name = "btnScore";
            this.btnScore.Size = new System.Drawing.Size(154, 39);
            this.btnScore.TabIndex = 3;
            this.btnScore.Text = "점수판";
            this.btnScore.UseSelectable = true;
            this.btnScore.Click += new System.EventHandler(this.btnScore_Click);
            // 
            // btnMulti
            // 
            this.btnMulti.BackColor = System.Drawing.Color.White;
            this.btnMulti.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnMulti.Location = new System.Drawing.Point(416, 427);
            this.btnMulti.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnMulti.Name = "btnMulti";
            this.btnMulti.Size = new System.Drawing.Size(154, 44);
            this.btnMulti.TabIndex = 3;
            this.btnMulti.Text = "멀티 플레이";
            this.btnMulti.UseSelectable = true;
            this.btnMulti.Click += new System.EventHandler(this.btnMulti_Click_1);
            // 
            // btnExplain
            // 
            this.btnExplain.BackColor = System.Drawing.Color.White;
            this.btnExplain.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnExplain.Location = new System.Drawing.Point(416, 497);
            this.btnExplain.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnExplain.Name = "btnExplain";
            this.btnExplain.Size = new System.Drawing.Size(154, 37);
            this.btnExplain.TabIndex = 3;
            this.btnExplain.Text = "게임 설명";
            this.btnExplain.Theme = MetroFramework.MetroThemeStyle.Light;
            this.btnExplain.UseSelectable = true;
            this.btnExplain.Click += new System.EventHandler(this.btnExplain_Click_1);
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.White;
            this.btnStart.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.btnStart.Location = new System.Drawing.Point(416, 385);
            this.btnStart.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(154, 44);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "게임 시작";
            this.btnStart.UseCustomBackColor = true;
            this.btnStart.UseSelectable = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click_1);
            // 
            // gameTitle
            // 
            this.gameTitle.Image = global::shishen_sho.Properties.Resources.gameTitle;
            this.gameTitle.Location = new System.Drawing.Point(287, 47);
            this.gameTitle.Name = "gameTitle";
            this.gameTitle.Size = new System.Drawing.Size(384, 120);
            this.gameTitle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.gameTitle.TabIndex = 4;
            this.gameTitle.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::shishen_sho.Properties.Resources.background;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(989, 651);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // metroButton1
            // 
            this.metroButton1.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.metroButton1.Location = new System.Drawing.Point(939, 0);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(50, 50);
            this.metroButton1.TabIndex = 5;
            this.metroButton1.Text = "X";
            this.metroButton1.UseSelectable = true;
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 651);
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.gameTitle);
            this.Controls.Add(this.btnExplain);
            this.Controls.Add(this.btnMulti);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnScore);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximumSize = new System.Drawing.Size(989, 651);
            this.MinimumSize = new System.Drawing.Size(989, 651);
            this.Name = "Main";
            this.Padding = new System.Windows.Forms.Padding(14, 60, 14, 13);
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gameTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private MetroFramework.Controls.MetroButton btnScore;
        private MetroFramework.Controls.MetroButton btnStart;
        private MetroFramework.Controls.MetroButton btnMulti;
        private MetroFramework.Controls.MetroButton btnExplain;
        private System.Windows.Forms.PictureBox gameTitle;
        private MetroFramework.Controls.MetroButton metroButton1;
    }
}

