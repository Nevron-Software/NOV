using Nevron.Nov.Dom;
using Nevron.Nov.Graphics;
using Nevron.Nov.Text;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Text
{
    /// <summary>
    /// The example demonstrates how to programmatically create paragraphs with differnt formatting
    /// </summary>
    public class NHeaderAndFooterExample : NTextExampleBase
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public NHeaderAndFooterExample()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        static NHeaderAndFooterExample()
        {
            NHeaderAndFooterExampleSchema = NSchema.Create(typeof(NHeaderAndFooterExample), NTextExampleBase.NTextExampleBaseSchema);
        }

        #endregion

        #region Overrides

        protected override void PopulateRichText()
        {
			m_RichText.Content.Layout = ENTextLayout.Print;

			for (int sectionIndex = 0; sectionIndex < 3; sectionIndex++)
			{
				NSection section = new NSection();

				NColor color = NColor.Empty;
				string sectionText = string.Empty;

				switch (sectionIndex)
				{
					case 0:
						{
							sectionText = "Red Section (uniform header and footer).";
							color = NColor.Red;

							section.Blocks.Add(GetDescriptionBlock("Section Headers and Footers", "This example shows how to create sections with different header and footer settings.", 1));

							section.DifferentFirstHeaderAndFooter = false;
							section.DifferentLeftRightHeadersAndFooters = false;

							NHeaderFooter header = CreateHeaderFooter("Uniform Header.");
							header.BackgroundFill = new NColorFill(color);
							section.Header = header;

							NHeaderFooter footer = CreateHeaderFooter("Uniform Footer.");
							footer.BackgroundFill = new NColorFill(color);
							section.Footer = footer;
						}
						break;
					case 1:
						{
							sectionText = "Green Section (different first header and footer).";
							color = NColor.Green;

							section.BreakType = ENSectionBreakType.NextPage;
							section.DifferentFirstHeaderAndFooter = true;
							section.DifferentLeftRightHeadersAndFooters = false;

							NHeaderFooter headerFirst = CreateHeaderFooter("First Page Header.");
							headerFirst.BackgroundFill = new NColorFill(color);
							section.HeaderFirst = headerFirst;

							NHeaderFooter footerFirst = CreateHeaderFooter("First Page Footer.");
							footerFirst.BackgroundFill = new NColorFill(color);
							section.FooterFirst = footerFirst;
						}
						break;
					case 2:
						{
							sectionText = "Blue Section (different left and right page header and footer).";
							color = NColor.Blue;

							section.BreakType = ENSectionBreakType.NextPage;
							section.DifferentFirstHeaderAndFooter = false;
							section.DifferentLeftRightHeadersAndFooters = true;

							NHeaderFooter headerLeft = CreateHeaderFooter("Left Page Header.");
							headerLeft.BackgroundFill = new NColorFill(color);
							section.HeaderLeft = headerLeft;

							NHeaderFooter headerRight = CreateHeaderFooter("Right Page Header.");
							headerRight.BackgroundFill = new NColorFill(color);
							section.HeaderRight = headerRight;

							NHeaderFooter footerLeft = CreateHeaderFooter("Left Page Footer.");
							footerLeft.BackgroundFill = new NColorFill(color);
							section.FooterLeft = footerLeft;

							NHeaderFooter footerRight = CreateHeaderFooter("Right Page Footer.");
							footerRight.BackgroundFill = new NColorFill(color);
							section.FooterRight = footerRight;
						}
						break;

				}

				m_RichText.Content.Sections.Add(section);

				// add some section contentparagraphs with some simple text
				for (int i = 0; i < 20; i++)
				{
					NParagraph paragraph = new NParagraph();

					NTextInline textInline = new NTextInline(GetRepeatingText(sectionText, 5));
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
	This example demonstrates how create sections with different header and footer. Each section has three different properties for header and footer that allow you to specify distinct header and footer for first, left and right pages.
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
			paragraph.Inlines.Add(new NTextInline(". Page "));
			paragraph.Inlines.Add(new NFieldInline(ENNumericFieldName.PageNumber));
			paragraph.Inlines.Add(new NTextInline(" of "));
			paragraph.Inlines.Add(new NFieldInline(ENNumericFieldName.PageCount));

			headerFooter.Blocks.Add(paragraph);

			return headerFooter;
		}

        #endregion

        #region Static

        public static readonly NSchema NHeaderAndFooterExampleSchema;

        #endregion
    }
}
