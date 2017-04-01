using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper.ObjectModel;

namespace FacebookApp
{
    public class PicsUserLiked : ICommand
    {
        private List<Photo> m_FilteredPhotos = new List<Photo>();
        private bool m_enable = true;

        public void Execute(User i_User, string i_ButtonText, List<Photo> o_Photos)
        {
            findPhotosUserLiked(i_User, o_Photos);
        }

        public void DisableFunctionality()
        {
            m_enable = false;
        }

        private void findPhotosUserLiked(User i_User, List<Photo> o_Photos)
        {
            List<Photo> photosTheUserLiked = new List<Photo>();

            if (m_FilteredPhotos.Count != 0)
            {
                m_FilteredPhotos.Clear();
            } 

            foreach (Photo photo in o_Photos)
            {
                foreach(User user in photo.LikedBy)
                {
                    if(user.Name == i_User.Name)
                    {
                        photosTheUserLiked.Add(photo);
                        break;
                    }                    
                }
            }

            o_Photos.Clear();
            foreach(Photo photo in photosTheUserLiked)
            {
                o_Photos.Add(photo);
            }
        }
    }
}
