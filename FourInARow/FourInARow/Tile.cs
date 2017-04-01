using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourInARow
{
    public class Tile
    {
        private string m_CharHolding = " ";
        private int m_NumOfRow;
        private int m_NumOfCol;

        public Tile(int i_NumOfRow, int i_NumOfCol)
        {
            m_NumOfRow = i_NumOfRow;
            m_NumOfCol = i_NumOfCol;
        }

        public int NumOfRow
        {
            get
            {
                return m_NumOfRow;
            }

            set
            {
                m_NumOfRow = value;
            }
        }

        public int NumOfCol
        {
            get
            {
                return m_NumOfCol;
            }

            set
            {
                m_NumOfCol = value;
            }
        }

        public string TileHoldingChar
        {
            get
            {
                return m_CharHolding;
            }

            set
            {
                m_CharHolding = value;
            }
        }
    }
}
