using System;
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
using System.Threading;

namespace FacebookApp
{
    public partial class FormApp : Form
    {
        private const int k_NumOfPhotos = 9;
        private const int k_NumOfRows = 3;
        private const int k_NumOfCols = 3;
        private User m_LoggedInUser;
        private Dictionary<int, Post> m_ListboxPosts = new Dictionary<int, Post>();
        private Post m_SelectedPost;
        private Comment m_SelectedComment;
        private int m_PostCount = 0;
        private int m_CommentCount = 0;
        private Dictionary<int, Comment> m_ListboxComments = new Dictionary<int, Comment>();
        private string m_LoggedInMessage = "You Need To Login First!";
        private string m_AlbumMessage = "You Need To Select An Album From The List!";
        private string m_EmptyFieldLeft = "You can't leave an empty field!";
        private PictureBox[,] m_TopNineLikedPhotos;
        private List<Photo> m_SortedDescLikedPhotos = new List<Photo>();
        private TextBox[] m_TopPhotosLikesCount;
        private Label[] m_TopPhotosLabels;
        private TopPosters m_TopUsers;
        private Dictionary<string, int> m_UsersWhoPosted = new Dictionary<string, int>();
        private FacebookObjectCollection<Album> m_Albums = new FacebookObjectCollection<Album>();
        private Album m_CurrentAlbumSelected;
        private LatestPost m_LatestPost;
        private FacebookObjectCollection<Post> m_Posts;
        private PicturesMenu m_PicturesMenu;
        private List<Photo> m_PhotosTaggedIn = new List<Photo>();
        private SafeMode m_safer;
        private List<Label> m_PhotoLabels = new List<Label>();
        private List<TextBox> m_PhotoTextBoxes = new List<TextBox>();

        public FormApp()
        {
            InitializeComponent();
            FacebookWrapper.FacebookService.s_CollectionLimit = 500;
            comboBoxSelectedObject.DataSource = Enum.GetNames(typeof(FacebookObjectFactory.eFaceType));
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            loginAndInit();
        }

        private void loginAndInit()
        {
            LoginResult result = FacebookService.Login(
                "282872155423400",
                "public_profile",
                "user_education_history",
                "user_birthday",
                "user_actions.video",
                "user_actions.news",
                "user_actions.music",
                "user_actions.fitness",
                "user_actions.books",
                "user_about_me",
                "user_friends",
                "publish_actions",
                "user_events",
                "user_games_activity",
                "user_groups",
                "user_hometown",
                "user_likes",
                "user_location",
                "user_managed_groups",
                "user_photos",
                "user_posts",
                "user_relationships",
                "user_relationship_details",
                "user_religion_politics",
                "user_status",
                "user_tagged_places",
                "user_videos",
                "user_website",
                "user_work_history",
                "read_custom_friendlists",
                "read_page_mailboxes",
                "read_stream",
                "manage_notifications",
                "manage_pages",
                "publish_pages",
                "publish_actions",
                "rsvp_event");

            if (!string.IsNullOrEmpty(result.AccessToken))
            {
                m_LoggedInUser = result.LoggedInUser;
                getUserInfo();
                LogFileRecorder.Instance.createLogFile(m_LoggedInUser.Name);
                MessageBox.Show(string.Format("A new log file have been created in: {0}", LogFileRecorder.Instance.LogFilePath));
                m_PicturesMenu = new PicturesMenu(new string[3] { buttonPicturesLikesCount.Text, buttonPicturesCommentCount.Text, buttonRecentPics.Text });
            }
            else
            {
                MessageBox.Show(result.ErrorMessage, "Wrong username or password!", MessageBoxButtons.OK);
            }
        }

        private void getUserInfo()
        {
            textBoxFirstName.Text = m_LoggedInUser.FirstName;
            textBoxLastName.Text = m_LoggedInUser.LastName;
            pictureBoxProfilePic.LoadAsync(m_LoggedInUser.PictureNormalURL);
        }

        private bool checkLogin()
        {
            bool isUserLoggedIn = false;

            if (m_LoggedInUser != null)
            {
                isUserLoggedIn = true;
            }
            else
            {
                MessageBox.Show(m_LoggedInMessage);
            }

            return isUserLoggedIn;
        }

