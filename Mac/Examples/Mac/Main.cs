using System;
using Nevron.Nov;
using Nevron.Nov.Mac;
using Nevron.Nov.Text;
using System.IO;
using Nevron.Nov.Chart;
using Nevron.Nov.Diagram;
using Nevron.Nov.Schedule;
using Nevron.Nov.Grid;
using Nevron.Nov.UI;

#if UNIFIEDAPI
using Foundation;
using AppKit;
using ObjCRuntime;
#else
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
#endif


namespace Nevron.Nov.ExamplesApp.Mac
{
	class MainClass
	{
		static void Main (string[] args)
		{
			NSApplication.Init ();

			// The following code tries to read the LicenseKey.txt file used to provide an 
			// evaluation license key to the examples. Please consult with the documentation on more information
			// how to apply an evaluation / retail license.
			string licenseKeyPath = Path.Combine(NSBundle.MainBundle.BundlePath, "../../LicenseKey.txt");

			if (File.Exists(licenseKeyPath))
			{
				string[] licenseLines = File.ReadAllLines(licenseKeyPath);

				if (licenseLines.Length > 0)
				{
					NLicenseManager.Instance.SetLicense(new NLicense(licenseLines[0].Trim()));
				}
			}

			// install the NOV Mac integration services
			NNovApplicationInstaller.Install (NTextModule.Instance, NGridModule.Instance, NChartModule.Instance, NDiagramModule.Instance, NScheduleModule.Instance);

			NSApplication.Main (args);
		}
	}
}

