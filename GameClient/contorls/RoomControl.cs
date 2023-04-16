using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{

    public partial class RoomControl : UserControl
    {
       
        public string selected_room;
        private string _roomname;
        private int _NumberofPlayers;
        private string _roomhost;
        //private string _roomguest;
        public string roomhost
        {
            get { return _roomhost; }
            set { _roomhost = value; label3.Text = "Room owner: " + value; }
        }

        //public string roomguest
        //{
        //    get { return _roomguest; }
        //    set { _roomguest = value; label4.Text = "Player2: " + value; }
        //}

        public string roomname
        {
            get { return _roomname; }
            set { _roomname = value; label1.Text = value; }
        }

        public int NumberofPlayers
        {
            get { return _NumberofPlayers; }
            set
            {
                _NumberofPlayers = value;
                label2.Text = "Online: " + value.ToString() + "/2";
            }
        }

        public RoomControl()
        {
            InitializeComponent();
        }

        private void RoomControl_MouseClick(object sender, MouseEventArgs e)
        {
            lobby parentForm = (this.Parent.Parent as lobby);

            if (e.Button == MouseButtons.Left)
            {
                if (parentForm.prev_select != null)
                {
                    parentForm.prev_select.BackColor = Color.White;
                }
                selected_room = this.label1.Text;
                this.BackColor = Color.Silver;
                parentForm.prev_select= this;
                parentForm.RoomSelected();
            }
        }
    }
}
