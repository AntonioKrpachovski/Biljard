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
        public Form1()
        {
            
            InitializeComponent();
            this.Height = 750;
            this.Width = 1300;
            scene = new Scene(this.Width, this.Height);
            Invalidate();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false; // disables maximize button
            

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            scene.Draw(e.Graphics);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            /*scene.ScreenWidth = this.Width;
            scene.ScreenHeight = this.Height;
            Invalidate();*/
        }
    }
}
