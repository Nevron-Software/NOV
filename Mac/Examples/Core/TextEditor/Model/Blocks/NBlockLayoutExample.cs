using Nevron.Nov.Dom;
using Nevron.Nov.Text;
using Nevron.Nov.UI;
using Nevron.Nov.Graphics;

namespace Nevron.Nov.Examples.Text
{
	/// <summary>
	/// The example demonstrates how to programmatically create paragraphs with differnt inline formatting
	/// </summary>
	public class NBlockLayoutExample : NTextExampleBase
	{
		#region Constructors

		/// <summary>
		/// 
		/// </summary>
		public NBlockLayoutExample()
		{
		}
		/// <summary>
		/// 
		/// </summary>
		static NBlockLayoutExample()
		{
			NBlockLayoutExampleSchema = NSchema.Create(typeof(NBlockLayoutExample), NTextExampleBase.NTextExampleBaseSchema);
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

			section.Blocks.Add(GetDescriptionBlock("Block Layout", "Every block in the document follows the HTML block formatting model", 1));

			section.Blocks.Add(GetDescriptionBlock("Block Margins, Padding and Border Thickness", "Every block in the document has margins, border thickness and padding.", 2));

			section.Blocks.Add(CreateSampleParagraph1());
			section.Blocks.Add(CreateSampleParagraph1());

			section.Blocks.Add(GetNoteBlock("The distance between the above two paragraphs is 10 dips as the margins collapse", 2));

			section.Blocks.Add(GetDescriptionBlock("Floating blocks", "Floating blocks can be positioned on the left of right of the the parent containing block", 2));

			section.Blocks.Add(CreateFloatingParagraph(ENFloatMode.Left));
			section.Blocks.Add(CreateNormalParagraph());
			section.Blocks.Add(CreateNormalParagraph());

			section.Blocks.Add(CreateFloatingParagraph(ENFloatMode.Right));
			section.Blocks.Add(CreateNormalParagraph());
			section.Blocks.Add(CreateNormalParagraph());

			section.Blocks.Add(GetDescriptionBlock("Clear Mode", "Clear mode allows you to position blocks at a space not occupied by other blocks", 2));

			section.Blocks.Add(CreateFloatingParagraph(ENFloatMode.Left));
			section.Blocks.Add(CreateNormalParagraph());

			NParagraph paragraph = CreateNormalParagraph();
			paragraph.ClearMode = ENClearMode.Left;
			section.Blocks.Add(paragraph);

			section.Blocks.Add(GetNoteBlock("The second paragraph has ClearMode set to left and is not obscured by the floating block.", 2));
		}
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected override string GetExampleDescription()
		{
			return @"
<p>
	This example demonstrates how to use margins, padding and borders as well as how to create floating blocks.
</p>
";
		}
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected NParagraph CreateFloatingParagraph(ENFloatMode floatMode)
		{
			NParagraph paragraph = new NParagraph(floatMode.ToString() + " flow paragraph.");

			paragraph.FloatMode = floatMode;
			paragraph.PreferredWidth = new NMultiLength(ENMultiLengthUnit.Dip, 100);
			paragraph.PreferredHeight = new NMultiLength(ENMultiLengthUnit.Dip, 100);
			paragraph.BorderThickness = new NMargins(1);
			paragraph.Border = NBorder.CreateFilledBorder(NColor.Black);

			return paragraph;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="floatMode"></param>
		/// <returns></returns>
		protected NParagraph CreateNormalParagraph()
		{
			return new NParagraph(GetRepeatingText("Normal flow paragraph.", 10));
		}
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected NParagraph CreateSampleParagraph1()
		{
			NParagraph paragraph = new NParagraph("This paragraph has margins, border thickness and padding of 10dips.");

			paragraph.Margins = new NMargins(10);
			paragraph.BorderThickness = new NMargins(10);
			paragraph.Padding = new NMargins(10);
			paragraph.Border = NBorder.CreateFilledBorder(NColor.Red);
			paragraph.BackgroundFill = new NStockGradientFill(NColor.White, NColor.LightYellow);

			return paragraph;
		}

		#endregion

		#region Static

		public static readonly NSchema NBlockLayoutExampleSchema;

		#endregion
	}
}
