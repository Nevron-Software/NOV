using Nevron.Nov.Dom;
using Nevron.Nov.Text;
using Nevron.Nov.UI;
using Nevron.Nov.Graphics;
using System;

namespace Nevron.Nov.Examples.Text
{
	/// <summary>
	/// The example demonstrates how to programmatically assign a bullet list template to a bullet list
	/// </summary>
	public class NBulletListTemplateExample : NTextExampleBase
	{
		#region Constructors

		/// <summary>
		/// 
		/// </summary>
		public NBulletListTemplateExample()
		{
		}
		/// <summary>
		/// 
		/// </summary>
		static NBulletListTemplateExample()
		{
			NBulletListTemplateExampleSchema = NSchema.Create(typeof(NBulletListTemplateExample), NTextExampleBase.NTextExampleBaseSchema);
		}

		#endregion

		#region Protected Overrides - Example

		/// <summary>
		/// 
		/// </summary>
		protected override void PopulateRichText()
		{
			NSection section = new NSection();
			m_RichText.Content.Sections.Add(section);

			section.Blocks.Add(GetDescriptionBlock("Bullet List Templates", "Bullet lists templates control the appearance of bullet items for a particular level.", 1));
			section.Blocks.Add(GetDescriptionBlock("Setting bullet list template", "Following is a nested bullet list that has a custom defined bullet list template.", 2));

			// create a custom bullet list template
			NBulletList bulletList = new NBulletList();
			NBulletListLevel bulletListLevel1 = new NBulletListLevel();

			bulletListLevel1.BulletType = ENBulletType.UpperRoman;
			bulletListLevel1.Format = "\\0.";

			bulletList.Levels.Add(bulletListLevel1);

			NBulletListLevel bulletListLevel2 = new NBulletListLevel();

			bulletListLevel2.BulletType = ENBulletType.Text;
			bulletListLevel2.BulletText = new string((char)ENBulletChar.BlackCircle, 1);

			bulletList.Levels.Add(bulletListLevel2);

			// Create the first bullet list
			for (int i = 0; i < 3; i++)
			{
				section.Blocks.Add(new NParagraph("Bullet List Item" + i.ToString(), bulletList, 0));

				for (int j = 0; j < 2; j++)
				{
					section.Blocks.Add(new NParagraph("Nested Bullet List Item" + i.ToString(), bulletList, 1));
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
	This example demonstrates how to programmatically create custom bullet list templates.
</p>
";
		}

		#endregion

		#region Schema

		public static readonly NSchema NBulletListTemplateExampleSchema;

		#endregion
	}
}
