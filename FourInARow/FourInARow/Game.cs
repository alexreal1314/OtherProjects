using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FourInARow
{
    public class Game
    {
        public const int k_PlayerOne = 1;
        public const int k_PlayerTwo = 2;
        private Player m_PlayerOne;
        private Player m_PlayerTwo;
        private Board m_Board;
        private Tile m_LastPlayedTile;
        private List<int> m_PairIndexesOfWiningTiles;

        public Game(int i_NumOfRows, int i_NumOfCols, string i_PlayerOneName, string i_PlayerTwoName, Board i_Board)
        {
            m_PlayerOne = new Player(i_PlayerOneName);
            m_PlayerTwo = new Player(i_PlayerTwoName);
            m_LastPlayedTile = new Tile(0, 0);
            m_Board = i_Board;
        }

        public Player GetPlayerX(int i_NumOfPlayer)
        {
            if (i_NumOfPlayer == 1)
            {
                return m_PlayerOne;
            }
            else
            {
                return m_PlayerTwo;
            }
        }

        public Board PlayingBoard
        {
            get
            {
                return m_Board;
            }
        }

        public string GetNameOfPlayerX(int i_NumOfPlayer)
        {
            if (i_NumOfPlayer == 1)
            {
                return m_PlayerOne.PlayerName;
            }
            else
            {
                return m_PlayerTwo.PlayerName;
            }
        }

        public int GetPointsOfPlayerX(int i_NumOfPlayer)
        {
            if (i_NumOfPlayer == 1)
            {
                return m_PlayerOne.NumOfPoints;
            }
            else
            {
                return m_PlayerTwo.NumOfPoints;
            }
        }

        public bool PlayTurn(int i_NumOfPlayer, int i_numOfCol, ref int o_LastTileInsertedRow, ref int o_LastTileInsertedCol)
        {
            bool insertSuccess = !true;
            bool colFull = !true;

            colFull = m_Board.CheckIfColumnFull(i_numOfCol);
            insertSuccess = m_Board.InsertTile(i_numOfCol, i_NumOfPlayer, ref o_LastTileInsertedRow, ref o_LastTileInsertedCol);         
            return colFull;
        }

        public void SetLastTilePlayed(int i_LastTileInsertedRow, int i_LastTileInsertedCol)
        {
            m_LastPlayedTile.NumOfRow = i_LastTileInsertedRow;
            m_LastPlayedTile.NumOfCol = i_LastTileInsertedCol;
            m_LastPlayedTile.TileHoldingChar = m_Board.GetTileChar(i_LastTileInsertedRow, i_LastTileInsertedCol);
        }

        public bool IsDraw()
        {
            return m_Board.CheckIfBoardIsFull();
        }

        public bool IsWinner()
        {
            bool foundWinningFour = !true;
            List<List<Tile>> winningSets = new List<List<Tile>>();

            List<Tile> tileInRow = getEntireRowTiles();
            checkWinningSets(tileInRow, winningSets);

            List<Tile> tileInCol = getEntireColTiles();
            checkWinningSets(tileInCol, winningSets);

            List<Tile> tileInDiagonalRight = getEntireDiagRightTiles();
            checkWinningSets(tileInDiagonalRight, winningSets);

            List<Tile> tileInDiagonalLeft = getEntireDiagLeftTiles();
            checkWinningSets(tileInDiagonalLeft, winningSets);
          
            for (int i = 0; i < winningSets.Count; i++)
            {
                if (winningSets[i].Count >= 4)
                {
                    List<Tile> tileWinners = winningSets[i];
                    foundWinningFour = true;
                    m_PairIndexesOfWiningTiles = new List<int>();
                    int indexOfTile = 0;
                    for (int j = 0; j < 8; j++)
                    {
                        if (j % 2 == 0)
                        {
                            m_PairIndexesOfWiningTiles.Add(tileWinners[indexOfTile].NumOfRow);
                        }
                        else
                        {
                            m_PairIndexesOfWiningTiles.Add(tileWinners[indexOfTile].NumOfCol);
                            indexOfTile++;
                        }
                    }
                }
            }

            return foundWinningFour;
        }

        private List<Tile> getEntireRowTiles()
        {
            int rowNum = m_LastPlayedTile.NumOfRow;
            int numOfCols = m_Board.NumOfCols;

            List<Tile> tiles = new List<Tile>();
            for (int j = 0; j < numOfCols; j++)
            {
                tiles.Add(m_Board.GetTile(rowNum, j));
            }

            return tiles;
        }

        private List<Tile> getEntireColTiles()
        {
            int colNum = m_LastPlayedTile.NumOfCol;
            int numOfRows = m_Board.NumOfRows;

            List<Tile> tiles = new List<Tile>();
            for (int i = 0; i < numOfRows; i++)
            {
                tiles.Add(m_Board.GetTile(i, colNum));
            }

            return tiles;
        }

        private List<Tile> getEntireDiagRightTiles()
        {
            int rowNum = m_LastPlayedTile.NumOfRow;
            int colNum = m_LastPlayedTile.NumOfCol;
            List<Tile> tiles = new List<Tile>();

            while (colNum > 0 && rowNum < m_Board.NumOfRows - 1)
            {
                colNum--;
                rowNum++;
            }

            while (colNum <= m_Board.NumOfCols - 1 && rowNum >= 0)
            {
                tiles.Add(m_Board.GetTile(rowNum, colNum));
                colNum++;
                rowNum--;
            }

            return tiles;
        }

        private List<Tile> getEntireDiagLeftTiles()
        {
            int rowNum = m_LastPlayedTile.NumOfRow;
            int colNum = m_LastPlayedTile.NumOfCol;
            List<Tile> tiles = new List<Tile>();

            while (colNum > 0 && rowNum > 0)
            {
                colNum--;
                rowNum--;
            }

            while (colNum <= m_Board.NumOfCols - 1 && rowNum <= m_Board.NumOfRows - 1)
            {
                tiles.Add(m_Board.GetTile(rowNum, colNum));
                colNum++;
                rowNum++;
            }

            return tiles;
        }

        private void checkWinningSets(List<Tile> i_ListFindFourInARow, List<List<Tile>> i_WinningSets)
        {
            List<Tile> winningTiles = new List<Tile>();
            winningTiles = getWinningFourFromList(i_ListFindFourInARow);
            if (winningTiles != null)
            {
                i_WinningSets.Add(winningTiles);
            }
        }

        private List<Tile> getWinningFourFromList(List<Tile> i_Tiles)
        {
            const int k_Sequence = 4;
            List<Tile> winningFour = new List<Tile>();
            bool foundFirst = !true;
            Tile tileSaver = i_Tiles[0];
            string charToCheckSets = m_LastPlayedTile.TileHoldingChar;
            int j = 0, listLength = i_Tiles.Count;
            int consecutiveTiles = 0;

            while (j < listLength && !foundFirst)
            {
                if (i_Tiles[j].TileHoldingChar.CompareTo(" ") != 0)
                {
                    winningFour.Add(i_Tiles[j]);
                    tileSaver = i_Tiles[j];
                    foundFirst = true;
                    consecutiveTiles++;
                }

                j++;
            }

            if (foundFirst)
            {
                while (consecutiveTiles < k_Sequence && j < listLength)
                {
                    if (tileSaver.TileHoldingChar.CompareTo(" ") != 0 && tileSaver.TileHoldingChar.CompareTo(i_Tiles[j].TileHoldingChar) == 0)
                    {
                        consecutiveTiles++;
                        winningFour.Add(i_Tiles[j]);
                        tileSaver = i_Tiles[j];
                        j++;
                    }
                    else if (tileSaver.TileHoldingChar.CompareTo(" ") != 0 && tileSaver.TileHoldingChar.CompareTo(i_Tiles[j].TileHoldingChar) != 0 && i_Tiles[j].TileHoldingChar.CompareTo(" ") != 0)
                    {
                        winningFour.Clear();
                        consecutiveTiles = 0;
                        tileSaver = i_Tiles[j];
                        winningFour.Add(tileSaver);
                        consecutiveTiles++;
                        j++;
                    }
                    else if (tileSaver.TileHoldingChar.CompareTo(" ") != 0 && tileSaver.TileHoldingChar.CompareTo(i_Tiles[j].TileHoldingChar) != 0 && i_Tiles[j].TileHoldingChar.CompareTo(" ") == 0)
                    {
                        winningFour.Clear();
                        consecutiveTiles = 0;
                        tileSaver = i_Tiles[j];
                        j++;
                    }
                    else if (tileSaver.TileHoldingChar.CompareTo(" ") == 0 && tileSaver.TileHoldingChar.CompareTo(i_Tiles[j].TileHoldingChar) != 0)
                    {
                        winningFour.Clear();
                        consecutiveTiles++;
                        tileSaver = i_Tiles[j];
                        winningFour.Add(tileSaver);
                        j++;
                    }
                    else
                    {
                        winningFour.Clear();
                        consecutiveTiles = 0;
                        tileSaver = i_Tiles[j];
                        j++;
                    }
                }
            }

            return winningFour;
        }

        public void incrementPointOfPlayer(int i_NumOfPlayer)
        {
            if (i_NumOfPlayer == k_PlayerOne)
            {
                m_PlayerOne.NumOfPoints = m_PlayerOne.NumOfPoints + 1;
            }
            else
            {
                m_PlayerTwo.NumOfPoints = m_PlayerTwo.NumOfPoints + 1;
            }
        }

        public List<int> ListOfWinnersIndexes
        {
            get
            {
                return m_PairIndexesOfWiningTiles;
            }
        }
    }
}