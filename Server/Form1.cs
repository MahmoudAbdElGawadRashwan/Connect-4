using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.IO;
using System.Globalization;

namespace serverAppConnect4
{
    public partial class Server : Form
    {
        #region server variables
        byte[] bt = new byte[] { 127, 0, 0, 1 };
        IPAddress add;
        TcpListener server;
        Socket connection;

        static List<player> Allplayers = new List<player>();
        static List<room> allRooms = new List<room>();


        public enum request
        {
            receiveLoginInfo = 100,
            getPlayers = 210,
            getRooms = 220,
            createRoom = 310,
            joinRoom = 320,
            askToplay = 400,
            waitToPlay = 405,
            SendMove = 410,
            updateBoard = 420,
            gameEnded = 500,
            disconnectPlayer = 700
        }
        #endregion

        public Server()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void startServerBtn_Click(object sender, EventArgs e)
        {
            Task acceptingClients = new Task(NewClientListener);
            acceptingClients.Start();
        }
        public void NewClientListener()
        {
            add = new IPAddress(bt);
            server = new TcpListener(add, 2222);
            server.Start();
            
            MessageBox.Show("The Server has Started !");

            while (true)
            {
                connection = server.AcceptSocket();
                player tempPlayer = new player();
                tempPlayer.Nstream = new NetworkStream(connection);
                tempPlayer.Br = new BinaryReader(tempPlayer.Nstream);
                tempPlayer.Bw = new BinaryWriter(tempPlayer.Nstream);
                tempPlayer.ct = tempPlayer.tokenSource.Token;
                tempPlayer.PlayerThread = new Task(tempPlayer.playerHandling, tempPlayer.tokenSource.Token);
                tempPlayer.PlayerThread.Start();


            }
        }

        public static void checkName(player tempPlayer, string requestedName)
        {
            bool exists = false;
            foreach (player p in Allplayers)
            {
                if (p.Name == requestedName)
                {
                    exists = true;
                }
            }
            if (!exists)
            {
                tempPlayer.Name = requestedName;
                Allplayers.Add(tempPlayer);
                tempPlayer.Bw.Write("100,1");
                foreach (var player in Allplayers)
                {
                    player.Bw.Write(getPlayer());
                }
            }
            else
            {
                tempPlayer.Bw.Write("100,0");
            }
        }
        public static string getPlayer()
        {
            string lobbyinfo = "210";
            foreach (player p in Allplayers)
            {
                lobbyinfo += "," + p.Name + "+" + p.IsPlaying;
            }
            return lobbyinfo;
        }
        public static string getRooms()
        {
            string roomsData = "220";
            foreach (room r in allRooms)
            {
                roomsData += "," + r.RoomName;
                foreach (player p in r.RoomPlayers)
                {
                    roomsData += "+" + p.Name + "-" + p.IsPlaying + "-" + p.Color;
                }
            }
            return roomsData;
        }

        public static void createRoom(player roomOwner, string Color, string roomName, int row, int col)
        {
            room tempRoom = new room(roomOwner, roomName, row, col);
            roomOwner.MyRoom = tempRoom;
            roomOwner.Color = Color;
            allRooms.Add(tempRoom);

            foreach (var player in Allplayers)
            {
                player.Bw.Write(getRooms());
            }
            foreach (var player in Allplayers)
            {
                player.Bw.Write(getPlayer());
            }
        }

        public static int joinRoom(player askingPlayer, string roomName)
        {
            int retVal = -1;
            room requestedRoom = null;
            bool isFound = false;
            for (var i = 0; i < allRooms.Count && !isFound; i++)
            {
                if (allRooms[i].RoomName == roomName)
                {
                    requestedRoom = allRooms[i];
                    isFound = true;
                }
            }

            if (isFound == true)
            {
                if (requestedRoom.RoomPlayers.Count == 1)
                {
                    askingPlayer.IsPlaying = true;
                    requestedRoom.RoomPlayers.Add(askingPlayer);
                    askingPlayer.MyRoom = requestedRoom;
                    askingPlayer.Bw.Write("320,1");
                }
                else
                {
                    requestedRoom.RoomPlayers.Add(askingPlayer);
                    askingPlayer.MyRoom = requestedRoom;
                    retVal = 2;
                    askingPlayer.Bw.Write($"320,2,{requestedRoom.RoomPlayers[0].Color},{requestedRoom.RoomPlayers[1].Color},{requestedRoom.Rows}+{requestedRoom.Cols}");
                }
                foreach (var player in Allplayers)
                {
                    player.Bw.Write(getRooms());
                }
                foreach (var player in Allplayers)
                {
                    player.Bw.Write(getPlayer());
                }
            }
            return retVal;
        }


