using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Cirrious.MvvmCross.Binding.Droid.Views;

namespace $safeprojectname$
{
    // Implement IMvxLayoutInflater
    [Activity(Label = "Simple Mvvm on Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity, IMvxLayoutInflater
    {
        // Binding context
        private MvxAndroidBindingContext _bindingContext;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Initialize binding setup
            BindingSetup.Instance.Initlialize(ApplicationContext);

            // Create service agent and view model
            ICustomerServiceAgent serviceAgent = new MockCustomerServiceAgent();
            var customerViewModel = new CustomerViewModel(serviceAgent);

            // Create binding context, passing view model
            _bindingContext = new MvxAndroidBindingContext(this, this, customerViewModel);

            // Create view by inflating binding on binding context
            var view = _bindingContext.BindingInflate(Resource.Layout.Main, null);

            // Set content view passing inflated view with bindings
            SetContentView(view);
        }
    }
}

