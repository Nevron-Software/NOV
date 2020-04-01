using Nevron.Nov.Dom;
using Nevron.Nov.Graphics;
using Nevron.Nov.Text;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Text
{
    /// <summary>
    /// The example demonstrates how to programmatically create paragraphs with differnt formatting
    /// </summary>
    public class NPageBreaksExample : NTextExampleBase
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public NPageBreaksExample()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        static NPageBreaksExample()
        {
            NPageBreaksExampleSchema = NSchema.Create(typeof(NPageBreaksExample), NTextExampleBase.NTextExampleBaseSchema);
        }

        #endregion

        #region Protected Overrides - Example

		protected override void PopulateRichText()
        {
            NSection section = new NSection();
			m_RichText.Content.Sections.Add(section);
			m_RichText.Content.Layout = ENTextLayout.Print;

			section.Blocks.Add(GetDescriptionBlock("Page Breaks", "The example shows how to add page break inlines.", 1));

            // paragraphs with different horizontal alignment
            section.Blocks.Add(GetTitleParagraph("Paragraphs can contain explicit page breaks", 2));

            for (int i = 0; i < 23; i++)
            {
                if (i % 10 == 0)
                {
                    section.Blocks.Add(GetParagraphWithPageBreak());
                }
                else
                {
                    section.Blocks.Add(GetParagraphWithoutPageBreak());                    
                }                
            }

			section.Blocks.Add(GetTitleParagraph("Tables can also contain page breaks", 2));

            NTable table = new NTable(3, 3);

            for (int row = 0; row < table.Rows.Count; row++)
            {
                for (int col = 0; col < table.Columns.Count; col++)
                {
                    // by default cells contain a single paragraph
					table.Rows[row].Cells[col].Blocks.Clear();
                    table.Rows[row].Cells[col].Blocks.Add(GetParagraphWithoutPageBreak());
                }
            }

			table.Rows[1].Cells[1].Blocks.Clear();
            table.Rows[1].Cells[1].Blocks.Add(GetParagraphWithPageBreak());

            section.Blocks.Add(table);
        }
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected override string GetExampleDescription()
		{
			return @"
<p>
	This example demonstrates how to programmatically add page breaks inlines to paragraphs.
</p>
";
		}

        #endregion

        #region Implementation

        /// <summary>
        /// Gets a paragraph without an explicit page break
        /// </summary>
        /// <param name="alignment"></param>
        /// <returns></returns>
        private static NParagraph GetParagraphWithoutPageBreak()
        {
            string text = string.Empty;

            for (int i = 0; i < 10; i++)
            {
                text += "This is a paragraph witout explicit page break.";
            }

            return new NParagraph(text);
        }
        /// <summary>
        /// Gets a paragraph with an explicit page break
        /// </summary>
        /// <param name="alignment"></param>
        /// <returns></returns>
        private static NParagraph GetParagraphWithPageBreak()
        {
            string text = string.Empty;
            NParagraph paragraph = new NParagraph();

            for (int i = 0; i < 5; i++)
            {
				if (text.Length > 0)
				{
					text += " ";
				}

                text += "This is a paragraph with explicit page break.";
            }

            paragraph.Inlines.Add(new NTextInline(text));
            
            NTextInline inline = new NTextInline("Page break appears here!");
            inline.FontStyle |= ENFontStyle.Bold;
            paragraph.Inlines.Add(inline);

            paragraph.Inlines.Add(new NPageBreakInline());
            paragraph.Inlines.Add(new NTextInline(text));

            return paragraph;
        }

        #endregion

        #region Schema

        public static readonly NSchema NPageBreaksExampleSchema;

        #endregion
    }
}
