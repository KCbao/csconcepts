"NotifyPropertyChanged" need to be carefully defined, either inherit from superclass, or define yourself, but remember need a `catch` to catch error from NotifyPropertyChanged

## DateTime
Datetime object is not immutable, you need to create/assign to a new object when you make changes

## App.Current.Properties
- Problem: It looks like it is working just only if the app still remains alive. If I close the app and start it again the Application.Current.Properties is empty.

Solution: await Application.Current.SavePropertiesAsync();

## ICommand
1. import `using System.Windows.Input;`

a2vmlfzuvnxvtr5nzhid6mrixjv2m3haergfq7zv7dmvruwfg3wq