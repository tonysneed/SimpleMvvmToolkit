using System;
using System.Linq;

using SimpleMvvmToolkit;
using System.Windows;

namespace AssociateProps
{
public class CustomerViewModel : ViewModelDetailBase<CustomerViewModel, Customer>
{
    public Customer Customer
    {
        get
        {
            return Model;
        }
        set
        {
            Model = value;

            // Associate model and view-model properties
            AssociateProperties(m => m.FirstName, vm => vm.CustomerName);
            AssociateProperties(m => m.LastName, vm => vm.CustomerName);
            AssociateProperties(m => m.IsActive, vm => vm.OrdersVisibility);
        }
    }

        public string CustomerName
        {
            get
            {
                // Concatenate first and last name
                return string.Format("{0} {1}",
                    Model.FirstName, Model.LastName);
            }
        }

        public Visibility OrdersVisibility
        {
            get
            {
                // Show orders only for active customers
                return Model.IsActive ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }
}
