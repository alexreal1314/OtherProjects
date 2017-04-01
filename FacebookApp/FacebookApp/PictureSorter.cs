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
    public interface IComparer
    {
        int Compare(Photo i_Photo1, Photo i_Photo2);
    }

    public class PictureSorter
    {
        public IComparer Comparer { get; set; }

        public PictureSorter(IComparer i_Comparer)
        {
            Comparer = i_Comparer;
        }

        public void Sort(List<Photo> o_Photos)
        {
            o_Photos.Sort(Comparer.Compare);
        }
    }
}
