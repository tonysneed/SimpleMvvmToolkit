using System;
using System.Windows;
using System.Threading;
using System.Collections.ObjectModel;

// Toolkit namespace
using SimpleMvvmToolkit;

// Toolkit extension methods
using SimpleMvvmToolkit.ModelExtensions;

namespace $rootnamespace$
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
    public class $safeitemname$ : ViewModelDetailBase<$safeitemname$, /* DetailType */>
    {
        #region Initialization and Cleanup

        // Default ctor
        public $safeitemname$() { }

        // TODO: Ctor to set base.Model to DetailType
        public $safeitemname$(/* DetailType */ model)
        {
            base.Model = model;
        }

        #endregion

        #region Notifications

        // TODO: Add events to notify the view or obtain data from the view
        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;

        #endregion

        #region Properties

        // TODO: Add properties using the mvvmprop code snippet

        #endregion

        #region Methods

        // TODO: Add methods that will be called by the view

        #endregion

        #region Completion Callbacks

        // TODO: Optionally add callback methods for async calls to the service agent
        
        #endregion

        #region Helpers

        // Helper method to notify View of an error
        private void NotifyError(string message, Exception error)
        {
            // Notify view of an error
            if (ErrorNotice != null)
            {
                ErrorNotice(this, new NotificationEventArgs<Exception>(message, error));
            }
        }

        #endregion
    }
}