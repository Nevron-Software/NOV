using System;

using Nevron.Nov.Examples;
using Nevron.Nov.Mac;

#if UNIFIEDAPI
using Foundation;
using AppKit;
#else
using MonoMac.Foundation;
using MonoMac.AppKit;
#endif

namespace Nevron.Nov.ExamplesApp.Mac
{
	public partial class MainWindow : NSWindow
	{
		#region Constructors

		// Called when created from unmanaged code
		public MainWindow(IntPtr handle) : base(handle)
		{
			Initialize();
		}
		// Called when created directly from a XIB file
		[Export("initWithCoder:")]
		public MainWindow(NSCoder coder) : base(coder)
		{
			Initialize();
		}
		// Shared initialization code
		void Initialize()
		{
			Title = "Nevron Open Vision Examples";
			SetFrame(NSScreen.MainScreen.VisibleFrame, true);

			//NUISettings.EnableMultiThreadedPainting = false;
			//NUISettings.EnablePaintCache = false;

			// place the host inside the mac window
			NNovWidgetHost widget = new NNovWidgetHost(new NExamplesContent());
			ContentView = widget;
		}

		#endregion
	}
}