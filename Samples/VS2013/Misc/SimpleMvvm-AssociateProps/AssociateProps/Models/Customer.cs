using System;
using System.Linq;

using SimpleMvvmToolkit;

namespace AssociateProps
{
    public class Customer : ModelBase<Customer>
    {
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                NotifyPropertyChanged(m => m.FirstName);
            }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                NotifyPropertyChanged(m => m.LastName);
            }
        }

        private int orders;
        public int Orders
        {
            get { return orders; }
            set
            {
                orders = value;
                NotifyPropertyChanged(m => m.Orders);
            }
        }

        private bool isActive;
        public bool IsActive
        {
            get { return isActive; }
            set
            {
                isActive = value;
                NotifyPropertyChanged(m => m.IsActive);
            }
        }
    }
}
