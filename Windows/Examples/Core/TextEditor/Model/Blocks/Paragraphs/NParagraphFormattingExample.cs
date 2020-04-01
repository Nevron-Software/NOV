using Nevron.Nov.Dom;
using Nevron.Nov.Graphics;
using Nevron.Nov.Text;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Text
{
    /// <summary>
    /// The example demonstrates how to programmatically create paragraphs with differnt formatting
    /// </summary>
    public class NParagraphFormattingExample : NTextExampleBase
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public NParagraphFormattingExample()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        static NParagraphFormattingExample()
        {
            NParagraphFormattingExampleSchema = NSchema.Create(typeof(NParagraphFormattingExample), NTextExampleBase.NTextExampleBaseSchema);
        }

        #endregion

        #region Protected Overrides - Example

		protected override void PopulateRichText()
        {
            NSection section = new NSection();
			m_RichText.Content.Sections.Add(section);

            // paragraphs with different horizontal alignment
			section.Blocks.Add(GetDescriptionBlock("Paragraph Formatting Example", "The following examples show different paragraph formatting properties.", 1));
			section.Blocks.Add(GetTitleParagraph("Paragraphs with different horizontal alignment", 2));

			NParagraph paragraph;

            for (int i = 0; i < 4; i++)
            {
                paragraph = new NParagraph();

				switch (i)
                {
                    case 0:
                        paragraph.HorizontalAlignment = ENAlign.Left;
                        paragraph.Inlines.Add(new NTextInline(GetAlignedParagraphText("left")));
                        break;
                    case 1:
                        paragraph.HorizontalAlignment = ENAlign.Center;
                        paragraph.Inlines.Add(new NTextInline(GetAlignedParagraphText("center")));
                        break;
                    case 2:
                        paragraph.HorizontalAlignment = ENAlign.Right;
                        paragraph.Inlines.Add(new NTextInline(GetAlignedParagraphText("right")));
                        break;
                    case 3:
                        paragraph.HorizontalAlignment = ENAlign.Justify;
                        paragraph.Inlines.Add(new NTextInline(GetAlignedParagraphText("justify")));
                        break;
                }

                section.Blocks.Add(paragraph);
            }

			section.Blocks.Add(GetTitleParagraph("Paragraphs with Margins, Padding and Borders", 2));
            {
                // borders
                paragraph = new NParagraph();
                paragraph.BorderThickness = new NMargins(2, 2, 2, 2);
                paragraph.Border = NBorder.CreateFilledBorder(NColor.Red);
                paragraph.PreferredWidth = NMultiLength.NewPercentage(50);
                paragraph.Margins = new NMargins(5, 5, 5, 5);
                paragraph.Padding = new NMargins(5, 5, 5, 5);
                paragraph.PreferredWidth = NMultiLength.NewFixed(300);
				paragraph.PreferredHeight = NMultiLength.NewFixed(100);
                NTextInline textInline1 = new NTextInline("Paragraphs can have border, margins and padding as well as preffered size");
                paragraph.Inlines.Add(textInline1);

                section.Blocks.Add(paragraph);
            }

			// First line indent and hanging indent
			section.Blocks.Add(GetTitleParagraph("Paragraph with First Line Indent and Hanging Indent", 2));

			NParagraph paragraphWithIndents = new NParagraph(GetRepeatingText("First line indent -10dip, hanging indent 15dip.", 5));
			paragraphWithIndents.FirstLineIndent = -10;
			paragraphWithIndents.HangingIndent = 15;
			section.Blocks.Add(paragraphWithIndents);

			// First line indent and hanging indent
			section.Blocks.Add(GetTitleParagraph("Line Spacing", 2));

			NParagraph paragraphWithMultipleLineSpacing = new NParagraph(GetRepeatingText("Line space is two times bigger than normal", 10));
			paragraphWithMultipleLineSpacing.LineHeightMode = ENLineHeightMode.Multiple;
			paragraphWithMultipleLineSpacing.LineHeightFactor = 2.0;
			section.Blocks.Add(paragraphWithMultipleLineSpacing);

			NParagraph paragraphWithAtLeastLineSpacing = new NParagraph(GetRepeatingText("Line space is at least 20 dips.", 10));
			paragraphWithAtLeastLineSpacing.LineHeightMode = ENLineHeightMode.AtLeast;
			paragraphWithAtLeastLineSpacing.LineHeight = 20.0;
			section.Blocks.Add(paragraphWithAtLeastLineSpacing);

			NParagraph paragraphWithExactLineSpacing = new NParagraph(GetRepeatingText("Line space is exactly 20 dips.", 10));
			paragraphWithExactLineSpacing.LineHeightMode = ENLineHeightMode.Exactly;
			paragraphWithExactLineSpacing.LineHeight = 20.0;
			section.Blocks.Add(paragraphWithExactLineSpacing);

            // BIDI formatting
			section.Blocks.Add(GetTitleParagraph("Paragraphs with BIDI text", 2));

            paragraph = new NParagraph();
            NTextInline latinText1 = new NTextInline("This is some text in English. Followed by Arabic:");
            NTextInline arabicText = new NTextInline("أساسًا، تتعامل الحواسيب فقط مع الأرقام، وتقوم بتخزين الأحرف والمحارف الأخرى بعد أن تُعطي رقما معينا لكل واحد منها. وقبل اختراع ");
            NTextInline latinText2 = new NTextInline("This is some text in English.");


            paragraph.Inlines.Add(latinText1);
            paragraph.Inlines.Add(arabicText);
            paragraph.Inlines.Add(latinText2);

            section.Blocks.Add(paragraph);
        }
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected override string GetExampleDescription()
		{
			return @"
<p>
	This example demonstrates how to use different paragraph formatting properties.
</p>
";
		}

        #endregion

        #region Implementation

        /// <summary>
        /// Gets dummy text for aligned paragraphs
        /// </summary>
        /// <param name="alignment"></param>
        /// <returns></returns>
        private static string GetAlignedParagraphText(string alignment)
        {
            string text = string.Empty;

            for (int i = 0; i < 10; i++)
            {
				if (text.Length > 0)
				{
					text += " ";
				}

                text += "This is " + alignment + " aligned paragraph.";
            }

            return text;
        }

        #endregion

        #region Static

        public static readonly NSchema NParagraphFormattingExampleSchema;

        #endregion
    }
}
