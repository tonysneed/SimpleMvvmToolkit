using System;
using System.Windows;
using System.Threading;
using System.Windows.Input;
using System.Collections.ObjectModel;

// Toolkit namespace
using SimpleMvvmToolkit;

// Toolkit extension methods
using SimpleMvvmToolkit.ModelExtensions;

namespace $safeprojectname$
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// Use the <strong>mvvmprop</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// </summary>
    public class MainPageViewModel : ViewModelBase<MainPageViewModel>
    {
        public MainPageViewModel()
        {
            // Register to get notified when a message arrives
            // at the MessageBus using the navigation message token
            this.RegisterToReceiveMessages(MessageTokens.Navigation, OnNavigationRequested);
        }
        
        #region Notifications

        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;

        #endregion

        #region Properties

        // Use the mvvmprop code snippet to add a SelectedPage property of type Uri
        private Uri selectedPage;
        public Uri SelectedPage
        {
            get { return selectedPage; }
            set
            {
                selectedPage = value;
                NotifyPropertyChanged(m => m.SelectedPage);
            }
        }

        #endregion

        #region Methods

        // Add Navigate method with string parameter
        // Create relative uri and set SelectedPage property to it
        private void Navigate(string pageName)
        {
            Uri pageUri = new Uri(pageName, UriKind.Relative);
            this.SelectedPage = pageUri;
        }

        // Create a callback method (EventHandler<NotificationEventArgs)
        // in which you call Navigate, passing e.Message
        public void OnNavigationRequested(object sender, NotificationEventArgs e)
        {
            Navigate(e.Message);
        }

        #endregion

        #region Commands

        // Add a command using the mvvmcommand snippet that calls Navigate
        private DelegateCommand<string> navigateCommand;
        public DelegateCommand<string> NavigateCommand
        {
            get
            {
                if (navigateCommand == null) navigateCommand =
                    new DelegateCommand<string>(Navigate);
                return navigateCommand;
            }
            private set { navigateCommand = value; }
        }

        #endregion Commands

        #region Helpers

        // Helper method to notify View of an error
        private void NotifyError(string message, Exception error)
        {
            // Notify view of an error
            Notify(ErrorNotice, new NotificationEventArgs<Exception>(message, error));
        }

        #endregion
    }
}