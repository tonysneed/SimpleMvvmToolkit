using System;
using System.Diagnostics;
using System.Drawing;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using SimpleMvvm_Portable_Library;

namespace SimpleMvvmiOS
{
    // Implement IMvxBindable
	public partial class SimpleMvvm_iOSViewController 
        : UIViewController, IMvxBindable
	{
		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public SimpleMvvm_iOSViewController (IntPtr handle) : base (handle)
		{
            // Create binding context
            this.CreateBindingContext();

            // Create view model with service agent
            ICustomerServiceAgent serviceAgent = new MockCustomerServiceAgent();

            // Assign view model to the data context
            DataContext = new CustomerViewModel(serviceAgent);
		}

        // Binding context property
	    public IMvxBindingContext BindingContext { get; set; }

        // Expose data context from binding context
	    public object DataContext
	    {
            get { return BindingContext.DataContext; }
            set { BindingContext.DataContext = value; }
	    }

        // Clear bindings when disposed
        protected override void Dispose(bool disposing)
        {
            if (disposing) BindingContext.ClearAllBindings();
            base.Dispose(disposing);
        }

        public override void DidReceiveMemoryWarning()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Bindings setup helper
		    var bindingSet = this.CreateBindingSet<SimpleMvvm_iOSViewController, CustomerViewModel>();
		    
            // Properties
            if (BannerLabel_iPhone != null)
                bindingSet.Bind(BannerLabel_iPhone).To(vm => vm.BannerText);
            if (BannerLabel_iPad != null)
                bindingSet.Bind(BannerLabel_iPad).To(vm => vm.BannerText);

            if (CustomerIdText_iPhone != null)
                bindingSet.Bind(CustomerIdText_iPhone).To(vm => vm.Model.CustomerId);
            if (CustomerIdText_iPad != null)
                bindingSet.Bind(CustomerIdText_iPad).To(vm => vm.Model.CustomerId);

            if (CustomerNameText_iPhone != null)
                bindingSet.Bind(CustomerNameText_iPhone).To(vm => vm.Model.CustomerName);
            if (CustomerNameText_iPad != null)
                bindingSet.Bind(CustomerNameText_iPad).To(vm => vm.Model.CustomerName);

            if (CustomerCityText_iPhone != null)
                bindingSet.Bind(CustomerCityText_iPhone).To(vm => vm.Model.City);
            if (CustomerCityText_iPad != null)
                bindingSet.Bind(CustomerCityText_iPad).To(vm => vm.Model.City);

            // Command
            if (NewCustomerButton_iPhone != null)
                bindingSet.Bind(NewCustomerButton_iPhone).To(vm => vm.NewCustomerCommand);
            if (NewCustomerButton_iPad != null)
                bindingSet.Bind(NewCustomerButton_iPad).To(vm => vm.NewCustomerCommand);

            // Apply bindings
            bindingSet.Apply();
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}

		#endregion
	}
}

