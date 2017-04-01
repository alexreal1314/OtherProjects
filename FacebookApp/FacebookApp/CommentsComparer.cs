using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper.ObjectModel;

namespace FacebookApp
{
    public class CommentsComparer : IComparer
    {
        public int Compare(Photo i_Photo1, Photo i_Photo2)
        {
            int toReturn = 1;

            if (i_Photo1.Comments.Count > i_Photo2.Comments.Count)
            {
                toReturn = -1;
            }
            else if (i_Photo2.Comments.Count < i_Photo1.Comments.Count)
            {
                toReturn = 1;
            }
            else
            {
                toReturn = 0;
            }

            return toReturn;
        }
    }
}
