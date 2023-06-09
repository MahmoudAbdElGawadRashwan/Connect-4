﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
   

    public partial class winORlose : Form
    {
        public static  int result;
        public static string winner;
        public winORlose()
        {

            InitializeComponent();
            ShowWinner();
        }
        public void ShowWinner()
        {
            if (result == 1)
            {
                label1.Text = "You Won !";
                pictureBox1.Image = global::Client.Properties.Resources.win;
            }
            else if (result==0)
            {
                label1.Text = "You Lost !";
                pictureBox1.Image = global::Client.Properties.Resources.lose;

            }

            else if (result == -1)
            {
                label1.Text = $"Game Over and {winner} was the Winner";
                pictureBox1.Image = global::Client.Properties.Resources.watch;
                button1.Text = "Watch Next Game"; 
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (result == -1)
            {
                GameBoard.currntGameboard.Close();
                DialogResult = DialogResult.Cancel;
            }
            else
            {
                if (lobby.currentroom.Host.Name != GameManger.CurrentPlayer.Name)
                {
                    GameManger.SendServerRequest(Flag.playAgain, "0");
                    GameBoard.currntGameboard.Close();
                    DialogResult = DialogResult.Cancel;
                }
                else
                {
                    GameBoard.currntGameboard.Close();
                    lobby.wait = new waiting();
                    lobby.wait.msg = "Waiting for somone to join \n so you can Play :)";
                    lobby.wait.Show();
                    GameManger.SendServerRequest(Flag.playAgain, "0");
                    DialogResult = DialogResult.Cancel;
                }
                
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (result == -1)
            {
                this.Close();
            }
            else
            {
                GameManger.SendServerRequest(Flag.playAgain, "1");
                DialogResult = DialogResult.OK;
            }
        }


        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }

        }
    }
}
