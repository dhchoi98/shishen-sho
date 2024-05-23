namespace shishen_sho
{
    partial class InGame
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pBarTime = new System.Windows.Forms.ProgressBar();
            this.picPause = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picPause)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pBarTime
            // 
            this.pBarTime.BackColor = System.Drawing.SystemColors.Control;
            this.pBarTime.Location = new System.Drawing.Point(30, 17);
            this.pBarTime.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pBarTime.Name = "pBarTime";
            this.pBarTime.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.pBarTime.RightToLeftLayout = true;
            this.pBarTime.Size = new System.Drawing.Size(665, 31);
            this.pBarTime.TabIndex = 0;
            this.pBarTime.Value = 100;
            // 
            // picPause
            // 
            this.picPause.BackColor = System.Drawing.Color.Transparent;
            this.picPause.Image = global::shishen_sho.Properties.Resources.pause;
            this.picPause.Location = new System.Drawing.Point(734, 17);
            this.picPause.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.picPause.Name = "picPause";
            this.picPause.Size = new System.Drawing.Size(29, 31);
            this.picPause.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPause.TabIndex = 1;
            this.picPause.TabStop = false;
            this.picPause.Click += new System.EventHandler(this.picPause_Click);
            // 
            // InGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 639);
            this.Controls.Add(this.picPause);
            this.Controls.Add(this.pBarTime);
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "InGame";
            this.Text = "InGame";
            ((System.ComponentModel.ISupportInitialize)(this.picPause)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ProgressBar pBarTime;
        private System.Windows.Forms.PictureBox picPause;
    }
}