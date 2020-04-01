using Nevron.Nov.Dom;
using Nevron.Nov.Graphics;
using Nevron.Nov.Text;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Text
{
    /// <summary>
    /// The example demonstrates how to programmatically create paragraphs tab stops
    /// </summary>
    public class NTabStopsExample : NTextExampleBase
    {
        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        public NTabStopsExample()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        static NTabStopsExample()
        {
            NTabStopsExampleSchema = NSchema.Create(typeof(NTabStopsExample), NTextExampleBase.NTextExampleBaseSchema);
        }

        #endregion

        #region Overrides


		protected override void PopulateRichText()
        {
            NSection section = new NSection();
			m_RichText.Content.Sections.Add(section);

            // paragraphs with different horizontal alignment
			section.Blocks.Add(GetDescriptionBlock("Paragraphs can contain tab stops", "This example shows how to programmatically add tab stops to paragraphs", 2));

			NParagraph paragraph = new NParagraph();

			paragraph.Inlines.Add(new NTabInline());
			paragraph.Inlines.Add(new NTextInline("Left."));
			paragraph.Inlines.Add(new NTabInline());
			paragraph.Inlines.Add(new NTextInline("Right."));
			paragraph.Inlines.Add(new NTabInline());
			paragraph.Inlines.Add(new NTextInline("Center."));
			paragraph.Inlines.Add(new NTabInline());
			paragraph.Inlines.Add(new NTextInline("Decimal 345.33"));

			NTabStopCollection tabStops = new NTabStopCollection();

			tabStops.Add(new Nevron.Nov.Text.NTabStop(100, ENTabStopAlignment.Left, ENTabStopLeaderStyle.Dots));
			tabStops.Add(new Nevron.Nov.Text.NTabStop(200, ENTabStopAlignment.Right, ENTabStopLeaderStyle.EqualSigns));
			tabStops.Add(new Nevron.Nov.Text.NTabStop(300, ENTabStopAlignment.Center, ENTabStopLeaderStyle.Hyphens));
			tabStops.Add(new Nevron.Nov.Text.NTabStop(500, ENTabStopAlignment.Decimal, ENTabStopLeaderStyle.Underline));

			paragraph.TabStops = tabStops;
			paragraph.WidowOrphanControl = false;

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
	This example demonstrates how to add tab stops with different settings to paragraphs.
</p>
";
		}

        #endregion
		
        #region Static

        public static readonly NSchema NTabStopsExampleSchema;

        #endregion
    }
}
