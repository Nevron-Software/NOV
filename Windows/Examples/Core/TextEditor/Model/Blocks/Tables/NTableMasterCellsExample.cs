using Nevron.Nov.Dom;
using Nevron.Nov.Graphics;
using Nevron.Nov.Text;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Text
{
	/// <summary>
	/// The example demonstrates how to programmatically create paragraphs with differnt inline formatting
	/// </summary>
	public class NTableMasterCellsExample : NTextExampleBase
	{
		#region Constructors

		/// <summary>
		/// 
		/// </summary>
		public NTableMasterCellsExample()
		{
		}
		/// <summary>
		/// 
		/// </summary>
		static NTableMasterCellsExample()
		{
            NTableMasterCellsExampleSchema = NSchema.Create(typeof(NTableMasterCellsExample), NTextExampleBase.NTextExampleBaseSchema);
		}

		#endregion

		#region Overrides

		protected override void PopulateRichText()
		{
            NSection section = new NSection();
			m_RichText.Content.Sections.Add(section);

			section.Blocks.Add(GetDescriptionBlock("Table Master Cells Example", "This example shows how to programmatically create and add master cells.", 1));

            // first create the table
            NTable table = new NTable(5, 5);
			table.AllowSpacingBetweenCells = false;

            for (int row = 0; row < table.Rows.Count; row++)
            {
                for (int col = 0; col < table.Columns.Count; col++)
                {
                    NParagraph paragraph = new NParagraph("Normal Cell");

					NTableCell tableCell = table.Rows[row].Cells[col];
					tableCell.BorderThickness = new Nov.Graphics.NMargins(1);
					tableCell.Border = NBorder.CreateFilledBorder(NColor.Black);

                    // by default cells contain a single paragraph
					tableCell.Blocks.Clear();
					tableCell.Blocks.Add(paragraph);
                }
            }

            // set cell [0, 2] to be column master
            NTableCell colMasterCell = table.Rows[0].Cells[2];
            colMasterCell.ColSpan = 2;
            colMasterCell.BackgroundFill = new NColorFill(ENNamedColor.LightSkyBlue);
			colMasterCell.Blocks.Clear();
            colMasterCell.Blocks.Add(new NParagraph("Column Master Cell"));

            // set cell [1, 0] to be row master
            NTableCell rowMasterCell = table.Rows[1].Cells[0];
            rowMasterCell.RowSpan = 2;
            rowMasterCell.BackgroundFill = new NColorFill(ENNamedColor.LightSteelBlue);
			rowMasterCell.Blocks.Clear();
            rowMasterCell.Blocks.Add(new NParagraph("Row Master Cell"));

            // set cell [2, 2] to be column and row master
            NTableCell rowColMasterCell = table.Rows[2].Cells[2];
            rowColMasterCell.ColSpan = 2;
            rowColMasterCell.RowSpan = 2;
            rowColMasterCell.BackgroundFill = new NColorFill(ENNamedColor.MediumTurquoise);
			rowColMasterCell.Blocks.Clear();
            rowColMasterCell.Blocks.Add(new NParagraph("Row\\Col Master Cell"));

            section.Blocks.Add(table);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected override string GetExampleDescription()
		{
			return @"
<p>
	This example demonstrates how to programmatically create row and column master cells.
</p>
";
		}

		#endregion

		#region Static

        public static readonly NSchema NTableMasterCellsExampleSchema;

		#endregion
	}
}
