using System;
using System.Windows;
using System.Threading;
using System.Collections.ObjectModel;

// Toolkit namespace
using SimpleMvvmToolkit;

// Toolkit extension methods
using SimpleMvvmToolkit.ModelExtensions;

namespace SimpleMvvm_Silverlight
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
        // Default ctor
        public CustomerViewModel() { }

        // Ctor to set base.Model to model created by service agent
        ICustomerServiceAgent serviceAgent;

        public CustomerViewModel(ICustomerServiceAgent serviceAgent)
        {
            this.serviceAgent = serviceAgent;
        }

        public event EventHandler<NotificationEventArgs<Exception>> ErrorNotice;

        // Set the model to a new customer
        public void NewCustomer()
        {
            base.Model = serviceAgent.CreateCustomer();
        }

        // Helper method to notify View of an error
        private void NotifyError(string message, Exception error)
        {
            // Notify view of an error
            Notify(ErrorNotice, new NotificationEventArgs<Exception>(message, error));
        }
    }
}