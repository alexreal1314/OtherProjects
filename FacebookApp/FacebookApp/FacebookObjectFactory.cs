using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;

namespace FacebookApp
{
    public static class FacebookObjectFactory
    {
        public enum eFaceType
        {
            Post,
            Photo,
            Album,
            Video
        }
    
        public static IEnumerable<PostedItem> factoryMethod(Enum i_userPic, User io_LoggedInUser)
        {
            if (i_userPic.Equals(eFaceType.Post))
            {
                return io_LoggedInUser.Posts;
            }
            else if (i_userPic.Equals(eFaceType.Photo))
            {
                return io_LoggedInUser.PhotosTaggedIn;
            }
            else if (i_userPic.Equals(eFaceType.Album))
            {
                return io_LoggedInUser.Albums;
            }
            else
            {
                return io_LoggedInUser.Videos;
            }
        }
    }
}