        private void buttonPost_Click(object sender, EventArgs e)
        {
            if (checkLogin())
            {
                if (!string.IsNullOrWhiteSpace(textBoxComment.Text))
                {
                    LogFileRecorder.Instance.WriteActionToLogFile((sender as Button).Text);
                    Status postedStatus = m_LoggedInUser.PostStatus(textBoxComment.Text);
                    MessageBox.Show("Status Posted! ID: " + postedStatus.Id);
                }
                else
                {
                    MessageBox.Show(m_EmptyFieldLeft);
                }
            }
        }

        private void buttonUploadPhoto_Click(object sender, EventArgs e)
        {
            if (checkLogin())
            {
                if (pictureBoxPhoto.ImageLocation != null)
                {
                    LogFileRecorder.Instance.WriteActionToLogFile((sender as Button).Text);
                    m_LoggedInUser.PostPhoto(pictureBoxPhoto.ImageLocation, textBoxImgTitle.Text);
                }
                else
                {
                    MessageBox.Show("You need to select a picture first!");
                }
            }
        }

        private void buttonBrowsePhoto_Click(object sender, EventArgs e)
        {
            openPictureBrowseDialog(pictureBoxPhoto);
        }

        private void buttonPages_Click(object sender, EventArgs e)
        {
            if (checkLogin())
            {
                LogFileRecorder.Instance.WriteActionToLogFile((sender as Button).Text);
                new Thread(fetchPages).Start();
            }
        }

        private void fetchPages()
        {
            listBoxPages.Items.Clear();
            listBoxPages.DisplayMember = "Name";

            listBoxPages.Invoke(new Action(() =>
            {
                if (m_LoggedInUser.LikedPages.Count == 0)
                {
                    MessageBox.Show("No liked pages to retrieve :(");
                }
                else
                {
                    foreach (Page page in m_LoggedInUser.LikedPages)
                    {
                        listBoxPages.Items.Add(page);
                    }
                }
            }));
        }

