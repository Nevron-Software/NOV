using Nevron.Nov.Dom;
using Nevron.Nov.Text.Formats;

namespace Nevron.Nov.Examples.Text
{
	public class NEpubImportExample : NTextExampleBase
	{
		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		public NEpubImportExample()
		{
		}

		/// <summary>
		/// Static constructor.
		/// </summary>
		static NEpubImportExample()
		{
			NEpubImportExampleSchema = NSchema.Create(typeof(NEpubImportExample), NTextExampleBase.NTextExampleBaseSchema);
		}

		#endregion

		#region Protected Overrides - Example

		protected override void PopulateRichText()
		{
			m_RichText.LoadFromResource(NResources.RBIN_EPUB_GeographyOfBliss_epub, new NEpubTextFormat());
		}
		protected override string GetExampleDescription()
		{
			return @"
<p>
	This example demonstrates how to import Electronic Publications (EPUB files) in Nevron Rich Text Editor.
</p>
";
		}

		#endregion

		#region Schema

		/// <summary>
		/// Schema associated with NEpubImportExample.
		/// </summary>
		public static readonly NSchema NEpubImportExampleSchema;

		#endregion
	}
}