        public static void askToPlay(player askingPlayer, string Color)
        {
            room currentRoom = askingPlayer.MyRoom;
            player roomOwner = currentRoom.RoomPlayers[0];
            askingPlayer.Color = Color;
            string askingstr = "400," + askingPlayer.Name + "+" + askingPlayer.Color;
            roomOwner.Bw.Write(askingstr);
        }

        public static void sendMove(player moveSender, int x, int y)
        {
            room currentRoom = moveSender.MyRoom;
            currentRoom.Board[x, y] = (currentRoom.PlayerTurn == 1) ? 1 : 2;
            int winnerPlayer = currentRoom.checkWin(currentRoom.PlayerTurn);
            currentRoom.PlayerTurn = (currentRoom.PlayerTurn == 1) ? 2 : 1;
            updateBoared(currentRoom);
            if (winnerPlayer == 1 || winnerPlayer == 2)
            {
                endGame(moveSender);
            }
            updateBoared(currentRoom);
        }
        public static void updateBoared(room currentRoom)
        {
            string updateStr = $"410,{currentRoom.PlayerTurn},";
            for (int row = 0; row < currentRoom.Rows; row++)
            {
                for (int col = 0; col < currentRoom.Cols; col++)
                {
                    if (col < currentRoom.Cols - 1)
                        updateStr += currentRoom.Board[row, col] + "+";
                    else
                        updateStr += currentRoom.Board[row, col];
                }
                if (row < currentRoom.Rows - 1)
                    updateStr += ",";
            }
            foreach (player p in currentRoom.RoomPlayers)
            {
                p.Bw.Write(updateStr);
            }
        }
        public static void endGame(player winner)
        {
            room currentRoom = winner.MyRoom;
            int winnerIndex = currentRoom.RoomPlayers.IndexOf(winner);
            player loser;
            if (winnerIndex == 0)
            {
                loser = currentRoom.RoomPlayers[1];
            }
            else
            {
                loser = currentRoom.RoomPlayers[0];
            }
            for (var i = 0; i < currentRoom.RoomPlayers.Count; i++)
            {
                player currentPlayer = currentRoom.RoomPlayers[i];
                if (currentPlayer.Name == winner.Name)
                {
                    currentPlayer.Bw.Write("500,1");
                    currentPlayer.Score++;
                }
                else if (currentPlayer.Name == loser.Name)
                {
                    currentPlayer.Bw.Write("500,0");
                }
                else
                {
                    currentPlayer.Bw.Write("500,-1," + winner.Name);
                }
            }
            for(int i=0;i<currentRoom.Board.GetLength(0);i++)
            {
                for (int j = 0; j < currentRoom.Board.GetLength(1); j++)
                {
                    currentRoom.Board[i, j] = 0;
                }
            }
            currentRoom.PlayerTurn = (currentRoom.PlayerTurn == 1) ? 2 : 1;
        }

        public static void playAgain(player moveSender, int PlayAgain)
        {
            room currentRoom = moveSender.MyRoom;
            if (currentRoom.GameEnded == 0)
            {
                currentRoom.GameEnded++;
                if (PlayAgain == 1)
                {
                    moveSender.PlayAgain = true;
                }
            }
            else
            {
                if (PlayAgain == 1)
                {
                    moveSender.PlayAgain = true;
                }
                if (currentRoom.RoomPlayers[0].Name == moveSender.Name)
                {
                    player guestPlayer = currentRoom.RoomPlayers[1];
                    if (moveSender.PlayAgain)
                    {
                        if (guestPlayer.PlayAgain)
                        {
                            Server.updateBoared(currentRoom);
                        }
                        else
                        {
                            currentRoom.RoomPlayers[1].PlayAgain = false;
                            waitToPlay(moveSender, 0);
                        }
                    }
                    else
                    {
                        currentRoom.RoomPlayers[1].PlayAgain = false;
                        waitToPlay(moveSender, 0);
                    }

                }
                else
                {
                    player roomOwner = currentRoom.RoomPlayers[0];
                    if (moveSender.PlayAgain)
                    {
                        if (roomOwner.PlayAgain)
                        {
                            Server.updateBoared(currentRoom);
                        }
                        else
                        {
                            currentRoom.RoomPlayers[1].PlayAgain = false;
                            waitToPlay(moveSender, 0);
                        }
                    }
                    else
                    {
                        currentRoom.RoomPlayers[1].PlayAgain = false;
                        waitToPlay(moveSender, 0);
                    }
                }
                currentRoom.GameEnded = 0;
                currentRoom.RoomPlayers[0].PlayAgain = false;
                currentRoom.Board = new int[currentRoom.Rows, currentRoom.Cols];
            }
        }



