using Client.Popups;
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
    public partial class lobby : Form
    {
        public choosecolor join_game;     
        public createRoom create_room;    
        public RoomControl prev_select;
        public string Roomname_new;
        public int board_hight;
        public int board_width;
        public Form board;
        public static lobby mainlobby;
        public static GameBoard seegamebaord;
        public static waiting wait;
        public static Room currentroom;
        private bool dragging;
        private Point lastLocation;

        public lobby()  
        {
            InitializeComponent();
            mainlobby = this;
            dragging = false;
        }

        private void lobby_Load(object sender, EventArgs e)
        {
            CreateRoomBtn.Select();
            JoinRoomBtn.Enabled = false;
            SpectateBtn.Enabled = false;
            JoinRoomBtn.BackColor= Color.Gray;
            SpectateBtn.BackColor= Color.Gray;
        }

        private void CreateRoomBtn_Click(object sender, EventArgs e)
        {
            create_room = new createRoom();
            var dg = create_room.ShowDialog();

            if (dg == DialogResult.OK)
            {
                Roomname_new = create_room.Roomname_new;
                board_width = create_room.board_width;
                board_hight = create_room.board_hight;

                GameManger.SendServerRequest(Flag.createRoom,
                    GameManger.CurrentPlayer.Name+"+"+GameManger.CurrentPlayer.PlayerColor.ToArgb().ToString(),
                    Roomname_new, board_width.ToString() +"+"+ board_hight.ToString()
                    );
                GameBoard.rows = board_width;
                GameBoard.columns = board_hight;
                GameBoard.HostColor = create_room.Selected_color1;
                GameBoard.ChallangerColor = Color.Purple;
                GameBoard.turn = 1;
                GameBoard.playerTurn = 1;
                currentroom = new Room(Roomname_new, GameManger.CurrentPlayer);
                board = new GameBoard();
                wait = new waiting();
                wait.msg = "Waiting for somone to join \n so you can Play :)";
                wait.Show();
                JoinRoomBtn.Enabled = false;
                SpectateBtn.Enabled = false;
                JoinRoomBtn.BackColor = Color.Gray;
                SpectateBtn.BackColor = Color.Gray;
            }
        }

        private void CreateRoomBtn_MouseEnter(object sender, EventArgs e)
        {
            CreateRoomBtn.BackColor = Color.Blue;

        }

        private void CreateRoomBtn_MouseLeave(object sender, EventArgs e)
        {
            CreateRoomBtn.BackColor = Color.FromArgb(87, 131, 219);

        }


        public void showplayer()    
        {
            PlayersPanel.Controls.Clear();
            player[] playerlist = new player[GameManger.playerslist.Count];           
            for (int i = 0; i < playerlist.Length; i++)                                 
            {
            playerlist[i] = new player
            {
                playername = GameManger.playerslist[i].Name,              
                PlayerIsplaying = GameManger.playerslist[i].isplaying
            };

            PlayersPanel.Controls.Add(playerlist[i]);
            }
        }


        public void showroom()    
        {
            RoomsPanel.Controls.Clear();

            RoomControl[] roomlist = new RoomControl[GameManger.Rommslist.Count];         
            for (int i = 0; i < roomlist.Length; i++)                                       
            {
                roomlist[i] = new RoomControl();
                roomlist[i].roomname = GameManger.Rommslist[i].Name;
                roomlist[i].roomhost = GameManger.Rommslist[i].Host.Name;
                if (GameManger.Rommslist[i].challenger !=null)                              
                {
                    roomlist[i].NumberofPlayers = 2;
                }

                RoomsPanel.Controls.Add(roomlist[i]);
            }
        }

        public void RoomSelected ()
        {
            var cons = RoomsPanel.Controls;
            var selected = from RoomControl con in cons
                           where con.BackColor == Color.Silver
                           select con.TabIndex;
            currentroom = GameManger.Rommslist[selected.ElementAt(0)];
                
            if (GameManger.Rommslist[selected.ElementAt(0)].challenger == null)
            {
                JoinRoomBtn.BackColor = Color.FromArgb(51, 178, 73);
                JoinRoomBtn.Enabled = true;
                SpectateBtn.BackColor = Color.Gray;
                SpectateBtn.Enabled = false;
            }
            else
            {
                JoinRoomBtn.Enabled = false;
                SpectateBtn.Enabled = true;
                JoinRoomBtn.BackColor = Color.Gray;
                SpectateBtn.BackColor = Color.FromArgb(51, 178, 73);
            }
        }

        private void JoinRoomBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var cons = RoomsPanel.Controls;
                var selected = from RoomControl con in cons
                               where con.BackColor == Color.Silver
                               select con.TabIndex;
                currentroom = GameManger.Rommslist[selected.ElementAt(0)];
                if (selected.Count() == 0)
                {
                    message ms = new message();
                    ms.msg = "Pick a Room before \n joining";
                    DialogResult res = ms.ShowDialog();
                }
                else
                {
                    if (GameManger.Rommslist[selected.ElementAt(0)].challenger == null) 
                    {
                        SpectateBtn.BackColor = Color.Gray;
                        SpectateBtn.Enabled = false;

                        GameManger.SendServerRequest(Flag.joinRoom, GameManger.Rommslist[selected.ElementAt(0)].Name);
                        join_game = new choosecolor();
                        var dg = join_game.ShowDialog();
                        if (dg == DialogResult.OK)
                        {
                            GameManger.SendServerRequest(Flag.asktoplay, GameManger.CurrentPlayer.Name, GameManger.CurrentPlayer.PlayerColor.ToArgb().ToString());
                            wait = new waiting();
                            wait.Show();

                        }
                    }
                    else
                    {
                        message messg = new message();
                        messg.msg = "Room is Full! \nYou can spectate only!";
                        DialogResult res = messg.ShowDialog();
                    }
                }
            }
            catch
            {
                message ms = new message();
                ms.msg = "Pick a Room before \n joining";
                DialogResult res = ms.ShowDialog();
            }
        }

        private void JoinRoomBtn_MouseEnter(object sender, EventArgs e)
        {
            JoinRoomBtn.BackColor = Color.Green;
        }

        private void JoinRoomBtn_MouseLeave(object sender, EventArgs e)
        {
            JoinRoomBtn.BackColor = Color.FromArgb(51, 178, 73);
        }

 
        private void SpectateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var cons = RoomsPanel.Controls;
                var selected = from RoomControl con in cons
                               where con.BackColor == Color.Silver
                               select con.TabIndex;
                currentroom = GameManger.Rommslist[selected.ElementAt(0)];
                if (selected.Count() == 0)
                {
                    message ms = new message();
                    ms.msg = "Pick a Room before \n joining";
                    DialogResult res = ms.ShowDialog();

                }
                else
                {
                    GameManger.SendServerRequest(Flag.joinRoom, GameManger.Rommslist[selected.ElementAt(0)].Name);
                }
            }
            catch
            {
                message ms = new message();
                ms.msg = "Pick a Room before \n joining";
                DialogResult res = ms.ShowDialog();
            }
        }

        private void SpectateBtn_MouseEnter(object sender, EventArgs e)
        {
            SpectateBtn.BackColor = Color.Green;
        }

        private void SpectateBtn_MouseLeave(object sender, EventArgs e)
        {
            SpectateBtn.BackColor = Color.FromArgb(51, 178, 73);
        }


        private void CloseBtn_Click(object sender, EventArgs e)
        {
            GameManger.SendServerRequest(Flag.disconnect, "");
            GameManger.recieve.Wait(1000);
            Application.Exit();

        }

        private void CloseBtn_MouseEnter(object sender, EventArgs e)
        {
            CloseBtn.BackColor = Color.DarkRed;
        }

        private void CloseBtn_MouseLeave(object sender, EventArgs e)
        {
            CloseBtn.BackColor = Color.FromArgb(217,83,79);
        }

        private void Lobby_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                GameManger.SendServerRequest(Flag.disconnect, "");
            }
            catch (Exception)
            {

            }
            Application.Exit();
        }

        ~lobby()
        {

            (this.Parent as login).Close();
        }

        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragging = true;
                lastLocation = e.Location;
            }
        }

        private void Panel1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                this.Location = new Point((this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);
                this.Update();
            }
        }

    }
}
