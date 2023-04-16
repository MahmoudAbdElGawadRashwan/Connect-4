namespace Client
{
    partial class lobby
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(lobby));
            this.PlayersPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.RoomsTitle = new System.Windows.Forms.Label();
            this.PlayersTitle = new System.Windows.Forms.Label();
            this.RoomsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.JoinRoomBtn = new System.Windows.Forms.Button();
            this.CreateRoomBtn = new System.Windows.Forms.Button();
            this.CloseBtn = new System.Windows.Forms.Button();
            this.SpectateBtn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PlayersPanel
            // 
            this.PlayersPanel.AutoScroll = true;
            this.PlayersPanel.BackColor = System.Drawing.Color.Transparent;
            this.PlayersPanel.Location = new System.Drawing.Point(767, 80);
            this.PlayersPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PlayersPanel.Name = "PlayersPanel";
            this.PlayersPanel.Size = new System.Drawing.Size(392, 450);
            this.PlayersPanel.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.RoomsTitle);
            this.panel1.Controls.Add(this.PlayersTitle);
            this.panel1.Location = new System.Drawing.Point(-1, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1173, 75);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panel1_MouseUp);
            // 
            // RoomsTitle
            // 
            this.RoomsTitle.AutoSize = true;
            this.RoomsTitle.Font = new System.Drawing.Font("Microsoft YaHei", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RoomsTitle.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.RoomsTitle.Location = new System.Drawing.Point(180, 11);
            this.RoomsTitle.Name = "RoomsTitle";
            this.RoomsTitle.Size = new System.Drawing.Size(257, 56);
            this.RoomsTitle.TabIndex = 1;
            this.RoomsTitle.Text = "Rooms List";
            this.RoomsTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel1_MouseDown);
            this.RoomsTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel1_MouseMove);
            this.RoomsTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panel1_MouseUp);
            // 
            // PlayersTitle
            // 
            this.PlayersTitle.AutoSize = true;
            this.PlayersTitle.Font = new System.Drawing.Font("Microsoft YaHei", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayersTitle.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.PlayersTitle.Location = new System.Drawing.Point(811, 11);
            this.PlayersTitle.Name = "PlayersTitle";
            this.PlayersTitle.Size = new System.Drawing.Size(261, 56);
            this.PlayersTitle.TabIndex = 0;
            this.PlayersTitle.Text = "Players List";
            this.PlayersTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel1_MouseDown);
            this.PlayersTitle.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel1_MouseMove);
            this.PlayersTitle.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panel1_MouseUp);
            // 
            // RoomsPanel
            // 
            this.RoomsPanel.AutoScroll = true;
            this.RoomsPanel.BackColor = System.Drawing.Color.Transparent;
            this.RoomsPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.RoomsPanel.Location = new System.Drawing.Point(12, 80);
            this.RoomsPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RoomsPanel.Name = "RoomsPanel";
            this.RoomsPanel.Size = new System.Drawing.Size(749, 450);
            this.RoomsPanel.TabIndex = 3;
            this.RoomsPanel.WrapContents = false;
            // 
            // JoinRoomBtn
            // 
            this.JoinRoomBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(178)))), ((int)(((byte)(73)))));
            this.JoinRoomBtn.Font = new System.Drawing.Font("Microsoft YaHei UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.JoinRoomBtn.ForeColor = System.Drawing.Color.White;
            this.JoinRoomBtn.Location = new System.Drawing.Point(353, 548);
            this.JoinRoomBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.JoinRoomBtn.Name = "JoinRoomBtn";
            this.JoinRoomBtn.Size = new System.Drawing.Size(193, 70);
            this.JoinRoomBtn.TabIndex = 4;
            this.JoinRoomBtn.Text = "Join";
            this.JoinRoomBtn.UseVisualStyleBackColor = false;
            this.JoinRoomBtn.Click += new System.EventHandler(this.JoinRoomBtn_Click);
            this.JoinRoomBtn.MouseEnter += new System.EventHandler(this.JoinRoomBtn_MouseEnter);
            this.JoinRoomBtn.MouseLeave += new System.EventHandler(this.JoinRoomBtn_MouseLeave);
            // 
            // CreateRoomBtn
            // 
            this.CreateRoomBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(131)))), ((int)(((byte)(219)))));
            this.CreateRoomBtn.Font = new System.Drawing.Font("Microsoft YaHei UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateRoomBtn.ForeColor = System.Drawing.Color.White;
            this.CreateRoomBtn.Location = new System.Drawing.Point(80, 548);
            this.CreateRoomBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CreateRoomBtn.Name = "CreateRoomBtn";
            this.CreateRoomBtn.Size = new System.Drawing.Size(193, 71);
            this.CreateRoomBtn.TabIndex = 5;
            this.CreateRoomBtn.Text = "Create Room";
            this.CreateRoomBtn.UseVisualStyleBackColor = false;
            this.CreateRoomBtn.Click += new System.EventHandler(this.CreateRoomBtn_Click);
            this.CreateRoomBtn.MouseEnter += new System.EventHandler(this.CreateRoomBtn_MouseEnter);
            this.CreateRoomBtn.MouseLeave += new System.EventHandler(this.CreateRoomBtn_MouseLeave);
            // 
            // CloseBtn
            // 
            this.CloseBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.CloseBtn.Font = new System.Drawing.Font("Microsoft YaHei UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseBtn.ForeColor = System.Drawing.Color.White;
            this.CloseBtn.Location = new System.Drawing.Point(900, 548);
            this.CloseBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(193, 65);
            this.CloseBtn.TabIndex = 6;
            this.CloseBtn.Text = "Exit Game";
            this.CloseBtn.UseVisualStyleBackColor = false;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            this.CloseBtn.MouseEnter += new System.EventHandler(this.CloseBtn_MouseEnter);
            this.CloseBtn.MouseLeave += new System.EventHandler(this.CloseBtn_MouseLeave);
            // 
            // SpectateBtn
            // 
            this.SpectateBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(178)))), ((int)(((byte)(73)))));
            this.SpectateBtn.Font = new System.Drawing.Font("Microsoft YaHei UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SpectateBtn.ForeColor = System.Drawing.Color.Gainsboro;
            this.SpectateBtn.Location = new System.Drawing.Point(627, 548);
            this.SpectateBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SpectateBtn.Name = "SpectateBtn";
            this.SpectateBtn.Size = new System.Drawing.Size(193, 70);
            this.SpectateBtn.TabIndex = 7;
            this.SpectateBtn.Text = "Spectate";
            this.SpectateBtn.UseVisualStyleBackColor = false;
            this.SpectateBtn.Click += new System.EventHandler(this.SpectateBtn_Click);
            this.SpectateBtn.MouseEnter += new System.EventHandler(this.SpectateBtn_MouseEnter);
            this.SpectateBtn.MouseLeave += new System.EventHandler(this.SpectateBtn_MouseLeave);
            // 
            // lobby
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1173, 654);
            this.Controls.Add(this.SpectateBtn);
            this.Controls.Add(this.CloseBtn);
            this.Controls.Add(this.CreateRoomBtn);
            this.Controls.Add(this.JoinRoomBtn);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.RoomsPanel);
            this.Controls.Add(this.PlayersPanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "lobby";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Lobby ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Lobby_FormClosing);
            this.Load += new System.EventHandler(this.lobby_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel PlayersPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label PlayersTitle;
        private System.Windows.Forms.FlowLayoutPanel RoomsPanel;
        private System.Windows.Forms.Label RoomsTitle;
        private System.Windows.Forms.Button JoinRoomBtn;
        private System.Windows.Forms.Button CreateRoomBtn;
        private System.Windows.Forms.Button CloseBtn;
        private System.Windows.Forms.Button SpectateBtn;
    }
}