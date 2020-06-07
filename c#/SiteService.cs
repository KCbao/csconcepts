using System;
using System.Runtime.Serialization;
using System.ComponentModel;
using Xamarin.Forms.Internals;

namespace Aqua.VIC.Mobile.App.Models.Chat
{
    /// <summary>
    /// Model for user profiles
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class UserProfile : INotifyPropertyChanged
    {
        #region Fields
        #endregion

        #region Event
        /// <summary>
        /// The declaration of property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the property that represents user id.
        /// </summary>
        [DataMember(Name = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the property that represents email.
        /// </summary>
        [DataMember(Name = "email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the property that represents first name.
        /// </summary>
        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }


        /// <summary>
        /// Gets or sets the property that represents last name.
        /// </summary>
        [DataMember(Name = "lastName")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the property that represents createDateTimeUtc for the profile.
        /// </summary>
        [DataMember(Name = "createDateTimeUtc")]
        public string CreateDateTimeUtc { get; set; }
        #endregion


        #region Methods

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
