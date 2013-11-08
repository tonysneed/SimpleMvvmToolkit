using System;
using System.Windows;
using System.Threading;
using System.Collections.ObjectModel;

// Toolkit namespace
using SimpleMvvmToolkit;

// Toolkit extension methods
using SimpleMvvmToolkit.ModelExtensions;

namespace SimpleMvvm
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
    public class ItemDetailViewModel : ViewModelDetailBase<ItemDetailViewModel, Item>
    {
        #region Initialization and Cleanup

        // Default ctor
        public ItemDetailViewModel() { }

        // Replace /* DetailType */ with Product,
        // both for the second type argument and the second ctor parameter
        public ItemDetailViewModel(Item model)
        {
            base.Model = model;
        }

        #endregion

        #region Notifications

        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;

        #endregion

        #region Properties

        #endregion

        #region Methods

        #endregion

        #region Completion Callbacks

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