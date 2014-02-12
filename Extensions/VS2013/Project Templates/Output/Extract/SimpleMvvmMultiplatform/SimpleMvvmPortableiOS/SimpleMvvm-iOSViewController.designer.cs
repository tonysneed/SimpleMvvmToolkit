// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace $safeprojectname$
{
	[Register ("SimpleMvvm_iOSViewController")]
	partial class SimpleMvvm_iOSViewController
	{
		[Outlet]
		[GeneratedCodeAttribute ("iOS Designer", "1.0")]
		MonoTouch.UIKit.UILabel BannerLabel_iPhone { get; set; }

		[Outlet]
		[GeneratedCodeAttribute ("iOS Designer", "1.0")]
		MonoTouch.UIKit.UILabel BannerLabel_iPad { get; set; }

		[Outlet]
		[GeneratedCodeAttribute ("iOS Designer", "1.0")]
		MonoTouch.UIKit.UITextField CustomerCityText_iPad { get; set; }

		[Outlet]
		[GeneratedCodeAttribute ("iOS Designer", "1.0")]
		MonoTouch.UIKit.UITextField CustomerCityText_iPhone { get; set; }

		[Outlet]
		[GeneratedCodeAttribute ("iOS Designer", "1.0")]
		MonoTouch.UIKit.UITextField CustomerIdText_iPad { get; set; }

		[Outlet]
		[GeneratedCodeAttribute ("iOS Designer", "1.0")]
		MonoTouch.UIKit.UITextField CustomerIdText_iPhone { get; set; }

		[Outlet]
		[GeneratedCodeAttribute ("iOS Designer", "1.0")]
		MonoTouch.UIKit.UITextField CustomerNameText_iPad { get; set; }

		[Outlet]
		[GeneratedCodeAttribute ("iOS Designer", "1.0")]
		MonoTouch.UIKit.UITextField CustomerNameText_iPhone { get; set; }

		[Outlet]
		[GeneratedCodeAttribute ("iOS Designer", "1.0")]
		MonoTouch.UIKit.UIButton NewCustomerButton_iPad { get; set; }

		[Outlet]
		[GeneratedCodeAttribute ("iOS Designer", "1.0")]
		MonoTouch.UIKit.UIButton NewCustomerButton_iPhone { get; set; }

		void ReleaseDesignerOutlets ()
		{
            if (BannerLabel_iPhone != null)
            {
                BannerLabel_iPhone.Dispose();
                BannerLabel_iPhone = null;
			}
			if (BannerLabel_iPad != null) {
				BannerLabel_iPad.Dispose ();
				BannerLabel_iPad = null;
			}
			if (CustomerCityText_iPad != null) {
				CustomerCityText_iPad.Dispose ();
				CustomerCityText_iPad = null;
			}
			if (CustomerCityText_iPhone != null) {
				CustomerCityText_iPhone.Dispose ();
				CustomerCityText_iPhone = null;
			}
			if (CustomerIdText_iPad != null) {
				CustomerIdText_iPad.Dispose ();
				CustomerIdText_iPad = null;
			}
			if (CustomerIdText_iPhone != null) {
				CustomerIdText_iPhone.Dispose ();
				CustomerIdText_iPhone = null;
			}
			if (CustomerNameText_iPad != null) {
				CustomerNameText_iPad.Dispose ();
				CustomerNameText_iPad = null;
			}
			if (CustomerNameText_iPhone != null) {
				CustomerNameText_iPhone.Dispose ();
				CustomerNameText_iPhone = null;
			}
			if (NewCustomerButton_iPad != null) {
				NewCustomerButton_iPad.Dispose ();
				NewCustomerButton_iPad = null;
			}
			if (NewCustomerButton_iPhone != null) {
				NewCustomerButton_iPhone.Dispose ();
				NewCustomerButton_iPhone = null;
			}
		}
	}
}
