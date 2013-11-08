using System;
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
    /// This class extends ViewModelDetailBase which implements IEditableDataObject.
    /// <para>
    /// Specify type being edited <strong>DetailType</strong> as the second type argument
    /// and as a parameter to the seccond ctor.
    /// </para>
    /// <para>
    /// Use the <strong>mvvmprop</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// </summary>
    public class CustomerDetailViewModel : ViewModelDetailBase<CustomerDetailViewModel, Customer>
    {
        #region Initialization and Cleanup

        public CustomerDetailViewModel()
        {
            // STEP 2: Register to get notified on order increase
            RegisterToReceiveMessages<IncreaseInfo, ApprovalInfo>
                (MessageTokens.IncreaseOrders, OnOrdersIncrease);
        }

        #endregion

        #region Notifications

        // STEP 3: Handle notification of total orders increase
        void OnOrdersIncrease(object sender, NotificationEventArgs
            <IncreaseInfo, ApprovalInfo> e)
        {
            // Exit if increase requested for another customer
            if (e.Data.CustomerName != Customer.CustomerName) return;

            // Prompt user for approval
            ApproveIncreaseView appoveView = new ApproveIncreaseView(e.Data);
            appoveView.Closed += (s, ea) =>
            {
                // Callback notifier with result
                ApprovalInfo approveInfo = new ApprovalInfo
                    (Customer.CustomerName, e.Data.Amount,
                        (bool)appoveView.DialogResult);
                e.Completed(approveInfo);
            };
            appoveView.Show();
        }

        #endregion

        #region Properties

        // Expose the model for data binding
        public Customer Customer
        {
            get { return base.Model; }
            set { base.Model = value; }
        }

        #endregion
    }
}