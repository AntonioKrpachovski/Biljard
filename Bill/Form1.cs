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
            foreach (Ball ball in scene.balls)
            {
                ball.Move();
            }
            if (!scene.firstHit)
            {
                scene.powerUp();
            }
            
            Invalidate();
        }
    }
}
