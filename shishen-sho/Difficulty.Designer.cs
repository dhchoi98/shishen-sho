namespace shishen_sho
{
    partial class Difficulty
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnDifficult = new System.Windows.Forms.Button();
            this.btnMiddle = new System.Windows.Forms.Button();
            this.btnEasy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(342, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "난이도 선택";
            // 
            // btnDifficult
            // 
            this.btnDifficult.Location = new System.Drawing.Point(583, 226);
            this.btnDifficult.Name = "btnDifficult";
            this.btnDifficult.Size = new System.Drawing.Size(142, 111);
            this.btnDifficult.TabIndex = 6;
            this.btnDifficult.Text = "어려움";
            this.btnDifficult.UseVisualStyleBackColor = true;
            this.btnDifficult.Click += new System.EventHandler(this.btnDifficult_Click);
            // 
            // btnMiddle
            // 
            this.btnMiddle.Location = new System.Drawing.Point(324, 226);
            this.btnMiddle.Name = "btnMiddle";
            this.btnMiddle.Size = new System.Drawing.Size(142, 111);
            this.btnMiddle.TabIndex = 5;
            this.btnMiddle.Text = "보통";
            this.btnMiddle.UseVisualStyleBackColor = true;
            this.btnMiddle.Click += new System.EventHandler(this.btnMiddle_Click);
            // 
            // btnEasy
            // 
            this.btnEasy.Location = new System.Drawing.Point(76, 226);
            this.btnEasy.Name = "btnEasy";
            this.btnEasy.Size = new System.Drawing.Size(142, 111);
            this.btnEasy.TabIndex = 4;
            this.btnEasy.Text = "쉬움";
            this.btnEasy.UseVisualStyleBackColor = true;
            this.btnEasy.Click += new System.EventHandler(this.btnEasy_Click);
            // 
            // Difficulty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDifficult);
            this.Controls.Add(this.btnMiddle);
            this.Controls.Add(this.btnEasy);
            this.Name = "Difficulty";
            this.Text = "Difficulty";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDifficult;
        private System.Windows.Forms.Button btnMiddle;
        private System.Windows.Forms.Button btnEasy;
    }
}