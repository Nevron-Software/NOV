using Nevron.Nov.Dom;
using Nevron.Nov.Graphics;
using Nevron.Nov.Text;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Text
{
	public class NInlineStylesExample : NTextExampleBase
	{
		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		public NInlineStylesExample()
		{
		}

		/// <summary>
		/// Static constructor.
		/// </summary>
		static NInlineStylesExample()
		{
			NInlineStylesExampleSchema = NSchema.Create(typeof(NInlineStylesExample), NTextExampleBase.NTextExampleBaseSchema);
		}

		#endregion

		#region Protected Overrides - Example

		protected override void PopulateRichText()
		{
			NDocumentBlock documentBlock = m_RichText.Content;
			NSection section = new NSection();
			documentBlock.Sections.Add(section);

			NParagraph paragraph = new NParagraph();
			section.Blocks.Add(paragraph);

			// Create the first inline
			NTextInline inline1 = new NTextInline("This is the first inline. ");
			paragraph.Inlines.Add(inline1);

			// Create and apply an inline style
			NInlineStyle style1 = new NInlineStyle("MyRedStyle");
			style1.Rule = new NInlineRule(NColor.Red);
			style1.Rule.FontStyle = ENFontStyle.Bold;
			style1.Apply(inline1);

			// Create the second inline
			NTextInline inline2 = new NTextInline("This is the second inline.");
			paragraph.Inlines.Add(inline2);

			// Create and apply an inline style
			NInlineStyle style2 = new NInlineStyle("MyBlueStyle");
			style2.Rule = new NInlineRule(NColor.Blue);
			style2.Rule.FontStyle = ENFontStyle.Italic;
			style2.Apply(inline2);
		}
		protected override string GetExampleDescription()
		{
			return @"
<p>
	This example demonstrates how to create and apply inline styles.
</p>
";
		}

		#endregion

		#region Schema

		/// <summary>
		/// Schema associated with NInlineStylesExample.
		/// </summary>
		public static readonly NSchema NInlineStylesExampleSchema;

		#endregion
	}
}