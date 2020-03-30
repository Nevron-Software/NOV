using Nevron.Nov.Dom;
using Nevron.Nov.Text;
using Nevron.Nov.UI;
using Nevron.Nov.Graphics;
using System;

namespace Nevron.Nov.Examples.Text
{
	/// <summary>
	/// The example demonstrates how to programmatically create paragraphs with differnt inline formatting
	/// </summary>
	public class NBulletListsExample : NTextExampleBase
	{
		#region Constructors

		/// <summary>
		/// 
		/// </summary>
		public NBulletListsExample()
		{
		}
		/// <summary>
		/// 
		/// </summary>
		static NBulletListsExample()
		{
			NBulletListsExampleSchema = NSchema.Create(typeof(NBulletListsExample), NTextExampleBase.NTextExampleBaseSchema);
		}

		#endregion

		#region Overrides

		protected override void PopulateRichText()
		{
			NSection section = new NSection();
			m_RichText.Content.Sections.Add(section);

			section.Blocks.Add(GetDescriptionBlock("Bullet Lists", "Bullet lists allow you to apply automatic numbering on paragraphs or groups of blocks.", 1));

			section.Blocks.Add(GetDescriptionBlock("Simple bullet list", "Following is a bullet list with default formatting.", 2));

			CreateSampleBulletList(section, ENBulletListTemplateType.Bullet, 3, "Bullet List Item");

			// setting bullet list template type
			{
				section.Blocks.Add(GetDescriptionBlock("Bullet Lists with Different Template", "Following are bullet lists with different formatting", 2));

				ENBulletListTemplateType[] values = NEnum.GetValues<ENBulletListTemplateType>();
				string[] names = NEnum.GetNames<ENBulletListTemplateType>();

				for (int i = 0; i < values.Length - 1; i++)
				{
					CreateSampleBulletList(section, values[i], 3, names[i] + " bullet list item ");
				}
			}

			// nested bullet lists
			{
				section.Blocks.Add(GetDescriptionBlock("Bullet List Levels", "Following is an example of bullet list levels", 2));

				NBulletList bulletList = new NBulletList(ENBulletListTemplateType.Decimal);
				m_RichText.Content.BulletLists.Add(bulletList);

				for (int i = 0; i < 3; i++)
				{
					NParagraph par1 = new NParagraph("Bullet List Item" + i.ToString());
					par1.SetBulletList(bulletList, 0);
					section.Blocks.Add(par1);

					for (int j = 0; j < 2; j++)
					{
						NParagraph par2 = new NParagraph("Nested Bullet List Item" + i.ToString());
						par2.SetBulletList(bulletList, 1);
						par2.MarginLeft = 20;
						section.Blocks.Add(par2);
					}
				}
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="section"></param>
		/// <param name="bulletListType"></param>
		/// <param name="items"></param>
		/// <param name="itemText"></param>
		private void CreateSampleBulletList(NSection section, ENBulletListTemplateType bulletListType, int items, string itemText)
		{
			NBulletList bulletList = new NBulletList(bulletListType);
			m_RichText.Content.BulletLists.Add(bulletList);

			for (int i = 0; i < items; i++)
			{
				NParagraph par = new NParagraph(itemText + i.ToString());
				par.SetBulletList(bulletList, 0);
				section.Blocks.Add(par);
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
	This example demonstrates how to set different bullet list templates to bullet lists as well as how to create nested (multilevel) bullet lists.
</p>
";
		}

		#endregion

		#region Static

		public static readonly NSchema NBulletListsExampleSchema;

		#endregion
	}
}