        public static void leaveRoom(player player)
        {
            if (player.MyRoom != null && player.MyRoom.RoomPlayers.Count<2)
            {
                if (player.MyRoom.RoomPlayers[0].Name == player.Name)
                {
                    room currentRoom = player.MyRoom;
                    player.MyRoom = null;
                    player.Score = 0;
                    player.IsPlaying = false;
                    currentRoom.RoomPlayers.Remove(player);
                    allRooms.Remove(currentRoom);
                    foreach (var p in Allplayers)
                    {
                        p.Bw.Write(getRooms());
                    }
                    foreach (var p in Allplayers)
                    {
                        p.Bw.Write(getPlayer());
                    }
                }
            }
        }

        public static void disconnectPlayer(player player)
        {
            player.Br.Close();
            player.Bw.Close();
            player.Nstream.Close();
            player.tokenSource.Cancel();
            Allplayers.Remove(player);
            foreach (var p in Allplayers)
            {
                p.Bw.Write(getPlayer());
            }
        }
        
        public void UpdateList()
        {
            roomList.Items.Clear();
            for (int i = 0; i < allRooms.Count; i++)
            {
                if (allRooms[i].RoomPlayers.Count != 0)
                {
                    roomList.Items.Add(allRooms[i].RoomName);
                    foreach (player p in allRooms[i].RoomPlayers)
                    {
                        roomList.Items.Add("    " + p.Name);
                    }
                }
                else
                {
                    roomList.Items.Remove(allRooms[i]);
                }
            }
            playerList.Items.Clear();
            foreach (player p in Allplayers)
            {
                playerList.Items.Add(p.Name + " entered");
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateList();
        }

        private static string getFormattedCurrentDate()
        {
            DateTime localDate = DateTime.Now;
            return localDate.ToString(new CultureInfo("en-GB"));
        }

        private static void saveScore(player hostPlayer, player guestPlayer)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string path = Path.Combine(desktopPath, "GameScore.txt");
            string currentDate = getFormattedCurrentDate();
            string gameScore = $"{hostPlayer.Name}: {hostPlayer.Score}\t{guestPlayer.Name}: {guestPlayer.Score}\t Date: {currentDate} \n";
            File.AppendAllText(path, gameScore);
        }


        public static int waitToPlay(player roomOwner, int response)
        {
            room currentRoom = roomOwner.MyRoom;
            player askingPlayer = currentRoom.RoomPlayers[1];
            int retVal = -1;

            if (response == 1)
            {
                askingPlayer.Bw.Write($"405,1,{currentRoom.Rows}+{currentRoom.Cols},{roomOwner.Color}");
                retVal = 1;
            }
            else
            {
                saveScore(currentRoom.RoomPlayers[0], askingPlayer);
                askingPlayer.Bw.Write("405,0,0,0");
                askingPlayer.MyRoom = null;
                askingPlayer.Score = 0;
                askingPlayer.IsPlaying = false;
                currentRoom.RoomPlayers.Remove(askingPlayer);
                currentRoom.PlayerTurn = 1;
                int roomPlayersCount = currentRoom.RoomPlayers.Count;
                for (int i = 1; i < roomPlayersCount; i++)
                {
                    currentRoom.RoomPlayers[i].MyRoom = null;
                }
                for (int i = 1; i < roomPlayersCount; i++)
                {
                    currentRoom.RoomPlayers.RemoveAt(1);
                }
                foreach (var player in Allplayers)
                {
                    player.Bw.Write(getRooms());
                }
                foreach (var player in Allplayers)
                {
                    player.Bw.Write(getPlayer());
                }
            }
            return retVal;
        }

    }
    
    
}
