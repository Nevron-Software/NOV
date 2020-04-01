using Nevron.Nov.Dom;
using Nevron.Nov.Text;
using Nevron.Nov.UI;
using Nevron.Nov.Graphics;

namespace Nevron.Nov.Examples.Text
{
	/// <summary>
	/// The example demonstrates how to programmatically create paragraphs with differnt inline formatting
	/// </summary>
	public class NBlockPagingExample : NTextExampleBase
	{
		#region Constructors

		/// <summary>
		/// 
		/// </summary>
		public NBlockPagingExample()
		{
		}
		/// <summary>
		/// 
		/// </summary>
		static NBlockPagingExample()
		{
			NBlockPagingExampleSchema = NSchema.Create(typeof(NBlockPagingExample), NTextExampleBase.NTextExampleBaseSchema);
		}

		#endregion

		#region Overrides

		/// <summary>
		/// 
		/// </summary>
		protected override void PopulateRichText()
		{
			NSection section = new NSection();
			m_RichText.Content.Sections.Add(section);
			m_RichText.Content.Layout = ENTextLayout.Print;

			section.Blocks.Add(GetDescriptionBlock("Block Paging Control", "For each block in the control you can specify whether it starts on a new page or whether it has to avoid page breaks", 1));

			section.Blocks.Add(GetDescriptionBlock("PageBreakBefore and PageBreakAfter", "The following paragraphs have PageBreakBefore and PageBreakAfter set to true", 2));

			NParagraph paragraph1 = CreateSampleParagraph1("Page break must appear before this paragraph.");
			paragraph1.PageBreakBefore = true;
			section.Blocks.Add(paragraph1);

			NParagraph paragraph2 = CreateSampleParagraph1("Page break must appear after this paragraph.");
			paragraph2.PageBreakAfter = true;
			section.Blocks.Add(paragraph2);

		}
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected NParagraph CreateSampleParagraph1(string text)
		{
			NParagraph paragraph = new NParagraph(GetRepeatingText(text, 10));

			paragraph.Margins = new NMargins(10);
			paragraph.BorderThickness = new NMargins(10);
			paragraph.Padding = new NMargins(10);
			paragraph.Border = NBorder.CreateFilledBorder(NColor.Red);
			paragraph.BackgroundFill = new NStockGradientFill(NColor.White, NColor.LightYellow);

			return paragraph;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected override string GetExampleDescription()
		{
			return @"
<p>
	This example demonstrates how to control text paging using the PageBreakBefore and PageBreakAfter properties of text block elements.
</p>
";
		}

		#endregion

		#region Static

		public static readonly NSchema NBlockPagingExampleSchema;

		#endregion
	}
}
