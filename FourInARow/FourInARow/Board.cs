using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FourInARow
{
    public class Board
    {
        public const int k_PlayerOne = 1;
        public const int k_PlayerTwo = 2;
        private readonly int m_NumOfCols;
        private readonly int m_NumOfRows;
        private Tile[,] m_Board;

        public Board(int i_NumOfRows, int i_NumOfCols)
        {
            m_NumOfRows = i_NumOfRows;
            m_NumOfCols = i_NumOfCols;
            m_Board = new Tile[i_NumOfRows, i_NumOfCols];

            for (int i = 0; i < i_NumOfRows; i++)
            {
                for (int j = 0; j < i_NumOfCols; j++)
                {
                    m_Board[i, j] = new Tile(i, j);
                }
            }
        }

        public string GetTileChar(int i_NumOfRow, int i_NumOfCol)
        {
            return m_Board[i_NumOfRow, i_NumOfCol].TileHoldingChar;
        }

        public Tile GetTile(int i_NumOfRow, int i_NumOfCol)
        {
            return m_Board[i_NumOfRow, i_NumOfCol];
        }

        public int NumOfCols
        {
            get
            {
                return m_NumOfCols;
            }
        }

        public int NumOfRows
        {
            get
            {
                return m_NumOfRows;
            }
        }
     
        public bool InsertTile(int i_NumOfCol, int i_NumOfPlayer, ref int o_LastTileInsertedRow, ref int o_LastTileInsertedCol)
        {
            string tileToDraw;
            bool inserted = !true;
            if (i_NumOfPlayer == k_PlayerOne)
            {
                tileToDraw = "X";
            }
            else
            {
                tileToDraw = "O";
            }

            int j = m_NumOfRows - 1;
            while (!inserted && j >= 0)
            {
                if (m_Board[j, i_NumOfCol - 1].TileHoldingChar.CompareTo(" ") == 0)
                {
                    m_Board[j, i_NumOfCol - 1].TileHoldingChar = tileToDraw;                                     
                    inserted = true;
                }
                else
                {
                    j--;
                }
            }

            if (inserted)
            {
                o_LastTileInsertedRow = j;
                o_LastTileInsertedCol = i_NumOfCol - 1;
            }

            return inserted;          
        }

        public bool CheckIfIsLegalColumn(int i_NumOfCol)
        {
            bool isLegal = !true;
            if (i_NumOfCol < 0 || i_NumOfCol > m_NumOfCols)
            {
                isLegal = !true;
            }
            else
            {
                isLegal = true;
            }

            return isLegal;
        }

        public bool CheckIfColumnFull(int i_NumOfCol)
        {
            bool isColFull = true;
            int j = m_NumOfRows - 1;
            while (j >= 0 && isColFull == true)
            {
                if (m_Board[j, i_NumOfCol - 1].TileHoldingChar.CompareTo(" ") == 0)
                {
                    isColFull = !true;
                }

                j--;
            }

            return isColFull;
        }

        public void ClearBoardForNextRound()
        {
            for (int i = 0; i < m_NumOfRows; i++)
            {
                for (int j = 0; j < m_NumOfCols; j++)
                {
                    m_Board[i, j].TileHoldingChar = " ";
                }
            }
        }

        public bool CheckIfBoardIsFull()
        {
            bool boardIsFull = true;

            for (int i = 0; i < m_NumOfRows; i++)
            {
                for (int j = 0; j < m_NumOfCols; j++)
                {
                    if (m_Board[i, j].TileHoldingChar.CompareTo(" ") == 0)
                    {
                        boardIsFull = !true;
                    }
                }
            }

            return boardIsFull;
        }
    }
}
