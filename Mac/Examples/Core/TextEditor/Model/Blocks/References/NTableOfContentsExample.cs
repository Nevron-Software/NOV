using System;
using Nevron.Nov.Dom;
using Nevron.Nov.Text;

namespace Nevron.Nov.Examples.Text
{
	public class NTableOfContentsExample : NTextExampleBase
	{
		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		public NTableOfContentsExample()
		{
		}

		/// <summary>
		/// Static constructor.
		/// </summary>
		static NTableOfContentsExample()
		{
			NTableOfContentsExampleSchema = NSchema.Create(typeof(NTableOfContentsExample), NTextExampleBase.NTextExampleBaseSchema);
		}

		#endregion

		#region Protected Overrides - Example

		protected override void PopulateRichText()
		{
			// Get references to the heading styles
			NDocumentBlock documentBlock = m_RichText.Content;
			NRichTextStyle heading1Style = documentBlock.Styles.FindStyleByTypeAndId(ENRichTextStyleType.Paragraph, "Heading1");
			NRichTextStyle heading2Style = documentBlock.Styles.FindStyleByTypeAndId(ENRichTextStyleType.Paragraph, "Heading2");

			NSection section = new NSection();
			m_RichText.Content.Sections.Add(section);

			// Add a table of contents block
			NTableOfContentsBlock tableOfContents = new NTableOfContentsBlock();
			section.Blocks.Add(tableOfContents);

			// Create chapter 1
			NParagraph paragraph = new NParagraph("Chapter 1");
			section.Blocks.Add(paragraph);
			heading1Style.Apply(paragraph);

			paragraph = new NParagraph("Topic 1.1");
			section.Blocks.Add(paragraph);
			heading2Style.Apply(paragraph);

			AddParagraphs(section, "This is a sample paragraph from the first topic of chapter one.", 20);

			paragraph = new NParagraph("Topic 1.2");
			section.Blocks.Add(paragraph);
			heading2Style.Apply(paragraph);

			AddParagraphs(section, "This is a sample paragraph from the second topic of chapter one.", 20);

			// Create chapter 2
			paragraph = new NParagraph("Chapter 2");
			section.Blocks.Add(paragraph);
			heading1Style.Apply(paragraph);

			paragraph = new NParagraph("Topic 2.1");
			section.Blocks.Add(paragraph);
			heading2Style.Apply(paragraph);

			AddParagraphs(section, "This is a sample paragraph from the first topic of chapter two.", 20);

			paragraph = new NParagraph("Topic 2.2");
			section.Blocks.Add(paragraph);
			heading2Style.Apply(paragraph);

			AddParagraphs(section, "This is a sample paragraph from the second topic of chapter two.", 20);

			// Update the table of contents
			m_RichText.Document.Evaluate();
			tableOfContents.Update();
		}
		protected override string GetExampleDescription()
		{
			return @"
<p>This example demonstrates how to programmatically create and add a table of contents block to a document.</p>
";
		}

		#endregion

		#region Implementation

		private void AddParagraphs(NSection section, string text, int count)
		{
			// Duplicate the given text 5 times
			text = String.Join(" ", new string[] { text, text, text, text, text });

			// Create the given number of paragraphs with the text
			for (int i = 0; i < count; i++)
			{
				section.Blocks.Add(new NParagraph(text));
			}
		}

		#endregion

		#region Schema

		/// <summary>
		/// Schema associated with NTableOfContentsExample.
		/// </summary>
		public static readonly NSchema NTableOfContentsExampleSchema;

		#endregion
	}
}