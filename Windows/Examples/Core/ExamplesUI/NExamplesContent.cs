using System.IO;

using Nevron.Nov.Dom;
using Nevron.Nov.UI;
using Nevron.Nov.Xml;
using Nevron.Nov.Graphics;

namespace Nevron.Nov.Examples
{
	/// <summary>
	/// The examples content.
	/// </summary>
	public class NExamplesContent : NContentHolder
	{
		#region Constructors

		/// <summary>
		/// Default constructor
		/// </summary>
		public NExamplesContent()
		{
			// Create the navigation panel
			m_ExamplesHomePage = new NExamplesHomePage();
			m_ExamplesHomePage.LoadFromStream(NResources.Instance.GetResourceStream("RSTR_Examples_xml"));
			m_ExamplesHomePage.TileSelected += OnTileSelected;

			// Host it
			Content = m_ExamplesHomePage;

			// Create the example panel
			m_ExampleHost = new NExampleHost();
			m_ExampleHost.HomeButton.Click += OnHomeButtonClick;

           
		}
		/// <summary>
		/// Static constructor
		/// </summary>
		static NExamplesContent()
		{
			NExamplesContentSchema = NSchema.Create(typeof(NExamplesContent), NContentHolder.NContentHolderSchema);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets/Sets the path to the examples.
		/// </summary>
		public string ExamplesPath
		{
			get
			{
				return m_ExampleHost.ExamplesPath;
			}
			set
			{
				m_ExampleHost.ExamplesPath = value;
			}
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Navigates to the given example. This is used by the Silverlight examples when
		/// there's an example specified in the query string, for example:
		/// "SilverlightTestPage.html?example=UI.NTooBarExample".
		/// </summary>
		/// <param name="exampleType"></param>
		public void NavigateToExample(string exampleType)
		{
			NXmlDocument document;
			using (Stream stream = NResources.Instance.GetResourceStream("RSTR_Examples_xml"))
			{
				document = NXmlDocument.LoadFromStream(stream);				
			}

			// Find the XML element with the given example type:
			NXmlElement element = GetExampleElement(document, exampleType);
			if (element != null)
			{
				NavigateToExample(element);
			}
		}
		/// <summary>
		/// Navigates to the given element.
		/// </summary>
		/// <param name="element"></param>
		public void NavigateToExample(NXmlElement element)
		{
			if (this.Content != m_ExampleHost)
			{
				this.Content = m_ExampleHost;
				m_ExampleHost.InitForElement(element, true);
			}
		}

		#endregion

		#region Event Handles - Navigation

		private void OnTileSelected(NXmlElement element)
		{
			NavigateToExample(element);
		}
		private void OnHomeButtonClick(NEventArgs arg)
		{
			// Show the home page
			this.Content = m_ExamplesHomePage;

			// Clear the text of the search box
			m_ExamplesHomePage.m_SearchBox.Text = null;
		}

		#endregion

		#region Fields

		internal NExamplesHomePage m_ExamplesHomePage;
		private NExampleHost m_ExampleHost;

		#endregion

		#region Schema

		public static readonly NSchema NExamplesContentSchema;

		#endregion

		#region Static Methods

		private static NXmlElement GetExampleElement(NXmlNode node, string type)
		{
			NXmlElement element = node as NXmlElement;
			if (element != null && element.Name == "example" && element.GetAttributeValue("type") == type)
				return element;

			for (int i = 0, count = node.ChildrenCount; i < count; i++)
			{
				element = GetExampleElement(node.GetChildAt(i), type);
				if (element != null)
					return element;
			}

			return null;
		}

		#endregion
	}
}