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
        public int time = 1200;
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
        private void startGame()
        {
            if (!scene.GameStarted)
            {
                scene.GameStarted = true;
                TimeLabel.Show();
                changeBgColor.Show();
                title.Hide();
                credits.Hide();
                pressToStart.Hide();
                Score.Show();
                ScoreLabel.Show();
                timer2.Start();
            }
        }
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            startGame();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            scene.Draw(e.Graphics, mousePos);
            if (!scene.GameStarted || scene.gameOverFlag)
            {
                scene.DrawTitleScreen(e.Graphics);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            /*scene.ScreenWidth = this.Width;
            scene.ScreenHeight = this.Height;
            Invalidate();*/
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer2.Stop();
            TimeLabel.Hide();
            changeBgColor.Hide();
            Score.Hide();
            ScoreLabel.Hide();
            restart.Hide();
            quit.Hide();
            Invalidate();
        }

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
            String text = scene.CheckGameOver(timeFlag);
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
                timer2.Stop();
                Score.Hide();
                ScoreLabel.Hide();
                changeBgColor.Hide();
                TimeLabel.Hide();
                title.Show();
                credits.Show();
                pressToStart.Show();
                restart.Show();
                quit.Show();

                if (text == "You won!")
                {
                    pressToStart.Text = "Score: " + (CalculateScore() + 2000).ToString();
                    title.Text = text;
                    credits.Text = "Thank you for playing!";
                }
                else if (text != "")
                {
                    title.Text = "You lost!";
                    credits.Text = text;
                    pressToStart.Text = "Score: " + (CalculateScore()).ToString();
                }
                
            }
            CalculateScore();
            Invalidate();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
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
            if (scene.gameOverFlag)
            {
                timer2.Stop();
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
                ScoreLabel.ForeColor = Color.WhiteSmoke;
                ScoreLabel.BackColor = Color.FromArgb(54, 52, 69);
                Score.BackColor = Color.FromArgb(54, 52, 69);
            }
            else
            {
                scene.DarkMode = false;
                BackColor = Color.WhiteSmoke;
                changeBgColor.BackColor = Color.Gainsboro;
                changeBgColor.ForeColor = Color.Black;
                TimeLabel.ForeColor = Color.Black;
                ScoreLabel.ForeColor = Color.Black;
                ScoreLabel.BackColor = Color.Gainsboro;
                Score.BackColor = Color.Gainsboro;
            }
            Invalidate();
        }
        private int CalculateScore()
        {
            int fallenScore = 0;
            for (int i = 0; i < 15; i++) {
                if (scene.balls[i].fallen)
                {
                    fallenScore += 200;
                }
            }
            int score = (4 - scene.failsCounter) * 1000 + time * 3 + fallenScore - 700;
            ScoreLabel.Text = "Score: " + score.ToString();
            return score;
         }

        private void quit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void restart_Click(object sender, EventArgs e)
        {
            restart.Hide();
            quit.Hide();
            scene.GameStarted = true;
            scene.gameOverFlag = false;
            TimeLabel.Show();
            changeBgColor.Show();
            title.Hide();
            credits.Hide();
            pressToStart.Hide();
            Score.Show();
            ScoreLabel.Show();
            time = 900;
            TimeLabel.Text = "15:00";
            timer2.Start();
            scene.populate();
            scene.failsCounter = 1;

            scene.mouseDown = false;
            scene.moving = false;
            scene.power = 0;
            scene.cueBallPlaced = false;
            scene.firstHit = true;
            scene.whiteBallJustFallen = false;
            scene.failsCounter = 0;
            scene.gameOverFlag = false;
            scene.ball8Fallen = false;
            scene.messageFlag = true;

            Invalidate();
        }

        private void title_Click(object sender, EventArgs e)
        {
            startGame();
        }

        private void credits_Click(object sender, EventArgs e)
        {
            startGame();
        }

        private void pressToStart_Click(object sender, EventArgs e)
        {
            startGame();
        }
    }
    
}
