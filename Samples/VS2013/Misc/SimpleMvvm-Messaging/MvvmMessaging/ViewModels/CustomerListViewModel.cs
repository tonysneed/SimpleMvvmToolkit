using System;
using System.Linq;
using System.Windows;
using System.Threading;
using System.Collections.ObjectModel;

// Toolkit namespace
using SimpleMvvmToolkit;

// Toolkit extension methods
using SimpleMvvmToolkit.ModelExtensions;

namespace MvvmMessaging
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// Use the <strong>mvvmprop</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// </summary>
    public class CustomerListViewModel : ViewModelBase<CustomerListViewModel>
    {
        #region Initialization and Cleanup

        public CustomerListViewModel()
        {
            // Init customers
            this.Customers = Customer.CustomersList;
            this.SelectedCustomer = this.Customers[0];
        }

        #endregion

        #region Properties

        private ObservableCollection<Customer> customers =
            new ObservableCollection<Customer>();
        public ObservableCollection<Customer> Customers
        {
            get { return customers; }
            set
            {
                customers = value;
                NotifyPropertyChanged(m => m.Customers);
            }
        }

        private int totalOrders;
        public int TotalOrders
        {
            get { return totalOrders; }
            set
            {
                totalOrders = value;
                NotifyPropertyChanged(m => m.TotalOrders);
            }
        }

        public int OrdersLimit
        {
            get { return Customer.TotalOrdersLimit; }
        }

        private Customer selectedCustomer;
        public Customer SelectedCustomer
        {
            get { return selectedCustomer; }
            set
            {
                selectedCustomer = value;
                NotifyPropertyChanged(m => m.SelectedCustomer);
            }
        }

        private string messageText;
        public string MessageText
        {
            get
            {
                if (this.IsInDesignMode()) return "Message";
                return messageText;
            }
            set
            {
                messageText = value;
                NotifyPropertyChanged(m => m.MessageText);
            }
        }

        private Visibility messageVisibility = Visibility.Collapsed;
        public Visibility MessageVisibility
        {
            get { return messageVisibility; }
            set
            {
                messageVisibility = value;
                NotifyPropertyChanged(m => m.MessageVisibility);
            }
        }

        #endregion

        #region Methods

        // Increase selected customer orders by 1000
        public void IncreaseOrders()
        {
            if (SelectedCustomer == null) return;

            // Hide message
            MessageVisibility = Visibility.Collapsed;

            // Increase orders
            SelectedCustomer.Orders += 1000;
            var increaseInfo = new IncreaseInfo(SelectedCustomer.CustomerName, 1000);

            // STEP 4: Broadcast increase message using the MessageBus,
            // specifying a callback that the subscriber can invoke
            SendMessage(MessageTokens.IncreaseOrders, new NotificationEventArgs
                <IncreaseInfo, ApprovalInfo>(null, increaseInfo, OnIncreaseResponse));
        }

        #endregion

        #region Completion Callbacks

        // STEP 5: Handle callback from notification subscriber
        void OnIncreaseResponse(ApprovalInfo approve)
        {
            // Set message text
            string resultText = approve.Result ? "approved" : "rejected";
            MessageText = string.Format("Order quantity of {0} {1} for {2}",
                approve.Amount, resultText, approve.CustomerName);
            MessageVisibility = Visibility.Visible;

            // Reverse increase if rejected
            if (!approve.Result)
            {
                SelectedCustomer.Orders -= 1000;
            }
        }

        #endregion
    }
}