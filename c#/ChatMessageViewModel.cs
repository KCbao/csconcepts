using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Aqua.VIC.Mobile.App.Models;
using Aqua.VIC.Mobile.App.Models.Chat;
using Aqua.VIC.Mobile.App.Models.History;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Aqua.VIC.Mobile.App.DataService;
using Syncfusion.XForms.Chat;
using System.Linq;
using System.Windows.Input;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Aqua.VIC.Mobile.App.ViewModels.Chat
{
    [Preserve(AllMembers = true)]
    public class ChatMessageViewModel : BaseViewModel
    {
        /// <summary>
        /// Collection of messages in a conversation.
        /// </summary>
        private ObservableCollection<object> messages;

        /// <summary>
        /// Current user of chat.
        /// </summary>
        private Author currentUser;
        private ObservableCollection<ChatMessage> chatMessageInfo = new ObservableCollection<ChatMessage>();
        private ObservableCollection<UserProfile> userprofile = new ObservableCollection<UserProfile>();
        
        private string fullName;
        private DateTime messageUtcTime;
        private DateTime messageLocalTime;
        private int houroffset;
        private int currentUserId= Int32.Parse(App.Current.Properties["CurrentUserId"].ToString());
        private int lastChatId = 0;

        private ICommand sendMessageCommand = new Command<SendMessageEventArgs>(async (e) => await temp(e));
        private static string newMessage;



        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ChatMessageViewModel" /> class.
        /// </summary>
        public ChatMessageViewModel()
        {
            this.messages = new ObservableCollection<object>();
            //this.currentUser = new Author() { Name = "Nancy", Avatar = "Nancy.png" };
            //this.GenerateMessages();
        }
        #endregion


       
        /// <summary>
        /// Gets or sets the collection of messages of a conversation.
        /// </summary>
        public ObservableCollection<object> Messages
        {
            get
            {
                houroffset = Int32.Parse(App.Current.Properties["SiteHoursDifferenceFromUtc"].ToString());

                this.messages.Clear();
                foreach (var item in ChatMessageInfo)
                {
                    foreach(var user in UserProfile)
                    {
                        if (user.Id == item.UserId && user.Id != currentUserId)
                        {
                            fullName = user.FirstName + " "+ user.LastName;

                            //get message Utc Time from backend and change time zone 
                            messageUtcTime = Convert.ToDateTime(item.CreateDateTimeUtc);
                            messageLocalTime = messageUtcTime.AddHours(houroffset);
                            this.messages.Add(new TextMessage()
                            {
                                Author = new Author() { Name = fullName, Avatar = null },
                                Text = item.Message,
                                DateTime = messageLocalTime,
                            });
                        }
                        else if (user.Id == item.UserId && user.Id == currentUserId)
                        { 
                            messageUtcTime = Convert.ToDateTime(item.CreateDateTimeUtc);
                            messageLocalTime = messageUtcTime.AddHours(houroffset);
                            this.messages.Add(new TextMessage()
                            {
                                Author = CurrentUser,
                                Text = item.Message,
                                DateTime = messageLocalTime,
                            });
                        }
                    }
                    
        
                }

        
                return this.messages;
            }

            set
            {
                foreach (var item in ChatMessageInfo)
                {
                    if (item.Id > lastChatId){
                        lastChatId = item.Id;
                    }
                }
     
                this.messages = value;
            }
        }

        /// <summary>
        /// Gets or sets the current user of the message.
        /// </summary>
        public Author CurrentUser
        {
            get
            {
                if (this.currentUser == null)
                {
                    foreach (var user in UserProfile)
                    {
                        if (user.Id == currentUserId)
                        {
                            fullName = user.FirstName + " " + user.LastName;
                            this.currentUser = new Author() { Name = fullName, Avatar = null };
                        }
                    }
                }
                
                return this.currentUser;
            }
            set
            {
                this.currentUser = value;
                //RaisePropertyChanged("CurrentUser");
            }
        }


        /// <summary>
        /// Gets or sets the user profiles.
        /// </summary>
        public ObservableCollection<UserProfile> UserProfile
        {
            get
            {
                return this.userprofile;
            }

            set
            {
                this.userprofile = value;
                this.NotifyPropertyChanged();
            }
        }

        public ObservableCollection<ChatMessage> ChatMessageInfo
        {
            get
            {
                return this.chatMessageInfo;
            }

            set
            {
                this.chatMessageInfo = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the message conversation.
        /// </summary>
        public ICommand SendMessageCommand
        {
            get
            {
                return this.sendMessageCommand;
            }
            set
            {
                this.sendMessageCommand = value;
                this.NotifyPropertyChanged();
            }
        }
        
        private static async Task temp(SendMessageEventArgs e)
        {
            newMessage = "{ \"Text\": "+ "\""+e.Message.Text +"\""+", \"DataType\": \"Text\",\"FileExtension\": null}";
            var res = await ApiService.PostToVicWebApi("chat/message", newMessage);
            await ChatDataService.Instance.LoadLatestChats();
        }
        

        /// <summary>
        /// Gets or sets the lastChatId.
        /// </summary>
        public int LastChatId
        {
            get
            {
                return this.lastChatId;
            }

            set
            {
                this.lastChatId = value;
                this.NotifyPropertyChanged();
            }
        }

        private void GenerateMessages()
        {
            this.messages.Add(new TextMessage()
            {
                Author = currentUser,
                Text = "Hi guys, good morning! I'm very delighted to share with you the news that our team is going to launch a new mobile application.",
            });

            this.messages.Add(new TextMessage()
            {
                Author = new Author() { Name = "Andrea", Avatar = "Andrea.png" },
                Text = "Oh! That's great.",
            });

            this.messages.Add(new TextMessage()
            {
                Author = new Author() { Name = "Harrison", Avatar = "Harrison.png" },
                Text = "That is good news.",
            });

            this.messages.Add(new TextMessage()
            {
                Author = new Author() { Name = "Margaret", Avatar = "Margaret.png" },
                Text = "What kind of application is it and when are we going to launch?",
            });

            this.messages.Add(new TextMessage()
            {
                Author = currentUser,
                Text = "A kind of Emergency Broadcast App.",
            });
        }
    }

    //May 30 new added
    public class SendMessageCommandExt : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            (parameter as SendMessageEventArgs).Handled = true;
        }
    }
}