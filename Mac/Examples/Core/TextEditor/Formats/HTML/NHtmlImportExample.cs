using System;
using System.IO;
using Nevron.Nov.Dom;
using Nevron.Nov.Editors;
using Nevron.Nov.IO;
using Nevron.Nov.Layout;
using Nevron.Nov.Networking;
using Nevron.Nov.Text;
using Nevron.Nov.Text.Formats;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Text
{
	public class NHtmlImportExample : NExampleBase
	{
		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		public NHtmlImportExample()
		{
		}

		/// <summary>
		/// Static constructor.
		/// </summary>
		static NHtmlImportExample()
		{
			NHtmlImportExampleSchema = NSchema.Create(typeof(NHtmlImportExample), NExampleBase.NExampleBaseSchema);
		}

		#endregion

		#region Protected Overrides - Example

		protected override NWidget CreateExampleContent()
		{
			NSplitter splitter = new NSplitter();
			splitter.SplitMode = ENSplitterSplitMode.Proportional;
			splitter.SplitFactor = 0.5;

			// Create the "HTML Code" group box
			m_HtmlTextBox = new NTextBox();
			m_HtmlTextBox.AcceptsEnter = true;
			m_HtmlTextBox.AcceptsTab = true;
			m_HtmlTextBox.Multiline = true;
			m_HtmlTextBox.WordWrap = false;
			m_HtmlTextBox.VScrollMode = ENScrollMode.WhenNeeded;
			m_HtmlTextBox.HScrollMode = ENScrollMode.WhenNeeded;

			NButton importButton = new NButton("Import");
			importButton.Content.HorizontalPlacement = ENHorizontalPlacement.Center;
			importButton.Click += new Function<NEventArgs>(OnImportButtonClick);

			NPairBox pairBox = CreatePairBox(m_HtmlTextBox, importButton);
			splitter.Pane1.Content = new NGroupBox("HTML Code", pairBox);

			// Create the "Preview" group box
			m_PreviewRichText = new NRichTextView();
			m_PreviewRichText.ReadOnly = true;
            m_PreviewRichText.DocumentLoaded += OnRichTextDocumentLoaded;
			splitter.Pane2.Content = new NGroupBox("Preview", m_PreviewRichText);

			return splitter;
		}
		protected override NWidget CreateExampleControls()
		{
			NStackPanel stack = new NStackPanel();
			stack.VerticalSpacing = 10;

			m_ElapsedTimeLabel = new NLabel();
			stack.Add(CreatePredefinedPageGroupBox());
#if !SILVERLIGHT
			stack.Add(CreateNavigationGroupBox());
#endif
			stack.Add(m_ElapsedTimeLabel);

			return stack;
		}
		protected override string GetExampleDescription()
		{
			return @"
<p>
	This example demonstrates the HTML import capabilities of the Nevron Rich Text widget. Simply select one of the preloaded examples
	from the combo box to the right and see it imported. You can also edit the source of the HTML code and press the <b>Import</b>
	button when ready.
</p>
";
		}

		#endregion

		#region Implementation - UI Controls

		private NGroupBox CreatePredefinedPageGroupBox()
		{
			const string HtmlSuitePrefix = "RSTR_HtmlSuite_";

			NListBox testListBox = new NListBox();
			string[] resourceName = NResources.Instance.GetResourceNames();
			for (int i = 0, count = resourceName.Length; i < count; i++)
			{
				string resName = resourceName[i];
				if (resName.StartsWith(HtmlSuitePrefix, StringComparison.Ordinal))
				{
					// The current resource is an HTML page, so add it to the list box
					string testName = resName.Substring(HtmlSuitePrefix.Length, resName.Length - HtmlSuitePrefix.Length - 5);
                    NListBoxItem item = new NListBoxItem(NStringHelpers.InsertSpacesBeforeUppersAndDigits(testName));
					item.Tag = resName;
					testListBox.Items.Add(item);
				}
			}

			testListBox.Selection.Selected += OnListBoxItemSelected;
			testListBox.Selection.SingleSelect(testListBox.Items[0]);
			return new NGroupBox("Predefined HTML pages", testListBox);
		}
		private NGroupBox CreateNavigationGroupBox()
		{
			// Create the navigation pair box
			m_NavigationTextBox = new NTextBox();
			m_NavigationTextBox.Text = DefaultAddress;
			m_NavigationTextBox.VerticalPlacement = ENVerticalPlacement.Center;

			NButton goButton = new NButton("Go");
			goButton.VerticalPlacement = ENVerticalPlacement.Center;
			goButton.Click += new Function<NEventArgs>(OnGoButtonClick);

			NPairBox pairBox = new NPairBox(m_NavigationTextBox, goButton);
			pairBox.FitMode = ENStackFitMode.First;
			pairBox.FillMode = ENStackFillMode.First;
			pairBox.Spacing = 3;

			return new NGroupBox("Import from URL", pairBox);
		}

		#endregion

		#region Implementation - Helper Methods

		private string NormalizeUri(string uri)
		{
			if (String.IsNullOrEmpty(uri))
				return uri;

			if (uri.StartsWith("http://") == false &&
				uri.StartsWith("https://") == false &&
				uri.StartsWith("file:///") == false)
			{
				uri = "http://" + uri;
			}

			return uri;
		}
		private void LoadSource(Stream stream)
		{
			byte[] bytes = NStreamHelpers.ReadToEnd(stream);
			m_HtmlTextBox.Text = NEncoding.UTF8.GetString(bytes);
		}
		private void LoadHtml(Stream stream, string baseUri)
		{
			m_ElapsedTimeLabel.Text = String.Empty;

			stream.Position = 0;
			m_Stopwatch = NStopwatch.StartNew();

			NTextLoadSettings settings = null;
            if (baseUri != null)
            {
				settings = new NTextLoadSettings();
				settings.BaseUri = new NUri(baseUri);
            }

			m_PreviewRichText.LoadFromStream(stream, new NHtmlTextFormat(), settings);
		}

		#endregion

		#region Event Handlers

		private void OnImportButtonClick(NEventArgs arg1)
		{
			byte[] bytes = NEncoding.UTF8.GetBytes(m_HtmlTextBox.Text);
			using (MemoryStream stream = new MemoryStream(bytes))
			{
				LoadHtml(stream, null);
			}
		}
		private void OnListBoxItemSelected(NSelectEventArgs<NListBoxItem> arg1)
		{
			if (arg1.TargetNode == null)
				return;

			// Determine the full name of the selected resource
			string resName = (string)arg1.Item.Tag;

			// Read the stream and set it as text of the HTML code text box
			using (Stream stream = NResources.Instance.GetResourceStream(resName))
			{
				LoadSource(stream);
				LoadHtml(stream, null);
			}
		}
		private void OnGoButtonClick(NEventArgs arg1)
		{
			string address = NStringHelpers.SafeTrim(m_NavigationTextBox.Text);
			if (address == null || address.Length == 0)
				return;

			if (NFile.Exists(address))
			{
				// Load from file
				using (Stream stream = NFile.OpenRead(address))
				{
					LoadSource(stream);
					LoadHtml(stream, address);
				}

				return;
			}

			// Load from URI
			try
			{
				address = NormalizeUri(address);
                m_Stopwatch = NStopwatch.StartNew();
				m_PreviewRichText.LoadFromUri(new NUri(address, ENUriKind.RelativeOrAbsolute));
			}
			catch (Exception ex)
			{
                m_Stopwatch.Stop();
				NMessageBox.Show(ex.Message, "Error", ENMessageBoxButtons.OK, ENMessageBoxIcon.Error);
			}
		}
        private void OnRichTextDocumentLoaded(NEventArgs args)
        {
            // Show the elapsed time
            if (m_Stopwatch != null)
            {
                m_Stopwatch.Stop();
                m_ElapsedTimeLabel.Text = "Elapsed time: " + m_Stopwatch.ElapsedMilliseconds + " ms";
            }
        }
		
		#endregion

		#region Fields

		private NTextBox m_HtmlTextBox;
		private NRichTextView m_PreviewRichText;
		private NTextBox m_NavigationTextBox;
		private NLabel m_ElapsedTimeLabel;

		private NStopwatch m_Stopwatch;

		#endregion

		#region Schema

		/// <summary>
		/// Schema associated with NHtmlImportExample.
		/// </summary>
		public static readonly NSchema NHtmlImportExampleSchema;

		#endregion

		#region Static Methods

		private static NPairBox CreatePairBox(NWidget widget1, NWidget widget2)
		{
			NPairBox pairBox = new NPairBox(widget1, widget2, ENPairBoxRelation.Box1AboveBox2);
			pairBox.FitMode = ENStackFitMode.First;
			pairBox.FillMode = ENStackFillMode.First;
			pairBox.Spacing = NDesign.VerticalSpacing;

			return pairBox;
		}

		#endregion

		#region Constants

		private const string DefaultAddress = @"http://en.wikipedia.org/wiki/Web_browser";

		#endregion
	}
}