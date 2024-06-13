namespace shishen_sho
{
    partial class MultiPlay_InRoom
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.readyButton1 = new MetroFramework.Controls.MetroButton();
            this.metroButton2 = new MetroFramework.Controls.MetroButton();
            this.metroButton3 = new MetroFramework.Controls.MetroButton();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.readyButton2 = new MetroFramework.Controls.MetroButton();
            this.player1Name = new MetroFramework.Controls.MetroLabel();
            this.player2Name = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(23, 139);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(81, 19);
            this.metroLabel1.TabIndex = 0;
            this.metroLabel1.Text = "플레이어 1 :";
            // 
            // readyButton1
            // 
            this.readyButton1.Enabled = false;
            this.readyButton1.Location = new System.Drawing.Point(257, 129);
            this.readyButton1.Name = "readyButton1";
            this.readyButton1.Size = new System.Drawing.Size(92, 39);
            this.readyButton1.TabIndex = 1;
            this.readyButton1.Text = "준비";
            this.readyButton1.UseSelectable = true;
            this.readyButton1.Click += new System.EventHandler(this.readyButton1_Click);
            // 
            // metroButton2
            // 
            this.metroButton2.Location = new System.Drawing.Point(429, 139);
            this.metroButton2.Name = "metroButton2";
            this.metroButton2.Size = new System.Drawing.Size(144, 62);
            this.metroButton2.TabIndex = 2;
            this.metroButton2.Text = "게임시작";
            this.metroButton2.UseSelectable = true;
            // 
            // metroButton3
            // 
            this.metroButton3.Location = new System.Drawing.Point(429, 247);
            this.metroButton3.Name = "metroButton3";
            this.metroButton3.Size = new System.Drawing.Size(144, 65);
            this.metroButton3.TabIndex = 3;
            this.metroButton3.Text = "나가기";
            this.metroButton3.UseSelectable = true;
            this.metroButton3.Click += new System.EventHandler(this.metroButton3_Click);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(23, 293);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(83, 19);
            this.metroLabel2.TabIndex = 4;
            this.metroLabel2.Text = "플레이어 2 :";
            this.metroLabel2.Click += new System.EventHandler(this.metroLabel2_Click);
            // 
            // readyButton2
            // 
            this.readyButton2.Enabled = false;
            this.readyButton2.Location = new System.Drawing.Point(257, 285);
            this.readyButton2.Name = "readyButton2";
            this.readyButton2.Size = new System.Drawing.Size(92, 37);
            this.readyButton2.TabIndex = 5;
            this.readyButton2.Text = "준비";
            this.readyButton2.UseSelectable = true;
            this.readyButton2.Click += new System.EventHandler(this.readyButton2_Click);
            // 
            // player1Name
            // 
            this.player1Name.AutoSize = true;
            this.player1Name.Location = new System.Drawing.Point(141, 139);
            this.player1Name.Name = "player1Name";
            this.player1Name.Size = new System.Drawing.Size(37, 19);
            this.player1Name.TabIndex = 6;
            this.player1Name.Text = "이름";
            // 
            // player2Name
            // 
            this.player2Name.AutoSize = true;
            this.player2Name.Location = new System.Drawing.Point(141, 293);
            this.player2Name.Name = "player2Name";
            this.player2Name.Size = new System.Drawing.Size(37, 19);
            this.player2Name.TabIndex = 7;
            this.player2Name.Text = "이름";
            // 
            // MultiPlay_InRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(642, 403);
            this.Controls.Add(this.player2Name);
            this.Controls.Add(this.player1Name);
            this.Controls.Add(this.readyButton2);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroButton3);
            this.Controls.Add(this.metroButton2);
            this.Controls.Add(this.readyButton1);
            this.Controls.Add(this.metroLabel1);
            this.Name = "MultiPlay_InRoom";
            this.Text = "방 대기화면";
            this.Load += new System.EventHandler(this.MultiPlay_CreateRoom_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroButton readyButton1;
        private MetroFramework.Controls.MetroButton metroButton2;
        private MetroFramework.Controls.MetroButton metroButton3;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroButton readyButton2;
        private MetroFramework.Controls.MetroLabel player1Name;
        private MetroFramework.Controls.MetroLabel player2Name;
    }
}