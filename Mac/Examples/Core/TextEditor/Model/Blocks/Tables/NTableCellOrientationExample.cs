using Nevron.Nov.Dom;
using Nevron.Nov.Graphics;
using Nevron.Nov.Text;
using Nevron.Nov.UI;

namespace Nevron.Nov.Examples.Text
{
	/// <summary>
	/// The example demonstrates how to control the table cell orientation and alignment
	/// </summary>
	public class NTableCellOrientationExample : NTextExampleBase
	{
		#region Constructors

		/// <summary>
		/// 
		/// </summary>
		public NTableCellOrientationExample()
		{
		}
		/// <summary>
		/// 
		/// </summary>
		static NTableCellOrientationExample()
		{
            NTableCellOrientationExampleSchema = NSchema.Create(typeof(NTableCellOrientationExample), NTextExampleBase.NTextExampleBaseSchema);
		}

		#endregion

		#region Overrides

		protected override void PopulateRichText()
		{
            NSection section = new NSection();
			m_RichText.Content.Sections.Add(section);

			section.Blocks.Add(GetDescriptionBlock("Table Cell Orientation Example", "This example shows how to programmatically modify the orientation of cells.", 1));

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

            // set cell [1, 0] to be row master
            NTableCell leftToRightCell = table.Rows[1].Cells[0];
            leftToRightCell.RowSpan = int.MaxValue;
            leftToRightCell.BackgroundFill = new NColorFill(ENNamedColor.LightSteelBlue);
			leftToRightCell.Blocks.Clear();
            leftToRightCell.Blocks.Add(new NParagraph("Cell With Left to Right Orientation"));
            leftToRightCell.TextDirection = ENTableCellTextDirection.LeftToRight;
            leftToRightCell.HorizontalAlignment = ENAlign.Center;
            leftToRightCell.VerticalAlignment = ENVAlign.Center;

            // set cell [1, 0] to be row master
            NTableCell rightToLeftCell = table.Rows[1].Cells[table.Columns.Count - 1];
            rightToLeftCell.RowSpan = int.MaxValue;
            rightToLeftCell.BackgroundFill = new NColorFill(ENNamedColor.LightGreen);
            rightToLeftCell.Blocks.Clear();
            rightToLeftCell.Blocks.Add(new NParagraph("Cell With Right to Left Orientation"));
            rightToLeftCell.TextDirection = ENTableCellTextDirection.RightToLeft;
            rightToLeftCell.HorizontalAlignment = ENAlign.Center;
            rightToLeftCell.VerticalAlignment = ENVAlign.Center;

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
	This example shows how to programmatically modify the orientation of cells.
</p>
";
		}

		#endregion

		#region Static

        public static readonly NSchema NTableCellOrientationExampleSchema;

		#endregion
	}
}
