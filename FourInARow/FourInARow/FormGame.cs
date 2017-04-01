using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FourInARow
{
    public partial class FormGame : Form
    {
        public enum ePlaceOfGraphicPath
        {
            Middle,
            Out
        }

        private const int k_Player1 = 1;
        private const int k_Player2 = 2;
        private Game m_Game;
        private FormStart m_FormStart;
        private int m_NumOfRows = 0;
        private int m_NumOfCols = 0;
        private Board m_Board;
        private PictureBox[,] m_MyPlayingBoard;
        private int m_CurrentPlayer = 1;
        private int m_LastTileInsertedRow = 0, m_LastTileInsertedCol = 0;
        private PictureBox[,] m_MyPlayingTiles;
        private PictureBox m_CurrentPlayerTile;
        private int m_ColOfTile = 0;
        private int[] m_RowIndex;
        private bool m_BlockMouseEvent = !true;
        private bool m_InitPropertiesNextRound = !true;
        private List<int> m_PairIndexesOfWinningTiles;
        private int m_FlickerCounter = 0;

        private static GraphicsPath CalculateGraphicsPath(Bitmap i_Bitmap, ePlaceOfGraphicPath i_Place)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            Color colorTransparent;

            if (i_Place == ePlaceOfGraphicPath.Middle)
            {
                colorTransparent = i_Bitmap.GetPixel(33, 33);
            }
            else
            {
                colorTransparent = i_Bitmap.GetPixel(0, 0);
            }

            int colOpaquePixelIdx = 0;

            for (int row = 0; row < i_Bitmap.Height; row++)
            {
                colOpaquePixelIdx = 0;

                for (int col = 0; col < i_Bitmap.Width; col++)
                {
                    if (i_Bitmap.GetPixel(col, row) != colorTransparent)
                    {
                        colOpaquePixelIdx = col;
                        int colNext = col;

                        for (colNext = colOpaquePixelIdx; colNext < i_Bitmap.Width; colNext++)
                        {
                            if (i_Bitmap.GetPixel(colNext, row) == colorTransparent)
                            {
                                break;
                            }
                        }

                        graphicsPath.AddRectangle(new Rectangle(colOpaquePixelIdx, row, colNext - colOpaquePixelIdx, 1));
                        col = colNext;
                    }
                }
            }

            return graphicsPath;
        }

        public FormGame()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            m_FormStart = new FormStart(this);
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            m_FormStart.ShowDialog();
        }

        public void OkPressed()
        {
            InitControls();
            InitStripStatusBar();
            InitBackground();
        }

        private void InitControls()
        {
            m_NumOfRows = (int)m_FormStart.NumOfRows;
            m_NumOfCols = (int)m_FormStart.NumOfCols;
            InitBoardLogic();
            m_RowIndex = new int[m_NumOfCols];
        }

        private void InitBoardLogic()
        {
            m_Board = new Board(m_NumOfRows, m_NumOfCols);
            m_Game = new Game(m_NumOfRows, m_NumOfCols, m_FormStart.PlayerOneName, m_FormStart.PlayerTwoName, m_Board);
        }

        private void InitBackground()
        {
            PanelBackground.Width = Properties.Resources.EmptyCell.Width * m_NumOfCols;
            PanelBackground.Height = Properties.Resources.EmptyCell.Height * (m_NumOfRows + 1);
            Width = PanelBackground.Width + 50;
            Height = PanelBackground.Height + 120;
            PanelBackground.Location = new Point((ClientSize.Width / 2) - (PanelBackground.Size.Width / 2), (ClientSize.Height / 2) - (PanelBackground.Size.Height / 2));
            int xLocation = PanelBackground.Left;
            int yLocation = PanelBackground.Top + Properties.Resources.EmptyCell.Height;
            m_MyPlayingBoard = new PictureBox[m_NumOfRows, m_NumOfCols];
            m_MyPlayingTiles = new PictureBox[m_NumOfRows, m_NumOfCols];

            for (int i = 0; i < m_NumOfRows; i++)
            {
                for (int j = 0; j < m_NumOfCols; j++)
                {
                    PictureBox currentTile = new PictureBox();
                    currentTile.Image = Properties.Resources.EmptyCell;
                    currentTile.SizeMode = PictureBoxSizeMode.AutoSize;
                    currentTile.Location = new Point(xLocation, yLocation);
                    xLocation += Properties.Resources.EmptyCell.Width;
                    m_MyPlayingBoard[i, j] = currentTile;
                    MakeTransparentCell(currentTile);
                    Controls.Add(currentTile);
                    currentTile.BringToFront();
                }

                xLocation = PanelBackground.Left;
                yLocation += Properties.Resources.EmptyCell.Height;
            }
        }

        private void MakeTransparentCell(PictureBox i_CurrentTile)
        {
            Bitmap bitmap = i_CurrentTile.Image as Bitmap;
            GraphicsPath graphicsPath = CalculateGraphicsPath(bitmap, ePlaceOfGraphicPath.Middle);
            i_CurrentTile.Region = new Region(graphicsPath);
        }

        private void InitStripStatusBar()
        {
            StripStatusCurrPlayer.Text = "Current Player:";
            StripStatusCurrName.Text = m_FormStart.PlayerOneName;
            StripPlayer1Name.Text = m_FormStart.PlayerOneName + ":";
            StripPlayer2Name.Text = m_FormStart.PlayerTwoName + ":";
            StripPlayer1Score.Text = "0";
            StripPlayer2Score.Text = "0";
        }

        private void PanelBackground_MouseDown(object sender, MouseEventArgs e)
        {
            if (!m_BlockMouseEvent)
            {
                int minAllowedHeightClick = (sender as Panel).Height - (m_NumOfRows * Properties.Resources.EmptyCell.Height);

                if (e.Y < minAllowedHeightClick)
                {
                    Point position = MousePosition;
                    int xPosition = position.X - Location.X - PanelBackground.Location.X;
                    m_ColOfTile = 1;
                    int widthCounter = m_MyPlayingBoard[0, 0].Width;

                    while (widthCounter <= xPosition)
                    {
                        m_ColOfTile++;
                        widthCounter += m_MyPlayingBoard[0, 0].Width;
                    }

                    if (m_ColOfTile > m_NumOfCols)
                    {
                        m_ColOfTile--;
                    }

                    bool colFull = m_Game.PlayTurn(m_CurrentPlayer, m_ColOfTile, ref m_LastTileInsertedRow, ref m_LastTileInsertedCol);
                    if (!colFull)
                    {
                        m_Game.SetLastTilePlayed(m_LastTileInsertedRow, m_LastTileInsertedCol);
                        CreateTileForPlayer(m_CurrentPlayer, m_ColOfTile);
                        timerFall.Start();
                        if (m_CurrentPlayer == k_Player1)
                        {
                            StripStatusCurrName.Text = m_Game.GetNameOfPlayerX(k_Player1);
                        }
                        else
                        {
                            StripStatusCurrName.Text = m_Game.GetNameOfPlayerX(k_Player2);
                        }

                        m_BlockMouseEvent = true;
                    }
                }
            }
        }

        private void CreateTileForPlayer(int i_CurrentPlayer, int i_ColOfTile)
        {
            m_CurrentPlayerTile = new PictureBox();

            if (i_CurrentPlayer == k_Player1)
            {
                m_CurrentPlayerTile.Image = Properties.Resources.CoinRed;
            }
            else
            {
                m_CurrentPlayerTile.Image = Properties.Resources.CoinYellow;
            }

            m_CurrentPlayerTile.BackColor = PanelBackground.BackColor;
            m_CurrentPlayerTile.SizeMode = PictureBoxSizeMode.AutoSize;
            int tileWidth = Properties.Resources.EmptyCell.Width;
            m_CurrentPlayerTile.Location = new Point((i_ColOfTile - 1) * tileWidth, PanelBackground.Location.Y);
            PanelBackground.Controls.Add(m_CurrentPlayerTile);
        }

        private void timerFall_Tick(object sender, EventArgs e)
        {
            int bottom = PanelBackground.Bottom - (m_RowIndex[m_ColOfTile - 1] * Properties.Resources.EmptyCell.Height) - 50;

            if (m_CurrentPlayerTile.Bottom >= bottom)
            {
                m_MyPlayingTiles[m_RowIndex[m_ColOfTile - 1], m_ColOfTile - 1] = m_CurrentPlayerTile;
                m_RowIndex[m_ColOfTile - 1]++;
                timerFall.Stop();
                m_BlockMouseEvent = !true;

                if (m_CurrentPlayer == k_Player1)
                {
                    StripStatusCurrName.Text = m_Game.GetNameOfPlayerX(k_Player2);
                    m_CurrentPlayerTile.Image = Properties.Resources.FullCellRed;
                    checkDrawOrWin();
                    m_CurrentPlayer++;
                }
                else
                {
                    StripStatusCurrName.Text = m_Game.GetNameOfPlayerX(k_Player1);
                    m_CurrentPlayerTile.Image = Properties.Resources.FullCellYellow;
                    checkDrawOrWin();
                    m_CurrentPlayer--;
                }

                Bitmap bitmapPlayer = m_CurrentPlayerTile.Image as Bitmap;
                GraphicsPath cirCleCoinPlayer = CalculateGraphicsPath(bitmapPlayer, ePlaceOfGraphicPath.Out);
                m_CurrentPlayerTile.Region = new Region(cirCleCoinPlayer);
                m_CurrentPlayerTile.BringToFront();
                return;
            }
            else
            {
                m_CurrentPlayerTile.Top += 3;
            }
        }

        private void checkDrawOrWin()
        {
            if (!checkForWin())
            {
                checkForDraw();
            }
        }

        private bool checkForWin()
        {
            bool isWin = m_Game.IsWinner();
            DialogResult result;
            if (isWin)
            {
                m_PairIndexesOfWinningTiles = m_Game.ListOfWinnersIndexes;
                timerFlicker.Start();
                string nameOfPlayer = m_Game.GetNameOfPlayerX(m_CurrentPlayer);
                StringBuilder sb = new StringBuilder();
                sb.Append(nameOfPlayer).Append(" Wins!").Append(string.Format("{0}Another Round?", Environment.NewLine));
                result = MessageBox.Show(sb.ToString(), "A Win!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    timerFlicker.Stop();
                    removeFlickerColor();

                    if (m_InitPropertiesNextRound)
                    {
                        clearPlayingBoard();
                        OkPressed();
                        m_InitPropertiesNextRound = !true;
                    }
                    else
                    {
                        m_Game.PlayingBoard.ClearBoardForNextRound();
                    }

                    m_Game.incrementPointOfPlayer(m_CurrentPlayer);
                    if (m_CurrentPlayer == k_Player1)
                    {
                        StripPlayer1Score.Text = m_Game.GetPointsOfPlayerX(m_CurrentPlayer).ToString();
                    }
                    else
                    {
                        StripPlayer2Score.Text = m_Game.GetPointsOfPlayerX(m_CurrentPlayer).ToString();
                    }

                    clearPictureBoxes();
                }
                else
                {
                    Close();
                }
            }

            return isWin;
        }

        private bool checkForDraw()
        {
            bool isDraw = m_Game.IsDraw();
            DialogResult result;

            if (isDraw)
            {
                string nameOfPlayer = m_Game.GetNameOfPlayerX(m_CurrentPlayer);
                string message = string.Format("Tie!!{0}AnotherRound?", Environment.NewLine);
                result = MessageBox.Show(message, "A Tie!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    if (m_InitPropertiesNextRound)
                    {
                        clearPlayingBoard();
                        OkPressed();
                        m_InitPropertiesNextRound = !true;
                    }
                    else
                    {
                        m_Game.PlayingBoard.ClearBoardForNextRound();
                    }

                    clearPictureBoxes();
                }
                else
                {
                    Close();
                }
            }

            return isDraw;
        }

        private void clearPictureBoxes()
        {
            for (int i = 0; i < m_NumOfCols; i++)
            {
                m_RowIndex[i] = 0;
            }

            PanelBackground.Controls.Clear();
        }

        private void StripMenuItemAbout_Click(object sender, EventArgs e)
        {
            FormAbout about = new FormAbout();
            about.StartPosition = FormStartPosition.CenterParent;
            about.ShowDialog();
        }

        private void stripNewGame_Click(object sender, EventArgs e)
        {
            m_CurrentPlayer = k_Player1;
            m_Game.PlayingBoard.ClearBoardForNextRound();
            clearPictureBoxes();
        }

        private void stripNewTournament_Click(object sender, EventArgs e)
        {
            m_CurrentPlayer = k_Player1;
            m_Game.PlayingBoard.ClearBoardForNextRound();
            m_Game.GetPlayerX(k_Player1).NumOfPoints = 0;
            m_Game.GetPlayerX(k_Player2).NumOfPoints = 0;
            StripPlayer1Score.Text = m_Game.GetPointsOfPlayerX(m_CurrentPlayer).ToString();
            StripPlayer2Score.Text = m_Game.GetPointsOfPlayerX(m_CurrentPlayer).ToString();
            clearPictureBoxes();
        }

        private void stripExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void StripMenuItemHowToPlay_Click(object sender, EventArgs e)
        {
            FormHelp helpForm = new FormHelp();
            helpForm.StartPosition = FormStartPosition.CenterParent;
            helpForm.ShowDialog();
        }

        private void stripProperties_Click(object sender, EventArgs e)
        {
            m_FormStart.ShowDialog();
        }

        public void ResetGameProperties()
        {
            m_Game.PlayingBoard.ClearBoardForNextRound();
            clearPictureBoxes();
            clearPlayingBoard();
        }

        private void clearPlayingBoard()
        {
            Controls.Clear();
            Controls.Add(PanelBackground);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
        }

        public bool InitGameNextRound
        {
            get
            {
                return m_InitPropertiesNextRound;
            }

            set
            {
                m_InitPropertiesNextRound = value;
            }
        }

        private void timerFlicker_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < m_PairIndexesOfWinningTiles.Count; i += 2)
            {
                if (m_FlickerCounter % 2 == 0)
                {
                    m_MyPlayingBoard[m_PairIndexesOfWinningTiles[i], m_PairIndexesOfWinningTiles[i + 1]].Image = null;
                    m_MyPlayingBoard[m_PairIndexesOfWinningTiles[i], m_PairIndexesOfWinningTiles[i + 1]].BackColor = Color.DeepPink;
                }
                else
                {
                    m_MyPlayingBoard[m_PairIndexesOfWinningTiles[i], m_PairIndexesOfWinningTiles[i + 1]].BackColor = Color.Transparent;
                    m_MyPlayingBoard[m_PairIndexesOfWinningTiles[i], m_PairIndexesOfWinningTiles[i + 1]].Image = Properties.Resources.EmptyCell;
                }
            }

            m_FlickerCounter++;
        }

        private void removeFlickerColor()
        {
            foreach (Control cell in Controls)
            {
                if (cell is PictureBox)
                {
                    if (cell.BackColor == Color.DeepPink)
                    {
                        cell.BackColor = Color.Transparent;
                        (cell as PictureBox).Image = Properties.Resources.EmptyCell;
                    }
                }
            }
        }
    }
}
