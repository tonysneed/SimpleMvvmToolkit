using System;
using System.Windows;
using System.Threading;
using System.Collections.ObjectModel;

// Toolkit namespace
using SimpleMvvmToolkit;

// Toolkit extension methods
using SimpleMvvmToolkit.ModelExtensions;

namespace ThreadSync
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// Use the <strong>mvvmprop</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// While on UI thread set base.SynchronizationContext to SynchronizationContext.Current
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase<MainViewModel>
    {
        #region Notifications

        // Events to notify the view or obtain data from the view
        public event EventHandler<NotificationEventArgs<bool, bool>> MaxReachedNotice;

        #endregion

        #region Properties

        // Properties added using the mvvmprop code snippet

        private int iterations = 10;
        public int Iterations
        {
            get { return iterations; }
            set
            {
                iterations = value;
                NotifyPropertyChanged(m => m.Iterations);
            }
        }

        private int max = 31;
        public int Max
        {
            get { return max; }
            set
            {
                max = value;
                NotifyPropertyChanged(m => m.Max);
            }
        }

        private int result;
        public int Result
        {
            get { return result; }
            private set
            {
                result = value;
                NotifyPropertyChanged(m => m.Result);
            }
        }

        public bool CanWork
        {
            get 
            { 
                return !IsBusy;
            }
            private set
            {
                NotifyPropertyChanged(m => m.CanWork);
            }
        }

        private bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            private set
            {
                CanWork = !value;
                isBusy = value;
                NotifyPropertyChanged(m => m.IsBusy);
            }
        }

        #endregion

        #region Methods

        // Perform work asynchronously
        public void DoWork()
        {
            // Spin up a thread
            Thread worker = new Thread(InternalDoWork);
            worker.Name = "My Worker Thread";
            worker.Start();
        }

        private void InternalDoWork()
        {
            // Cancelled flag
            bool cancelled = false;

            // Max reached flag
            bool maxReached = false;

            // Wait handle
            var wait = new AutoResetEvent(false);

            // Flip IsBusy
            IsBusy = true;

            // Do the work
            for (int i = 0; i < Iterations; i++)
            {
                // Pause
                Thread.Sleep(500);

                // Calc value
                int val = (i + 1) * 10;

                // Notify view if we've reached the max
                if (val >= max && !maxReached)
                {
                    var ea = new NotificationEventArgs<bool, bool>
                        (null, true, cancel =>
                        {
                            // Set cancelled and signal wait handle
                            cancelled = cancel;
                            wait.Set();
                        });
                    Notify<bool, bool>(MaxReachedNotice, ea);

                    // Here we want to pause for the user
                    wait.WaitOne();

                    // Set max reached
                    maxReached = true;

                    // Exit loop if cancelled
                    if (cancelled) break;
                }

                // Set Result
                Result = val;
            }

            // Flip IsBusy
            IsBusy = false;
        }

        #endregion
    }
}