        private void ListBoxPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxPages.SelectedItems.Count == 1)
            {
                Page selectedPage = listBoxPages.SelectedItem as Page;
                pictureBoxPage.LoadAsync(selectedPage.PictureNormalURL);
            }
        }

        private void buttonPosts_Click(object sender, EventArgs e)
        {
            if (checkLogin())
            {
                if (m_Posts == null)
                {
                    LogFileRecorder.Instance.WriteActionToLogFile((sender as Button).Text);
                    if (!listBoxPosts.InvokeRequired)
                    {
                        m_Posts = m_LoggedInUser.Posts;
                        postBindingSource.DataSource = m_Posts;
                    }
                    else
                    {
                        listBoxPosts.Invoke(new Action(() => postBindingSource.DataSource = m_Posts));
                    }

                    Thread thread = new Thread(fetchPosts);
                    thread.Start();
                }
            }
        }

        private void fetchPosts()
        {
            listBoxPosts.Invoke(new Action(() =>
            {
                m_PostCount = 0;
                m_ListboxPosts.Clear();

                foreach (Post pst in m_LoggedInUser.Posts)
                {
                    if (pst.Message != null)
                    {
                        m_ListboxPosts.Add(m_PostCount, pst);
                        m_PostCount++;
                    }
                }
            }));
        }

        private void ListBoxPosts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxPosts.SelectedItem != null)
            {
                m_ListboxPosts.TryGetValue((listBoxPosts.SelectedIndex == 0) ? listBoxPosts.SelectedIndex : listBoxPosts.SelectedIndex - 1, out m_SelectedPost);
            }
        }

        private void linkLabelComment_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (m_SelectedPost != null)
            {
                LogFileRecorder.Instance.WriteActionToLogFile((sender as LinkLabel).Text);
                m_SelectedPost.Comment(textBoxPostCom.Text);
            }
        }

        private void linkLabelLike_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (m_SelectedPost != null)
            {
                LogFileRecorder.Instance.WriteActionToLogFile((sender as LinkLabel).Text);
                m_SelectedPost.Like();
            }
        }

        private void linkLabelComments_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (m_SelectedPost != null)
            {
                LogFileRecorder.Instance.WriteActionToLogFile((sender as LinkLabel).Text);
                if (m_SelectedPost.Comments.Count != 0)
                {
                    listBoxComments.Items.Clear();
                    m_ListboxComments.Clear();
                    m_CommentCount = 0;
                    DateTime dateOfComment;

                    foreach (Comment cmnt in m_SelectedPost.Comments)
                    {
                        m_ListboxComments.Add(m_CommentCount, cmnt);
                        listBoxComments.Items.Add(cmnt.Message);
                        dateOfComment = (DateTime)cmnt.CreatedTime;
                        m_CommentCount++;
                    }
                }
            }
            else
            {
                MessageBox.Show("No Posts Have Been Selected!");
            }
        }

        private void ListBoxComments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxComments.SelectedItem != null)
            {
                m_ListboxComments.TryGetValue(listBoxComments.SelectedIndex, out m_SelectedComment);
            }
        }

        private void linkLabelEvents_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (checkLogin())
            {
                LogFileRecorder.Instance.WriteActionToLogFile((sender as LinkLabel).Text);
                Thread thread = new Thread(getEvents);
                thread.Start();
            }
        }

        private void getEvents()
        {
            listBoxEvents.Invoke(new Action(() =>
            {
                List<EventProxy> events = new List<EventProxy>();
                foreach (Event FbEvent in m_LoggedInUser.Events)
                {
                    events.Add(new EventProxy(FbEvent));
                }

                eventProxyBindingSource.DataSource = events;
            }));
        }

        private void linkLabelMostCom_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (checkLogin())
            {
                LogFileRecorder.Instance.WriteActionToLogFile((sender as LinkLabel).Text);
                string pic = comboBoxSelectedObject.SelectedItem.ToString();
                FacebookObjectFactory.eFaceType choise = (FacebookObjectFactory.eFaceType)Enum.Parse(typeof(FacebookObjectFactory.eFaceType), pic);
                fetchItemWithMostComments(FacebookObjectFactory.factoryMethod(choise, m_LoggedInUser));
            }
        }

        private void linkLabelMostLikedPhotos_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (checkLogin())
            {
                LogFileRecorder.Instance.WriteActionToLogFile((sender as LinkLabel).Text);
            }
        }

        private void linkLabelTimeLastPost_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (checkLogin())
            {
                LogFileRecorder.Instance.WriteActionToLogFile((sender as LinkLabel).Text);
                m_LatestPost = new LatestPost();
                m_LatestPost.getTimeFromLastPost(m_LoggedInUser.Posts);
                textBoxTimePassed.Text = string.Format("{0}:{1}", m_LatestPost.HoursLastPost, m_LatestPost.MinutsLastPost);
                m_LoggedInUser.PostStatus(m_LatestPost.GetUserNameAndTimeLatestPost());
            }
        }

        private void linkLabelMostPosts_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (checkLogin())
            {
                LogFileRecorder.Instance.WriteActionToLogFile((sender as LinkLabel).Text);
                new Thread(fetchUsersMostPosts).Start();
            }
        }

        private void fetchUsersMostPosts()
        {
            new Thread(() =>
            {
                int topFive = 5;
                int i = 0;
                m_TopUsers = new TopPosters();
                m_TopUsers.getUserWithMostPosts(m_LoggedInUser.Posts);
                m_UsersWhoPosted = m_TopUsers.PostsFromUsers;

                Invoke(new Action(() =>
                {
                    foreach (KeyValuePair<string, int> pair in m_UsersWhoPosted.OrderByDescending(key => key.Value))
                    {
                        if (i < topFive)
                        {
                            listBoxTopUsersPost.Items.Add(pair.Key);
                            listBoxTopUsersPostsNum.Items.Add(pair.Value.ToString());
                            i++;
                        }
                        else
                        {
                            break;
                        }
                    }
                }));

                m_TopUsers.PostsFromUsers = m_UsersWhoPosted;
            }).Start();
        }

        private void linkLabelPublish_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (checkLogin())
            {
                LogFileRecorder.Instance.WriteActionToLogFile((sender as LinkLabel).Text);
                if (m_TopUsers != null)
                {
                    string str = m_TopUsers.CreateMessage();
                    m_LoggedInUser.PostStatus(str);
                }
                else
                {
                    MessageBox.Show("You need to fetch the top 5 users first!");
                }
            }
        }

        private void buttonCreateAlbum_Click(object sender, EventArgs e)
        {
            if (checkLogin())
            {
                if (!string.IsNullOrEmpty(textBoxAlbum.Text))
                {
                    LogFileRecorder.Instance.WriteActionToLogFile((sender as Button).Text);
                    m_LoggedInUser.CreateAlbum(textBoxAlbum.Text);
                }
                else
                {
                    MessageBox.Show(m_EmptyFieldLeft);
                }
            }
        }

        private void linkLabelAlbums_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (checkLogin())
            {
                LogFileRecorder.Instance.WriteActionToLogFile((sender as LinkLabel).Text);
                getAlbums();
            }
        }

        private void getAlbums()
        {
            if (m_Albums.Count != 0)
            {
                m_Albums.Clear();
            }

            m_Albums = m_LoggedInUser.Albums;
            listBoxAlbums.Items.Clear();

            foreach (Album albm in m_Albums)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(albm.Name).Append(", Photos Number: ").Append(albm.Count == 0 ? 0 : albm.Count);
                listBoxAlbums.Items.Add(sb.ToString());
            }
        }

        private void linkLabelAddPhotoToAlbum_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (listBoxAlbums.SelectedItem != null)
            {
                LogFileRecorder.Instance.WriteActionToLogFile((sender as LinkLabel).Text);
                m_CurrentAlbumSelected.UploadPhoto(pictureBoxPhoto.ImageLocation, textBoxImgTitle.Text);
            }
            else
            {
                MessageBox.Show(m_AlbumMessage);
            }
        }

        private void listBoxAlbums_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_CurrentAlbumSelected = m_Albums[listBoxAlbums.SelectedIndex];
        }

        private void buttonUpdateEventPic_Click(object sender, EventArgs e)
        {
            openPictureBrowseDialog(pictureNormalURLPictureBoxEvent);
        }

        private void openPictureBrowseDialog(PictureBox i_PictureBox)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "C:\\";
            openFileDialog.Filter = "Image Files (*.jpg)|*.jpg|(*.png)|*.png|(*.gif)|*.gif|(*.jpeg)|*.jpeg";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    i_PictureBox.Image = new Bitmap(openFileDialog.FileName);
                    i_PictureBox.ImageLocation = openFileDialog.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image" + ex.Message);
                }
            }
        }

        private void fetchItemWithMostComments(IEnumerable<PostedItem> collection)
        {
            int maxComments = 0;
            PostedItem postMaxComments = new Status();
            foreach (PostedItem pst in collection)
            {
                if (pst.Comments.Count > maxComments)
                {
                    maxComments = pst.Comments.Count;
                    postMaxComments = pst;
                }
            }

            if (m_LoggedInUser.Posts.Count != 0)
            {
                foreach (Comment cmnt in postMaxComments.Comments)
                {
                    listBoxCommentedBy.Items.Add(cmnt.Message);
                }

                textBoxMessages.Text = postMaxComments.Message;
                textBoxLikes.Text = postMaxComments.LikedBy.Count.ToString();
                DateTime dateOfPost = (DateTime)postMaxComments.CreatedTime;
                textBoxUpdateTime.Text = dateOfPost.ToString();
            }
        }

        private void buttonPicturesLikesCount_Click(object sender, EventArgs e)
        {
            initMenuItem((sender as Button).Text, m_PhotosTaggedIn);
            InitPB("Likes");
        }

        private void buttonPicturesCommentCount_Click(object sender, EventArgs e)
        {
            initMenuItem((sender as Button).Text, m_PhotosTaggedIn);
            InitPB("Comments");
        }

        private void buttonRecentPics_Click(object sender, EventArgs e)
        {
            initMenuItem((sender as Button).Text, m_PhotosTaggedIn);
            InitPB("UserLiked");
        }

        private void initMenuItem(string i_ButtonText, List<Photo> i_PhotosTaggedIn)
        {
            LogFileRecorder.Instance.WriteActionToLogFile(i_ButtonText);
            fetchPhotos();
            m_PicturesMenu.SelectItem(m_LoggedInUser, i_ButtonText, m_PhotosTaggedIn);
        }

        private void fetchPhotos()
        {
            if (m_PhotosTaggedIn.Count != 0)
            {
                m_PhotosTaggedIn.Clear();
            }

            foreach (Photo photo in m_LoggedInUser.PhotosTaggedIn)
            {
                m_PhotosTaggedIn.Add(photo);
            }
        }

        private void InitPB(string i_PropertySortedBy)
        {
            foreach (Control control in tabPageFeature1.Controls)
            {
                if (control is PictureBox)
                {
                    tabPageFeature1.Controls.Remove(control);
                    control.Dispose();
                }
            }

            foreach (Label label in m_PhotoLabels)
            {
                label.Dispose();
            }

            foreach (TextBox textbox in m_PhotoTextBoxes)
            {
                textbox.Dispose();
            }

            m_PhotoLabels.Clear();
            m_PhotoTextBoxes.Clear();

            if (label2.Visible == false)
            {
                new Thread(() =>
                {
                    m_TopNineLikedPhotos = new PictureBox[k_NumOfRows, k_NumOfCols];
                    m_TopPhotosLikesCount = new TextBox[k_NumOfPhotos];
                    m_TopPhotosLabels = new Label[k_NumOfPhotos];
                    int xLocation = labelWaitLikes.Left;
                    int yLocation = labelWaitLikes.Bottom + 90;
                    int k = 0;

                    Invoke(new Action(() =>
                    {
                        for (int i = 0; i < k_NumOfRows; i++)
                        {
                            for (int j = 0; j < k_NumOfCols; j++)
                            {
                                PictureBox currentPhoto = new PictureBox();
                                currentPhoto.Height = 90;
                                currentPhoto.Width = 90;
                                currentPhoto.SizeMode = PictureBoxSizeMode.StretchImage;
                                currentPhoto.LoadAsync(m_PhotosTaggedIn[k].PictureNormalURL);
                                currentPhoto.Location = new Point(xLocation, yLocation);

                                Label curretPhotoLikeLabel = new Label();
                                curretPhotoLikeLabel.AutoSize = true;
                                curretPhotoLikeLabel.Location = new Point(xLocation, yLocation + currentPhoto.Height);
                                m_TopPhotosLabels[k] = curretPhotoLikeLabel;
                                m_PhotoLabels.Add(curretPhotoLikeLabel);

                                TextBox currentPhotoLikes = new TextBox();
                                currentPhotoLikes.ReadOnly = true;
                                currentPhotoLikes.Width = 30;
                                currentPhotoLikes.Location = new Point(curretPhotoLikeLabel.Location.X + 60, yLocation + currentPhoto.Height);
                                m_PhotoTextBoxes.Add(currentPhotoLikes);

                                switch (i_PropertySortedBy)
                                {
                                    case "Likes":
                                        curretPhotoLikeLabel.Text = "Likes:";
                                        currentPhotoLikes.Text = m_PhotosTaggedIn[k].LikedBy.Count.ToString();
                                        break;
                                    case "Comments":
                                        curretPhotoLikeLabel.Text = "Comments:";
                                        currentPhotoLikes.Text = m_PhotosTaggedIn[k].Comments.Count.ToString();
                                        break;
                                    case "UserLiked":
                                        break;
                                }

                                m_TopPhotosLikesCount[k] = currentPhotoLikes;
                                xLocation += currentPhoto.Width + 30;
                                m_TopNineLikedPhotos[i, j] = currentPhoto;

                                tabPageFeature1.Controls.AddRange(new Control[] { curretPhotoLikeLabel, currentPhotoLikes, currentPhoto });
                                k++;
                            }

                            xLocation = labelWaitLikes.Left;
                            yLocation = yLocation + 120;
                        }
                    }));
                }).Start();
            }
        }

        private void buttonSafe_Click(object sender, EventArgs e)
        {
            m_safer = new SafeMode();

            label2.Text = "Safe Mode is activated - menu buttons are deactivated";
            label2.Visible = true;
            label2.Width = 40;
            label2.Height = 25;
            label2.Refresh();
            label2.Location = new Point(buttonPicturesLikesCount.Location.X, buttonPicturesLikesCount.Location.Y + 30);
            Dictionary<string, ICommand> tmp = new Dictionary<string, ICommand>();
            tmp = m_PicturesMenu.getCommands();

            foreach (KeyValuePair<string, ICommand> runner in tmp)
            {
                ICommand curr = runner.Value;
                m_safer.DisableCommands += curr.DisableFunctionality;
            }

            m_safer.Notify();
        }
    }
}
