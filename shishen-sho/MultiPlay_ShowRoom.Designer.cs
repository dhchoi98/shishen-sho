namespace shishen_sho
{
    partial class MultiPlay_ShowRoom
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
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.winScore = new MetroFramework.Controls.MetroLabel();
            this.loseScore = new MetroFramework.Controls.MetroLabel();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.room1Available = new MetroFramework.Controls.MetroLabel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.enterButton1 = new MetroFramework.Controls.MetroButton();
            this.nickname1 = new MetroFramework.Controls.MetroLabel();
            this.metroButton2 = new MetroFramework.Controls.MetroButton();
            this.metroPanel2 = new MetroFramework.Controls.MetroPanel();
            this.room2Available = new MetroFramework.Controls.MetroLabel();
            this.metroLabel7 = new MetroFramework.Controls.MetroLabel();
            this.enterButton2 = new MetroFramework.Controls.MetroButton();
            this.metroLabel8 = new MetroFramework.Controls.MetroLabel();
            this.metroPanel3 = new MetroFramework.Controls.MetroPanel();
            this.room3Available = new MetroFramework.Controls.MetroLabel();
            this.metroLabel10 = new MetroFramework.Controls.MetroLabel();
            this.enterButton3 = new MetroFramework.Controls.MetroButton();
            this.metroLabel11 = new MetroFramework.Controls.MetroLabel();
            this.metroPanel1.SuspendLayout();
            this.metroPanel2.SuspendLayout();
            this.metroPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(476, 119);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(69, 19);
            this.metroLabel1.TabIndex = 0;
            this.metroLabel1.Text = "내 닉네임";
            // 
            // metroButton1
            // 
            this.metroButton1.Location = new System.Drawing.Point(476, 500);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(110, 43);
            this.metroButton1.TabIndex = 1;
            this.metroButton1.Text = "나가기";
            this.metroButton1.UseSelectable = true;
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(476, 174);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(30, 19);
            this.metroLabel2.TabIndex = 2;
            this.metroLabel2.Text = "승 :";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(476, 219);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(34, 19);
            this.metroLabel3.TabIndex = 3;
            this.metroLabel3.Text = "패 : ";
            // 
            // winScore
            // 
            this.winScore.AutoSize = true;
            this.winScore.Location = new System.Drawing.Point(527, 174);
            this.winScore.Name = "winScore";
            this.winScore.Size = new System.Drawing.Size(16, 19);
            this.winScore.TabIndex = 4;
            this.winScore.Text = "0";
            // 
            // loseScore
            // 
            this.loseScore.AutoSize = true;
            this.loseScore.Location = new System.Drawing.Point(527, 219);
            this.loseScore.Name = "loseScore";
            this.loseScore.Size = new System.Drawing.Size(16, 19);
            this.loseScore.TabIndex = 5;
            this.loseScore.Text = "0";
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.room1Available);
            this.metroPanel1.Controls.Add(this.metroLabel4);
            this.metroPanel1.Controls.Add(this.enterButton1);
            this.metroPanel1.Controls.Add(this.nickname1);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(40, 149);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(313, 91);
            this.metroPanel1.TabIndex = 6;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            this.metroPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.metroPanel1_Paint);
            // 
            // room1Available
            // 
            this.room1Available.AutoSize = true;
            this.room1Available.Location = new System.Drawing.Point(14, 48);
            this.room1Available.Name = "room1Available";
            this.room1Available.Size = new System.Drawing.Size(73, 19);
            this.room1Available.TabIndex = 5;
            this.room1Available.Text = "(입장불가)";
            this.room1Available.Click += new System.EventHandler(this.metroLabel5_Click);
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel4.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel4.Location = new System.Drawing.Point(128, 32);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(39, 25);
            this.metroLabel4.TabIndex = 4;
            this.metroLabel4.Text = "2/1";
            // 
            // enterButton1
            // 
            this.enterButton1.Enabled = false;
            this.enterButton1.Location = new System.Drawing.Point(214, 24);
            this.enterButton1.Name = "enterButton1";
            this.enterButton1.Size = new System.Drawing.Size(75, 43);
            this.enterButton1.TabIndex = 3;
            this.enterButton1.Text = "입장";
            this.enterButton1.UseSelectable = true;
            this.enterButton1.Click += new System.EventHandler(this.enterButton1_Click);
            // 
            // nickname1
            // 
            this.nickname1.AutoSize = true;
            this.nickname1.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.nickname1.Location = new System.Drawing.Point(14, 14);
            this.nickname1.Name = "nickname1";
            this.nickname1.Size = new System.Drawing.Size(83, 19);
            this.nickname1.TabIndex = 2;
            this.nickname1.Text = "유저 닉네임";
            // 
            // metroButton2
            // 
            this.metroButton2.Location = new System.Drawing.Point(476, 434);
            this.metroButton2.Name = "metroButton2";
            this.metroButton2.Size = new System.Drawing.Size(110, 41);
            this.metroButton2.TabIndex = 7;
            this.metroButton2.Text = "새로고침";
            this.metroButton2.UseSelectable = true;
            this.metroButton2.Click += new System.EventHandler(this.metroButton2_Click);
            // 
            // metroPanel2
            // 
            this.metroPanel2.Controls.Add(this.room2Available);
            this.metroPanel2.Controls.Add(this.metroLabel7);
            this.metroPanel2.Controls.Add(this.enterButton2);
            this.metroPanel2.Controls.Add(this.metroLabel8);
            this.metroPanel2.HorizontalScrollbarBarColor = true;
            this.metroPanel2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel2.HorizontalScrollbarSize = 10;
            this.metroPanel2.Location = new System.Drawing.Point(40, 303);
            this.metroPanel2.Name = "metroPanel2";
            this.metroPanel2.Size = new System.Drawing.Size(313, 91);
            this.metroPanel2.TabIndex = 8;
            this.metroPanel2.VerticalScrollbarBarColor = true;
            this.metroPanel2.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel2.VerticalScrollbarSize = 10;
            // 
            // room2Available
            // 
            this.room2Available.AutoSize = true;
            this.room2Available.Location = new System.Drawing.Point(14, 48);
            this.room2Available.Name = "room2Available";
            this.room2Available.Size = new System.Drawing.Size(73, 19);
            this.room2Available.TabIndex = 5;
            this.room2Available.Text = "(입장불가)";
            // 
            // metroLabel7
            // 
            this.metroLabel7.AutoSize = true;
            this.metroLabel7.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel7.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel7.Location = new System.Drawing.Point(128, 32);
            this.metroLabel7.Name = "metroLabel7";
            this.metroLabel7.Size = new System.Drawing.Size(39, 25);
            this.metroLabel7.TabIndex = 4;
            this.metroLabel7.Text = "2/1";
            // 
            // enterButton2
            // 
            this.enterButton2.Enabled = false;
            this.enterButton2.Location = new System.Drawing.Point(214, 24);
            this.enterButton2.Name = "enterButton2";
            this.enterButton2.Size = new System.Drawing.Size(75, 43);
            this.enterButton2.TabIndex = 3;
            this.enterButton2.Text = "입장";
            this.enterButton2.UseSelectable = true;
            // 
            // metroLabel8
            // 
            this.metroLabel8.AutoSize = true;
            this.metroLabel8.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel8.Location = new System.Drawing.Point(14, 14);
            this.metroLabel8.Name = "metroLabel8";
            this.metroLabel8.Size = new System.Drawing.Size(83, 19);
            this.metroLabel8.TabIndex = 2;
            this.metroLabel8.Text = "유저 닉네임";
            // 
            // metroPanel3
            // 
            this.metroPanel3.Controls.Add(this.room3Available);
            this.metroPanel3.Controls.Add(this.metroLabel10);
            this.metroPanel3.Controls.Add(this.enterButton3);
            this.metroPanel3.Controls.Add(this.metroLabel11);
            this.metroPanel3.HorizontalScrollbarBarColor = true;
            this.metroPanel3.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel3.HorizontalScrollbarSize = 10;
            this.metroPanel3.Location = new System.Drawing.Point(40, 452);
            this.metroPanel3.Name = "metroPanel3";
            this.metroPanel3.Size = new System.Drawing.Size(313, 91);
            this.metroPanel3.TabIndex = 9;
            this.metroPanel3.VerticalScrollbarBarColor = true;
            this.metroPanel3.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel3.VerticalScrollbarSize = 10;
            // 
            // room3Available
            // 
            this.room3Available.AutoSize = true;
            this.room3Available.Location = new System.Drawing.Point(14, 48);
            this.room3Available.Name = "room3Available";
            this.room3Available.Size = new System.Drawing.Size(73, 19);
            this.room3Available.TabIndex = 5;
            this.room3Available.Text = "(입장불가)";
            // 
            // metroLabel10
            // 
            this.metroLabel10.AutoSize = true;
            this.metroLabel10.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel10.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel10.Location = new System.Drawing.Point(128, 32);
            this.metroLabel10.Name = "metroLabel10";
            this.metroLabel10.Size = new System.Drawing.Size(39, 25);
            this.metroLabel10.TabIndex = 4;
            this.metroLabel10.Text = "2/1";
            // 
            // enterButton3
            // 
            this.enterButton3.Enabled = false;
            this.enterButton3.Location = new System.Drawing.Point(214, 24);
            this.enterButton3.Name = "enterButton3";
            this.enterButton3.Size = new System.Drawing.Size(75, 43);
            this.enterButton3.TabIndex = 3;
            this.enterButton3.Text = "입장";
            this.enterButton3.UseSelectable = true;
            // 
            // metroLabel11
            // 
            this.metroLabel11.AutoSize = true;
            this.metroLabel11.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.metroLabel11.Location = new System.Drawing.Point(14, 14);
            this.metroLabel11.Name = "metroLabel11";
            this.metroLabel11.Size = new System.Drawing.Size(83, 19);
            this.metroLabel11.TabIndex = 2;
            this.metroLabel11.Text = "유저 닉네임";
            // 
            // MultiPlay_ShowRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 611);
            this.Controls.Add(this.metroPanel3);
            this.Controls.Add(this.metroPanel2);
            this.Controls.Add(this.metroButton2);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.loseScore);
            this.Controls.Add(this.winScore);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.metroLabel1);
            this.Name = "MultiPlay_ShowRoom";
            this.Text = "방 조회";
            this.Load += new System.EventHandler(this.MultiPlay_CreateRoom_Load);
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            this.metroPanel2.ResumeLayout(false);
            this.metroPanel2.PerformLayout();
            this.metroPanel3.ResumeLayout(false);
            this.metroPanel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroButton metroButton1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel winScore;
        private MetroFramework.Controls.MetroLabel loseScore;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroLabel nickname1;
        private MetroFramework.Controls.MetroLabel room1Available;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroButton enterButton1;
        private MetroFramework.Controls.MetroButton metroButton2;
        private MetroFramework.Controls.MetroPanel metroPanel2;
        private MetroFramework.Controls.MetroLabel room2Available;
        private MetroFramework.Controls.MetroLabel metroLabel7;
        private MetroFramework.Controls.MetroButton enterButton2;
        private MetroFramework.Controls.MetroLabel metroLabel8;
        private MetroFramework.Controls.MetroPanel metroPanel3;
        private MetroFramework.Controls.MetroLabel room3Available;
        private MetroFramework.Controls.MetroLabel metroLabel10;
        private MetroFramework.Controls.MetroButton enterButton3;
        private MetroFramework.Controls.MetroLabel metroLabel11;
    }
}