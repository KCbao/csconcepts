using Syncfusion.XForms.Chat;
using Xamarin.Forms;
using Aqua.VIC.Mobile.App.DataService;
using Aqua.VIC.Mobile.App.ViewModels.Chat;
using System;
using Aqua.VIC.Mobile.App.Models.Chat;
using Aqua.VIC.Mobile.App.Models.History;
using Syncfusion.DataSource;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using System.Linq;


namespace Aqua.VIC.Mobile.App.Views.Chat
{

    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class ChatMessagePage : ContentPage
    {
        //SfChat sfChat;
        //ChatMessageViewModel viewModel;
        

        public ChatMessagePage()
        {
            InitializeComponent();
            BindingContext = ChatDataService.Instance.TheChatPageViewModel;
        }

        
    }
}