using Nevron.Nov.Dom;
using Nevron.Nov.Graphics;
using Nevron.Nov.Text;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Text
{
	/// <summary>
	/// The example demonstrates how to programmatically create inline elements with different formatting
	/// </summary>
	public class NInlineFormattingExample : NTextExampleBase
	{
		#region Constructors

		/// <summary>
		/// 
		/// </summary>
		public NInlineFormattingExample()
		{
		}
		/// <summary>
		/// 
		/// </summary>
		static NInlineFormattingExample()
		{
			NInlineFormattingExampleSchema = NSchema.Create(typeof(NInlineFormattingExample), NTextExampleBase.NTextExampleBaseSchema);
		}

		#endregion

		#region Overrides

		protected override void PopulateRichText()
		{
			NSection section = new NSection();
			m_RichText.Content.Sections.Add(section);

			section.Blocks.Add(GetDescriptionBlock("Inline Formatting", "The example shows how to add inlines with different formatting to a paragraph.", 1));

			{
				// font style control
				NParagraph paragraph = new NParagraph();

				NTextInline textInline1 = new NTextInline("This paragraph contains text inlines with altered ");
				paragraph.Inlines.Add(textInline1);

				NTextInline textInline2 = new NTextInline("Font Name, ");
				textInline2.FontName = "Tahoma";
				paragraph.Inlines.Add(textInline2);

				NTextInline textInline3 = new NTextInline("Font Size, ");
				textInline3.FontSize = 14;
				paragraph.Inlines.Add(textInline3);

				NTextInline textInline4 = new NTextInline("Font Style (Bold), ");
				textInline4.FontStyle |= ENFontStyle.Bold;
				paragraph.Inlines.Add(textInline4);

				NTextInline textInline5 = new NTextInline("Font Style (Italic), ");
				textInline5.FontStyle |= ENFontStyle.Italic;
				paragraph.Inlines.Add(textInline5);

				NTextInline textInline6 = new NTextInline("Font Style (Underline), ");
				textInline6.FontStyle |= ENFontStyle.Underline;
				paragraph.Inlines.Add(textInline6);

				NTextInline textInline7 = new NTextInline("Font Style (StrikeTrough) ");
                textInline7.FontStyle |= ENFontStyle.Strikethrough;
				paragraph.Inlines.Add(textInline7);

				NTextInline textInline8 = new NTextInline("and Font Style All.");
                textInline8.FontStyle = ENFontStyle.Bold | ENFontStyle.Italic | ENFontStyle.Underline | ENFontStyle.Strikethrough;
				paragraph.Inlines.Add(textInline8);

				section.Blocks.Add(paragraph);
			}

			{
				// appearance control
				NParagraph paragraph = new NParagraph();

				NTextInline textInline1 = new NTextInline("Each text inline element can contain text with differeant fill and background. ");
				paragraph.Inlines.Add(textInline1);

				NTextInline textInline2 = new NTextInline("Fill (Red), Background Fill Inherit. ");
				textInline2.Fill = new NColorFill(ENNamedColor.Red);
				paragraph.Inlines.Add(textInline2);

				NTextInline textInline3 = new NTextInline("Fill inherit, Background Fill (Green).");
				textInline3.BackgroundFill = new NColorFill(ENNamedColor.Green);
				paragraph.Inlines.Add(textInline3);

				section.Blocks.Add(paragraph);
			}

			{
				// line breaks
				// appearance control
				NParagraph paragraph = new NParagraph();

				NTextInline textInline1 = new NTextInline("Line breaks allow you to break...");
				paragraph.Inlines.Add(textInline1);

				NLineBreakInline lineBreak = new NLineBreakInline();
				paragraph.Inlines.Add(lineBreak);

				NTextInline textInline2 = new NTextInline("the current line in the paragraph.");
				paragraph.Inlines.Add(textInline2);

				section.Blocks.Add(paragraph);
			}

			{
				// tabs
				NParagraph paragraph = new NParagraph();

				NTabInline tabInline = new NTabInline();
				paragraph.Inlines.Add(tabInline);

				NTextInline textInline1 = new NTextInline("(Tabs) are not supported by HTML, however they are essential when importing text documents.");
				paragraph.Inlines.Add(textInline1);

				section.Blocks.Add(paragraph);
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected override string GetExampleDescription()
		{
			return @"
<p>
	This example demonstrates how to text style and appearance settings of inline elements as well as how to add line breaks and tabs to paragraphs.
</p>
";
		}

		#endregion

		#region Static

		public static readonly NSchema NInlineFormattingExampleSchema;

		#endregion
	}
}
