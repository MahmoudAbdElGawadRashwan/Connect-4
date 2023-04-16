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

    public partial class choosecolor : Form
    {
        public Color selected_color;
        public choosecolor()
        {
            InitializeComponent();
            colorDialog1.Color = Color.Red;
            selected_color = Color.Red;
        }

        private void Button4_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (colorDialog1.ShowDialog() == DialogResult.OK)
                {
                    selected_color = colorDialog1.Color;
                    button4.BackColor = selected_color;
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            GameManger.UpdatePlayer(selected_color);
            this.DialogResult = DialogResult.OK;
        }



        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Player_color_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

    }
}
