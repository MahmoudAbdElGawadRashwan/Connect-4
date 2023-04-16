﻿using Client.Popups;
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
    public partial class GameBoard : Form
    {
        private Rectangle[] boardcolumns;
        public int[,] board;
        int x;
        int y;
        public static int turn;

         public static int playerTurn;
         public string player;
         public static int rows;
         public static int columns;

         public static Color HostColor;
         public static Color ChallangerColor; 
         public static Brush HostBrush;
         public static Brush ChallangerBrush;
         public static GameBoard currntGameboard;
         public static winORlose winandlose;


        public GameBoard()
        {
            InitializeComponent();
            this.boardcolumns = new Rectangle[columns];
            this.board = new int[rows, columns];

            label1.Text = GameManger.CurrentPlayer.Name;

            HostBrush = new SolidBrush(HostColor);
            ChallangerBrush = new SolidBrush(ChallangerColor);
            currntGameboard = this;
        }
        public void repaintBord()
        {
            Graphics g = this.CreateGraphics();

            for (int i = 0; i <rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (board[i,j] == 1)
                    {
                        g.FillEllipse(HostBrush, 32 + 48 * j, 32 + 48 * i, 32, 32);
                    }
                    else if (board[i, j] == 2)
                    {
                        g.FillEllipse(ChallangerBrush, 32 + 48 * j, 32 + 48 * i, 32, 32);
                    }
                    else if (board[i, j] == 0)
                    {
                        g.FillEllipse(Brushes.White, 32 + 48 * j, 32 + 48 * i, 32, 32);
                    }

                }
            }
            

        }



        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Blue, 24, 24, columns * 48,  rows * 48);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (i == 0)
                    {
                        this.boardcolumns[j] = new Rectangle(32 + 48 * j, 24, 32, rows* 48);
                    }
                    e.Graphics.FillEllipse(Brushes.White, (32 + 48 * j), (32 + 48 * i), 32, 32);

                }
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            int columnIndex = this.columNumber(e.Location);
            if (columnIndex != -1)
            {
                int rowindex = this.EmptyRow(columnIndex);
                if (rowindex != -1)
                {
                    this.board[rowindex, columnIndex] = turn; 
                

                    if (playerTurn == turn)
                    {
                        if (GameManger.CurrentPlayer.Name != label1.Text)
                        {
                            label1.ForeColor= Color.Green;
                        }
                        if (GameManger.CurrentPlayer.Name == label1.Text)
                        {
                            label1.ForeColor= Color.Red;
                        }
                        GameManger.SendServerRequest(Flag.SendMove, rowindex.ToString(), columnIndex.ToString());
                    }
                    else if (playerTurn == 3) 
                    {
                        message ms = new message();
                        ms.msg = " you are spectating the Game \n  you can't play";
                        DialogResult res = ms.ShowDialog();
                    }
                    else
                    {
                        message ms = new message();
                        ms.msg = " That is not your turn please \n wait for the Other player Move ";
                        DialogResult res = ms.ShowDialog();
                    }

                }
            }
        }

        private int columNumber(Point mouse)
        {
            for (int i = 0; i < this.boardcolumns.Length; i++)
            {
                
                if ((mouse.X >= this.boardcolumns[i].X) && (mouse.Y >= this.boardcolumns[i].Y))
                {
                    if ((mouse.X <= this.boardcolumns[i].X + this.boardcolumns[i].Width) && (mouse.Y <= this.boardcolumns[i].Y + this.boardcolumns[i].Height))
                    {
                        return i;
                    }
                }

            }
            return -1;
        }
        private int EmptyRow(int col)
        {
            for (int i = rows-1; i >= 0; i--)
            {
                if (this.board[i, col] == 0)
                {
                    return i;
                }
            }

            return -1;

        }

        private void GameBoard_FormClosing(object sender, FormClosingEventArgs e)
        {
            lobby.mainlobby.Show();
            GameManger.SendServerRequest(Flag.leaveRoom);
        }

        private void GameBoard_Load(object sender, EventArgs e)
        {

        }
    }
}
