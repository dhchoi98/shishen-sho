namespace shishen_sho
{
    partial class Score
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
            this.ListViewScore = new MetroFramework.Controls.MetroListView();
            this.btnBack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ListViewScore
            // 
            this.ListViewScore.BackColor = System.Drawing.SystemColors.Window;
            this.ListViewScore.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.ListViewScore.FullRowSelect = true;
            this.ListViewScore.GridLines = true;
            this.ListViewScore.Location = new System.Drawing.Point(17, 77);
            this.ListViewScore.Name = "ListViewScore";
            this.ListViewScore.OwnerDraw = true;
            this.ListViewScore.Size = new System.Drawing.Size(526, 207);
            this.ListViewScore.TabIndex = 0;
            this.ListViewScore.UseCompatibleStateImageBehavior = false;
            this.ListViewScore.UseSelectable = true;
            // 
            // btnBack
            // 
            this.btnBack.Image = global::shishen_sho.Properties.Resources.back1;
            this.btnBack.Location = new System.Drawing.Point(498, 25);
            this.btnBack.Margin = new System.Windows.Forms.Padding(2);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(45, 47);
            this.btnBack.TabIndex = 3;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // Score
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 300);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.ListViewScore);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Score";
            this.Padding = new System.Windows.Forms.Padding(14, 40, 14, 13);
            this.Text = "Score";
            this.Load += new System.EventHandler(this.Score_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroListView ListViewScore;
        private System.Windows.Forms.Button btnBack;
    }
}