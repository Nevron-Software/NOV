using Nevron.Nov.Dom;
using Nevron.Nov.Text;
using Nevron.Nov.UI;
using Nevron.Nov.Graphics;

namespace Nevron.Nov.Examples.Text
{
	/// <summary>
	/// The example demonstrates how to programmatically create paragraphs with differnt inline formatting
	/// </summary>
	public class NBlockSizeExample : NTextExampleBase
	{
		#region Constructors

		/// <summary>
		/// 
		/// </summary>
		public NBlockSizeExample()
		{
		}
		/// <summary>
		/// 
		/// </summary>
		static NBlockSizeExample()
		{
			NBlockSizeExampleSchema = NSchema.Create(typeof(NBlockSizeExample), NTextExampleBase.NTextExampleBaseSchema);
		}

		#endregion

		#region Protected Overrides - Example

		protected override void PopulateRichText()
		{
			NSection section = new NSection();
			m_RichText.Content.Sections.Add(section);

			section.Blocks.Add(GetDescriptionBlock("Block Size", "Every block in the document can have a specified Preferred, Minimum and Maximum size.", 1));

			section.Blocks.Add(GetDescriptionBlock("Preferred Width and Height", "The following paragraph has specified Preferred Width and Height.", 2));

			NParagraph paragraph1 = new NParagraph("Paragraph with Preferred Width 50% and Preferred Height of 200dips.");

			paragraph1.BackgroundFill = new NColorFill(NColor.LightGray);
			paragraph1.PreferredWidth = new NMultiLength(ENMultiLengthUnit.Percentage, 50);
			paragraph1.PreferredHeight = new NMultiLength(ENMultiLengthUnit.Dip, 200);

			section.Blocks.Add(paragraph1);

			section.Blocks.Add(GetDescriptionBlock("Minimum and Maximum Width and Height", "The following paragraph has specified Minimum and Maximum Width.", 2));

			NParagraph paragraph2 = new NParagraph("Paragraph with Preferred Width 50% and Preferred Height of 200dips and Minimum Width of 200 dips and Maximum Width 300 dips.");

			paragraph2.BackgroundFill = new NColorFill(NColor.LightGray);
			paragraph2.PreferredWidth = new NMultiLength(ENMultiLengthUnit.Percentage, 50);
			paragraph2.PreferredHeight = new NMultiLength(ENMultiLengthUnit.Dip, 200);
			paragraph2.MinWidth = new NMultiLength(ENMultiLengthUnit.Dip, 200);
			paragraph2.MaxWidth = new NMultiLength(ENMultiLengthUnit.Dip, 300);

			section.Blocks.Add(paragraph2);
		}

		protected override string GetExampleDescription()
		{
			return @"
<p>
	This example demonstrates how to control the size of text blocks using the preferred, minimum and maximum width and height properties of each block element.
</p>
";
		}

		#endregion
		 
		#region Schema

		public static readonly NSchema NBlockSizeExampleSchema;

		#endregion
	}
}
