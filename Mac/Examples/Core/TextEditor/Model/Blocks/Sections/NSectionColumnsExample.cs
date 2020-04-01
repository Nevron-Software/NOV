using Nevron.Nov.Dom;
using Nevron.Nov.Graphics;
using Nevron.Nov.Text;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Text
{
    /// <summary>
    /// The example demonstrates how to programmatically create paragraphs with differnt formatting
    /// </summary>
    public class NSectionColumnsExample : NTextExampleBase
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public NSectionColumnsExample()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        static NSectionColumnsExample()
        {
            NSectionColumnsExampleSchema = NSchema.Create(typeof(NSectionColumnsExample), NTextExampleBase.NTextExampleBaseSchema);
        }

        #endregion

        #region Overrides

		/// <summary>
		/// 
		/// </summary>
        protected override void PopulateRichText()
        {
			NSection mainSection = new NSection();
			mainSection.Blocks.Add(GetDescriptionBlock("Section Columns", "This example shows how to create sections with multiple columns and different break types", 1));
			m_RichText.Content.Sections.Add(mainSection);
			m_RichText.Content.Layout = ENTextLayout.Print;

			for (int sectionIndex = 0; sectionIndex < 6; sectionIndex++)
			{
				NSection section = new NSection();

				section.Margins = NMargins.Zero;
				section.Padding = NMargins.Zero;

				string sectionText = string.Empty;
				NColor color = NColor.White;

				switch (sectionIndex)
				{
					case 0:
						sectionText = "Red Section (single column, continuous break)";
						section.ColumnCount = 1;
						color = NColor.Red;
						break;
					case 1:
						sectionText = "Green Section (two columns, continuous break)";
						section.ColumnCount = 2;
						color = NColor.Green;
						break;
					case 2:
						sectionText = "Blue Section (three columns, continuous break)";
						section.ColumnCount = 3;
						color = NColor.Blue;
						break;

					case 3:
						sectionText = "Tomato Section (three column, even page break)";
						section.ColumnCount = 3;
						section.BreakType = ENSectionBreakType.EvenPage;
						color = NColor.Tomato;
						break;
					case 4:
						sectionText = "DarkSlateBlue Section (three columns, column break)";
						section.ColumnCount = 3;
						section.BreakType = ENSectionBreakType.ColumnBreak;
						color = NColor.DarkSlateBlue;
						break;
					case 5:
						sectionText = "RoyalBlue Section (three columns, next page break)";
						section.ColumnCount = 3;
						section.BreakType = ENSectionBreakType.NextPage;
						color = NColor.RoyalBlue;
						break;
				}

				m_RichText.Content.Sections.Add(section);

				section.DifferentFirstHeaderAndFooter = false;
				section.DifferentLeftRightHeadersAndFooters = false;

				section.Header = CreateHeaderFooter(sectionText);
				section.Footer = CreateHeaderFooter(sectionText);

				// paragraphs with some simple text
				for (int i = 0; i < 10; i++)
				{
					NParagraph paragraph = new NParagraph();

					NTextInline textInline = new NTextInline(GetRepeatingText(sectionText + ".", 5));
					textInline.Fill = new NColorFill(color);
					paragraph.Inlines.Add(textInline);

					section.Blocks.Add(paragraph);
				}
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
	This example demonstrates how to create sections with multiple columns and different break types.
</p>
";
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		private NHeaderFooter CreateHeaderFooter(string text)
		{
			NHeaderFooter headerFooter = new NHeaderFooter();

			NParagraph paragraph = new NParagraph();

			paragraph.Inlines.Add(new NTextInline(text));
			paragraph.Inlines.Add(new NTextInline("Page "));
			paragraph.Inlines.Add(new NFieldInline(ENNumericFieldName.PageNumber));
			paragraph.Inlines.Add(new NTextInline(" of "));
			paragraph.Inlines.Add(new NFieldInline(ENNumericFieldName.PageCount));

			headerFooter.Blocks.Add(paragraph);

			return headerFooter;
		}

        #endregion

        #region Static

        public static readonly NSchema NSectionColumnsExampleSchema;

        #endregion
    }
}
