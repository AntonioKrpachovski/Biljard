using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bill
{
    public partial class Form1 : Form
    {
        public Scene scene;
        public Point mousePos;
        public int time = 900;
        public bool timeFlag = false;
        public Form1()
        {
            
            InitializeComponent();
            DoubleBuffered = true;
            this.Height = 750;
            this.Width = 1300;
            scene = new Scene(this.Width, this.Height);
            Invalidate();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false; // disables maximize button, napraveno so pomosh na ChatGPT
            timer1.Start();
            timer2.Start();

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e) {}

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            scene.Draw(e.Graphics, mousePos);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            /*scene.ScreenWidth = this.Width;
            scene.ScreenHeight = this.Height;
            Invalidate();*/
        }

        private void Form1_Load(object sender, EventArgs e) {}

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            mousePos = e.Location;
            this.Invalidate();
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            scene.mouseDown = false;
            if (!scene.cueBallPlaced) scene.Strike(mousePos);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            scene.mouseDown = true;
            scene.cueBallPlaced = scene.placeCueBall(mousePos);
            Invalidate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            scene.HandleBallCollisions();
            scene.CheckIfMoving();
            scene.CheckGameOver(timeFlag);
            foreach (Ball ball in scene.balls)
            {
                ball.Move();
            }
            if (!scene.firstHit)
            {
                scene.powerUp();
            }
            if (scene.gameOverFlag)
            {
                this.Close();
            }
            CalculateScore();
            Invalidate();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (scene.failsCounter >= 4)
            {
                timer2.Stop();
            }
            if (time > 0)
            {
                time -= 1;
                int seconds = time % 60;
                int minutes = time / 60;
                StringBuilder sbSeconds = new StringBuilder();
                if (seconds < 10)
                {
                    sbSeconds.Append("0");
                }
                sbSeconds.Append(seconds.ToString());
                StringBuilder sbMinutes = new StringBuilder();
                if (minutes < 10)
                {
                    sbMinutes.Append("0");
                }
                sbMinutes.Append(minutes.ToString());
                TimeLabel.Text = sbMinutes.ToString() + ":" + sbSeconds.ToString();
            }
            if (time == 0)
            {
                timeFlag = true;
            }
        }

        private void changeBgColor_Click(object sender, EventArgs e)
        {
            if (BackColor == Color.WhiteSmoke)
            {
                scene.DarkMode = true;
                BackColor = Color.FromArgb(30, 30, 35);
                changeBgColor.BackColor = Color.FromArgb(54, 52, 69);
                changeBgColor.ForeColor = Color.WhiteSmoke;
                TimeLabel.ForeColor = Color.WhiteSmoke;
            }
            else
            {
                scene.DarkMode = false;
                BackColor = Color.WhiteSmoke;
                changeBgColor.BackColor = Color.Gainsboro;
                changeBgColor.ForeColor = Color.Black;
                TimeLabel.ForeColor = Color.Black;
            }
            Invalidate();
        }
        private void CalculateScore()
        {
            int fallenScore = 0;
            for (int i = 0; i < 15; i++) {
                if (scene.balls[i].fallen)
                {
                    fallenScore += 100;
                }
            }
            int score = (4 - scene.failsCounter) * 1000 + time + fallenScore;
            ScoreLabel.Text = "Score: " + score.ToString();
         }
    }
    
}
