using Nevron.Nov.Dom;
using Nevron.Nov.Graphics;
using Nevron.Nov.Text;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Text
{
    /// <summary>
    /// The example demonstrates how to programmatically create paragraphs with differnt formatting
    /// </summary>
    public class NSectionPagesExample : NTextExampleBase
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public NSectionPagesExample()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        static NSectionPagesExample()
        {
            NSectionPagesExampleSchema = NSchema.Create(typeof(NSectionPagesExample), NTextExampleBase.NTextExampleBaseSchema);
        }

        #endregion

        #region Overrides

		/// <summary>
		/// 
		/// </summary>
        protected override void PopulateRichText()
        {
			m_RichText.Content.Layout = ENTextLayout.Print;
			m_RichText.Content.ZoomFactor = 0.5;

			for (int sectionIndex = 0; sectionIndex < 4; sectionIndex++)
			{
				NSection section = new NSection();

				section.Margins = NMargins.Zero;
				section.Padding = NMargins.Zero;

				string sectionText = string.Empty;

				switch (sectionIndex)
				{
					case 0:
						sectionText = "Paper size A4.";
						section.PageSize = new NPageSize(ENPaperKind.A4);
						section.BreakType = ENSectionBreakType.NextPage;
						section.Blocks.Add(GetDescriptionBlock("Section Pages", "This example shows how to set different page properties, like page size, page orientation and page border", 1));
						break;
					case 1:
						sectionText = "Paper size A5.";
						section.PageSize = new NPageSize(ENPaperKind.A5);
						section.BreakType = ENSectionBreakType.NextPage;
						break;
					case 2:
						sectionText = "Paper size A4, paper orientation portrait.";
						section.PageOrientation = ENPageOrientation.Landscape;
						section.PageSize = new NPageSize(ENPaperKind.A4);
						section.BreakType = ENSectionBreakType.NextPage;
						break;
					case 3:
						sectionText = "Paper size A4, page border solid 10dip.";
						section.PageBorder = NBorder.CreateFilledBorder(NColor.Black);
						section.PageBorderThickness = new NMargins(10);
						section.PageSize = new NPageSize(ENPaperKind.A4);
						section.BreakType = ENSectionBreakType.NextPage;
						break;

				}

				m_RichText.Content.Sections.Add(section);

				// add some content
				NParagraph paragraph = new NParagraph(sectionText);
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
	This example demonstrates how to set different page properties, like page size, page orientation and page border.
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

        public static readonly NSchema NSectionPagesExampleSchema;

        #endregion
    }
}
