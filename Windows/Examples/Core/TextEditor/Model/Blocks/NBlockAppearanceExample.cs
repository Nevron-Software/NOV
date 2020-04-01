using Nevron.Nov.Dom;
using Nevron.Nov.Text;
using Nevron.Nov.UI;
using Nevron.Nov.Graphics;
using System.IO;
using Nevron.Nov.Text.Data;
using Nevron.Nov.Data;

namespace Nevron.Nov.Examples.Text
{
	/// <summary>
	/// The example demonstrates how to programmatically create paragraphs with differnt inline formatting
	/// </summary>
	public class NBlockAppearanceExample : NTextExampleBase
	{
		#region Constructors

		/// <summary>
		/// 
		/// </summary>
		public NBlockAppearanceExample()
		{
		}
		/// <summary>
		/// 
		/// </summary>
		static NBlockAppearanceExample()
		{
			NBlockAppearanceExampleSchema = NSchema.Create(typeof(NBlockAppearanceExample), NTextExampleBase.NTextExampleBaseSchema);
		}

		#endregion

		#region Overrides

		protected override void PopulateRichText()
		{
			NSection section = new NSection();
			m_RichText.Content.Sections.Add(section);

			section.Blocks.Add(GetDescriptionBlock("Block Appearance", "Every block in the document follows the HTML block formatting model which allows you to specify filling and border. The following paragraph has gradient background filling and solid border.", 1));

			NParagraph paragraph = new NParagraph();

			NTextInline textInline = new NTextInline("Paragraph with solid border and gradient filling applied on the background.");
			textInline.Fill = new NColorFill(NColor.Aquamarine);
			textInline.FontSize = 24;
			paragraph.Inlines.Add(textInline);

			paragraph.BackgroundFill = new NStockGradientFill(ENGradientStyle.FromCorner, ENGradientVariant.Variant1, NColor.Gainsboro, NColor.SlateGray);
			paragraph.Border = NBorder.CreateFilledBorder(NColor.DarkBlue);
			paragraph.BorderThickness = new NMargins(3);

			paragraph.PreferredWidth = new NMultiLength(ENMultiLengthUnit.Dip, 500);

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
	This example demonstrates how to control the appearance of text blocks.
</p>
";
		}


		#endregion

		#region Static

		public static readonly NSchema NBlockAppearanceExampleSchema;

		#endregion
	}
}
