using System;
using System.Windows;
using System.Threading;
using System.Collections.ObjectModel;

// Toolkit namespace
using SimpleMvvmToolkit;

// Toolkit extension methods
using SimpleMvvmToolkit.ModelExtensions;

namespace SimpleMvvmCommands
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// Use the <strong>mvvmprop</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// </summary>
    public class CalcViewModel : ViewModelBase<CalcViewModel>
    {
        #region Initialization and Cleanup

        // Default ctor
        public CalcViewModel() { }

        #endregion

        #region Notifications

        // TODO: Add events to notify the view or obtain data from the view
        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;

        #endregion

        #region Properties

        private int amount;
        public int Amount
        {
            get { return amount; }
            set
            {
                amount = value;
                NotifyPropertyChanged(m => m.Amount);

                // Raise can execute changed event on command
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        private int total;
        public int Total
        {
            get { return total; }
            set
            {
                total = value;
                NotifyPropertyChanged(m => m.Total);
            }
        }

        #endregion

        #region Methods

        void Add(int num)
        {
            Total = Amount + num;
        }

        bool CanAdd(int num)
        {
            return Amount > 0;
        }

        #endregion

        #region Commands

        private DelegateCommand<int> addCommand;
        public DelegateCommand<int> AddCommand
        {
            get { return addCommand ?? (addCommand = new DelegateCommand<int>(Add, CanAdd)); }
            private set { addCommand = value; }
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