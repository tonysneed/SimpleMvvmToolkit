using System;
using System.Windows;
using System.Threading;
using System.Collections.ObjectModel;

// Toolkit namespace
using SimpleMvvmToolkit;

// Toolkit extension methods
using SimpleMvvmToolkit.ModelExtensions;
using System.Collections.Generic;

namespace MvvmNavigation
{
    /// <summary>
    /// This class extends ViewModelDetailBase which implements IEditableDataObject.
    /// <para>
    /// Specify type being edited <strong>DetailType</strong> as the second type argument
    /// and as a parameter to the seccond ctor.
    /// </para>
    /// <para>
    /// Use the <strong>mvvmprop</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// </summary>
    public class CustomerViewModel : ViewModelDetailBase<CustomerViewModel, Customer>
    {
        #region Initialization and Cleanup

        // Default ctor
        public CustomerViewModel() { }

        #endregion

        #region Notifications

        // TODO: Add events to notify the view or obtain data from the view
        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;

        #endregion

        #region Properties

        public Customer Customer
        {
            get { return Model; }
            set { Model = value; }
        }

        #endregion

        #region Methods

        // STEP 4: Send a message to navigate home
        public void Save()
        {
            // Create page value to send saved customer to home page
            var pageValues = new KeyValuePair<string, object>("SavedCustomer", Customer);
    
            // Navigate to home page with selected customer
            SendMessage(MessageTokens.Navigation, new NotificationEventArgs
                <KeyValuePair<string, object>>(PageNames.Home, pageValues));
        }

        #endregion

        #region Completion Callbacks

        // TODO: Optionally add callback methods for async calls to the service agent
        
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