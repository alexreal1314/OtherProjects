using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacebookApp
{
    public class SafeMode
    {
        public delegate void NotifyObserversHandler();

        public event NotifyObserversHandler DisableCommands;
     
        public void Notify()
        {        
            DisableCommands();
        }
    }
}