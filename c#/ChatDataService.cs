using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aqua.VIC.Mobile.App.Models;
using Aqua.VIC.Mobile.App.Models.Chat;
using Aqua.VIC.Mobile.App.ViewModels;
using Aqua.VIC.Mobile.App.ViewModels.Chat;
using Aqua.VIC.Mobile.App.ViewModels.History;
using PCLStorage;
using Xamarin.Forms.Internals;

namespace Aqua.VIC.Mobile.App.DataService
{
    // <summary>
    /// Data service to load the chat data from json file or web api
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ChatDataService
    {
        #region fields

        private static ChatDataService instance;
        private ChatMessageViewModel theChatMessageViewModel;
        private int loading = 0; // initialize loading for cache
      
        #endregion

        #region Constructor
        public ChatDataService()
        {
        }
        #endregion

        #region Properties
        //<summary>
        ///Gets an instance of the <see cref="ChatDataService"/>.
        ///</summary>
        public static ChatDataService Instance => instance ?? (instance = new ChatDataService());

        ///<summary>
        ///Gets the unique instance of the <see cref="theChatPageViewModel"/>. Singleton pattern here. 
        ///</summary>
        public ChatMessageViewModel TheChatPageViewModel
         {
            get
            {
                if (theChatMessageViewModel == null)
                {
                    theChatMessageViewModel = new ChatMessageViewModel();
                }
            
                var task = Task.Run(() => LoadCachedChat());
                task.Wait();
                Task.Run(() => LoadLatestChats());

                return theChatMessageViewModel;
            }
            }
        #endregion

        #region Methods
        ///<summary>
        ///Create chat list from chat.json
        ///</summary>
        ///<returns>true if at least one chat is loaded</returns>
       
        public async Task<bool> LoadCachedChat()
        {
            if (1 == Interlocked.Exchange(ref loading, 1))
            {
                return true;
            }
            bool ret = false;
            string json;
            string userjson; // json for userprofile
            
            try
            {
                // read chats from file chat.json
                IFolder rootFolder = FileSystem.Current.LocalStorage;
                IFile file = await rootFolder.GetFileAsync("chat.json");
                json = await file.ReadAllTextAsync();
                MergeChats(ApiService.PopulateDataFromString<ObservableCollection<ChatMessage>>(json));
                // read user profiles from file userprofile.json
                IFile userfile = await rootFolder.GetFileAsync("userprofile.json");
                userjson = await userfile.ReadAllTextAsync();
                GetUserProfile(ApiService.PopulateDataFromString<ObservableCollection<UserProfile>>(userjson));
                
            }
            catch (Exception)
            {
                //file read failed
            }
            Interlocked.Exchange(ref loading, 0);
            return ret;
        }

        /// <summary>
        /// Updates chat by fetching from web api
        /// </summary>
        /// <returns>true if success</returns>
        public async Task<bool> LoadLatestChats()
        {
            if (1 == Interlocked.Exchange(ref loading, 1))
            {
                return true;
            }

            bool ret = false;

            string json;
            string userjson; // for user profiles
            

            if (theChatMessageViewModel.ChatMessageInfo.Count() == 0)
            {
                // the alerts list is empty
                json = await ApiService.GetFromVicWebApi($"chat/message/query");
                userjson = await ApiService.GetFromVicWebApi("user");
                
            }
            else
            {
                json = await ApiService.GetFromVicWebApi($"chat/message/query-next?nextChatMessageId={theChatMessageViewModel.LastChatId}");
                userjson = await ApiService.GetFromVicWebApi("user");
             
            }

            if (!string.IsNullOrEmpty(json))
            {
                var incomingChats =
                    ApiService.PopulateDataFromString<ObservableCollection<ChatMessage>>(json);
                MergeChats(incomingChats);
                ret = true;
            }

            if (!string.IsNullOrEmpty(userjson))
            {
                var userprofiles =
                    ApiService.PopulateDataFromString<ObservableCollection<UserProfile>>(userjson);
                GetUserProfile(userprofiles);
                ret = true;
            }

            try
            {
                // write chats to file
                IFolder rootFolder = FileSystem.Current.LocalStorage;
                IFile file = await rootFolder.CreateFileAsync("chat.json",
                    CreationCollisionOption.ReplaceExisting);
                var chats = ApiService.CreateJsonString(theChatMessageViewModel.ChatMessageInfo);
                await file.WriteAllTextAsync(chats);

                // write user profile to file
                IFile userfile = await rootFolder.CreateFileAsync("userprofile.json",
                    CreationCollisionOption.ReplaceExisting);
                var userprofile = ApiService.CreateJsonString(theChatMessageViewModel.UserProfile);
                await userfile.WriteAllTextAsync(userprofile);

            }
            catch (Exception)
            {
                // file write failed
            }

            Interlocked.Exchange(ref loading, 0);
            return ret;
        }

        private void MergeChats(ObservableCollection<ChatMessage> chats)
        {
            var mergedChatsSet = new HashSet<ChatMessage>();
            var mergedChats = new ObservableCollection<ChatMessage>();
            //int minAlertId = int.MaxValue, maxAlertId = int.MinValue;

            // write what's existing to mergedChatsSet
            if (theChatMessageViewModel.ChatMessageInfo != null)
            {
                foreach (var a in theChatMessageViewModel.ChatMessageInfo)
                {
                    mergedChatsSet.Add(a);
                }
            }

            // write new chats to mergedChatsSet
            foreach (var a in chats)
            {
                mergedChatsSet.Add(a);
            }

         
            // write everything in mergedChatsSet to mergedChats
            // then replace ViewModel to mergedChats
            foreach (var a in mergedChatsSet)
            {
                mergedChats.Add(a);
            }

            
            theChatMessageViewModel.ChatMessageInfo = mergedChats;
        }



        private void GetUserProfile(ObservableCollection<UserProfile> userprofiles)
        {
            var userProfileSet = new ObservableCollection<UserProfile>();

            foreach (var a in userprofiles)
            {
                userProfileSet.Add(a);
            }
            theChatMessageViewModel.UserProfile = userProfileSet;
        }

      
        #endregion
    }
}
