Simple Mvvm Portable Library ReadMe

The Simple Mvvm Portable Library project template provides sample models, view models, services and locators
that can be shared across multiple platform-specific projects, such as WPF, Silverlight, Windows Store,
Windows Phone, iOS and Android.

The project references the Portable Simple Mvvm Toolkit NuGet package, which includes MessageBus, ModelBase,
ViewModelBase and ViewModelDetailBase classes.  UIDispatcher is not included because each platform has
a different mechanism for thread marshalling.  However, this is not a problem because all platforms support
the C# async / await pattern for multi-threading in UI applications.