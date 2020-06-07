using System;
using System.Runtime.Serialization;
using System.ComponentModel;
using Xamarin.Forms.Internals;

namespace Aqua.VIC.Mobile.App.Models.Chat
{
    /// <summary>
    /// Model for chat message 
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class ChatMessage : INotifyPropertyChanged
    {
        #region Fields

        //private string message;

        private DateTime time;

        private string imagePath;

        #endregion

        #region Event

        /// <summary>
        /// The declaration of property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the property that represents chat message id.
        /// </summary>
        [DataMember(Name = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the property that represents userid.
        /// </summary>
        [DataMember(Name = "userId")]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the property that describe the message.
        /// </summary>
        [DataMember(Name = "text")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the property that describes the message create time
        ///  e.g. 2019-11-20T22:13:46.903
        /// </summary>
        [DataMember(Name = "createDateTimeUtc")]
        public string CreateDateTimeUtc { get; set; }

        public DateTime Time
        {
            get
            {
                return DateTime.Parse(CreateDateTimeUtc);
            }

            set
            {
                this.time = DateTime.Parse(CreateDateTimeUtc);
                this.OnPropertyChanged("Time");
            }
        }

        /// <summary>
        /// Gets or sets the property that describes the data type.
        /// e.g. text, video
        /// </summary>
        [DataMember(Name = "dataType")]
        public string DataType { get; set; }

        /// <summary>
        /// Gets or sets the property that describes the file extension.
        /// e.g. null
        /// </summary>
        //[DataMember(Name = "fileExtension")]
        //public string? FileExtension { get; set; }

        /// <summary>
        /// Gets or sets the profile image.
        /// </summary>
        public string ImagePath
        {
            get
            {
                return this.imagePath;
            }

            set
            {
                this.imagePath = value;
                this.OnPropertyChanged("ImagePath");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the message is received or sent.
        /// </summary>
        public bool IsReceived { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Helps HashSet to distinguish between alerts
        /// </summary>
        /// <returns>Custom hashcode</returns>
        public override int GetHashCode()
        {
            return Id;
        }

        public override bool Equals(object obj)
        {
            if (obj is ChatMessage)
            {
                var a = obj as ChatMessage;
                return Id == a.Id;
            }

            return base.Equals(obj);
        }

        /// <summary>
        /// The PropertyChanged event occurs when property value is changed.
        /// </summary>
        /// <param name="property">property name</param>
        protected void OnPropertyChanged(string property)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        #endregion
    }
}
