using Nevron.Nov.DataStructures;
using Nevron.Nov.Diagram;
using Nevron.Nov.Diagram.Expressions;
using Nevron.Nov.Diagram.Layout;
using Nevron.Nov.Diagram.Shapes;
using Nevron.Nov.Dom;
using Nevron.Nov.Graphics;
using Nevron.Nov.Layout;

namespace Nevron.Nov.Examples.Diagram
{
	public class NCountryShapesExample : NDiagramExampleBase
	{
		#region Constructors

		/// <summary>
		/// Default constructor.
		/// </summary>
		public NCountryShapesExample()
		{
		}

		/// <summary>
		/// Static constructor.
		/// </summary>
		static NCountryShapesExample()
		{
			NCountryShapesExampleSchema = NSchema.Create(typeof(NCountryShapesExample), NDiagramExampleBase.NDiagramExampleBaseSchema);
		}

		#endregion

		#region Protected Overrides - Example

		protected override void InitDiagram()
		{
			base.InitDiagram();

			m_DrawingDocument.HistoryService.Pause();
			try
			{
				NDrawing drawing = m_DrawingDocument.Content;
				NPage activePage = drawing.ActivePage;

				// Hide grid and ports
				drawing.ScreenVisibility.ShowGrid = false;
				drawing.ScreenVisibility.ShowPorts = false;

				// Create all shapes
				NCountryShapeFactory factory = new NCountryShapeFactory();
				factory.DefaultSize = new NSize(100, 100);

				for (int i = 0; i < factory.ShapeCount; i++)
				{
					NShape shape = factory.CreateShape(i);
					shape.HorizontalPlacement = ENHorizontalPlacement.Center;
					shape.VerticalPlacement = ENVerticalPlacement.Center;
					shape.Text = factory.GetShapeInfo(i).Name;
					MoveTextBelowShape(shape);
					activePage.Items.Add(shape);
				}

				// Arrange them
				NList<NShape> shapes = activePage.GetShapes(false);
				NLayoutContext layoutContext = new NDrawingLayoutContext(m_DrawingDocument, activePage);

				NTableFlowLayout tableLayout = new NTableFlowLayout();
				tableLayout.HorizontalSpacing = 70;
				tableLayout.VerticalSpacing = 40;
				tableLayout.Direction = ENHVDirection.LeftToRight;
				tableLayout.MaxOrdinal = 5;

				tableLayout.Arrange(shapes.CastAll<object>(), layoutContext);

				// size page to content
                activePage.Layout.ContentPadding = new NMargins(50);
				activePage.SizeToContent();
			}
			finally
			{
				m_DrawingDocument.HistoryService.Resume();
			}
		}
		protected override string GetExampleDescription()
		{
			return @"
<p>
    This example demonstrates the country shapes, which are created by the NCountryShapeFactory.
</p>
";
		}

		#endregion
		
		#region Schema

		/// <summary>
		/// Schema associated with NCountryShapesExample.
		/// </summary>
		public static readonly NSchema NCountryShapesExampleSchema;

		#endregion
	}
}