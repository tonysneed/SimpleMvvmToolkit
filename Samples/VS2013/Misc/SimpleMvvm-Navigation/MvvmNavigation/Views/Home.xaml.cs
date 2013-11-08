using System;
using System.Collections.Generic;
//using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SimpleMvvmToolkit;

namespace MvvmNavigation
{
    public partial class Home : Page
    {
        HomeViewModel viewModel;

        public Home()
        {
            InitializeComponent();
            viewModel = (HomeViewModel)DataContext;
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Get selected customer
            Dictionary<string, object> pageValues;
            if (NavigationHelper.PageValues.TryGetValue(PageNames.Home, out pageValues))
            {
                object propertyValue;
                if (pageValues.TryGetValue("SavedCustomer", out propertyValue))
                {
                    pageValues.Remove("SavedCustomer");
                    Customer savedCustomer = propertyValue as Customer;
                    if (savedCustomer != null)
                    {
                        viewModel.SavedCustomer = savedCustomer;
                    }
                }
            }
        }
    }
}