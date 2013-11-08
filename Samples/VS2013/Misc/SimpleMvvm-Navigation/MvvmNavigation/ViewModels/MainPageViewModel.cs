using System;
using System.Windows;
using System.Threading;
using System.Collections.ObjectModel;

// Toolkit namespace
using SimpleMvvmToolkit;

// Toolkit extension methods
using SimpleMvvmToolkit.ModelExtensions;
using System.Windows.Input;
using System.Collections.Generic;

namespace MvvmNavigation
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// Use the <strong>mvvmprop</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// </summary>
    public class MainPageViewModel : ViewModelBase<MainPageViewModel>
    {
        #region Initialization and Cleanup

        public MainPageViewModel()
        {
            // STEP 5: Subscribe to navigation message, passing OnNavigationRequested
            // for the callback method
            RegisterToReceiveMessages<KeyValuePair<string, object>>(MessageTokens.Navigation, OnNavigationRequested);
        }

        #endregion

        #region Notifications

        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;

        #endregion

        #region Properties

        // STEP 1a: Add SelectedPage property (Uri) using mvvmprop snippet
        // Navigation frame's Source property is bound to SelectedPage
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

        // STEP 1b: Add Navigate method with string parameter
        // Create relative uri to set SelectedPage property
        // Page name is name of view (without .xaml)
        private void Navigate(string pageName)
        {
            Uri pageUri = new Uri("/" + pageName, UriKind.Relative);
            this.SelectedPage = pageUri;
        }

        #endregion

        #region Completion Callbacks

        // STEP 5a: Create a callback method (EventHandler<NotificationEventArgs)
        // in which you call Navigate, passing e.Message
        void OnNavigationRequested(object sender, 
            NotificationEventArgs<KeyValuePair<string, object>> e)
        {
            // Pass data to destination page
            var pageValues = new Dictionary<string, object> { { e.Data.Key, e.Data.Value } };
            NavigationHelper.PageValues.Add(e.Message, pageValues);

            // Navigate to the page
            Navigate(e.Message);
        }

        #endregion

        #region Commands

        // STEP 1c: Add command using mvvmcommand snippet that calls Navigate
        private DelegateCommand<string> navigateCommand;
        public DelegateCommand<string> NavigateCommand
        {
            get
            {
                if (navigateCommand == null) navigateCommand = new DelegateCommand<string>(Navigate);
                return navigateCommand;
            }
            private set { navigateCommand = value; }
        }

        #endregion

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