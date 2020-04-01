using Nevron.Nov.Dom;
using Nevron.Nov.Text;
using Nevron.Nov.UI;
using Nevron.Nov.Graphics;

namespace Nevron.Nov.Examples.Text
{
	/// <summary>
	/// The example demonstrates how to programmatically create paragraphs with differnt inline formatting
	/// </summary>
	public class NTableColumnTypesExample : NTextExampleBase
	{
		#region Constructors

		/// <summary>
		/// 
		/// </summary>
		public NTableColumnTypesExample()
		{
		}
		/// <summary>
		/// 
		/// </summary>
		static NTableColumnTypesExample()
		{
			NTableColumnTypesExampleSchema = NSchema.Create(typeof(NTableColumnTypesExample), NTextExampleBase.NTextExampleBaseSchema);
		}

		#endregion

		#region Overrides

		protected override void PopulateRichText()
		{
			NSection section = new NSection();
			m_RichText.Content.Sections.Add(section);

			section.Blocks.Add(GetDescriptionBlock("Table Column Types Example", "This example shows how to set the table column preferred width.", 1));

			{
				// create the table
				NTable table = new NTable();

				table.AllowSpacingBetweenCells = false;

				int columnCount = 5;
				int rowCount = 5;
				
				for (int row = 0; row < rowCount; row++)
				{
					NTableRow tableRow = new NTableRow();
					table.Rows.Add(tableRow);

					for (int col = 0; col < columnCount; col++)
					{
						NParagraph paragraph;

						if (row == 0)
						{
							// set table column preferred width
							string headerText = string.Empty;
							NTableColumn tableColumn = new NTableColumn();

							if (col % 2 == 0)
							{
								tableColumn.BackgroundFill = new NColorFill(NColor.LightGray);
							}
							else
							{
								tableColumn.BackgroundFill = new NColorFill(NColor.Beige);
							}

							switch (col)
							{
								case 0: // Fixed column
									tableColumn.PreferredWidth = new NMultiLength(ENMultiLengthUnit.Dip, 80);
									headerText = "Fixed [80dips]";
									break;
								case 1: // Auto
									headerText = "Automatic";
									break;
								case 2: // Percentage
									tableColumn.PreferredWidth = new NMultiLength(ENMultiLengthUnit.Percentage, 20);
									headerText = "Percentage [20%]";
									break;
								case 3: // Fixed
									tableColumn.PreferredWidth = new NMultiLength(ENMultiLengthUnit.Dip, 160);
									headerText = "Fixed [160dips]";
									break;
								case 4: // Percentage
									tableColumn.PreferredWidth = new NMultiLength(ENMultiLengthUnit.Percentage, 30);
									headerText = "Percentage [30%]";
									break;
							}

							table.Columns.Add(tableColumn);
							paragraph = new NParagraph(headerText);
						}
						else
						{
							paragraph = new NParagraph("Cell");
						}

						// by default cells contain a single paragraph
						NTableCell tableCell = new NTableCell();
						tableCell.Border = NBorder.CreateFilledBorder(NColor.Black);
						tableCell.BorderThickness = new NMargins(1);
						tableCell.Blocks.Add(paragraph);

						tableRow.Cells.Add(tableCell);
					}
				}

				section.Blocks.Add(table);
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
	This example demonstrates how to set the table column preferred width.
</p>
";
		}

		#endregion

		#region Static

		public static readonly NSchema NTableColumnTypesExampleSchema;

		#endregion
	}
}