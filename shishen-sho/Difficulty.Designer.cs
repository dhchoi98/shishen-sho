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
            this.easybutton = new MetroFramework.Controls.MetroButton();
            this.normalbutton = new MetroFramework.Controls.MetroButton();
            this.hardbutton = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // easybutton
            // 
            this.easybutton.Location = new System.Drawing.Point(23, 95);
            this.easybutton.Name = "easybutton";
            this.easybutton.Size = new System.Drawing.Size(132, 66);
            this.easybutton.TabIndex = 0;
            this.easybutton.Text = "EASY";
            this.easybutton.UseSelectable = true;
            // 
            // normalbutton
            // 
            this.normalbutton.Location = new System.Drawing.Point(237, 95);
            this.normalbutton.Name = "normalbutton";
            this.normalbutton.Size = new System.Drawing.Size(132, 66);
            this.normalbutton.TabIndex = 0;
            this.normalbutton.Text = "NORMAL";
            this.normalbutton.UseSelectable = true;
            // 
            // hardbutton
            // 
            this.hardbutton.Location = new System.Drawing.Point(457, 95);
            this.hardbutton.Name = "hardbutton";
            this.hardbutton.Size = new System.Drawing.Size(132, 66);
            this.hardbutton.TabIndex = 0;
            this.hardbutton.Text = "HARD";
            this.hardbutton.UseSelectable = true;
            // 
            // Difficulty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 190);
            this.Controls.Add(this.hardbutton);
            this.Controls.Add(this.normalbutton);
            this.Controls.Add(this.easybutton);
            this.Name = "Difficulty";
            this.Text = "난이도";
            this.Load += new System.EventHandler(this.Difficulty_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroButton easybutton;
        private MetroFramework.Controls.MetroButton normalbutton;
        private MetroFramework.Controls.MetroButton hardbutton;
    }
}