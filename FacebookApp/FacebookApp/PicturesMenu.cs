using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;

namespace FacebookApp
{
    public interface ICommand
    {
        void Execute(User i_User, string i_ButtonText, List<Photo> o_Photos);

        void DisableFunctionality();
    }

    public class PicturesMenu
    {
        private Dictionary<string, ICommand> m_MenuItems = new Dictionary<string, ICommand>();    
        private ICommand m_Current;

        public Dictionary<string, ICommand> getCommands()
        {
            return m_MenuItems;
        }

        public PicturesMenu(string[] i_ItemsToAdd)
        {
            m_MenuItems.Add(i_ItemsToAdd[0], new MostPopularPic());
            m_MenuItems.Add(i_ItemsToAdd[1], new MostPopularPic());
            m_MenuItems.Add(i_ItemsToAdd[2], new PicsUserLiked());
        }

        public void SelectItem(User i_User, string i_ButtonText, List<Photo> o_Photos)
        {
           if(m_MenuItems.TryGetValue(i_ButtonText, out m_Current))
           {
                m_Current.Execute(i_User, i_ButtonText, o_Photos);
           }
        }
    }
}
