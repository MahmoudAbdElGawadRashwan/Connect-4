﻿using Client.Popups;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{

    public static class GameManger
    {

        static string ip;
        static int port;
        public static bool connStatues;
        static string UserName;


        static  IPAddress ServerIP;
        static  TcpClient server;
        static  NetworkStream ConnectionStream;
        static  BinaryReader br;
        static  BinaryWriter bwr;
        public static Task recieve;

        public static List<Player> playerslist;
        public static List<Room> Rommslist;

        public static Player CurrentPlayer;
        public static Room CurrentRoom;

     

        static GameManger()
        {
            ip = "127.0.0.1";
            port = 2222;
            connStatues = false;
            ServerIP =  IPAddress.Parse(ip);

            playerslist = new List<Player>();
            Rommslist = new List<Room>();
        }
   
        public static void Login(string userName)
        {
    

            try
            {
                TcpClient server = new TcpClient();
                server.Connect(ServerIP, port);
                ConnectionStream = server.GetStream();
                br = new BinaryReader(ConnectionStream);
                bwr = new BinaryWriter(ConnectionStream);

                SendServerRequest(Flag.sendLoginInfo, userName);
                UserName = userName;

            }
            catch (Exception e)
            {
            
                throw e;
            }

           



        }
        public static void SendServerRequest(Flag flag,params string[] data)
        {
            var f = (int)flag;
            string msg = f.ToString();
           

            if (data.Length > 0)
            {
                foreach (var item in data)
                {
                    msg += "," + item;

                }
            }
            bwr.Write(msg);
        }

        public static bool isloginSuc(string userName)
        {
            var msg = br.ReadString();
            var msgArray = msg.Split(',');
            Flag flag = (Flag)int.Parse(msgArray[0]);
            var data = msgArray.ToList();
            data.RemoveAt(0);
            if (data.ElementAt(0) == "1")
            {
                UserName = userName;


                connStatues = true;

                return true;
            }
            else
            {
                MessageBox.Show("Name already taken");
     
                return false;
            }
        }

        public static void ReceiveServerRequest()
        {
            var msg = br.ReadString();
            var msgArray = msg.Split(',');
            Flag flag = (Flag)int.Parse(msgArray[0]);
            var data = msgArray.ToList();
            data.RemoveAt(0);

            switch (flag)
            {
                case Flag.getPlayers:
                     playerslist = Getplayers(data);
                    lobby.mainlobby.Invoke(new MethodInvoker(delegate ()
                    {
                        lobby.mainlobby.showplayer();
                    }));
                    break;

                case Flag.getRooms:
                    Rommslist = GetRooms(data);
                    lobby.mainlobby.Invoke(new MethodInvoker(delegate ()
                    {
                        lobby.mainlobby.showroom();
                    }));
                    break;

                case Flag.waittopaly:
                    playgame(data.ElementAt(0), data.ElementAt(1), data.ElementAt(2));
                    break;
                case Flag.createRoom:
                    break;
                case Flag.joinRoom:
                    if (data.ElementAt(0) == "2")
                    {
                        joinASspectator(data.ElementAt(1), data.ElementAt(2), data.ElementAt(3));
                    }
                   
                    break;
                case Flag.SendMove:
                    GameBoard.turn = int.Parse(data.ElementAt(0));
                    data.RemoveAt(0);
                    updateBoard(data);
                    break;
                case Flag.updateBoard:
                    break;
                case Flag.asktoplay:                  
                    acceptTheChallenger(data[0]);
                    GameBoard.ChallangerColor = Color.FromArgb(Int32.Parse(data[0].Split('+')[1]));
                    GameBoard.ChallangerBrush = new SolidBrush(GameBoard.ChallangerColor);
                    
                    break;
                case Flag.gameResult:
                    showWinningMesg(data);
                        break;
                default:
                    break;
            }
        
            ReceiveServerRequest();

        }

        private static void joinASspectator(string hostColor,string ChallngerColor,string size)
        {
            var sizear = size.Split('+');
            lobby.mainlobby.Invoke(new MethodInvoker(delegate ()
            {
               
              
                message ms = new message();
                ms.msg = "YOU ARE NOW SPECTATING \n THE GAME";
                DialogResult res = ms.ShowDialog();
                GameBoard.columns = int.Parse(sizear[1]);
                GameBoard.rows = int.Parse(sizear[0]);
                GameBoard.HostColor = Color.FromArgb(Int32.Parse(hostColor));
                GameBoard.ChallangerColor = Color.FromArgb(Int32.Parse(ChallngerColor));
                GameBoard.turn = 0;
                GameBoard.playerTurn = 3;

                lobby.seegamebaord = new GameBoard();
                lobby.seegamebaord.Show();
            }));
        }
        private static void acceptTheChallenger(string data)
        {
            
            acceptTheChallenger dlg = new acceptTheChallenger();
            string[] arr = data.Split('+');
            dlg.challengerLabel = $"{arr[0]} wants to challange you ";
            DialogResult ownerResponse = dlg.ShowDialog();

            if (ownerResponse == DialogResult.OK)
            {
                SendServerRequest(Flag.waittopaly, "1");
                lobby.mainlobby.Invoke(new MethodInvoker(delegate ()
                {
                    message ms = new message();
                    ms.msg = $"  You are currently playing against: {arr[0]}";
                    lobby.wait.Close();
                    lobby.mainlobby.board.Show();

                }));
            }
            else
            {
                SendServerRequest(Flag.waittopaly, "0");
            }
        }

        private static void showWinningMesg(List<string> data)
        {

            switch (data[0])
            {
                case "0":
                    GameBoard.currntGameboard.Invoke(new MethodInvoker(delegate ()
                    {
                        winORlose.result = 0;
                        GameBoard.winandlose = new winORlose();
                        GameBoard.winandlose.ShowDialog();
                    }));
                    break;
                case "1":
                    GameBoard.currntGameboard.Invoke(new MethodInvoker(delegate ()
                    {
                        winORlose.result = 1;
                        GameBoard.winandlose = new winORlose();
                        GameBoard.winandlose.ShowDialog();
                    }));
                    break;
                case "-1":
                    GameBoard.currntGameboard.Invoke(new MethodInvoker(delegate ()
                    {
                        winORlose.result = -1;
                        winORlose.winner = data[1];
                        GameBoard.winandlose = new winORlose();
                        GameBoard.winandlose.ShowDialog();
                    }));
                    break;
                default:
                    break;
            }
        }


        private static void playgame(string response, string size,string hostcolor)
        {
            if (int.Parse(response) == 1)
            {
                var sizear = size.Split('+');
                lobby.mainlobby.Invoke(new MethodInvoker(delegate ()
                {
                    lobby.wait.Close();
                    Accepted accept = new Accepted();
                    DialogResult res = accept.ShowDialog();
                    GameBoard.columns = int.Parse(sizear[1]);
                    GameBoard.rows = int.Parse(sizear[0]);
                    GameBoard.HostColor = Color.FromArgb(Int32.Parse(hostcolor));
                    GameBoard.ChallangerColor = CurrentPlayer.PlayerColor;
                    GameBoard.turn = 1;
                    GameBoard.playerTurn = 2;

                    lobby.seegamebaord = new GameBoard();
                    lobby.seegamebaord.Show();
                }));

       

            }

            else
            {
                lobby.mainlobby.Invoke(new MethodInvoker(delegate ()
                {
                    message ms = new message();
                    ms.msg = "Your Opponent has refused you!";
                    DialogResult res = ms.ShowDialog();
                    lobby.currentroom = null;
                    

                    lobby.seegamebaord.Close();
                }));

            }
        }
       private static void updateBoard(List<string> data)
       {
   

            for (int i = 0; i < data.Count; i++)
            {
                var rowstring = data.ElementAt(i);
                var row = rowstring.Split('+');
                for (int j = 0; j < row.Length; j++)
                {

                    GameBoard.currntGameboard.board[i, j] = int.Parse(row[j]);
                    

                }
             
            }

            if (GameBoard.currntGameboard.Visible)
            {
                GameBoard.currntGameboard.BeginInvoke(new MethodInvoker(delegate () {

                    GameBoard.currntGameboard.repaintBord();
                }));

            }

        }


        public static List<Player> Getplayers(List<string> data)
        {
            var players = new List<Player>();

            foreach (var item in data)
            {
                var name = item.Split('+')[0];
                bool isplaying =bool.Parse( item.Split('+')[1]);
                players.Add(new Player(name, isplaying));
            }
            return players;
        }
        public static List<Room> GetRooms(List<string> data)
        {   
            var rooms = new List<Room>();

            foreach (var item in data)
            {
                var rom = item.Split('+');
                var roomName = rom[0];
                var host = rom[1].Split('-');
                var addedRoom = new Room(roomName, new Player(host[0]));
                if (rom.Length == 3)
                {
                    addedRoom.challenger = new Player(rom[2].Split('-')[0]);
                }

                rooms.Add(addedRoom);
    
            }
            return rooms;
        }


        public static void UpdatePlayer(Color colorName)
        {
            var currPlayer= new Player(UserName);

            currPlayer.PlayerColor = colorName;

            CurrentPlayer = currPlayer;
        }


        public static Room UpdateRoom(List<string> data)
        {
            
            var currroom = new Room(data[0],new Player(data[1]));
         

            return currroom;
        }


    }

    public class Player
    {
        public string Name { get;}
        public bool isplaying { get; set; }
        public Color PlayerColor { get; set; }

        public  Player(string name, bool isPlaying)
        {
            Name = name;
            this.isplaying = isPlaying;
        }
        public Player(string name)
        {
            Name = name;
            this.isplaying = isplaying;
        }
    }


    public class Room
    {
        public string Name { get; set; }
        public Player Host { get; set; }

        public Player challenger { get; set; }
        public Player[] inspectors { get; set; }
        public  bool occupied = false;

        public Room(string name ,Player host)
        {
            Name = name;
            Host = host;
        }

    }
   public enum Flag
    {
     sendLoginInfo = 100,
     getPlayers = 210,
     getRooms = 220,
     createRoom = 310,
     joinRoom = 320,
     asktoplay = 400,
     waittopaly = 405,
     SendMove = 410,
     updateBoard = 420,
     gameResult =500,
     playAgain =600,
     leaveRoom = 650,
     disconnect = 700
    }
}
