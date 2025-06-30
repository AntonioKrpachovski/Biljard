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
        public List<Ball> balls;
        public Form1()
        {
            InitializeComponent();
            balls = new List<Ball>();
            
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            balls.Add(new Ball(e.Location.X, e.Location.Y, "10", Color.Green));
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Ball ball in balls) {
                ball.Draw(e.Graphics);
            }
        }
    }
}
