using System;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MvvmMessaging
{
    public partial class CustomerDetailView : UserControl
    {
        CustomerDetailViewModel vm;

        public CustomerDetailView()
        {
            InitializeComponent();

            // Get a reference to the view-model
            vm = (CustomerDetailViewModel)DataContext;
        }

        private int customerIndex;
        public int CustomerIndex
        {
            get { return customerIndex; }
            set
            {
                customerIndex = value;
                vm.Customer = Customer.CustomersList[value];
            }
        }
    }
}
