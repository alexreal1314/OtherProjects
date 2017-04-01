using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourInARow
{
    public class Player
    {
        private string m_PlayerName;
        private int m_NumOfPoints = 0;

        public Player(string i_PlayerName)
        {
            m_PlayerName = i_PlayerName;
        }

        public int NumOfPoints
        {
            get
            {
                return m_NumOfPoints;
            }

            set
            {
                m_NumOfPoints = value;
            }
        }

        public string PlayerName
        {
            get
            {
                return m_PlayerName;
            }

            set
            {
                m_PlayerName = value;
            }
        }
    }
}
