using Nevron.Nov.Dom;
using Nevron.Nov.Graphics;
using Nevron.Nov.Text;
using Nevron.Nov.UI;
using Nevron.Nov.DataStructures;

namespace Nevron.Nov.Examples.Text
{
	/// <summary>
	/// This example demonstrates how to programmatically create tables.
	/// </summary>
	public class NTableExample : NTextExampleBase
	{
		#region Constructors

		/// <summary>
		/// 
		/// </summary>
		public NTableExample()
		{
		}
		/// <summary>
		/// 
		/// </summary>
		static NTableExample()
		{
            NTableExampleSchema = NSchema.Create(typeof(NTableExample), NTextExampleBase.NTextExampleBaseSchema);
		}

		#endregion

		#region Protected Overrides - Example

		protected override void PopulateRichText()
		{
            NSection section = new NSection();
			m_RichText.Content.Sections.Add(section);

			section.Blocks.Add(GetDescriptionBlock("Table example", "This example shows how to programmatically create and add a table to the text control.", 1));

			NParagraph p = GetTitleParagraph("Table with cell spacing.", 2);
			section.Blocks.Add(p);

			NTable tableWithCellSpacing = CreateTable();
			tableWithCellSpacing.AllowSpacingBetweenCells = true;
			section.Blocks.Add(tableWithCellSpacing);

			section.Blocks.Add(GetTitleParagraph("Table without cell spacing.", 2));

			NTable tableWithoutCellSpacing = CreateTable();
			tableWithoutCellSpacing.AllowSpacingBetweenCells = false;
			section.Blocks.Add(tableWithoutCellSpacing);
		}
		protected override string GetExampleDescription()
		{
			return @"
<p>
	This example demonstrates how to programmatically create tables.
</p>
";
		}

		#endregion

		#region Implementation

		private NTable CreateTable()
		{
			NTable table = new NTable();

			int rowCount = 3;
			int colCount = 3;

			// first create the columns
			for (int i = 0; i < colCount; i++)
			{
				table.Columns.Add(new NTableColumn());
			}

			// then add rows with cells count matching the number of columns
			for (int row = 0; row < rowCount; row++)
			{
				NTableRow tableRow = new NTableRow();
				table.Rows.Add(tableRow);

				for (int col = 0; col < colCount; col++)
				{
					NTableCell tableCell = new NTableCell();
					tableRow.Cells.Add(tableCell);
					tableCell.Margins = new NMargins(4);

					tableCell.Border = NBorder.CreateFilledBorder(NColor.Black);
					tableCell.BorderThickness = new NMargins(1);

					NParagraph paragraph = new NParagraph("This is table cell [" + row.ToString() + ", " + col.ToString() + "]");
					tableCell.Blocks.Add(paragraph);
				}
			}

			return table;
		}

		#endregion

		#region Schema

        public static readonly NSchema NTableExampleSchema;

		#endregion
	}
}