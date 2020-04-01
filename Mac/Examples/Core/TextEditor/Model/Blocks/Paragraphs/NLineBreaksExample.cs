using Nevron.Nov.Dom;
using Nevron.Nov.Graphics;
using Nevron.Nov.Text;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Text
{
    /// <summary>
    /// The example demonstrates how to programmatically create paragraphs with line breaks
    /// </summary>
    public class NLineBreaksExample : NTextExampleBase
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public NLineBreaksExample()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        static NLineBreaksExample()
        {
            NLineBreaksExampleSchema = NSchema.Create(typeof(NLineBreaksExample), NTextExampleBase.NTextExampleBaseSchema);
        }

        #endregion

        #region Protected Overrides - Example

		protected override void PopulateRichText()
        {
            NSection section = new NSection();
			m_RichText.Content.Sections.Add(section);

			section.Blocks.Add(GetDescriptionBlock("Paragraphs can contain explicit line breaks", "This example shows how to programmatically add line breaks to paragraphs", 2));

			NParagraph paragraph = new NParagraph();

			paragraph.Inlines.Add(new NTextInline("This text appears on the first line."));
			paragraph.Inlines.Add(new NLineBreakInline());
			paragraph.Inlines.Add(new NTextInline("This text appears on the second line (after the line break)."));

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
	This example demonstrates how to programmatically add line breaks to paragraphs.
</p>
";
		}

        #endregion
		
        #region Schema

        public static readonly NSchema NLineBreaksExampleSchema;

        #endregion
    }
}
