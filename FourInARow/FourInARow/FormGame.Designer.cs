namespace FourInARow
{
    partial class FormGame
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.StripMenuItemGame = new System.Windows.Forms.ToolStripMenuItem();
            this.stripNewGame = new System.Windows.Forms.ToolStripMenuItem();
            this.stripNewTournament = new System.Windows.Forms.ToolStripMenuItem();
            this.stripProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.stripExit = new System.Windows.Forms.ToolStripMenuItem();
            this.StripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.StripMenuItemHowToPlay = new System.Windows.Forms.ToolStripMenuItem();
            this.StripMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StripStatusCurrPlayer = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.StripStatusCurrPlayerName = new System.Windows.Forms.ToolStripStatusLabel();
            this.StripStatusCurrName = new System.Windows.Forms.ToolStripStatusLabel();
            this.StripPlayer1Name = new System.Windows.Forms.ToolStripStatusLabel();
            this.StripPlayer1Score = new System.Windows.Forms.ToolStripStatusLabel();
            this.StripPlayer2Name = new System.Windows.Forms.ToolStripStatusLabel();
            this.StripPlayer2Score = new System.Windows.Forms.ToolStripStatusLabel();
            this.PanelBackground = new System.Windows.Forms.Panel();
            this.timerFall = new System.Windows.Forms.Timer(this.components);
            this.timerFlicker = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StripMenuItemGame,
            this.StripMenuItemHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(390, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // StripMenuItemGame
            // 
            this.StripMenuItemGame.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stripNewGame,
            this.stripNewTournament,
            this.stripProperties,
            this.stripExit});
            this.StripMenuItemGame.Name = "StripMenuItemGame";
            this.StripMenuItemGame.Size = new System.Drawing.Size(50, 20);
            this.StripMenuItemGame.Text = "Game";
            // 
            // stripNewGame
            // 
            this.stripNewGame.Name = "stripNewGame";
            this.stripNewGame.Size = new System.Drawing.Size(176, 22);
            this.stripNewGame.Text = "Start a New Game";
            this.stripNewGame.Click += new System.EventHandler(this.stripNewGame_Click);
            // 
            // stripNewTournament
            // 
            this.stripNewTournament.Name = "stripNewTournament";
            this.stripNewTournament.Size = new System.Drawing.Size(176, 22);
            this.stripNewTournament.Text = "Start a New Tournament";
            this.stripNewTournament.Click += new System.EventHandler(this.stripNewTournament_Click);
            // 
            // stripProperties
            // 
            this.stripProperties.Name = "stripProperties";
            this.stripProperties.Size = new System.Drawing.Size(176, 22);
            this.stripProperties.Text = "Properties...";
            this.stripProperties.Click += new System.EventHandler(this.stripProperties_Click);
            // 
            // stripExit
            // 
            this.stripExit.Name = "stripExit";
            this.stripExit.Size = new System.Drawing.Size(176, 22);
            this.stripExit.Text = "Exit";
            this.stripExit.Click += new System.EventHandler(this.stripExit_Click);
            // 
            // StripMenuItemHelp
            // 
            this.StripMenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StripMenuItemHowToPlay,
            this.StripMenuItemAbout});
            this.StripMenuItemHelp.Name = "StripMenuItemHelp";
            this.StripMenuItemHelp.Size = new System.Drawing.Size(44, 20);
            this.StripMenuItemHelp.Text = "Help";
            // 
            // StripMenuItemHowToPlay
            // 
            this.StripMenuItemHowToPlay.Name = "StripMenuItemHowToPlay";
            this.StripMenuItemHowToPlay.Size = new System.Drawing.Size(143, 22);
            this.StripMenuItemHowToPlay.Text = "How to play?";
            this.StripMenuItemHowToPlay.Click += new System.EventHandler(this.StripMenuItemHowToPlay_Click);
            // 
            // StripMenuItemAbout
            // 
            this.StripMenuItemAbout.Name = "StripMenuItemAbout";
            this.StripMenuItemAbout.Size = new System.Drawing.Size(143, 22);
            this.StripMenuItemAbout.Text = "About";
            this.StripMenuItemAbout.Click += new System.EventHandler(this.StripMenuItemAbout_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StripStatusCurrPlayer,
            this.toolStripStatusLabel1,
            this.StripStatusCurrPlayerName,
            this.StripStatusCurrName,
            this.StripPlayer1Name,
            this.StripPlayer1Score,
            this.StripPlayer2Name,
            this.StripPlayer2Score});
            this.statusStrip1.Location = new System.Drawing.Point(0, 403);
            this.statusStrip1.Margin = new System.Windows.Forms.Padding(2);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(390, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StripStatusCurrPlayer
            // 
            this.StripStatusCurrPlayer.Name = "StripStatusCurrPlayer";
            this.StripStatusCurrPlayer.Size = new System.Drawing.Size(10, 17);
            this.StripStatusCurrPlayer.Text = " ";
            this.StripStatusCurrPlayer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // StripStatusCurrPlayerName
            // 
            this.StripStatusCurrPlayerName.Name = "StripStatusCurrPlayerName";
            this.StripStatusCurrPlayerName.Size = new System.Drawing.Size(0, 17);
            // 
            // StripStatusCurrName
            // 
            this.StripStatusCurrName.AutoSize = false;
            this.StripStatusCurrName.Name = "StripStatusCurrName";
            this.StripStatusCurrName.Size = new System.Drawing.Size(70, 17);
            this.StripStatusCurrName.Text = " ";
            this.StripStatusCurrName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StripPlayer1Name
            // 
            this.StripPlayer1Name.Name = "StripPlayer1Name";
            this.StripPlayer1Name.Size = new System.Drawing.Size(10, 17);
            this.StripPlayer1Name.Text = " ";
            // 
            // StripPlayer1Score
            // 
            this.StripPlayer1Score.Name = "StripPlayer1Score";
            this.StripPlayer1Score.Size = new System.Drawing.Size(10, 17);
            this.StripPlayer1Score.Text = " ";
            // 
            // StripPlayer2Name
            // 
            this.StripPlayer2Name.Name = "StripPlayer2Name";
            this.StripPlayer2Name.Size = new System.Drawing.Size(10, 17);
            this.StripPlayer2Name.Text = " ";
            // 
            // StripPlayer2Score
            // 
            this.StripPlayer2Score.Name = "StripPlayer2Score";
            this.StripPlayer2Score.Size = new System.Drawing.Size(10, 17);
            this.StripPlayer2Score.Text = " ";
            // 
            // PanelBackground
            // 
            this.PanelBackground.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.PanelBackground.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelBackground.Location = new System.Drawing.Point(12, 38);
            this.PanelBackground.Name = "PanelBackground";
            this.PanelBackground.Size = new System.Drawing.Size(366, 355);
            this.PanelBackground.TabIndex = 2;
            this.PanelBackground.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelBackground_MouseDown);
            // 
            // timerFall
            // 
            this.timerFall.Interval = 10;
            this.timerFall.Tick += new System.EventHandler(this.timerFall_Tick);
            // 
            // timerFlicker
            // 
            this.timerFlicker.Interval = 500;
            this.timerFlicker.Tick += new System.EventHandler(this.timerFlicker_Tick);
            // 
            // FormGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(390, 425);
            this.Controls.Add(this.PanelBackground);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "4 In A Row!";
            this.TransparencyKey = System.Drawing.Color.Transparent;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem StripMenuItemGame;
        private System.Windows.Forms.ToolStripMenuItem stripNewGame;
        private System.Windows.Forms.ToolStripMenuItem stripNewTournament;
        private System.Windows.Forms.ToolStripMenuItem stripProperties;
        private System.Windows.Forms.ToolStripMenuItem stripExit;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem StripMenuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem StripMenuItemHowToPlay;
        private System.Windows.Forms.ToolStripMenuItem StripMenuItemAbout;
        private System.Windows.Forms.ToolStripStatusLabel StripStatusCurrPlayer;
        private System.Windows.Forms.ToolStripStatusLabel StripStatusCurrPlayerName;
        private System.Windows.Forms.ToolStripStatusLabel StripStatusCurrName;
        private System.Windows.Forms.ToolStripStatusLabel StripPlayer1Name;
        private System.Windows.Forms.ToolStripStatusLabel StripPlayer1Score;
        private System.Windows.Forms.ToolStripStatusLabel StripPlayer2Name;
        private System.Windows.Forms.ToolStripStatusLabel StripPlayer2Score;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Panel PanelBackground;
        private System.Windows.Forms.Timer timerFall;
        private System.Windows.Forms.Timer timerFlicker;
    }
}