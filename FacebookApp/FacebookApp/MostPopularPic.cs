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
    public class MostPopularPic : ICommand
    {
        private PictureSorter m_PicSorter;
        private List<string> m_CompareClassesList = new List<string>();
        private bool m_enable = true;

        public MostPopularPic()
        {
            m_CompareClassesList = new List<string>(new string[] { "Fetch Most Liked Pictures", "Fetch Most Commented Pictures" });
        }

        public void Execute(User i_User, string i_ButtonText, List<Photo> o_Photos)
        {
            if (m_enable == true)
            {            
                chooseStratagy(i_ButtonText);
                m_PicSorter.Sort(o_Photos);
            }
        }

        public void DisableFunctionality()
        {
            m_enable = false;
        }

        private void chooseStratagy(string i_ButtonText)
        {
            if (i_ButtonText == m_CompareClassesList[0])
            {
                m_PicSorter = new PictureSorter(new LikesComparer());
            }
            else
            {
                m_PicSorter = new PictureSorter(new CommentsComparer());
            }
        }
    }
}
