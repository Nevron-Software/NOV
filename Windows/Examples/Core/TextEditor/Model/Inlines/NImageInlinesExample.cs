using Nevron.Nov.Dom;
using Nevron.Nov.Text;

namespace Nevron.Nov.Examples.Text
{
	/// <summary>
	/// The example demonstrates how to programmatically create inline elements with different formatting
	/// </summary>
	public class NImageInlinesExample : NTextExampleBase
	{
		#region Constructors

		/// <summary>
		/// 
		/// </summary>
		public NImageInlinesExample()
		{
		}
		/// <summary>
		/// 
		/// </summary>
		static NImageInlinesExample()
		{
			NImageInlinesExampleSchema = NSchema.Create(typeof(NImageInlinesExample), NTextExampleBase.NTextExampleBaseSchema);
		}

		#endregion

		#region Protected Overrides - Example

		protected override void PopulateRichText()
		{
			NSection section = new NSection();
			m_RichText.Content.Sections.Add(section);

			section.Blocks.Add(GetDescriptionBlock("Image Inlines", "The example shows how to add image inlines with altered preferred width and height.", 1));

			// adding a raster image with automatic size
			{
				NParagraph paragraph = new NParagraph();

				paragraph.Inlines.Add(new NTextInline("Raster image in its original size (125x100):"));
				paragraph.Inlines.Add(new NLineBreakInline());

				NImageInline imageInline = new NImageInline();
				imageInline.Image = NResources.Image_Artistic_FishBowl_jpg;
				paragraph.Inlines.Add(imageInline);

				section.Blocks.Add(paragraph);
			}

			// adding a raster image with specified preferred width and height
			{
				NParagraph paragraph = new NParagraph();

				paragraph.Inlines.Add(new NTextInline("Raster image with preferred width and height (250x200):"));
				paragraph.Inlines.Add(new NLineBreakInline());

				NImageInline imageInline = new NImageInline();

				imageInline.Image = NResources.Image_Artistic_FishBowl_jpg;
				imageInline.PreferredWidth = new NMultiLength(ENMultiLengthUnit.Dip, 250);
				imageInline.PreferredHeight = new NMultiLength(ENMultiLengthUnit.Dip, 200);

				paragraph.Inlines.Add(imageInline);

				section.Blocks.Add(paragraph);
			}

			// adding a metafile image with preferred width and height
			{
				NParagraph paragraph = new NParagraph();

				paragraph.Inlines.Add(new NTextInline("Metafile image with preferred width and height (125x100):"));
				paragraph.Inlines.Add(new NLineBreakInline());

				NImageInline imageInline = new NImageInline();
				imageInline.Image = NResources.Image_FishBowl_wmf;
				imageInline.PreferredWidth = new NMultiLength(ENMultiLengthUnit.Dip, 125);
				imageInline.PreferredHeight = new NMultiLength(ENMultiLengthUnit.Dip, 100);

				paragraph.Inlines.Add(imageInline);

				section.Blocks.Add(paragraph);
			}

			// adding a metafile image with preferred width and height
			{
				NParagraph paragraph = new NParagraph();

				paragraph.Inlines.Add(new NTextInline("Metafile image with preferred width and height (250x200):"));
				paragraph.Inlines.Add(new NLineBreakInline());

				NImageInline imageInline = new NImageInline();
				imageInline.Image = NResources.Image_FishBowl_wmf;
				imageInline.PreferredWidth = new NMultiLength(ENMultiLengthUnit.Dip, 250);
				imageInline.PreferredHeight = new NMultiLength(ENMultiLengthUnit.Dip, 200);				

				paragraph.Inlines.Add(imageInline);

				section.Blocks.Add(paragraph);
			}
		}
		protected override string GetExampleDescription()
		{
			return @"
<p>
	This example demonstrates how to add raster and metafile image inlines to text documents. Note that metafile images scale without loss of quality.
</p>
";
		}

		#endregion

		#region Schema

		public static readonly NSchema NImageInlinesExampleSchema;

		#endregion
	}
}
