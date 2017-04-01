using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;

namespace FacebookApp
{
    public class TopPosters
    {
        private const int k_TopFiveUsers = 5;
        
        public Dictionary<string, int> PostsFromUsers { get; set; }

        public void getUserWithMostPosts(FacebookObjectCollection<Post> i_Posts)
        {
            PostsFromUsers = new Dictionary<string, int>();
            FacebookObjectCollection<Post> posts = i_Posts;         

            int num = 0;
            foreach (Post pst in posts)
            {
                if (pst.From.Name != null)
                {
                    if (PostsFromUsers.TryGetValue(pst.From.Name, out num) == true)
                    {
                        num += 1;
                        PostsFromUsers[pst.From.Name] = num;
                    }
                    else
                    {
                        PostsFromUsers.Add(pst.From.Name, 1);
                    }
                }
            }
        }

        public string CreateMessage()
        {
            int i = 1;
            StringBuilder message = new StringBuilder();
            message.Append("Top 5 Users Who Posted On My Wall :):").Append(Environment.NewLine);

            foreach (KeyValuePair<string, int> pair in PostsFromUsers)
            {
                message.Append(pair.Key).Append(Environment.NewLine);
                if (i == k_TopFiveUsers)
                {
                    break;
                }

                i++;
            }

            return message.ToString();
        }
    }
}