using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FourInARow
{
    public class Program
    {
       
        public static void Main()
        {
            FormGame theGame = new FormGame();
            theGame.ShowDialog();
        }
    }
}
