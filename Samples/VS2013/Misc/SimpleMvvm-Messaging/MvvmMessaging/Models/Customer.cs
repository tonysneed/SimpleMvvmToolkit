using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using SimpleMvvmToolkit;

namespace MvvmMessaging
{
    public class Customer : ModelBase<Customer>
    {
        // Manufacture a list of customers
        private static ObservableCollection<Customer> customersList;
        public static ObservableCollection<Customer> CustomersList
        {
            get
            {
                if (customersList == null)
                {
                    customersList = new ObservableCollection<Customer>
                    {
                        new Customer { CustomerName = "Bill Gates", Orders = 1000 },
                        new Customer { CustomerName = "Steve Jobs", Orders = 2000 },
                        new Customer { CustomerName = "Mark Zuckerberg", Orders = 3000 }
                    };
                }
                return customersList;
            }
        }

        // Total orders limit
        public static int TotalOrdersLimit
        {
            get { return 10000; }
        }

        private string customerName;
        public string CustomerName
        {
            get { return customerName; }
            set
            {
                customerName = value;
                NotifyPropertyChanged(m => m.CustomerName);
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
    }
}
