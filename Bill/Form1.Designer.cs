namespace Bill
{
    partial class Form1
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
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.TimeLabel = new System.Windows.Forms.Label();
            this.changeBgColor = new System.Windows.Forms.Button();
            this.ScoreLabel = new System.Windows.Forms.Label();
            this.pressToStart = new System.Windows.Forms.Label();
            this.credits = new System.Windows.Forms.Label();
            this.title = new System.Windows.Forms.Label();
            this.Score = new System.Windows.Forms.Button();
            this.restart = new System.Windows.Forms.Button();
            this.quit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 40;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // TimeLabel
            // 
            this.TimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeLabel.Location = new System.Drawing.Point(1545, 11);
            this.TimeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(151, 55);
            this.TimeLabel.TabIndex = 0;
            this.TimeLabel.Text = "15:00";
            // 
            // changeBgColor
            // 
            this.changeBgColor.BackColor = System.Drawing.Color.Gainsboro;
            this.changeBgColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changeBgColor.Location = new System.Drawing.Point(25, 783);
            this.changeBgColor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.changeBgColor.Name = "changeBgColor";
            this.changeBgColor.Size = new System.Drawing.Size(304, 75);
            this.changeBgColor.TabIndex = 1;
            this.changeBgColor.Text = "Change Theme";
            this.changeBgColor.UseVisualStyleBackColor = false;
            this.changeBgColor.Click += new System.EventHandler(this.changeBgColor_Click);
            // 
            // ScoreLabel
            // 
            this.ScoreLabel.AutoSize = true;
            this.ScoreLabel.BackColor = System.Drawing.Color.Gainsboro;
            this.ScoreLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScoreLabel.Location = new System.Drawing.Point(1445, 805);
            this.ScoreLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ScoreLabel.Name = "ScoreLabel";
            this.ScoreLabel.Size = new System.Drawing.Size(96, 32);
            this.ScoreLabel.TabIndex = 2;
            this.ScoreLabel.Text = "Score:";
            // 
            // pressToStart
            // 
            this.pressToStart.BackColor = System.Drawing.Color.Transparent;
            this.pressToStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pressToStart.Location = new System.Drawing.Point(-1, 375);
            this.pressToStart.Name = "pressToStart";
            this.pressToStart.Size = new System.Drawing.Size(1713, 91);
            this.pressToStart.TabIndex = 8;
            this.pressToStart.Text = "Click To Start";
            this.pressToStart.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // credits
            // 
            this.credits.BackColor = System.Drawing.Color.Transparent;
            this.credits.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.credits.Location = new System.Drawing.Point(-1, 318);
            this.credits.Name = "credits";
            this.credits.Size = new System.Drawing.Size(1713, 36);
            this.credits.TabIndex = 7;
            this.credits.Text = "Ана Мацановиќ 231178, Антонио Крпачовски 231024, Лина Анѓелковска 231123";
            this.credits.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // title
            // 
            this.title.BackColor = System.Drawing.Color.Transparent;
            this.title.Font = new System.Drawing.Font("Microsoft Sans Serif", 100.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.title.Location = new System.Drawing.Point(-2, 128);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(1714, 190);
            this.title.TabIndex = 6;
            this.title.Text = "Billiard";
            this.title.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Score
            // 
            this.Score.BackColor = System.Drawing.Color.Gainsboro;
            this.Score.Enabled = false;
            this.Score.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Score.Location = new System.Drawing.Point(1376, 783);
            this.Score.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Score.Name = "Score";
            this.Score.Size = new System.Drawing.Size(304, 75);
            this.Score.TabIndex = 9;
            this.Score.UseVisualStyleBackColor = false;
            // 
            // restart
            // 
            this.restart.BackColor = System.Drawing.Color.Gainsboro;
            this.restart.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.restart.Location = new System.Drawing.Point(502, 468);
            this.restart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.restart.Name = "restart";
            this.restart.Size = new System.Drawing.Size(304, 75);
            this.restart.TabIndex = 11;
            this.restart.Text = "Play Again";
            this.restart.UseVisualStyleBackColor = false;
            this.restart.Click += new System.EventHandler(this.restart_Click);
            // 
            // quit
            // 
            this.quit.BackColor = System.Drawing.Color.Gainsboro;
            this.quit.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quit.Location = new System.Drawing.Point(916, 468);
            this.quit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.quit.Name = "quit";
            this.quit.Size = new System.Drawing.Size(304, 75);
            this.quit.TabIndex = 12;
            this.quit.Text = "Quit Game";
            this.quit.UseVisualStyleBackColor = false;
            this.quit.Click += new System.EventHandler(this.quit_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1712, 875);
            this.Controls.Add(this.quit);
            this.Controls.Add(this.restart);
            this.Controls.Add(this.pressToStart);
            this.Controls.Add(this.credits);
            this.Controls.Add(this.title);
            this.Controls.Add(this.ScoreLabel);
            this.Controls.Add(this.changeBgColor);
            this.Controls.Add(this.TimeLabel);
            this.Controls.Add(this.Score);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Billiard";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.Button changeBgColor;
        private System.Windows.Forms.Label ScoreLabel;
        private System.Windows.Forms.Label pressToStart;
        private System.Windows.Forms.Label credits;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Button Score;
        private System.Windows.Forms.Button restart;
        private System.Windows.Forms.Button quit;
    }
}

