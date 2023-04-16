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
    public partial class createRoom : Form
    {
        public string Roomname_new;
        public int board_hight;
        public int board_width;
        public Color Selected_color1;
        bool dragging;
        private Point lastLocation;

        public createRoom()
        {
            InitializeComponent();
            dragging = false;
            colorDialog1.Color = Color.Yellow;
            Selected_color1 = Color.Yellow;
        }

        private void Button5_MouseClick(object sender, MouseEventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                Selected_color1 = colorDialog1.Color;
                button5.BackColor = Selected_color1;
            }
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text=="")
            {
                MessageBox.Show("Please Fill Information");
            }
            else
            {
                Roomname_new = textBox1.Text;
                board_hight = int.Parse(comboBox1.Text == "H" ? "6" : comboBox1.Text) ;
                board_width = int.Parse(comboBox2.Text == "W" ? "7" : comboBox2.Text);
                GameManger.UpdatePlayer(Selected_color1);
                this.DialogResult = DialogResult.OK;
            }
        }


        private void Button6_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void CreateRoom_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragging = true;
                lastLocation = e.Location;
            }

        }

        private void CreateRoom_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void CreateRoom_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }
    }
}
