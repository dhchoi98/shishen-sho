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
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnMulti = new System.Windows.Forms.Button();
            this.btnExplain = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("한컴 울주 반구대 암각화체", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTitle.Location = new System.Drawing.Point(274, 137);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(493, 111);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "사천성 게임";
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("함초롬돋움", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnStart.Location = new System.Drawing.Point(427, 295);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(174, 73);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "게임 시작";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnMulti
            // 
            this.btnMulti.Font = new System.Drawing.Font("함초롬돋움", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnMulti.Location = new System.Drawing.Point(427, 408);
            this.btnMulti.Name = "btnMulti";
            this.btnMulti.Size = new System.Drawing.Size(174, 73);
            this.btnMulti.TabIndex = 1;
            this.btnMulti.Text = "멀티 플레이";
            this.btnMulti.UseVisualStyleBackColor = true;
            this.btnMulti.Click += new System.EventHandler(this.btnMulti_Click);
            // 
            // btnExplain
            // 
            this.btnExplain.Font = new System.Drawing.Font("함초롬돋움", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnExplain.Location = new System.Drawing.Point(427, 518);
            this.btnExplain.Name = "btnExplain";
            this.btnExplain.Size = new System.Drawing.Size(174, 73);
            this.btnExplain.TabIndex = 1;
            this.btnExplain.Text = "게임 설명";
            this.btnExplain.UseVisualStyleBackColor = true;
            this.btnExplain.Click += new System.EventHandler(this.btnExplain_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 742);
            this.Controls.Add(this.btnExplain);
            this.Controls.Add(this.btnMulti);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblTitle);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Main";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnMulti;
        private System.Windows.Forms.Button btnExplain;
    }
}

