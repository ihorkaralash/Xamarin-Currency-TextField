// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace CurrencyTextField.Sample.iOS
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UIButton GetValueButton { get; set; }

		[Outlet]
		Plugin.TextField.iOS.CurrencyTextField TextField { get; set; }

		[Outlet]
		UIKit.UILabel ValueLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (GetValueButton != null) {
				GetValueButton.Dispose ();
				GetValueButton = null;
			}

			if (TextField != null) {
				TextField.Dispose ();
				TextField = null;
			}

			if (ValueLabel != null) {
				ValueLabel.Dispose ();
				ValueLabel = null;
			}
		}
	}
}
