using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace MvvmMessaging
{
    public partial class CustomerListView : UserControl
    {
        CustomerListViewModel vm;

        public CustomerListView()
        {
            InitializeComponent();

            // Get a reference to the view-model
            vm = (CustomerListViewModel)DataContext;
        }

        public ObservableCollection<Customer> Customers
        {
            get { return vm.Customers; }
            set { vm.Customers = value; }
        }
    }
}
