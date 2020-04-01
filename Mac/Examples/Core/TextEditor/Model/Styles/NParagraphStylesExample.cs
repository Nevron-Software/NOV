using Nevron.Nov.Dom;
using Nevron.Nov.Graphics;
using Nevron.Nov.Text;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Text
{
	public class NParagraphStylesExample : NTextExampleBase
	{
		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		public NParagraphStylesExample()
		{
		}

		/// <summary>
		/// Static constructor.
		/// </summary>
		static NParagraphStylesExample()
		{
			NParagraphStylesExampleSchema = NSchema.Create(typeof(NParagraphStylesExample), NTextExampleBase.NTextExampleBaseSchema);
		}

		#endregion

		#region Protected Overrides - Example

		protected override void PopulateRichText()
		{
			NDocumentBlock documentBlock = m_RichText.Content;
			NSection section = new NSection();
			documentBlock.Sections.Add(section);

			// Create the first paragraph
			NParagraph paragraph1 = new NParagraph("This paragraph is styled with a predefined paragraph style.");
			section.Blocks.Add(paragraph1);

			// Apply a predefined paragraph style
			NRichTextStyle predefinedStyle = documentBlock.Styles.FindStyleByTypeAndId(ENRichTextStyleType.Paragraph, "Heading2");
			predefinedStyle.Apply(paragraph1);

			// Create the second paragraph
			NParagraph paragraph2 = new NParagraph("This paragraph is styled with a custom paragraph style.");
			section.Blocks.Add(paragraph2);

			// Create a custom paragraph style and apply it
			NParagraphStyle customStyle = new NParagraphStyle("CustomStyle");
			customStyle.ParagraphRule = new NParagraphRule();
			customStyle.ParagraphRule.BorderRule = new NBorderRule(ENPredefinedBorderStyle.Dash, NColor.Red, new NMargins(1));
			customStyle.ParagraphRule.HorizontalAlignment = ENAlign.Center;
			customStyle.ParagraphRule.Padding = new NMargins(20);

			customStyle.InlineRule = new NInlineRule();
			customStyle.InlineRule.Fill = new NColorFill(NColor.Blue);

			customStyle.Apply(paragraph2);
			paragraph2.MarginTop = 30;
		}
		protected override string GetExampleDescription()
		{
			return @"
<p>
	This example demonstrates how to create and apply paragraph styles.
</p>
";
		}

		#endregion

		#region Schema

		/// <summary>
		/// Schema associated with NParagraphStylesExample.
		/// </summary>
		public static readonly NSchema NParagraphStylesExampleSchema;

		#endregion
	}
}