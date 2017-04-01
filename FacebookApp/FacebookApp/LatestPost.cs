using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;

namespace FacebookApp
{
    public class LatestPost
    {
        private FacebookObjectCollection<Post> m_PostsOnMyWall;
   
        public double HoursLastPost { get; set; }

        public double MinutsLastPost { get; set; }

        public void getTimeFromLastPost(FacebookObjectCollection<Post> i_Posts)
        {
            m_PostsOnMyWall = i_Posts;
            DateTime dateOfPost = (DateTime)m_PostsOnMyWall[0].CreatedTime;
            DateTime todaysTime = DateTime.Now;
            double totalMinuts = Math.Round(todaysTime.Subtract(dateOfPost).TotalMinutes);
            HoursLastPost = convertTime(totalMinuts);
        }

        public string GetUserNameAndTimeLatestPost()
        {
            return string.Format("Latest post from: {0}, posted {1} hours and {2} minuts ago", m_PostsOnMyWall[0].From.Name, HoursLastPost, MinutsLastPost);
        }

        private int convertTime(double i_TotalMinutes)
        {
            int hours = (int)i_TotalMinutes / 60;
            MinutsLastPost = i_TotalMinutes % 60;       
            return hours;
        }
    }
}
