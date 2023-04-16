using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class waiting : Form
    {
        public string msg { get { return label1.Text; } set { label1.Text = value; } }
        private bool dragging;
        private Point lastLocation;
        public waiting()
        {
            InitializeComponent();
        }



        private void Waiting_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragging = true;
                lastLocation = e.Location;
            }
        }

        private void Waiting_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                this.Location = new Point((this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);
                this.Update();
            }
        }

        private void Waiting_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }



        private void Label3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
